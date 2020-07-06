using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Extends the XmlDocument class
    /// </summary>
    public static class XmlDocumentExtension
    {
        /// <summary>
        /// Outputs a string equivalent to the xml document
        /// </summary>
        /// <param name="item">XmlDocument to output</param>
        /// <returns>XML document serialized as a string</returns>
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static string Serialize(this XmlDocument item)
        {
            var returnValue = string.Empty;

            using (var stringWrite = new StringWriter())
            {
                using (var xmlWrite = XmlWriter.Create(stringWrite))
                {
                    item.WriteTo(xmlWrite);
                    xmlWrite.Flush();
                    returnValue = stringWrite.GetStringBuilder().ToString();
                }
            }

            return returnValue;
        }
    }
}
