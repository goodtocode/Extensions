using System;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Net
{
    /// <summary>
    /// Encapsulates common Uri and Routing components
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>    
    public class UrlInfo : UrlBuilder
    {        
        /// <summary>
        /// DisplayName of this Url
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// RootValue
        /// </summary>
        private string RootValue = string.Empty;

        /// <summary>
        /// Root
        /// </summary>
        public string Root { get { return RootValue; } protected set { RootValue = value.RemoveLast("/"); } }

        /// <summary>
        /// Controller
        /// </summary>
        public string Controller { get; protected set; } = string.Empty;

        /// <summary>
        /// Action
        /// </summary>
        public string Action { get; protected set; } = string.Empty;

        /// <summary>
        /// Route
        /// </summary>
        public string Route { get { return Path.RemoveFirst("/").RemoveLast("/"); } }

        /// <summary>
        /// Constructor
        /// </summary>
        public UrlInfo() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public UrlInfo(string rootUrl, string path) : base(rootUrl, path) { }

        /// <summary>
        /// Constructor tuned for MVC pattern
        /// </summary>
        public UrlInfo(string rootUrl, string controller, string action) : base(String.Format("{0}/{1}/{2}", rootUrl.RemoveLast("/"), controller, action))
        {
            Root = rootUrl;
            Controller = controller;
            Action = action;
        }

        /// <summary>
        /// Constructor tuned for MVC pattern
        /// </summary>
        public UrlInfo(string fullUrl) : base(fullUrl) { }

        /// <summary>
        /// Checks for HTTPS, or returns true if localhost
        /// </summary>
        /// <returns>True if request is secured, or is localhost</returns>
        public bool IsSecuredOrLocal()
        {
            var returnValue = false;

            if (Uri.ToString().Contains("https://") | Uri.ToString().Contains("://localhost"))
            {
                returnValue = true;
            }

            return returnValue;
        }
    }    
}
