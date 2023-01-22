using System.Collections.Generic;
using UnityEngine;

namespace IESLights
{
    public class IESData
    {
        /// <summary>
        /// The angles at which candela measurements were taken, from the center of the photometric sphere to the hull. Each vertical slice uses these same angles.
        /// </summary>
        public List<float> VerticalAngles { get; set; }
        /// <summary>
        /// The angles around the polar axis at which vertical slices were measured.
        /// </summary>
        public List<float> HorizontalAngles { get; set; }
        /// <summary>
        /// Nested list of all candela values - the inner list are the actual candela measurements done in a vertical slice, the outer list groups them per vertical slice for each horizontal angle.
        /// </summary>
        public List<List<float>> CandelaValues { get; set; }
        /// <summary>
        /// The normalized candela values. These values can be the natural log of the actual values, depending on whether or not enhanced mode was enabled.
        /// </summary>
        public List<List<float>> NormalizedValues { get; set; }

        public PhotometricType PhotometricType { get; set; }
        /// <summary>
        /// Convenient way of checking what range of vertical angles is provided.
        /// </summary>
        public VerticalType VerticalType { get; set; }
        /// <summary>
        /// Convenient way of checking what range of horizontal angles is provided.
        /// </summary>
        public HorizontalType HorizontalType { get; set; }
        /// <summary>
        /// Only used for automotive cookies, this is the amount of values that were padded before the data to create a square cookie.
        /// </summary>
        public int PadBeforeAmount { get; set; }
        /// <summary>
        /// Only used for automotive cookies, this is the amount of values that were padded after the data to create a square cookie.
        /// </summary>
        public int PadAfterAmount { get; set; }
        /// <summary>
        /// The field of view of the spot light. -1 if the light can't be represented as a spot light.
        /// </summary>
        public float HalfSpotlightFov { get; set; }
    }

    /// <summary>
    /// Three photometric types are defined in the IES standard. Type C are architectural and street lights, type A are automotive lights, and type B is never really used.
    /// </summary>
    public enum PhotometricType
    {
        TypeC = 1,
        TypeB = 2,
        TypeA = 3
    }

    /// <summary>
    /// An IES file can either specify the full 180 degree range of angles, or only 90, in which case half of the photometric sphere stays black.
    /// </summary>
    public enum VerticalType
    {
        Full,
        Bottom,
        Top
    }

    /// <summary>
    /// An IES file can either specify the full 360 range of horizontal angles, have regular symmetry, quadrant symmetry, or specify no horizontal angles at all, in which case a single vertical slice is lathed around the polar axis.
    /// </summary>
    public enum HorizontalType
    {
        Full,
        Half,
        Quadrant,
        None
    }

    public struct EXRData
    {
        public Color[] Pixels;
        public uint Width;
        public uint Height;

        public EXRData(Color[] pixels, int width, int height)
        {
            Pixels = pixels;
            Width = (uint)width;
            Height = (uint)height;
        }
    }
}