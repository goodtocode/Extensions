using System;
using System.Drawing;
using System.IO;
using System.Xml.Linq;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Extension methods to the System.IO.Stream class
    /// </summary>
    public static class StreamExtension
    {
        /// <summary>
        /// Validates a stream to be a image-like type and is less than a maximum size
        /// </summary>
        /// <param name="item">Stream array to check for image</param>
        /// <param name="maxSizeInKb">Default is 4 Mb</param>
        /// <returns>True if stream is image</returns>
        public static bool IsImage(this Stream item, int maxSizeInKb = 4096)
        {            
            return new Bitmap(item).ToBytes().IsImage(maxSizeInKb);
        }
    }
}
