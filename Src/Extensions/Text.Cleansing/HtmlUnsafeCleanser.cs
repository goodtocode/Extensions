using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Text.Cleansing
{
    /// <summary>
    /// Cleanses and removes Html unsafe characters
    /// </summary>
    
    public class HtmlUnsafeCleanser : Cleanser
    {
        private string wrapperTag = "wrapperTag";
        private string safeTag = "span";

        /// <summary>
        /// Target of this cleanser
        /// </summary>
        public override CleanserIds CleanserId { get; } = CleanserIds.UnsafeHtml;

        /// <summary>
        /// Array of safe tags
        /// </summary>
        public string[] SafeTags { get; set; } = { "#text", "p", "br", "strong", "b", "em", "i", "u", "strike", "ol", "ul", "li", "a", "q", "site", "abbr", "acronym", "del", "ins" };

        /// <summary>
        /// Array of safe attributes
        /// </summary>
        public string[] SafeAttributes { get; set; } = { "height", "width", "alt" };

        /// <summary>
        /// Array of safe keys
        /// </summary>
        public string[] SafeStyleKeys { get; set; } = { "" };

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlUnsafeCleanser() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="textToCleanse">Plain text to have characters cleansed</param>
        public HtmlUnsafeCleanser(string textToCleanse)
            : this()
        {
            TextToCleanse = textToCleanse;
        }

        /// <summary>
        /// Cleanses a string
        /// </summary>
        public override string Cleanse()
        {
            var docToParse = XDocument.Parse(String.Format("{0}{1}{2}", FormatBeginTag(wrapperTag), this.TextToCleanse, FormatEndTag(wrapperTag)));

            CleanseUnsafeHtml(docToParse);
            TextCleansed = docToParse.ToString();
            TextCleansed = this.TextCleansed.RemoveFirst(FormatBeginTag(wrapperTag)).RemoveLast(FormatEndTag(wrapperTag));

            return TextCleansed;
        }

        /// <summary>
        /// Recursive parser
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="rootElement"></param>
        private void CleanseUnsafeHtml(XDocument doc, XElement rootElement = null)
        {
            IEnumerable<XElement> elements;
            rootElement = rootElement ?? doc.Elements(wrapperTag).FirstOrDefault(); // We forced a root placeholder element to ensure one child begins the tree
            elements = rootElement.Elements();

            foreach (XElement element in elements)
            {
                if (!(SafeTags.Any(x => x.ToLowerInvariant() == element.Name.LocalName.ToLowerInvariant())))
                {
                    element.Name = safeTag;
                    element.RemoveAttributes();
                } 
                else if (element.Attributes() != null)
                {
                    var attrList = element.Attributes().OfType<XAttribute>().ToList();
                    foreach (XAttribute attr in attrList)
                    {
                        if (!(SafeAttributes.Any(x => x.ToLowerInvariant() == attr.Name)))
                        {
                            element.RemoveAttributes();
                        }
                    }
                }
                if (element.Descendants().Count() > 0)
                    CleanseUnsafeHtml(doc, element);
            }
        }

        /// <summary>
        /// Formats a begin tag
        /// </summary>
        /// <param name="tagName">Name of tag</param>
        /// <returns></returns>
        private string FormatBeginTag(string tagName)
        {
            return String.Format("<{0}>", tagName);
        }

        /// <summary>
        /// Formats a end tag
        /// </summary>
        /// <param name="tagName">Name of tag</param>
        /// <returns></returns>
        private string FormatEndTag(string tagName)
        {
            return String.Format("</{0}>", tagName);
        }
    }
}
