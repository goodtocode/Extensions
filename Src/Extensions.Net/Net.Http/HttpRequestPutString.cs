using System;
using System.Net.Http;
using System.Threading.Tasks;
using GoodToCode.Extensions;
using GoodToCode.Extensions.Security.Cryptography;

namespace GoodToCode.Extensions.Net
{
    /// <summary>
    /// Communicates via PUT, string in and string out
    /// </summary>
    
    public class HttpRequestPutString : HttpRequestClient
    {
        /// <summary>
        /// DataToSend
        /// </summary>
        public string DataToSend { get; set; } = string.Empty;
        /// <summary>
        /// Mime content type of data to send
        /// </summary>
        public string ContentType { get; set; } = MimeTypes.Json;

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPutString(Uri url) : base(url) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPutString(string url) : base(new Uri(url, UriKind.RelativeOrAbsolute)) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPutString(Uri url, string dataToSend) : this(url) { DataToSend = dataToSend; }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPutString(Uri url, string dataToSend, IEncryptor encrptor) : this(url, dataToSend) { Encryptor = Encryptor; }

        /// <summary>
        /// Sends a GET request, Receives string response
        ///     Overrides HttpRequestBase.Send()
        /// Warning: Not thread safe, particularly in Web-based UIs. This is a stopgap to allow legacy code to operate with blocking/deadlock risk.
        /// </summary>
        /// <returns></returns>
        public override string Send()
        {
            var returnValue = string.Empty;
            using (var client = new HttpClientBuilder())
            {
                var data = new StringContent(DataToSend, System.Text.Encoding.UTF8, this.ContentType);
                var response = Task.Run<HttpResponseMessage>(async () => await client.PutAsync(this.Url, data));
                Response = response.Result;
                if (this.Response.IsSuccessStatusCode)
                {
                    var responseString = Task.Run<string>(async () => await Response.Content.ReadAsStringAsync());
                    DataReceivedRaw = responseString.Result;
                    if (SendPlainText == false)
                    { DataReceivedDecrypted = this.Encryptor.Decrypt(DataReceivedRaw); }
                    else { DataReceivedDecrypted = DataReceivedRaw; }
                }
            }
            return DataReceivedDecrypted;
        }

        /// <summary>
        /// Sends a GET request, Receives string response
        ///     Overrides HttpRequestBase.SendAsync()
        /// </summary>
        /// <returns></returns>
        public override async Task<string> SendAsync()
        {
            using (var client = new HttpClientBuilder())
            {
                using (var data = new StringContent(DataToSend, System.Text.Encoding.UTF8, this.ContentType))
                {
                    Response = await client.PutAsync(this.Url, data);
                    if (this.Response.IsSuccessStatusCode)
                    {
                        DataReceivedRaw = await this.Response.Content.ReadAsStringAsync();
                        if (SendPlainText == false)
                        { DataReceivedDecrypted = this.Encryptor.Decrypt(DataReceivedRaw); }
                        else { DataReceivedDecrypted = DataReceivedRaw; }
                    }
                }
            }
            return DataReceivedDecrypted;
        }
    }
}
