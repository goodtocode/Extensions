using System;

namespace GoodToCode.Extensions.Media
{    
    /// <summary>
    /// Color info in RGB converted format
    /// </summary>
    
    public class RGBByteInfo
    {
        /// <summary>
        /// Alpha channel (transparency)
        /// </summary>
        public byte Alpha { get; set; } = 0;
        /// <summary>
        /// Blue channel
        /// </summary>
        public byte Blue { get; set; } = 0;
        /// <summary>
        /// Green channel
        /// </summary>
        public byte Green { get; set; } = 0;
        /// <summary>
        /// Red channel
        /// </summary>
        public byte Red { get; set; } = 0;
        
        /// <summary>
        /// Converts RGB to Hex #RRGGBB
        /// </summary>
        /// <returns></returns>
        public string ToHex()
        {
            return String.Format("#{0}{1}{2}", this.Red.ToString("X2"), this.Green.ToString("X2"), this.Blue.ToString("X2"));
        }
        /// <summary>
        /// Converts RGB to RGB(RR,GG,BB)
        /// </summary>
        /// <returns></returns>
        public string ToRGBString()
        {
            return String.Format("RGB({0},{1},{2})", this.Red.ToString(), this.Green.ToString(), this.Blue.ToString());
        }       
    }
}
