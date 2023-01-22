using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace IESLights
{
    public class IESWizard : ScriptableWizard
    {
        public bool IncludeSubFolders = true;
        public bool AllowSpotlightCookies = true;
        public bool LogarithmicNormalization;
        public bool RawImport;
        public bool ApplyVignette = true;

        private static string[] _resolutions = new[] { "64", "128", "256", "512", "1024", "2048", "4096" };
        private int _resolutionIndex = 1;

        [MenuItem("Assets/Import IES lights")]
        static void CreateWizard()
        {
            DisplayWizard<IESWizard>("Import IES lights", "Import file", "Import folder");
        }

        override protected bool DrawWizardGUI()
        {
            bool valueChanged = false;

            EditorGUILayout.BeginVertical();

            // Resolution.
            EditorGUI.BeginChangeCheck();
            _resolutionIndex = EditorGUILayout.Popup("Cookie resolution", _resolutionIndex, _resolutions);
            valueChanged |= EditorGUI.EndChangeCheck();

            // Include sub folders.
            EditorGUI.BeginChangeCheck();
            IncludeSubFolders = EditorGUILayout.Toggle("Include sub folders", IncludeSubFolders);
            valueChanged |= EditorGUI.EndChangeCheck();

            // Logarithmic normalization.
            EditorGUI.BeginChangeCheck();
            EditorGUI.BeginDisabledGroup(RawImport);
            LogarithmicNormalization = EditorGUILayout.Toggle("Enhance detail", LogarithmicNormalization);
            if (LogarithmicNormalization)
            {
                EditorGUILayout.HelpBox("Enhanced import uses logarithmic normalization to reduce the impact of outliers, making darker areas more visible. Files are prefixed with [E].", MessageType.Info);
            }
            valueChanged |= EditorGUI.EndChangeCheck();
            EditorGUI.EndDisabledGroup();

            // Allow spotlight cookies.
            EditorGUI.BeginChangeCheck();
            AllowSpotlightCookies = EditorGUILayout.Toggle("Allow spotlight cookies", AllowSpotlightCookies);
            if (!AllowSpotlightCookies)
            {
                EditorGUILayout.HelpBox("The importer will always create point light cookies, regardless of the ies data provided.", MessageType.Info);
            }
            valueChanged |= EditorGUI.EndChangeCheck();

            // Apply vignette.
            EditorGUI.BeginChangeCheck();
            EditorGUI.BeginDisabledGroup(RawImport);
            ApplyVignette = EditorGUILayout.Toggle("Apply vignette", ApplyVignette);
            if (!ApplyVignette)
            {
                EditorGUILayout.HelpBox("Imported spotlight cookies will not be automatically vignetted, which can lead to graphical glitches if there are lit pixels at the edges. It may however be desirable to keep this disabled to get the complete light information. If you require full artistic control, use Raw HDR import instead.", MessageType.Info);
            }
            valueChanged |= EditorGUI.EndChangeCheck();
            EditorGUI.EndDisabledGroup();

            // Raw import.
            EditorGUI.BeginChangeCheck();
            RawImport = EditorGUILayout.Toggle("Raw HDR import", RawImport);
            if (RawImport)
            {
                EditorGUILayout.HelpBox("Instead of generating a cookie that's directly usable in Unity, raw import will output a .exr (HDR) import of the IES data, without applying any vignetting or enhancements. Please refer to the documentation for more info.", MessageType.Info);
            }
            valueChanged |= EditorGUI.EndChangeCheck();

            EditorGUILayout.EndVertical();

            return valueChanged;
        }

        void OnWizardCreate()
        {
            string file = null;
#if UNITY_5_3_OR_NEWER
            // Pick the file with a fixed filter.
            file = EditorUtility.OpenFilePanelWithFilters("Pick IES file", Application.dataPath, new[] { "IES files", "ies" });
#else
            // Pick the file using the filterless file panel, and check the extension afterwards.
            file = EditorUtility.OpenFilePanel("Pick IES file", Application.dataPath, "ies");
#endif
            if (string.IsNullOrEmpty(file))
                return;
            if (Path.GetExtension(file).ToLower() != ".ies")
            {
                Debug.Log("[IES] Selected file is not an .ies file.");
                return;
            }

            // Fetch and instantiate the cubemap sphere.
            GameObject cubemapSphere;
            IESConverter iesConverter;
            GetIESConverterAndCubeSphere(out cubemapSphere, out iesConverter);

            // Parse the single file.
            try
            {
                ImportIES(file, "", iesConverter);
                Debug.LogFormat("[IES] Successfully imported {0} to IES/Imports.", Path.GetFileName(file));
            }
            catch (IESParseException ex)
            {
                Debug.LogError(string.Format("[IES] Encountered invalid IES data in {0}. Error message: {1}", file, ex.Message));
            }
            catch (Exception ex)
            {
                Debug.LogError(string.Format("[IES] Error while parsing {0}. Please contact me through the forums or thomasmountainborn.com. Error message: {1}", file, ex.Message));
            }

            // Clean up.
            DestroyImmediate(cubemapSphere);
        }

        void OnWizardOtherButton()
        {
            string folder = EditorUtility.OpenFolderPanel("Pick root IES folder", Application.dataPath, "");
            if (string.IsNullOrEmpty(folder))
                return;

            // Fetch and instantiate the cubemap sphere.
            GameObject cubemapSphere;
            IESConverter iesConverter;
            GetIESConverterAndCubeSphere(out cubemapSphere, out iesConverter);

            // Parse all IES files in the folder hierarchy.
            ParseFolderHierarchy(folder, iesConverter);

            // Clean up.
            DestroyImmediate(cubemapSphere);
        }

        private void ImportIES(string filePath, string targetPath, IESConverter iesConverter)
        {
            Cubemap cubemap = null;
            Texture2D spotlightCookie = null;
            EXRData exrData;
            string targetFilename = null;

            iesConverter.ConvertIES(filePath, targetPath, AllowSpotlightCookies, RawImport, ApplyVignette, out cubemap, out spotlightCookie, out exrData, out targetFilename);

            if (!RawImport)
            {
                if (cubemap != null)
                {
                    AssetDatabase.CreateAsset(cubemap, targetFilename);
                }
                else
                {
                    AssetDatabase.CreateAsset(spotlightCookie, targetFilename);
                }
            }
            else
            {
                if (exrData.Pixels == null)
                    return;

                MiniEXR.MiniEXR.MiniEXRWrite(targetFilename, exrData.Width, exrData.Height, exrData.Pixels);
            }
        }

        private void GetIESConverterAndCubeSphere(out GameObject cubemapSphere, out IESConverter iesConverter)
        {
            UnityEngine.Object cubemapSpherePrefab = Resources.Load("IES cubemap sphere");
            cubemapSphere = (GameObject)Instantiate(cubemapSpherePrefab);
            iesConverter = cubemapSphere.GetComponent<IESConverter>();
            iesConverter.NormalizationMode = LogarithmicNormalization ? NormalizationMode.Logarithmic : NormalizationMode.Linear;
            iesConverter.Resolution = int.Parse(_resolutions[_resolutionIndex]);
        }

        private void ParseFolderHierarchy(string folder, IESConverter iesConverter)
        {
            EditorUtility.DisplayProgressBar("Finding .ies files...", "", 0);

            // Get all ies files in the folder (hierarchy).
            string[] iesFiles = Directory.GetFiles(folder, "*.ies", IncludeSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            if (iesFiles.Length == 0)
            {
                Debug.Log("[IES] There are no .ies files in the selected folder.");
                EditorUtility.ClearProgressBar();
                return;
            }

            Uri rootUri = new Uri(folder, UriKind.Absolute);

            int fileCount = iesFiles.Length;
            float currentFileIndex = 1;
            int successCount = 0;

            foreach (string file in iesFiles)
            {
                // Create the relative path, to be used in the IES/Imports folder.
                Uri fileUri = new Uri(Path.GetDirectoryName(file), UriKind.Absolute);
                string relativePath = "";
                // If the file is in the root import folder, simply use that folder name.
                if (fileUri == rootUri)
                {
                    relativePath = Path.GetFileName(folder);
                }
                // Else, create a relative path originating in the root import folder.
                else
                {
                    relativePath = Uri.UnescapeDataString(rootUri.MakeRelativeUri(fileUri).OriginalString);
                }
                string currentFileName = string.Format("{0}/{1}", relativePath, Path.GetFileName(file));

                // Display the progress, and allow cancelling.
                if (EditorUtility.DisplayCancelableProgressBar(string.Format("Parsing IES files... {0}/{1}", currentFileIndex, fileCount), currentFileName, currentFileIndex / fileCount))
                {
                    break;
                }

                try
                {
                    ImportIES(file, relativePath, iesConverter);
                    successCount++;
                }
                catch (IESParseException ex)
                {
                    Debug.LogError(string.Format("[IES] Encountered invalid IES data in {0}. Error message: {1}", file, ex.Message));
                }
                catch (Exception ex)
                {
                    // If an error occurs while parsing, log it and continue with the remaining files.
                    Debug.LogError(string.Format("[IES] Error while parsing {0}. Please contact me through the forums or thomasmountainborn.com. Error message: {1}", file, ex.Message));
                }

                currentFileIndex++;
            }

            Debug.LogFormat("[IES] Successfully imported {0} IES file{1} to IES/Imports/{2}.", successCount, successCount == 1 ? "" : "s", Path.GetFileName(folder));
            EditorUtility.ClearProgressBar();
        }
    }
}