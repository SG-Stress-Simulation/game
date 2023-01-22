using System.IO;
using System.Linq;
using UnityEngine;

namespace IESLights
{
    [RequireComponent(typeof(IESToCubemap)), RequireComponent(typeof(IESToSpotlightCookie))]
    public class IESConverter : MonoBehaviour
    {
        public int Resolution = 512;
        public NormalizationMode NormalizationMode;

        private Texture2D _iesTexture;

        /// <summary>
        /// Converts an IES file to either a point or spot light cookie.
        /// </summary>
        public void ConvertIES(string filePath, string targetPath, bool createSpotlightCookies, bool rawImport, bool applyVignette, out Cubemap pointLightCookie, out Texture2D spotlightCookie, out EXRData exrData, out string targetFilename)
        {
            // Parse the ies data.
            IESData iesData = ParseIES.Parse(filePath, rawImport ? NormalizationMode.Linear : NormalizationMode);
            // Create a texture from the normalized IES data.
            _iesTexture = IESToTexture.ConvertIesData(iesData);

            // Regular import - creates cookies that are directly usable within Unity.
            if (!rawImport)
            {
                exrData = default(EXRData);
                RegularImport(filePath, targetPath, createSpotlightCookies, applyVignette, out pointLightCookie, out spotlightCookie, out targetFilename, iesData);
            }
            // Raw import - creates a .exr import of the IES data instead, to give the user full control.
            else
            {
                pointLightCookie = null;
                spotlightCookie = null;
                RawImport(iesData, filePath, targetPath, createSpotlightCookies, out exrData, out targetFilename);
            }

            // Clean up.
            if (_iesTexture != null)
            {
#if UNITY_EDITOR
                DestroyImmediate(_iesTexture);
#else
                Destroy(_iesTexture);
#endif
            }
        }

        private void RegularImport(string filePath, string targetPath, bool createSpotlightCookies, bool applyVignette, out Cubemap pointLightCookie, out Texture2D spotlightCookie, out string targetFilename, IESData iesData)
        {
            // If spot light cookie creation is enabled, check if the ies data can be projected into a spot light cookie.
            // Only half the sphere may be provided if the ies data is to fit inside a spot light cookie.
            // Automotive IES data is always a spotlight cookie.
            if ((createSpotlightCookies && iesData.VerticalType != VerticalType.Full) || iesData.PhotometricType == PhotometricType.TypeA)
            {
                pointLightCookie = null;
                GetComponent<IESToSpotlightCookie>().CreateSpotlightCookie(_iesTexture, iesData, Resolution, applyVignette, false, out spotlightCookie);
            }
            // Create a point light cookie cubemap in all other cases.
            else
            {
                spotlightCookie = null;
                GetComponent<IESToCubemap>().CreateCubemap(_iesTexture, iesData, Resolution, out pointLightCookie);
            }

            // Create the target file name and required folders.
            BuildTargetFilename(Path.GetFileNameWithoutExtension(filePath), targetPath, pointLightCookie != null, false, NormalizationMode, iesData, out targetFilename);
        }

        private void RawImport(IESData iesData, string filePath, string targetPath, bool createSpotlightCookie, out EXRData exrData, out string targetFilename)
        {
            // Use the spotlight cookie shader to apply symmetry to the ies data, but don't apply any vignetting.
            if ((createSpotlightCookie && iesData.VerticalType != VerticalType.Full) || iesData.PhotometricType == PhotometricType.TypeA)
            {
                Texture2D spotlightCookie = null;
                GetComponent<IESToSpotlightCookie>().CreateSpotlightCookie(_iesTexture, iesData, Resolution, false, true, out spotlightCookie);
                exrData = new EXRData(spotlightCookie.GetPixels(), Resolution, Resolution);
                DestroyImmediate(spotlightCookie);
            }
            // Create a cubemap and extract the faces into a pixel array.
            else
            {
                exrData = new EXRData(GetComponent<IESToCubemap>().CreateRawCubemap(_iesTexture, iesData, Resolution), Resolution * 6, Resolution);
            }

            BuildTargetFilename(Path.GetFileNameWithoutExtension(filePath), targetPath, false, true, NormalizationMode.Linear, iesData, out targetFilename);
        }

        private void BuildTargetFilename(string name, string folderHierarchy, bool isCubemap, bool isRaw, NormalizationMode normalizationMode, IESData iesData, out string targetFilePath)
        {
            if (!Directory.Exists(Path.Combine(Application.dataPath, string.Format("IES/Imports/{0}", folderHierarchy))))
            {
                Directory.CreateDirectory(Path.Combine(Application.dataPath, string.Format("IES/Imports/{0}", folderHierarchy)));
            }

            float spotlightFov = 0;
            if (iesData.PhotometricType == PhotometricType.TypeA)
            {
                spotlightFov = iesData.HorizontalAngles.Max() - iesData.HorizontalAngles.Min();
            }
            else if(!isCubemap)
            {
                spotlightFov = iesData.HalfSpotlightFov * 2;
            }

            // If this in an enhanced import, add the appropriate prefix.
            string prefix = "";
            if (normalizationMode == NormalizationMode.EqualizeHistogram)
            {
                prefix = "[H] ";
            }
            else if (normalizationMode == NormalizationMode.Logarithmic)
            {
                prefix = "[E] ";
            }

            // Set the correct extension.
            string extension = "";
            if (isRaw)
            {
                extension = "exr";
            }
            else
            {
                extension = isCubemap ? "cubemap" : "asset";
            }

            targetFilePath = Path.Combine(Path.Combine("Assets/IES/Imports/", folderHierarchy),
                string.Format("{0}{1}{2}.{3}", prefix, iesData.PhotometricType == PhotometricType.TypeA || !isCubemap ? "[FOV " + spotlightFov + "] " : "", name, extension));
            if (File.Exists(targetFilePath))
            {
                File.Delete(targetFilePath);
            }
        }
    }
}