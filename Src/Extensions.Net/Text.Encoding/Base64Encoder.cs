using System;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Text.Encoding
{
    /// <summary>
    /// Encoders and decodes Base64 text
    /// </summary>
    
    public class Base64Encoder : IEncoder
    {
        private string dataIn = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataToProcess">Data to encrypt or decrypt</param>
        public Base64Encoder(string dataToProcess) : base()
        {
            dataIn = dataToProcess;
        }

        /// <summary>
        /// Encodes to Base64
        /// </summary>
        /// <returns></returns>
        public string Encode()
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(dataIn);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Encodes to Base64
        /// </summary>
        /// <param name="stringToEncode"></param>
        /// <returns></returns>
        public static string Encode(string stringToEncode)
        {
            Base64Encoder encoder = new Base64Encoder(stringToEncode);
            return encoder.Encode();
        }

        /// <summary>
        /// Decodes from Base64
        /// </summary>
        /// <returns></returns>
        public string Decode()
        {
            var bytes = Convert.FromBase64String(dataIn);
            return System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Decodes from Base64
        /// </summary>
        /// <param name="stringToDecode"></param>
        /// <returns></returns>
        public static string Decode(string stringToDecode)
        {
            var encoder = new Base64Encoder(stringToDecode);
            return encoder.Decode();
        }
    }
}
