using System;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Text.Encoding
{
    /// <summary>
    /// Encoders and decodes Base64 text
    /// </summary>
    
    public class UrlEncoder : IEncoder
    {
        private string dataIn = Defaults.String;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataToProcess">Data to encrypt or decrypt</param>
        public UrlEncoder(string dataToProcess) : base()
        {
            dataIn = dataToProcess;
        }

        /// <summary>
        /// Encodes to Base64
        /// </summary>
        /// <returns>Url Encoded escaped string</returns>
        public string Encode()
        {            
            return Uri.EscapeDataString(dataIn).Replace("+", "%20");
        }

        /// <summary>
        /// Encodes to Base64
        /// </summary>
        /// <param name="stringToEncode"></param>
        /// <returns></returns>
        public static string Encode(string stringToEncode)
        {
            var encoder = new UrlEncoder(stringToEncode);
            return encoder.Encode();
        }

        /// <summary>
        /// Decodes from Base64
        /// </summary>
        /// <returns></returns>
        public string Decode()
        {
            return Uri.UnescapeDataString(dataIn).Replace("%20", "+");
        }

        /// <summary>
        /// Decodes from Base64
        /// </summary>
        /// <param name="stringToDecode"></param>sd
        /// <returns></returns>
        public static string Decode(string stringToDecode)
        {
            var encoder = new UrlEncoder(stringToDecode);
            return encoder.Decode();
        }
    }
}
