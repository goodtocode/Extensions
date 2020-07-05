using System;
using System.Security.Cryptography;
using System.Text;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Security.Cryptography
{
    /// <summary>
    /// Builds a hashed string from raw text
    /// </summary>
    
    public class Md5HashBuilder
    {
        private readonly string salt = "0873F24C-FA01-4811-AB36-2F079D4CA0D9";
        /// <summary>
        /// HashedString
        /// </summary>
        public string HashedString { get; protected set; } = string.Empty;

        /// <summary>
        /// Force immutability
        /// </summary>
        private Md5HashBuilder() : base() {}

        /// <summary>
        /// Force immutability
        /// </summary>
        private Md5HashBuilder(string salt) : this() { if (salt != string.Empty) this.salt = salt; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stringToHash">string to hash</param>
        /// <param name="salt">Salted value to add to hashed string</param>
        public Md5HashBuilder(string stringToHash, string salt = "")
            : this(salt)
        {
            HashedString = this.HashCreate(stringToHash);
        }

        /// <summary>
        /// Hashes a String
        /// </summary>
        /// <param name="stringToHash">string to hash</param>
        /// <returns>Hashed string data</returns>
        private string HashCreate(string stringToHash)
        {
            var returnValue = string.Empty;
            var hashValue = new StringBuilder();

            try
            {
                stringToHash += this.salt;
                using (MD5 MD55Hash = MD5.Create())
                {
                var Data = MD55Hash.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
                    for (var Count = 0; Count <= Data.Length - 1; Count++)
                    {
                        hashValue.Append(Data[Count].ToString("x2"));
                    }
                }
                returnValue = hashValue.ToString();
            }
            catch
            {
                returnValue = string.Empty;
            }

            return returnValue;
        }
        
        /// <summary>
        /// Hashes and compares a string
        /// </summary>
        /// <param name="rawString">string to hash compare</param>
        /// <returns>True if string + salt hashed matches current hash string</returns>
        public bool Compare(string rawString)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            var returnValue = false;
            string rawStringHash = HashCreate(rawString);
            if (0 == comparer.Compare(rawStringHash, this.HashedString))
            {
                returnValue = true;
            }

            return returnValue;
        }
    }
}
