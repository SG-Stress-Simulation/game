using System.Linq;
using UnityEngine;

namespace IESLights
{
    [ExecuteInEditMode]
    public class IESToSpotlightCookie : MonoBehaviour
    {
        private Material _spotlightMaterial, _fadeSpotlightEdgesMaterial, _verticalFlipMaterial;

        void OnDestroy()
        {
            // Clean up.
#if UNITY_EDITOR
            if (_spotlightMaterial != null)
            {
                DestroyImmediate(_spotlightMaterial);
            }
            if (_fadeSpotlightEdgesMaterial != null)
            {
                DestroyImmediate(_fadeSpotlightEdgesMaterial);
            }
            if (_verticalFlipMaterial != null)
            {
                DestroyImmediate(_verticalFlipMaterial);
            }
#else
            if(_spotlightMaterial != null)
            {
                Destroy(_spotlightMaterial);
            }
            if(_fadeSpotlightEdgesMaterial != null)
            {
                Destroy(_fadeSpotlightEdgesMaterial);
            }
            if (_verticalFlipMaterial != null)
            {
                Destroy(_verticalFlipMaterial);
            }
#endif
        }

        public void CreateSpotlightCookie(Texture2D iesTexture, IESData iesData, int resolution, bool applyVignette, bool flipVertically, out Texture2D cookie)
        {
            // Create regular spotlight cookies using the designated shader which handles symmetry and vignetting.
            if (iesData.PhotometricType != PhotometricType.TypeA)
            {
                // Init the material.
                if (_spotlightMaterial == null)
                {
                    _spotlightMaterial = new Material(Shader.Find("Hidden/IES/IESToSpotlightCookie"));
                }

                CalculateAndSetSpotHeight(iesData);
                SetShaderKeywords(iesData, applyVignette);

                cookie = CreateTexture(iesTexture, resolution, flipVertically);
            }
            // Blit automotive photometry straight from the prepared ies texture onto a texture of the target cookie size. A vignette may be applied to prevent sharp cookie edges.
            else
            {
                // Init the material.
                if (_fadeSpotlightEdgesMaterial == null)
                {
                    _fadeSpotlightEdgesMaterial = new Material(Shader.Find("Hidden/IES/FadeSpotlightCookieEdges"));
                }

                // Calculate the position of the vertical center in the cookie.
                float verticalCenter = applyVignette ? CalculateCookieVerticalCenter(iesData) : 0;
                // Create the cookie using the aspect of the cookie (automotive data is most often in a wide horizontal aspect).
                Vector2 fadeEllipse = applyVignette ? CalculateCookieFadeEllipse(iesData) : Vector2.zero;
                cookie = BlitToTargetSize(iesTexture, resolution, fadeEllipse.x, fadeEllipse.y, verticalCenter, applyVignette, flipVertically);
            }
        }

        private float CalculateCookieVerticalCenter(IESData iesData)
        {
            float normalizedTop = 1f - (float)iesData.PadBeforeAmount / iesData.NormalizedValues[0].Count;
            float normalizedCenter = (float)(iesData.NormalizedValues[0].Count - iesData.PadBeforeAmount - iesData.PadAfterAmount) / iesData.NormalizedValues.Count / 2f;
            return normalizedTop - normalizedCenter;
        }

        private Vector2 CalculateCookieFadeEllipse(IESData iesData)
        {
            if (iesData.HorizontalAngles.Count > iesData.VerticalAngles.Count)
            {
                return new Vector2(0.5f, 0.5f * ((float)(iesData.NormalizedValues[0].Count - iesData.PadBeforeAmount - iesData.PadAfterAmount) / iesData.NormalizedValues[0].Count));
            }
            else if (iesData.HorizontalAngles.Count < iesData.VerticalAngles.Count)
            {
                return new Vector2(0.5f * (iesData.HorizontalAngles.Max() - iesData.HorizontalAngles.Min()) / (iesData.VerticalAngles.Max() - iesData.VerticalAngles.Min()), 0.5f);
            }
            else
            {
                return new Vector2(0.5f, 0.5f);
            }
        }

        /// <summary>
        /// Creates a regular architectural spotlight cookie using the appropriate shader.
        /// </summary>
        private Texture2D CreateTexture(Texture2D iesTexture, int resolution, bool flipVertically)
        {
            RenderTexture renderTarget = RenderTexture.GetTemporary(resolution, resolution, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            renderTarget.filterMode = FilterMode.Trilinear;
            renderTarget.DiscardContents();
            RenderTexture.active = renderTarget;
            Graphics.Blit(iesTexture, _spotlightMaterial);
            
            // Flip vertically if desired - required for .exr imports, as these are saved upside down otherwise.
            if (flipVertically)
            {
                // A second render texture is required to blit the result of the spotlight shader using the vertical flip shader.
                RenderTexture tempCopy = RenderTexture.GetTemporary(resolution, resolution, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
                Graphics.Blit(renderTarget, tempCopy);
                FlipVertically(tempCopy, renderTarget);
                RenderTexture.ReleaseTemporary(tempCopy);
            }

            Texture2D cookieTexture = new Texture2D(resolution, resolution, TextureFormat.RGBAFloat, false, true) { filterMode = FilterMode.Trilinear };
            cookieTexture.wrapMode = TextureWrapMode.Clamp;
            cookieTexture.ReadPixels(new Rect(0, 0, resolution, resolution), 0, 0);
            cookieTexture.Apply();

            // Clean up.
            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(renderTarget);

            return cookieTexture;
        }

        /// <summary>
        /// Directly blits the ies texture to the target size cookie. Used for automotive spotlight cookies, which get prepared completely in the IES parsing phase.
        /// </summary>
        private Texture2D BlitToTargetSize(Texture2D iesTexture, int resolution, float horizontalFadeDistance, float verticalFadeDistance, float verticalCenter, bool applyVignette, bool flipVertically)
        {
            if (applyVignette)
            {
                _fadeSpotlightEdgesMaterial.SetFloat("_HorizontalFadeDistance", horizontalFadeDistance);
                _fadeSpotlightEdgesMaterial.SetFloat("_VerticalFadeDistance", verticalFadeDistance);
                _fadeSpotlightEdgesMaterial.SetFloat("_VerticalCenter", verticalCenter);
            }

            RenderTexture renderTarget = RenderTexture.GetTemporary(resolution, resolution, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            renderTarget.filterMode = FilterMode.Trilinear;
            renderTarget.DiscardContents();

            // Regular import - apply vignette and blit to target size.
            if (applyVignette)
            {
                RenderTexture.active = renderTarget;
                Graphics.Blit(iesTexture, _fadeSpotlightEdgesMaterial);
            }
            // No vignette - only blit to target size.
            else
            {
                // Flip vertically if desired - required for .exr imports, as these are saved upside down otherwise.
                if (flipVertically)
                {
                    FlipVertically(iesTexture, renderTarget);
                }
                else
                {
                    Graphics.Blit(iesTexture, renderTarget);
                }
            }

            Texture2D cookieTexture = new Texture2D(resolution, resolution, TextureFormat.RGBAFloat, false, true) { filterMode = FilterMode.Trilinear };
            cookieTexture.wrapMode = TextureWrapMode.Clamp;
            cookieTexture.ReadPixels(new Rect(0, 0, resolution, resolution), 0, 0);
            cookieTexture.Apply();

            // Clean up.
            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(renderTarget);

            return cookieTexture;
        }

        private void FlipVertically(Texture iesTexture, RenderTexture renderTarget)
        {
            if (_verticalFlipMaterial == null)
            {
                _verticalFlipMaterial = new Material(Shader.Find("Hidden/IES/VerticalFlip"));
            }

            Graphics.Blit(iesTexture, renderTarget, _verticalFlipMaterial);
        }

        /// <summary>
        /// To make optimal usage of the spot light, the ies cookie is filled to fit the field of view of the spot.
        /// </summary>
        private void CalculateAndSetSpotHeight(IESData iesData)
        {
            // The spot height is defined by the max spot angle over the radius of the uv plane (0.5).
            float spotHeight = 0.5f / Mathf.Tan(iesData.HalfSpotlightFov * Mathf.Deg2Rad);
            _spotlightMaterial.SetFloat("_SpotHeight", spotHeight);
        }

        private void SetShaderKeywords(IESData iesData, bool applyVignette)
        {
            // Enable or disable the vignette.
            if (applyVignette)
            {
                _spotlightMaterial.EnableKeyword("VIGNETTE");
            }
            else
            {
                _spotlightMaterial.DisableKeyword("VIGNETTE");
            }

            // Set the appropriate keyword for whether or not the top half of the sphere is used.
            if (iesData.VerticalType == VerticalType.Top)
            {
                _spotlightMaterial.EnableKeyword("TOP_VERTICAL");
            }
            else
            {
                _spotlightMaterial.DisableKeyword("TOP_VERTICAL");
            }

            // Also set the approrpiate keyword for horizontal symmetry.
            if (iesData.HorizontalType == HorizontalType.None)
            {
                _spotlightMaterial.DisableKeyword("QUAD_HORIZONTAL");
                _spotlightMaterial.DisableKeyword("HALF_HORIZONTAL");
                _spotlightMaterial.DisableKeyword("FULL_HORIZONTAL");
            }
            else if (iesData.HorizontalType == HorizontalType.Quadrant)
            {
                _spotlightMaterial.EnableKeyword("QUAD_HORIZONTAL");
                _spotlightMaterial.DisableKeyword("HALF_HORIZONTAL");
                _spotlightMaterial.DisableKeyword("FULL_HORIZONTAL");
            }
            else if (iesData.HorizontalType == HorizontalType.Half)
            {
                _spotlightMaterial.DisableKeyword("QUAD_HORIZONTAL");
                _spotlightMaterial.EnableKeyword("HALF_HORIZONTAL");
                _spotlightMaterial.DisableKeyword("FULL_HORIZONTAL");
            }
            else if (iesData.HorizontalType == HorizontalType.Full)
            {
                _spotlightMaterial.DisableKeyword("QUAD_HORIZONTAL");
                _spotlightMaterial.DisableKeyword("HALF_HORIZONTAL");
                _spotlightMaterial.EnableKeyword("FULL_HORIZONTAL");
            }
        }
    }
}