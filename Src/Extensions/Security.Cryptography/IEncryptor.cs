using System;

namespace GoodToCode.Extensions.Security.Cryptography
{
    /// <summary>
    /// Encryptor interface
    /// </summary>
    
    public interface IEncryptor
    {
        /// <summary>
        /// Key
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Salt
        /// </summary>
        string Salt { get; }

        /// <summary>
        /// EncodeForURL
        /// </summary>
        bool EncodeForURL { get; }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="plainString"></param>
        /// <returns></returns>
        string Encrypt(string plainString);

        /// <summary>
        /// EncryptedString
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        string Decrypt(string encryptedString);
    }
}
