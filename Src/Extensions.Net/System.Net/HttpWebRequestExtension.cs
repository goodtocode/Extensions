using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// HttpRequestBaseExtension
    /// </summary>
    public static class HttpWebRequestExtension
    {
        /// <summary>
        /// Finds the root of the URL in format: HTTP://SERVER_NAME:SERVER_PORT
        /// </summary>
        /// <param name="item">Item to parse</param>
        /// <returns>Url from item</returns>
        public static string TryParseUrl(this HttpWebRequest item)
        {
            return item.RequestUri.AbsolutePath;
        }
        
        /// <summary>
        /// Checks for HTTPS, or returns true if ://localhost
        /// </summary>
        /// <param name="item">Item to parse</param>
        /// <returns>True if secured</returns>
        public static bool IsSecured(this HttpWebRequest item)
        {
            var returnValue = false;

            if (item.IsSecured() | item.RequestUri.ToString().ToString().Contains("://localhost"))
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH
        /// No trailing slash.
        /// </summary>
        /// <param name="protocol">Protocol for Url. I.e. http</param>
        /// <param name="serverName">Server name for Url. I.e. www.YourDomain.com</param>
        /// <param name="port">Port for Url. I.e. 80</param>
        /// <param name="applicationPath">Application path for Url. I.e. /Home/Index</param>
        /// <returns>Constructed url</returns>
        public static string ConstructUrl(string protocol, string serverName, string port, string applicationPath)
        {
            if (protocol == null || protocol == "0")
            {
                protocol = "http://";
            } else
            {
                protocol = "https://";
            }
            if (port == null || port == "80" || port == "443")
            {
                port = "";
            } else
            {
                port = ":" + port;
            }
            string urlComplete = protocol + serverName + port + applicationPath;
            urlComplete = urlComplete.RemoveLast("/");

            return urlComplete;
        }

        /// <summary>
        /// Formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH?Param1=Value1
        /// </summary>
        /// <param name="urlNoQuerystring">Url with everything but parameters and punctuation</param>
        /// <param name="parametersAndValues">Collection of parameters to add to Url</param>
        /// <returns>Constructed url</returns>
        public static string ConstructUrl(string urlNoQuerystring, List<KeyValuePair<string, string>> parametersAndValues)
        {
            var returnValue = new StringBuilder();

            returnValue.Append(urlNoQuerystring.RemoveLast("/"));
            if (parametersAndValues.Count > 0)
            {
                returnValue.Append("?" + Uri.EscapeDataString(parametersAndValues[0].Key) + "=" + Uri.EscapeDataString(parametersAndValues[0].Value));
                parametersAndValues.RemoveAt(0);
            }
            foreach (var Item in parametersAndValues)
            {
                returnValue.Append("&" + Uri.EscapeDataString(Item.Key) + "=" + Uri.EscapeDataString(Item.Value));
            }

            return returnValue.ToString();
        }

        /// <summary>
        /// Formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH?Param1=Value1
        /// </summary>
        /// <param name="urlNoQuerystring">Url with everything but parameters and punctuation</param>
        /// <param name="parametersAndValues">Collection of parameters to add to Url</param>
        /// <returns>Constructed url</returns>
        public static string ConstructUrl(string urlNoQuerystring, List<string> parametersAndValues)
        {
            var returnValue = new StringBuilder();

            returnValue.Append(urlNoQuerystring.RemoveLast("/"));
            foreach (var item in parametersAndValues)
            {
                returnValue.Append("/" + Uri.EscapeDataString(item));
            }

            return returnValue.ToString();
        }
    }
}
