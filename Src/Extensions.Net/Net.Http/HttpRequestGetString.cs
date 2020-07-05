using GoodToCode.Extensions.Security.Cryptography;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoodToCode.Extensions.Net
{
    /// <summary>
    /// Communicates via GET, all transmissions String
    /// </summary>

    public class HttpRequestGetString : HttpRequestClient
    {
        /// <summary>
        /// Immutable
        /// </summary>
        protected internal HttpRequestGetString() : base() { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestGetString(Uri url) : base(url) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestGetString(string url) : base(new Uri(url, UriKind.RelativeOrAbsolute)) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestGetString(Uri url, IEncryptor encrptor) : base(url, encrptor) { }

        /// <summary>
        /// Synchronously sends a GET request, Receives string response
        /// Warning: Not thread safe, particularly in Web-based UIs. This is a stopgap to allow legacy code to operate with blocking/deadlock risk.
        /// </summary>
        /// <returns>Result</returns>
        public override string Send()
        {
            var client = Task.Run<HttpResponseMessage>(async () => await Client.GetAsync(this.Url, this.CompletionOption));
            var Response = client.Result;

            if (this.Response.IsSuccessStatusCode)
            {
                var responseString = Task.Run<string>(async () => await Response.Content.ReadAsStringAsync());
                DataReceivedRaw = responseString.Result;
                DataReceivedRaw = DataReceivedRaw;
                if (ThrowExceptionWithEmptyReponse == true && DataReceivedRaw == string.Empty)
                { throw new System.DataMisalignedException("Response is empty. Expected data to be returned."); } else if (SendPlainText == false)
                { DataReceivedDecrypted = this.Encryptor.Decrypt(DataReceivedRaw); } else { DataReceivedDecrypted = DataReceivedRaw; }
            }
            return DataReceivedDecrypted;
        }

        /// <summary>
        /// Asynchronously sends a GET request, Receives strongly typed response
        /// </summary>
        /// <returns>Response data</returns>
        public override async Task<string> SendAsync()
        {
            Response = await this.Client.GetAsync(this.Url, this.CompletionOption);
            if (this.Response.IsSuccessStatusCode)
            {
                DataReceivedRaw = await this.Response.Content.ReadAsStringAsync();
                if (ThrowExceptionWithEmptyReponse == true && DataReceivedRaw == string.Empty)
                { throw new System.DataMisalignedException("Response is empty. Expected data to be returned."); } 
                else if (SendPlainText == false)
                    { DataReceivedDecrypted = this.Encryptor.Decrypt(DataReceivedRaw); } 
                else { DataReceivedDecrypted = DataReceivedRaw; }
            }
            return DataReceivedDecrypted;
        }
    }
}
