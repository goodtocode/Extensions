using System;
using System.Drawing;
using System.IO;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Icon Extension
    /// </summary>
    public static class IconExtension
    {
        /// <summary>
        /// Converts a System.Drawing.Icon to a byte array
        /// </summary>
        /// <returns>Byte array containing the icon</returns>
        public static byte[] ToBytes(this Icon item)
        {
            var returnValue = new MemoryStream();
            if ((item == null == false) && (item.Size.Width > 0 & item.Size.Height > 0))
            {
                item.Save(returnValue);
            }
            return returnValue.ToArray();
        }
    }
}
