using Microsoft.AspNetCore.Mvc;
using System;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Extensions to byte class
    /// </summary>
    public static class ByteExtension
    {
        /// <summary>
        /// Converts a byte Array to FileContentResult.
        /// Typically to show an image returned from MVC controller in an Html Img tag.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="contentType"></param>
        /// <returns>Content (typically images) to display from MVC controller.</returns>
        public static FileContentResult ToFileContentResult(this Byte[] item, string contentType)
        {
            return new FileContentResult(item, contentType);
        }
    }
}
