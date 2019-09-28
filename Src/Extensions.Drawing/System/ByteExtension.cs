using System;
using System.Drawing;
using System.IO;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Extensions to byte class
    /// </summary>
    public static class ByteExtension
    {
        /// <summary>
        /// Converts a byte array to System.Drawing.Image class
        /// </summary>
        /// <param name="item">Byte array of valid image-compatible data</param>
        /// <returns>Converted image class, or empty transparent 1px by 1px image</returns>
        public static Image ToImage(this Byte[] item)
        {
            Image returnValue = null;
            MemoryStream stream = null;

            if ((item == null == false) && (item.Length > 0))
            {
                stream = new MemoryStream(item, 0, item.Length);
                returnValue = Image.FromStream(stream);
            }
            else
            {
                returnValue = ImageExtension.ImageEmpty;
            }

            return returnValue;
        }

        /// <summary>
        /// Validates a byte array to be a image-like type and is less than a maximum size
        /// </summary>
        /// <param name="item">Byte array to check if it is an image.</param>
        /// <param name="maxSizeInKb">Invalidate if over this size in Kb. Default is 4mb/4096kb.</param>
        /// <returns>True if the byte array is an image</returns>
        public static bool IsImage(this byte[] item, int maxSizeInKb = 4096)
        {
            var returnValue = Defaults.Boolean;
            Image testImage;

            try
            {
                using (var ms = new MemoryStream(item))
                {
                    testImage = Image.FromStream(ms);
                    if (ms.Length <= (maxSizeInKb * 1024))
                    {
                        returnValue = true;
                    }
                }
            }
            catch (ArgumentException)
            {
                returnValue = false;
            }

            return returnValue;
        }
    }
}
