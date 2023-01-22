using System;
using System.IO;
using UnityEngine;

namespace IESLights
{
    /// <summary>
    /// Exposes IES importing for runtime use.
    /// </summary>
    public class RuntimeIESImporter : MonoBehaviour
    {
        /// <summary>
        /// Imports the target .ies file and returns either the spotlight or point light cookie. If the given .ies file describes a spotlight, only the spotlight cookie will be returned. Else, only the point light cookie will be returned.
        /// </summary>
        /// <param name="path">The full path of the ies file. The file does not need to be inside StreamingAssets.</param>
        /// <param name="resolution">The resolution to create the cookie at.</param>
        /// <param name="enhancedImport">Whether or not to use enhanced import. Enhanced imports have greatly increased detail, but are not scientifically correct.</param>
        /// <param name="spotlightCookie">The parsed spotlight cookie - may be null if the given .ies file does not describe a spotlight.</param>
        /// <param name="pointLightCookie">The parsed point light cookie - may be null if the given .ies file could be parsed as a spotlight cookie instead.</param>
        /// <param name="applyVignette">The spotlight cookie will automatically be faded to black at the edges to prevent a visible cut off which leads to graphical glitches.</param>
        public static void Import(string path, out Texture2D spotlightCookie, out Cubemap pointLightCookie, int resolution = 128, bool enhancedImport = false, bool applyVignette = true)
        {
            // Init return values.
            spotlightCookie = null;
            pointLightCookie = null;

            // Check file validity.
            if (!IsFileValid(path))
                return;

            // Fetch and instantiate the cubemap sphere.
            GameObject cubemapSphere;
            IESConverter iesConverter;
            GetIESConverterAndCubeSphere(enhancedImport, resolution, out cubemapSphere, out iesConverter);

            // Parse the IES file.
            ImportIES(path, iesConverter, true, applyVignette, out spotlightCookie, out pointLightCookie);

            // Clean up.
            Destroy(cubemapSphere);
        }

        /// <summary>
        /// Imports the target .ies file and returns the spotlight cookie, unless the .ies file does not describe a spotlight.
        /// </summary>
        /// <param name="path">The full path of the ies file. The file does not need to be inside StreamingAssets.</param>
        /// <param name="resolution">The resolution to create the cookie at.</param>
        /// <param name="enhancedImport">Whether or not to use enhanced import. Enhanced imports have greatly increased detail, but are not scientifically correct.</param>
        /// /// <param name="applyVignette">The spotlight cookie will automatically be faded to black at the edges to prevent a visible cut off which leads to graphical glitches.</param>
        /// <returns>The imported spotlight cookie, or null if a cookie could not be created.</returns>
        public static Texture2D ImportSpotlightCookie(string path, int resolution = 128, bool enhancedImport = false, bool applyVignette = true)
        {
            // Check file validity.
            if (!IsFileValid(path))
                return null;

            // Fetch and instantiate the cubemap sphere.
            GameObject cubemapSphere;
            IESConverter iesConverter;
            GetIESConverterAndCubeSphere(enhancedImport, resolution, out cubemapSphere, out iesConverter);

            // Parse the IES file.
            Texture2D spotlightCookie;
            Cubemap pointLightCookie;
            ImportIES(path, iesConverter, true, applyVignette, out spotlightCookie, out pointLightCookie);

            // Clean up.
            Destroy(cubemapSphere);
            return spotlightCookie;
        }

        /// <summary>
        /// Imports the target .ies file and returns a point light cookie, unless the .ies file describes an automotive light - these can only be imported as spot lights.
        /// </summary>
        /// <param name="path">The full path of the ies file. The file does not need to be inside StreamingAssets.</param>
        /// <param name="resolution">The resolution to create the cookie at.</param>
        /// <param name="enhancedImport">Whether or not to use enhanced import. Enhanced imports have greatly increased detail, but are not scientifically correct.</param>
        /// <returns>The imported point light cookie, or null if a cookie could not be created.</returns>
        public static Cubemap ImportPointLightCookie(string path, int resolution = 128, bool enhancedImport = false)
        {
            // Check file validity.
            if (!IsFileValid(path))
                return null;

            // Fetch and instantiate the cubemap sphere.
            GameObject cubemapSphere;
            IESConverter iesConverter;
            GetIESConverterAndCubeSphere(enhancedImport, resolution, out cubemapSphere, out iesConverter);

            // Parse the IES file.
            Cubemap pointLightCookie;
            Texture2D spotlightCookie;
            ImportIES(path, iesConverter, false, false, out spotlightCookie, out pointLightCookie);

            // Clean up.
            Destroy(cubemapSphere);
            return pointLightCookie;
        }

        private static void GetIESConverterAndCubeSphere(bool logarithmicNormalization, int resolution, out GameObject cubemapSphere, out IESConverter iesConverter)
        {
            UnityEngine.Object cubemapSpherePrefab = Resources.Load("IES cubemap sphere");
            cubemapSphere = (GameObject)Instantiate(cubemapSpherePrefab);
            iesConverter = cubemapSphere.GetComponent<IESConverter>();
            iesConverter.NormalizationMode = logarithmicNormalization ? NormalizationMode.Logarithmic : NormalizationMode.Linear;
            iesConverter.Resolution = resolution;
        }

        private static void ImportIES(string path, IESConverter iesConverter, bool allowSpotlightCookies, bool applyVignette, out Texture2D spotlightCookie, out Cubemap pointlightCookie)
        {
            EXRData exrData;
            string targetFilename = null;
            spotlightCookie = null;
            pointlightCookie = null;

            try
            {
                iesConverter.ConvertIES(path, "", allowSpotlightCookies, false, applyVignette, out pointlightCookie, out spotlightCookie, out exrData, out targetFilename);
            }
            catch (IESParseException ex)
            {
                Debug.LogError(string.Format("[IES] Encountered invalid IES data in {0}. Error message: {1}", path, ex.Message));
            }
            catch (Exception ex)
            {
                Debug.LogError(string.Format("[IES] Error while parsing {0}. Please contact me through the forums or thomasmountainborn.com. Error message: {1}", path, ex.Message));
            }
        }

        private static bool IsFileValid(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogWarningFormat("[IES] The file \"{0}\" does not exist.", path);
                return false;
            }
            if (Path.GetExtension(path).ToLower() != ".ies")
            {
                Debug.LogWarningFormat("[IES] The file \"{0}\" is not an IES file.", path);
                return false;
            }

            return true;
        }
    }
}