using System;
using System.Net.Http;
using System.Threading.Tasks;
using GoodToCode.Extensions;
using GoodToCode.Extensions.Security.Cryptography;
using System.Collections.Generic;
using System.Text;

namespace GoodToCode.Extensions.Net
{
    /// <summary>
    /// Communicates via GET, all transmissions String
    /// </summary>
    
    public abstract class HttpRequestClient : IDisposable
    {
        private bool disposed = false;
        private HttpClientBuilder clientValue = new HttpClientBuilder();
        private HttpResponseMessage responseValue = new HttpResponseMessage();

        /// <summary>
        /// Raw data received
        /// </summary>
        public string DataReceivedRaw { get; set; } = string.Empty;

        /// <summary>
        /// DataReceivedRaw value decrypted
        /// </summary>
        public string DataReceivedDecrypted { get; set; } = string.Empty;

        /// <summary>
        /// HttpClient for request
        /// </summary>
        public HttpClientBuilder Client { get { return clientValue; } protected set { clientValue = value; } }

        /// <summary>
        /// Response after request
        /// </summary>
        public HttpResponseMessage Response { get { return responseValue; } protected set { responseValue = value; } }

        /// <summary>
        /// Sets the HttpResponse completion option.
        ///  Default - Reading response header and content: HttpCompletionOption.ResponseContentRead
        ///  For reading response header, no content (i.e. Large files): HttpCompletionOption.ResponseHeadersRead
        /// </summary>
        public HttpCompletionOption CompletionOption { get; set; } = HttpCompletionOption.ResponseContentRead;

        /// <summary>
        /// Url of request
        /// </summary>
        public Uri Url { get; protected set; } = new Uri("http://localhost:80", UriKind.RelativeOrAbsolute);

        /// <summary>
        /// Specify if want to send plain text with no alterations
        /// </summary>
        public bool SendPlainText { get; protected set; } = false;

        /// <summary>
        /// Specify if want to send plain text with no alterations
        /// </summary>
        public bool ThrowExceptionWithEmptyReponse { get; set; } = false;

        /// <summary>
        /// Encryptor if plain text is off
        /// </summary>
        public IEncryptor Encryptor { get; protected set; } = new CaesarEncryptor(); // Start with simple cross platform class, replace with encryption of choice

        /// <summary>
        /// Immutable
        /// </summary>
        protected internal HttpRequestClient() : base()
        {
            SendPlainText = true;
#if (DEBUG)
            ThrowExceptionWithEmptyReponse = true;
#endif
        }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestClient(Uri url) : this() { Url = url; }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestClient(string url) : this(new Uri(url, UriKind.RelativeOrAbsolute)) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestClient(Uri url, IEncryptor encrptor) : this(url) { Encryptor = Encryptor; }

        /// <summary>
        /// Constructor that formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH
        /// No trailing slash.
        /// </summary>
        /// <param name="protocol">Protocol for Url. I.e. http</param>
        /// <param name="serverName">Server name for Url. I.e. www.YourDomain.com</param>
        /// <param name="port">Port for Url. I.e. 80</param>
        /// <param name="applicationPath">Application path for Url. I.e. /Home/Index</param>
        /// <returns>Constructed url</returns>
        public HttpRequestClient(string protocol, string serverName, int port, string applicationPath)
        {
            Url = new UrlBuilder(protocol, serverName, port, applicationPath).Uri;
        }

        /// <summary>
        /// Constructor that formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH?Param1=Value1
        /// </summary>
        /// <param name="urlNoQuerystring">Url with everything but parameters and punctuation</param>
        /// <param name="parametersAndValues">Collection of parameters to add to Url</param>
        /// <returns>Constructed url</returns>
        public HttpRequestClient(string urlNoQuerystring, List<KeyValuePair<string, string>> parametersAndValues)
        {
            var requestUri = new StringBuilder();

            requestUri.Append(urlNoQuerystring.RemoveLast("/"));
            if (parametersAndValues.Count > 0)
            {
                requestUri.Append("?" + Uri.EscapeDataString(parametersAndValues[0].Key) + "=" + Uri.EscapeDataString(parametersAndValues[0].Value));
                parametersAndValues.RemoveAt(0);
            }
            foreach (var Item in parametersAndValues)
            {
                requestUri.Append("&" + Uri.EscapeDataString(Item.Key) + "=" + Uri.EscapeDataString(Item.Value));
            }

            Url = new Uri(requestUri.ToString(), UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Constructor that formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH/Parameter1/Parameter2/.../ParameterN/
        /// </summary>
        /// <param name="urlNoQuerystring">Url with everything but parameters and punctuation</param>
        /// <param name="parametersAndValues">Collection of parameters to add to Url</param>
        /// <returns>Constructed url</returns>
        public HttpRequestClient(string urlNoQuerystring, IEnumerable<string> parametersAndValues)
        {
            Url = new UrlBuilder(urlNoQuerystring, parametersAndValues).Uri;
        }

        /// <summary>
        /// Synchronously sends a GET request, Receives string response
        /// </summary>
        /// <returns>Result</returns>
        public abstract string Send();

        /// <summary>
        /// Asynchronously sends a GET request, Receives strongly typed response
        /// </summary>
        /// <returns></returns>
        public abstract Task<string> SendAsync();

        /// <summary>
        /// string format of the request Url
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Url.ToString();
        }

        /// <summary>
        /// Dispose this object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Inheritance disposal
        /// </summary>
        /// <param name="disposing">Disposing state flag</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    clientValue.Dispose();
                    responseValue.Dispose();
                }
                disposed = true;
            }
        }
    }
}
