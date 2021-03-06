using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using GoodToCode.Extensions;
using GoodToCode.Extensions.Collections;
using System.Diagnostics.CodeAnalysis;

namespace GoodToCode.Extensions.Serialization
{
    /// <summary>
    /// JSON serialization and deserialization
    /// </summary>
    /// <remarks></remarks>
    
    public class JsonSerializer<T> : Serializer<T>
    {
        /// <summary>
        /// Gets or sets a DateTimeFormat that defines the culturally appropriate format of displaying dates and times.
        /// Default is ISO 8601 DateTime Format. Does not default to microsoft Date()
        /// </summary>
        public DateTimeFormat DateTimeFormatString { get; set; } = new DateTimeFormat(DateTimeExtension.Formats.ISO8601F) { DateTimeStyles = System.Globalization.DateTimeStyles.RoundtripKind };

        /// <summary>
        /// Gets or sets the data contract JSON serializer settings to emit type information.
        /// </summary>
        public EmitTypeInformation EmitTypeInformation { get; set; } = EmitTypeInformation.Never;

        /// <summary>
        /// Ignore types that are readonly?
        /// </summary>
        public bool SerializeReadOnly { get; set; } = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonSerializer() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonSerializer(T entityToSerialize) : base(entityToSerialize) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonSerializer(string entityToDeserialize) : base(entityToDeserialize) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonSerializer(IListSafe<Type> knownTypes) : base(knownTypes) { }

        /// <summary>
        /// Serializes and returns the JSON as a string
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public override string Serialize()
        {
            var returnValue = string.Empty;
            DataContractJsonSerializer serializer;

            try
            {
                if (entityObject == null && this.EmptyStringAndNullSupported == false) { throw new System.ArgumentNullException("Passed parameter is null. Unable to serialize null objects."); }
                serializer = new DataContractJsonSerializer(entityObject.GetType(), new DataContractJsonSerializerSettings() { EmitTypeInformation = this.EmitTypeInformation, DateTimeFormat = DateTimeFormatString, KnownTypes = this.KnownTypes });
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, entityObject);
                    stream.Position = 0;
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        returnValue = reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                if (ThrowException) throw;
            }

            return returnValue;
        }

        /// <summary>
        /// De-serializes the passed string to an object
        /// </summary>
        /// <returns>Concrete class</returns>
        public override T Deserialize()
        {
            T returnValue = TypeExtension.InvokeConstructorOrDefault<T>();
            object result = null;

            try
            {
                if (entityString == string.Empty && this.EmptyStringAndNullSupported == false) { throw new System.ArgumentNullException("Passed parameter is empty. Unable to deserialize empty strings."); }
                foreach(var format in DateTimeExtension.FormatList)
                {
                    result = DeserializeWorker(format);
                    if (result != null)
                    {
                        returnValue = (T)result;
                        break;
                    }
                }
            }
            catch
            {
                if (ThrowException) throw;
            }

            return returnValue;
        }

        /// <summary>
        /// De-serializes the passed string to an object
        /// </summary>
        /// <returns>NULL or Concrete class.</returns>
        private object DeserializeWorker(string format)
        {
            object returnValue = null;
            DataContractJsonSerializer serializer;
            var formatStrong = new DateTimeFormat(format) { DateTimeStyles = System.Globalization.DateTimeStyles.RoundtripKind };
            var settings = new DataContractJsonSerializerSettings() { EmitTypeInformation = this.EmitTypeInformation, DateTimeFormat = formatStrong, KnownTypes = this.KnownTypes, SerializeReadOnlyTypes = this.SerializeReadOnly };            

            try
            {
                serializer = new DataContractJsonSerializer(typeof(T), settings);
                byte[] bytes = Encoding.Unicode.GetBytes(entityString);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    returnValue = serializer.ReadObject(stream);                    
                }
            }
            catch
            {
                returnValue = null;
            }

            return returnValue;
        }
    }
}
