using System;
using System.Drawing.Imaging;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Extends the Image Format class
    /// </summary>
    public static class ImageFormatExtension
    {
        /// <summary>
        /// Returns mime content type for an Image Format item
        /// </summary>
        /// <param name="item">MIME content type of item</param>
        /// <returns>string containing the MIME content type text</returns>
        public static string ToContentType(this ImageFormat item)
        {
            var returnValue = "image/unknown";
            var imgguid = item.Guid;
            foreach (var codec in ImageCodecInfo.GetImageDecoders())
            {
                if (codec.FormatID == imgguid)
                {
                    returnValue = codec.MimeType;
                    break;
                }
            }

            return returnValue;
        }
    }
}
