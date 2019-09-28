using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoodToCode.Extensions.Security.Cryptography;
using GoodToCode.Extensions.Serialization;

namespace GoodToCode.Extensions.Net
{
    /// <summary>
    /// Communicates via GET, strongly typed
    /// </summary>
    
    public class HttpRequestGet<TypeToReceive> : HttpRequestGetString where TypeToReceive : new()
    {
        /// <summary>
        /// DataReceivedRaw value decrypted
        /// </summary>
        public TypeToReceive DataReceivedDeserialized { get; set; } = new TypeToReceive();
        /// <summary>
        /// De-serializer of response
        /// </summary>
        public ISerializer<TypeToReceive> Deserializer { get; protected set; } = new JsonSerializer<TypeToReceive>();
        /// <summary>
        /// KnownTypes assist the serializer with types that cannot be mapped by default
        /// </summary>
        public List<Type> KnownTypes { get; protected set; } = new List<Type>();
        
        /// <summary>
        /// Immutable
        /// </summary>
        public HttpRequestGet(Uri url) : base(url) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestGet(string url) : base(new Uri(url, UriKind.RelativeOrAbsolute)) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestGet(Uri url, ISerializer<TypeToReceive> deserializer) : this(url) { Deserializer = deserializer; }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestGet(Uri url, IEncryptor encrptor) : base(url, encrptor) { }
        
        /// <summary>
        /// Sync send and Receive
        /// </summary>
        /// <returns></returns>
        public virtual new TypeToReceive Send()
        {
            base.Send();
            DataReceivedDeserialized = Deserializer.Deserialize(base.DataReceivedDecrypted);
            return DataReceivedDeserialized; 
        }

        /// <summary>
        /// Async send and Receive
        /// </summary>
        /// <returns></returns>
        public virtual new async Task<TypeToReceive> SendAsync()
        {
            await base.SendAsync();
            DataReceivedDeserialized = Deserializer.Deserialize(base.DataReceivedDecrypted);
            return DataReceivedDeserialized;
        }
    }
}
