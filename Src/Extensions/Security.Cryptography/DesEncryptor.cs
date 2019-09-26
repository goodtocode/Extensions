//-----------------------------------------------------------------------
// <copyright file="DesEncryptorFull.cs" company="GoodToCode">
//      Copyright (c) GoodToCode. All rights reserved.
//      Licensed to the Apache Software Foundation (ASF) under one or more 
//      contributor license agreements.  See the NOTICE file distributed with 
//      this work for additional information regarding copyright ownership.
//      The ASF licenses this file to You under the Apache License, Version 2.0 
//      (the 'License'); you may not use this file except in compliance with 
//      the License.  You may obtain a copy of the License at 
//       
//        http://www.apache.org/licenses/LICENSE-2.0 
//       
//       Unless required by applicable law or agreed to in writing, software  
//       distributed under the License is distributed on an 'AS IS' BASIS, 
//       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  
//       See the License for the specific language governing permissions and  
//       limitations under the License. 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Security.Cryptography;
using System.Text;
using GoodToCode.Extensions;
using GoodToCode.Extensions.Mathematics;
using GoodToCode.Extensions.Text.Encoding;

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
        public bool EncodeForURL { get; protected set; } = Defaults.Boolean;
        
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
            var returnValue = Defaults.String;

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
                    returnValue = UrlEncoder.Encode(returnValue);
                }
            }
            catch
            {
                returnValue = Defaults.String;
            }

            return returnValue;
        }
        
        /// <summary>
        /// Decrypts an salted string
        /// </summary>
        /// <param name="encryptedString"></param>
        public string Decrypt(string encryptedString)
        {
            var returnValue = Defaults.String;
            var itemToDecrypt = Defaults.String;

            try
            {
                itemToDecrypt = encryptedString;
                if (this.EncodeForURL)
                {
                    itemToDecrypt = UrlEncoder.Decode(encryptedString);
                }
                TripleDES des = CreateDes();
                ICryptoTransform decryptor = des.CreateDecryptor();
                var encryptedByte = Convert.FromBase64String(itemToDecrypt);
                var decryptedByte = decryptor.TransformFinalBlock(encryptedByte, 0, encryptedByte.Length);
                var decryptedSaltedString = Encoding.Unicode.GetString(decryptedByte);
                // Final decryption and return
                returnValue = decryptedSaltedString.Remove(decryptedSaltedString.Length - this.Salt.Length);
            }
            catch
            {
                returnValue = Defaults.String;
            }

            return returnValue;
        }
        
        /// <summary>
        /// Takes a string as key value, calculates MD5 hash on input parameter string.  
        /// This hash value would be used as real key for the encryption. 
        /// </summary>
        private TripleDES CreateDes()
        {
            MD5 md5Provider = new MD5CryptoServiceProvider();
            TripleDES returnValue = new TripleDESCryptoServiceProvider();
            returnValue.Key = md5Provider.ComputeHash(Encoding.Unicode.GetBytes(this.Key));
            returnValue.IV = new byte[Convert.ToInt32(returnValue.BlockSize / 8 - 1) + 1];

            return returnValue;
        }
    }
}
