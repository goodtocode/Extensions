using System;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Media
{
    /// <summary>
    /// Color info in RGB converted format
    /// </summary>
    
    public class RGBStandardInfo
    {
        /// <summary>
        /// Alpha channel (transparency)
        /// </summary>
        public float Alpha { get; set; } = Defaults.Single;
        /// <summary>
        /// Blue channel
        /// </summary>
        public float Blue { get; set; } = Defaults.Single;
        /// <summary>
        /// Green channel
        /// </summary>
        public float Green { get; set; } = Defaults.Single;
        /// <summary>
        /// Red channel
        /// </summary>
        public float Red { get; set; } = Defaults.Single;
        
        /// <summary>
        /// Inverses the current RGB values
        /// </summary>
        /// <returns></returns>
        public RGBStandardInfo Inverse()
        {
            RGBStandardInfo returnValue = new RGBStandardInfo
            {
                Red = (1.0f - this.Red),
                Green = (1.0f - this.Green),
                Blue = (1.0f - this.Blue)
            };
            return returnValue;
        }
    }
}
