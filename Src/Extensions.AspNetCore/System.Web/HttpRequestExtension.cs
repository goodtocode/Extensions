using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// HttpRequestExtension
    /// </summary>
    public static class HttpRequestExtension
    {
        /// <summary>
        /// Finds the root of the url in format: http://SERVER_NAME:SERVER_PORT
        /// </summary>
        /// <param name="item">Request to supply url</param>
        /// <returns>Parsed Url</returns>
        public static string TryParseUrl(this HttpRequest item)
        {
            return item.Path.ToString().RemoveLast(@"/");
        }

        /// <summary>
        /// Checks for HTTPS, or returns true if localhost
        /// </summary>
        /// <param name="item">Url to check</param>
        /// <returns>True if request is secured, or is localhost</returns>
        public static bool IsSecuredOrLocal(this HttpRequest item)
        {
            var returnValue = false;

            if (item.IsHttps | item.Path.ToStringSafe().Contains("://localhost"))
                returnValue = true;

            return returnValue;
        }

        /// <summary>
        /// Gets HttpRequest header by name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="headerName"></param>
        /// <returns></returns>
        public static T GetHeader<T>(this HttpRequest item, string headerName)
        {
            var returnValue = default(T);

            if (item?.Headers?.TryGetValue(headerName, out StringValues values) ?? false)
            {
                string rawValues = values.ToString();
                if (rawValues?.Length > 0)
                    returnValue = (T)Convert.ChangeType(values.ToString(), typeof(T));
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
            string urlComplete;

            protocol = protocol == null || protocol == "0" ? "http://" : "https://";
            port = port == null || port == "80" || port == "443" ? "" : ":" + port;
            urlComplete = protocol + serverName + port + applicationPath;
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
        /// Formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH/Parameter1/Parameter2/.../ParameterN/
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
