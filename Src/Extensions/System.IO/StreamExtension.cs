using System;
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
        /// Converts a Stream to XDocument. I.e. Reading an XML file
        /// </summary>
        /// <param name="item">Stream array containing the XDocument data.</param>
        /// <returns>XDocument from a valid Stream, or empty XDocument</returns>
        public static XDocument ToXDocument(this Stream item)
        {
            var returnValue = new XDocument();

            try
            {
                if (item?.Length > 0)
                {
                    returnValue = XDocument.Load(item);
                }
            }
            catch (NullReferenceException)
            {
                returnValue = new XDocument();
            }

            return returnValue;
        }
    }
}
