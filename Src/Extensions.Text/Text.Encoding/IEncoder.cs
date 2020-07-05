using System;

namespace GoodToCode.Extensions.Text.Encoding
{
    /// <summary>
    /// Encoding interface
    /// </summary>
    
    public interface IEncoder
    {
        /// <summary>
        /// Encode
        /// </summary>
        /// <returns></returns>
        string Encode();

        /// <summary>
        /// Decode
        /// </summary>
        /// <returns></returns>
        string Decode();
    }
}
