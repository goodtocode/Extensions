using GoodToCode.Extensions.Mathematics;
using System;
using System.Security.Cryptography;
using System.Text;

namespace GoodToCode.Extensions.Security.Cryptography
{
    /// <summary>
    /// Encrypts/Decrypts using 3 DES algorithms
    /// </summary>

    public class DesEncryptor : IEncryptor
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; private set; } = "193FFC71-1DD6-4AAD-B75C-936A002940B3";
        /// <summary>
        /// Salt
        /// </summary>
        public string Salt { get; private set; } = Arithmetic.Random().ToString();
        /// <summary>
        /// EncodeForURL
        /// </summary>
        public bool EncodeForURL { get; protected set; } = false;
        
        /// <summary>
        /// Force immutability
        /// </summary>
        private DesEncryptor()
            : base()
        {
            EncodeForURL = true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="encryptionKey"></param>
        /// <param name="urlEncode"></param>
        public DesEncryptor(string encryptionKey = "", bool urlEncode = true)
            : this()
        {            
            Key = encryptionKey;
            EncodeForURL = urlEncode;
        }
        
        /// <summary>
        /// Encrypts a string
        /// </summary>
        public string Encrypt(string originalString)
        {
            string returnValue;
            try
            {
                var saltedString = originalString + this.Salt;
                TripleDES des = CreateDes();
                ICryptoTransform encryptor = des.CreateEncryptor();
                var encryptedByte = Encoding.Unicode.GetBytes(saltedString);
                // Final encryption and return
                returnValue = Convert.ToBase64String(encryptor.TransformFinalBlock(encryptedByte, 0, encryptedByte.Length));
                if (this.EncodeForURL)
                {
                    returnValue = Uri.EscapeDataString(returnValue).Replace("+", "%20");
                }
            }
            catch
            {
                returnValue = string.Empty;
            }

            return returnValue;
        }
        
        /// <summary>
        /// Decrypts an salted string
        /// </summary>
        /// <param name="encryptedString"></param>
        public string Decrypt(string encryptedString)
        {
            string returnValue;
            try
            {
                string itemToDecrypt = encryptedString;
                if (this.EncodeForURL)
                {
                    itemToDecrypt = Uri.UnescapeDataString(encryptedString).Replace("%20", "+");
                }
                using (TripleDES des = CreateDes())
                {
                    ICryptoTransform decryptor = des.CreateDecryptor();
                    var encryptedByte = Convert.FromBase64String(itemToDecrypt);
                    var decryptedByte = decryptor.TransformFinalBlock(encryptedByte, 0, encryptedByte.Length);
                    var decryptedSaltedString = Encoding.Unicode.GetString(decryptedByte);
                    // Final decryption and return
                    returnValue = decryptedSaltedString.Remove(decryptedSaltedString.Length - this.Salt.Length);
                }
            }
            catch
            {
                returnValue = string.Empty;
            }

            return returnValue;
        }
        
        /// <summary>
        /// Takes a string as key value, calculates MD5 hash on input parameter string.  
        /// This hash value would be used as real key for the encryption. 
        /// </summary>
        private TripleDES CreateDes()
        {
            TripleDES returnValue = new TripleDESCryptoServiceProvider();
            using (MD5 md5Provider = new MD5CryptoServiceProvider())
            {                
                returnValue.Key = md5Provider.ComputeHash(Encoding.Unicode.GetBytes(this.Key));
            }
            returnValue.IV = new byte[Convert.ToInt32(returnValue.BlockSize / 8 - 1) + 1];

            return returnValue;
        }
    }
}
