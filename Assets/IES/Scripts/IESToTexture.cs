using UnityEngine;

namespace IESLights
{
    public class IESToTexture : MonoBehaviour
    {
        /// <summary>
        /// Converts IES data to a 2D map representing the unwrapped photometric sphere.
        /// </summary>
        /// <returns>
        /// 2D texture containing the full lighting information.
        /// </returns>
        public static Texture2D ConvertIesData(IESData data)
        {
            Texture2D texture = new Texture2D(data.NormalizedValues.Count, data.NormalizedValues[0].Count, TextureFormat.RGBAFloat, false, true) { filterMode = FilterMode.Trilinear, wrapMode = TextureWrapMode.Clamp };
            Color[] pixels = new Color[texture.width * texture.height];

            // Apply the normalized value of each candela measurement as a pixel.
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    float value = data.NormalizedValues[x][y];
                    pixels[x + y * texture.width] = new Color(value, value, value, value);
                }
            }

            texture.SetPixels(pixels);
            texture.Apply();

            return texture;
        }
    }
}