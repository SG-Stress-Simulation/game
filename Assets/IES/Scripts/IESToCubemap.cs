using UnityEngine;

namespace IESLights
{
    [ExecuteInEditMode]
    public class IESToCubemap : MonoBehaviour
    {
        private Material _iesMaterial;
        private Material _horizontalMirrorMaterial;

        void OnDestroy()
        {
            // Clean up.
            if(_horizontalMirrorMaterial != null)
            {
                DestroyImmediate(_horizontalMirrorMaterial);
            }
        }

        public void CreateCubemap(Texture2D iesTexture, IESData iesData, int resolution, out Cubemap cubemap)
        {
            PrepMaterial(iesTexture, iesData);

            CreateCubemap(resolution, out cubemap);
        }

        public Color[] CreateRawCubemap(Texture2D iesTexture, IESData iesData, int resolution)
        {
            // Prepare the material.
            PrepMaterial(iesTexture, iesData);

            // Create the render textures for the raw cubemap. Viewrect is not compatible with render textures, so render to 6 individual render textures and combine them afterwards.
            RenderTexture[] renderTargets = new RenderTexture[6];
            for(int i = 0; i < 6; i++)
            {
                renderTargets[i] = RenderTexture.GetTemporary(resolution, resolution, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
                renderTargets[i].filterMode = FilterMode.Trilinear;
            }
            // Apply the render target and render each face to the texture.
            Camera[] faceCameras = transform.GetChild(0).GetComponentsInChildren<Camera>();
            for(int i = 0; i < 6; i++)
            {
                faceCameras[i].targetTexture = renderTargets[i];
                faceCameras[i].Render();
                faceCameras[i].targetTexture = null;
            }

            // Combine the faces into a single render target.
            RenderTexture combinedRenderTarget = RenderTexture.GetTemporary(resolution * 6, resolution, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            combinedRenderTarget.filterMode = FilterMode.Trilinear;
            // Faces have to be flipped horizontally.
            if (_horizontalMirrorMaterial == null)
            {
                _horizontalMirrorMaterial = new Material(Shader.Find("Hidden/IES/HorizontalFlip"));
            }
            // Blit each face to a single texture.
            RenderTexture.active = combinedRenderTarget;
            for(int i = 0; i < 6; i++)
            {
                GL.PushMatrix();
                GL.LoadPixelMatrix(0, resolution * 6, 0, resolution);
                Graphics.DrawTexture(new Rect(i * resolution, 0, resolution, resolution), renderTargets[i], _horizontalMirrorMaterial);
                GL.PopMatrix();
            }

            // Read back the texture data.
            Texture2D rawCubemap = new Texture2D(resolution * 6, resolution, TextureFormat.RGBAFloat, false, true) { filterMode = FilterMode.Trilinear };
            rawCubemap.ReadPixels(new Rect(0, 0, rawCubemap.width, rawCubemap.height), 0, 0);
            Color[] pixels = rawCubemap.GetPixels();

            // Clean up.
            RenderTexture.active = null;
            foreach(var renderTarget in renderTargets)
            {
                RenderTexture.ReleaseTemporary(renderTarget);
            }
            RenderTexture.ReleaseTemporary(combinedRenderTarget);
            DestroyImmediate(rawCubemap);

            return pixels;
        }

        private void PrepMaterial(Texture2D iesTexture, IESData iesData)
        {
            if (_iesMaterial == null)
            {
                _iesMaterial = GetComponent<Renderer>().sharedMaterial;
            }
            _iesMaterial.mainTexture = iesTexture;

            SetShaderKeywords(iesData, _iesMaterial);
        }

        private void SetShaderKeywords(IESData iesData, Material iesMaterial)
        {
            // Fill either the entire sphere or only the bottom of top half, depending on the vertical angles described in the file.
            if (iesData.VerticalType == VerticalType.Bottom)
            {
                iesMaterial.EnableKeyword("BOTTOM_VERTICAL");
                iesMaterial.DisableKeyword("TOP_VERTICAL");
                iesMaterial.DisableKeyword("FULL_VERTICAL");
            }
            else if(iesData.VerticalType == VerticalType.Top)
            {
                iesMaterial.EnableKeyword("TOP_VERTICAL");
                iesMaterial.DisableKeyword("BOTTOM_VERTICAL");
                iesMaterial.DisableKeyword("FULL_VERTICAL");
            }
            else
            {
                iesMaterial.DisableKeyword("TOP_VERTICAL");
                iesMaterial.DisableKeyword("BOTTOM_VERTICAL");
                iesMaterial.EnableKeyword("FULL_VERTICAL");
            }

            // Also set the approrpiate keyword for horizontal symmetry.
            if (iesData.HorizontalType == HorizontalType.None)
            {
                iesMaterial.DisableKeyword("QUAD_HORIZONTAL");
                iesMaterial.DisableKeyword("HALF_HORIZONTAL");
                iesMaterial.DisableKeyword("FULL_HORIZONTAL");
            }
            else if (iesData.HorizontalType == HorizontalType.Quadrant)
            {
                iesMaterial.EnableKeyword("QUAD_HORIZONTAL");
                iesMaterial.DisableKeyword("HALF_HORIZONTAL");
                iesMaterial.DisableKeyword("FULL_HORIZONTAL");
            }
            else if (iesData.HorizontalType == HorizontalType.Half)
            {
                iesMaterial.DisableKeyword("QUAD_HORIZONTAL");
                iesMaterial.EnableKeyword("HALF_HORIZONTAL");
                iesMaterial.DisableKeyword("FULL_HORIZONTAL");
            }
            else if (iesData.HorizontalType == HorizontalType.Full)
            {
                iesMaterial.DisableKeyword("QUAD_HORIZONTAL");
                iesMaterial.DisableKeyword("HALF_HORIZONTAL");
                iesMaterial.EnableKeyword("FULL_HORIZONTAL");
            }
        }

        private void CreateCubemap(int resolution, out Cubemap cubemap)
        {
            // Mip maps in cookies can lead to artefacts - they are disabled to be sure. https://en.wikibooks.org/wiki/Cg_Programming/Unity/Cookies
            cubemap = new Cubemap(resolution, TextureFormat.ARGB32, false) { filterMode = FilterMode.Trilinear }; 
            GetComponent<Camera>().RenderToCubemap(cubemap);
        }
    }
}