using Microsoft.AspNetCore.Http;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// HttpResponseExtension
    /// </summary>
    public static class HttpResponseExtension
    {
        /// <summary>
        /// Writes bytes to Html Binary Output Stream. 
        /// Mainly for sending Images/Blobs over http, typically from data access framework to a Html Page img tag.
        /// </summary>
        /// <param name="item">Item to write byte array</param>
        /// <param name="blobBytes">Byte array to write</param>
        public static void BinaryWriteSafe(this HttpResponse item, byte[] blobBytes)
        {
            if ((blobBytes == null == false) && (blobBytes.Length > 0))
            {
                item.Body.Write(blobBytes, 0, blobBytes.Length);
            }
        }
    }
}
