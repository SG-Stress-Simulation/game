using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

namespace IESLights
{
    public static class ParseIES
    {
        /// <summary>
        /// The minimum of normalized candela required to be considered lit in spot light fov calculation.
        /// </summary>
        private const float SpotlightCutoff = 0.1f;

        public static IESData Parse(string path, NormalizationMode normalizationMode)
        {
            // Find the line containing the number of vertical and horizontal angles.
            string[] lines = File.ReadAllLines(path);
            int lineNumber = 0;
            FindNumberOfAnglesLine(lines, ref lineNumber);
            if (lineNumber == lines.Length - 1)
            {
                throw new IESParseException("No line containing number of angles found.");
            }

            // Read the number of vertical and horizontal angles.
            int numberOfVerticalAngles, numberOfHorizontalAngles;
            PhotometricType photometricType;
            ReadProperties(lines, ref lineNumber, out numberOfVerticalAngles, out numberOfHorizontalAngles, out photometricType);

            // Read the vertical angles.
            List<float> verticalAngles = ReadValues(lines, numberOfVerticalAngles, ref lineNumber);

            // Read the horizontal angles.
            List<float> horizontalAngles = ReadValues(lines, numberOfHorizontalAngles, ref lineNumber);

            // Read the candela values for all vertical slices for each horizontal angle.
            List<List<float>> values = new List<List<float>>();
            for (int i = 0; i < numberOfHorizontalAngles; i++)
            {
                values.Add(ReadValues(lines, numberOfVerticalAngles, ref lineNumber));
            }

            IESData iesData = new IESData()
            {
                VerticalAngles = verticalAngles,
                HorizontalAngles = horizontalAngles,
                CandelaValues = values,
                PhotometricType = photometricType
            };

            // Normalize the candela values, applying the enhanced mode if requested.
            // The normalized values will also be used to discard practically unlit areas from spot light cookies.
            NormalizeValues(iesData, normalizationMode == NormalizationMode.Logarithmic);
            if (normalizationMode == NormalizationMode.EqualizeHistogram)
            {
                EqualizeHistogram(iesData);
            }

            // Prepare type C (and B, if it ever exists) photometry for possible spotlight cookie usage.
            if (photometricType != PhotometricType.TypeA)
            {
                DiscardUnusedVerticalHalf(iesData);
                SetVerticalAndHorizontalType(iesData);
                iesData.HalfSpotlightFov = CalculateHalfSpotFov(iesData);
            }
            // Prepare type A (automotive) photometry.
            else
            {
                PadToSquare(iesData);
            }

            return iesData;
        }

        /// <summary>
        /// The given ies file may have vertical angles from 0 to 180, but only one half actually contains light.
        /// If this is the case, the unused half is discarded, and a spot light cookie can be created.
        /// </summary>
        private static void DiscardUnusedVerticalHalf(IESData iesData)
        {
            if (iesData.VerticalAngles[0] != 0 || iesData.VerticalAngles[iesData.VerticalAngles.Count - 1] != 180)
                return;

            // Check bottom half.
            for (int i = 0; i < iesData.VerticalAngles.Count; i++)
            {
                // There is light in the bottom half.
                if (iesData.NormalizedValues.Any(slice => slice[i] > SpotlightCutoff))
                    break;

                // There is no light in the bottom half. Discard it.
                if (iesData.VerticalAngles[i] == 90)
                {
                    DiscardBottomHalf(iesData);
                    return;
                }
                // There was no fixed 90 degree value - this can only happen in an improperly formatted file.
                else if (iesData.VerticalAngles[i] > 90)
                {
                    iesData.VerticalAngles[i] = 90;
                    DiscardBottomHalf(iesData);
                    return;
                }
            }

            // Check top half.
            for (int i = iesData.VerticalAngles.Count - 1; i >= 0; i--)
            {
                // There is light in the top half.
                if (iesData.NormalizedValues.Any(slice => slice[i] > SpotlightCutoff))
                    break;

                // There is no light in the top half. Discard it.
                if (iesData.VerticalAngles[i] == 90)
                {
                    DiscardTopHalf(iesData);
                    return;
                }
                // There was no fixed 90 degree value - this can only happen in an improperly formatted file.
                else if (iesData.VerticalAngles[i] < 90)
                {
                    iesData.VerticalAngles[i] = 90;
                    DiscardTopHalf(iesData);
                    return;
                }
            }
        }

        private static void DiscardBottomHalf(IESData iesData)
        {
            int range = 0;
            for (int i = 0; i < iesData.VerticalAngles.Count; i++)
            {
                if (iesData.VerticalAngles[i] == 90)
                    break;

                range++;
            }

            DiscardHalf(iesData, 0, range);
        }

        private static void DiscardTopHalf(IESData iesData)
        {
            int start = 0;
            for (int i = 0; i < iesData.VerticalAngles.Count; i++)
            {
                if (iesData.VerticalAngles[i] == 90)
                {
                    start = i + 1;
                    break;
                }
            }

            int range = iesData.VerticalAngles.Count - start;
            DiscardHalf(iesData, start, range);
        }

        private static void DiscardHalf(IESData iesData, int start, int range)
        {
            iesData.VerticalAngles.RemoveRange(start, range);
            for (int i = 0; i < iesData.CandelaValues.Count; i++)
            {
                iesData.CandelaValues[i].RemoveRange(start, range);
                iesData.NormalizedValues[i].RemoveRange(start, range);
            }
        }

        /// <summary>
        /// Adds black padding around automotive ies data to make it square - automotive ies usually has a wide aspect, which if left unchecked would create a squashed cookie.
        /// </summary>
        private static void PadToSquare(IESData iesData)
        {
            // Don't attempt to pad an already square image.
            if (Mathf.Abs(iesData.HorizontalAngles.Count - iesData.VerticalAngles.Count) <= 1)
                return;

            int longestSide = Mathf.Max(iesData.HorizontalAngles.Count, iesData.VerticalAngles.Count);

            if (iesData.HorizontalAngles.Count < longestSide)
            {
                PadHorizontal(iesData, longestSide);
            }
            else
            {
                PadVertical(iesData, longestSide);
            }
        }

        private static void PadHorizontal(IESData iesData, int longestSide)
        {
            int totalPadAmount = longestSide - iesData.HorizontalAngles.Count;
            int halfPadAmount = totalPadAmount / 2;
            iesData.PadBeforeAmount = iesData.PadAfterAmount = halfPadAmount;

            // Add empty vertical slices on the left side.
            var emptySlice = Enumerable.Repeat<float>(0, iesData.VerticalAngles.Count).ToList();
            for (int i = 0; i < halfPadAmount; i++)
            {
                iesData.NormalizedValues.Insert(0, emptySlice);
            }
            // Add empty vertical slices on the right side.
            for (int i = 0; i < totalPadAmount - halfPadAmount; i++)
            {
                iesData.NormalizedValues.Add(emptySlice);
            }
        }

        private static void PadVertical(IESData iesData, int longestSide)
        {
            int totalPadAmount = longestSide - iesData.VerticalAngles.Count;

            // If the sign of the highest and lowest angle are equal, just use the halfway point as center point.
            if (Mathf.Sign(iesData.VerticalAngles[0]) == Math.Sign(iesData.VerticalAngles[iesData.VerticalAngles.Count - 1]))
            {
                int halfPadAmount = totalPadAmount / 2;

                iesData.PadBeforeAmount = halfPadAmount;
                iesData.PadAfterAmount = totalPadAmount - halfPadAmount;
                foreach (var verticalSlice in iesData.NormalizedValues)
                {
                    verticalSlice.InsertRange(0, new List<float>(new float[halfPadAmount]));
                    verticalSlice.AddRange(new List<float>(new float[totalPadAmount - halfPadAmount]));
                }
            }
            // Otherwise, keep 0 degrees on the horizon.
            else
            {
                // Calculate the amount of entries to add at the top (angle > 0)
                int topPadAmount = longestSide / 2 - iesData.VerticalAngles.Count(v => v >= 0);

                // If vertical angles start negative...
                if (iesData.VerticalAngles[0] < 0)
                {
                    iesData.PadBeforeAmount = totalPadAmount - topPadAmount;
                    iesData.PadAfterAmount = topPadAmount;
                    foreach (var verticalSlice in iesData.NormalizedValues)
                    {
                        verticalSlice.InsertRange(0, new List<float>(new float[totalPadAmount - topPadAmount]));
                        verticalSlice.AddRange(new List<float>(new float[topPadAmount]));
                    }
                }
                // Else, if the vertical angles start positive...
                else
                {
                    iesData.PadBeforeAmount = topPadAmount;
                    iesData.PadAfterAmount = totalPadAmount - topPadAmount;
                    foreach (var verticalSlice in iesData.NormalizedValues)
                    {
                        verticalSlice.InsertRange(0, new List<float>(new float[topPadAmount]));
                        verticalSlice.AddRange(new List<float>(new float[totalPadAmount - topPadAmount]));
                    }
                }
            }
        }

        private static void SetVerticalAndHorizontalType(IESData iesData)
        {
            // Vertical angles are usually from 0° to 90° (type C), but can also be from -90° to 0°. (type A or B).
            if (iesData.VerticalAngles[0] == 0 && iesData.VerticalAngles[iesData.VerticalAngles.Count - 1] == 90
                || iesData.VerticalAngles[0] == -90 && iesData.VerticalAngles[iesData.VerticalAngles.Count - 1] == 0)
            {
                iesData.VerticalType = VerticalType.Bottom;
            }
            // If the last vertical angle is 180 and the first is 90, only the top half of the sphere is used.
            else if (iesData.VerticalAngles[iesData.VerticalAngles.Count - 1] == 180 && iesData.VerticalAngles[0] == 90)
            {
                iesData.VerticalType = VerticalType.Top;
            }
            else
            {
                iesData.VerticalType = VerticalType.Full;
            }

            // There can either be 1 horizontal angle (a single vertical slice is lathed around the polar axis), a set of 90 degrees with quadrant symmetry,
            // a set of 180 degrees with regular symmetry, or a full set of 360 degrees.
            if (iesData.HorizontalAngles.Count == 1)
            {
                iesData.HorizontalType = HorizontalType.None;
            }
            else if (iesData.HorizontalAngles[iesData.HorizontalAngles.Count - 1] - iesData.HorizontalAngles[0] == 90)
            {
                iesData.HorizontalType = HorizontalType.Quadrant;
            }
            else if (iesData.HorizontalAngles[iesData.HorizontalAngles.Count - 1] - iesData.HorizontalAngles[0] == 180)
            {
                iesData.HorizontalType = HorizontalType.Half;
            }
            else
            {
                iesData.HorizontalType = HorizontalType.Full;

                if (iesData.HorizontalAngles[iesData.HorizontalAngles.Count - 1] != 360)
                {
                    StitchHorizontalAssymetry(iesData);
                }
            }
        }

        /// <summary>
        /// Copies the first vertical slice to the 360 degree position to make fully assymetric data line up.
        /// </summary>
        private static void StitchHorizontalAssymetry(IESData iesData)
        {
            iesData.HorizontalAngles.Add(360);
            iesData.CandelaValues.Add(iesData.CandelaValues[0]);
            iesData.NormalizedValues.Add(iesData.NormalizedValues[0]);
        }

        private static float CalculateHalfSpotFov(IESData iesData)
        {
            // Handle [0,90] degree range.
            if (iesData.VerticalType == VerticalType.Bottom && iesData.VerticalAngles[0] == 0)
            {
                return CalculateHalfSpotlightFovForBottomHalf(iesData);
            }
            // Handle [90,180] and [-90,0] degree range.
            else if (iesData.VerticalType == VerticalType.Top || (iesData.VerticalType == VerticalType.Bottom && iesData.VerticalAngles[0] == -90))
            {
                return CalculateHalfSpotlightFovForTopHalf(iesData);
            }
            // [0,180] degree range.
            else
            {
                return -1;
            }
        }

        private static float CalculateHalfSpotlightFovForBottomHalf(IESData iesData)
        {
            // Find the first angle at which the candela is greater than 0.
            for (int i = iesData.VerticalAngles.Count - 1; i >= 0; i--)
            {
                // Loop over all horizontal angles.
                for (int h = 0; h < iesData.NormalizedValues.Count; h++)
                {
                    if (iesData.NormalizedValues[h][i] >= SpotlightCutoff)
                    {
                        if (i < iesData.VerticalAngles.Count - 1)
                        {
                            return iesData.VerticalAngles[i + 1];
                        }
                        else
                        {
                            return iesData.VerticalAngles[i];
                        }
                    }
                }
            }

            return 0;
        }

        private static float CalculateHalfSpotlightFovForTopHalf(IESData iesData)
        {
            for (int i = 0; i < iesData.VerticalAngles.Count; i++)
            {
                // Loop over all horizontal angles.
                for (int h = 0; h < iesData.NormalizedValues.Count; h++)
                {
                    if (iesData.NormalizedValues[h][i] >= SpotlightCutoff)
                    {
                        // [90,180]
                        if (iesData.VerticalType == VerticalType.Top)
                        {
                            if (i > 0)
                            {
                                return 180f - iesData.VerticalAngles[i - 1];
                            }
                            else
                            {
                                return 180f - iesData.VerticalAngles[i];
                            }
                        }
                        // [-90,0]
                        else
                        {
                            if (i > 0)
                            {
                                return -iesData.VerticalAngles[i - 1];
                            }
                            else
                            {
                                return -iesData.VerticalAngles[i];
                            }
                        }
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Normalizes the values to a continious 0-180 and 0-360 list, and normalizes all values to the 0-1 range.
        /// </summary>
        /// <param name="squashHistogram">
        /// Take the natural log of candela values before normalization, to reduce the impact of outliers. This results in a more detailed IES light, with less influence of the most illuminated region.
        /// </param>
        private static void NormalizeValues(IESData iesData, bool squashHistogram)
        {
            iesData.NormalizedValues = new List<List<float>>();

            float maxValue = iesData.CandelaValues.SelectMany(v => v).Max();
            if (squashHistogram)
            {
                maxValue = Mathf.Log(maxValue);
            }

            // Normalize each vertical slice.
            foreach (List<float> verticalSlice in iesData.CandelaValues)
            {
                List<float> normalizedSlice = new List<float>();

                if (squashHistogram)
                {
                    // Take the natural log of candela values before normalization, to reduce the impact of outliers. 
                    // This results in a more detailed IES light, with less influence of the most illuminated region.
                    for (int i = 0; i < verticalSlice.Count; i++)
                    {
                        normalizedSlice.Add(Mathf.Log(verticalSlice[i]));
                    }
                }
                else
                {
                    normalizedSlice.AddRange(verticalSlice);
                }

                // Normalize the values to 0-1 range.
                for (int i = 0; i < verticalSlice.Count; i++)
                {
                    normalizedSlice[i] /= maxValue;
                    normalizedSlice[i] = Mathf.Clamp01(normalizedSlice[i]);
                }

                iesData.NormalizedValues.Add(normalizedSlice);
            }
        }

        /// <summary>
        /// Equalized the histogram of the IES data, instead of squashing it using the natural log. Unused for now, since results are not worth the long processing time.
        /// </summary>
        private static void EqualizeHistogram(IESData iesData)
        {
            int grayscaleValues = Mathf.Min((int)iesData.CandelaValues.SelectMany(v => v).Max(), 10000);
            float[] probabilityMassFunction = new float[grayscaleValues];
            float[] cumulativeDistributiveFunction = new float[grayscaleValues];

            // Count the amount of times a grayscale value appears.
            foreach (var slice in iesData.NormalizedValues)
            {
                foreach (float value in slice)
                {
                    probabilityMassFunction[(int)(value * (grayscaleValues - 1))]++;
                }
            }

            // Divide by the total amount of values.
            float totalValuesCount = iesData.HorizontalAngles.Count * iesData.VerticalAngles.Count;
            for (int i = 0; i < probabilityMassFunction.Length; i++)
            {
                probabilityMassFunction[i] /= totalValuesCount;
            }

            // Calculate the cumulative distributive function.
            for (int i = 0; i < grayscaleValues; i++)
            {
                cumulativeDistributiveFunction[i] = probabilityMassFunction.Take(i + 1).Sum();
            }

            // Remap the values.
            foreach (var slice in iesData.NormalizedValues)
            {
                for (int i = 0; i < slice.Count; i++)
                {
                    // Calculate the current grayscale value.
                    int currentGrayscaleValue = (int)(slice[i] * (grayscaleValues - 1));
                    // Use it to look up the target equalized value.
                    slice[i] = (cumulativeDistributiveFunction[currentGrayscaleValue] * (grayscaleValues - 1)) / grayscaleValues;
                }
            }
        }

        /// <summary>
        /// The number of angles line is found underneath the TILT= line. If TILT=NONE, the line is directly underneath, otherwise there's four lines of tilt info in between.
        /// The TILT line is looked for because any number of user defined attributes can be defined in front of this line, but the format is fixed after this line.
        /// </summary>
        private static void FindNumberOfAnglesLine(string[] lines, ref int lineNumber)
        {
            int i;
            for (i = 0; i < lines.Length; i++)
            {
                // Move to the TILT= line.
                if (!lines[i].Trim().StartsWith("TILT"))
                    continue;
                // If there is tilt information present in the file, skip the next 4 lines, as they only relate to tilt info.
                try
                {
                    if (lines[i].Split('=')[1].Trim() != "NONE")
                    {
                        i += 5;
                        break;
                    }
                    // If there is no tilt information, the next line is the one we want.
                    else
                    {
                        i++;
                        break;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new IESParseException("No TILT line present.");
                }
            }

            lineNumber = i;
        }

        private static void ReadProperties(string[] lines, ref int lineNumber, out int numberOfVerticalAngles, out int numberOfHorizontalAngles, out PhotometricType photometricType)
        {
            // Read 13 values - these values contain several bits of information, of which only a couple are of importance to us.
            // All values are read so the line is advanced to the start of the angles declaration. Normally these properties should be fixed on 2 lines,
            // but ill formatted ies files are not uncommon.
            List<float> properties = ReadValues(lines, 13, ref lineNumber);

            // The fourth item is the number of vertical angles.
            numberOfVerticalAngles = (int)properties[3];
            // The fifth is the number of horizontal angles.
            numberOfHorizontalAngles = (int)properties[4];
            // The sixth item is the photometric type enum.
            photometricType = (PhotometricType)(properties[5]);
        }

        /// <summary>
        /// Tries to read a given number of floating point values, separated by spaces and lines.
        /// </summary>
        private static List<float> ReadValues(string[] lines, int numberOfValuesToFind, ref int lineNumber)
        {
            List<float> values = new List<float>();

            // The angles can be split over multiple lines. Read all of them until the given amount of angles is found.
            while (values.Count < numberOfValuesToFind)
            {
                if (lineNumber >= lines.Length)
                {
                    throw new IESParseException("Reached end of file before the given number of values was read.");
                }

                // Split the line to get the individual angles.
                // Space by default.
                char[] separator = null;
                // Some incorrectly formatted IES files use commas instead.
                if (lines[lineNumber].Contains(","))
                    separator = new char[] { ',' };

                string[] anglesOnLine = lines[lineNumber].Split(separator, StringSplitOptions.RemoveEmptyEntries);

                // Parse all angles and add them to the list.
                foreach (string angle in anglesOnLine)
                {
                    try
                    {
                        values.Add(float.Parse(angle, CultureInfo.InvariantCulture));
                    }
                    catch (Exception ex)
                    {
                        throw new IESParseException("Invalid value declaration.", ex);
                    }
                }
                // Move to the next line.
                lineNumber++;
            }

            return values;
        }
    }

    public enum NormalizationMode
    {
        Linear,
        Logarithmic,
        EqualizeHistogram
    }

    [Serializable]
    public class IESParseException : Exception
    {
        public IESParseException() { }
        public IESParseException(string message) : base(message) { }
        public IESParseException(string message, Exception inner) : base(message, inner) { }
        protected IESParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}