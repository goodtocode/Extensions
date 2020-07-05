using System;
using GoodToCode.Extensions.Collections;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Serialization
{
    /// <summary>
    /// Generically typed Serialization and Deserialization
    /// </summary>
    /// <typeparam name="T">Type to serialize</typeparam>
    public abstract class Serializer<T> : ISerializer<T>
    {
        /// <summary>
        /// Member variable for the object to serialize, or that was deserialized
        /// </summary>
        protected T entityObject;

        /// <summary>
        /// Member variable for the string to deserialize, or that was serialized
        /// </summary>
        protected string entityString;

        /// <summary>
        /// List of types that allow serializer to use a type not explicitly defined. 
        ///   Primarily used to define ISerialier Of IMyType, but pass in object of MyType 
        ///   (serializer blows up on now knowing that MyType exists, only knows about IMyType)
        /// </summary>
        public IListSafe<Type> KnownTypes { get; set; } = new ListSafe<Type>();

        /// <summary>
        /// Setting to throw exception
        /// </summary>
        public bool ThrowException { get; set; } = false;

        /// <summary>
        /// Setting to throw exception
        /// </summary>
        public bool EmptyStringAndNullSupported { get; set; } = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public Serializer() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public Serializer(T entityToSerialize) : this() { entityObject = entityToSerialize; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Serializer(string entityToDeserialize) : this() { entityString = entityToDeserialize; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Serializer(IListSafe<Type> knownTypes) : base()
        {
            {
                KnownTypes.AddRange(knownTypes);
            }
        }

        /// <summary>
        /// Serializes and returns the object as a string
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public abstract string Serialize();

        /// <summary>
        /// Serializes and returns the object as a string
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Serialize(T objectToSerialize)
        {
            entityObject = objectToSerialize;
            entityString = Serialize();
            return entityString;
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public abstract T Deserialize();
        
        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="stringToDeserialize"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public T Deserialize(string stringToDeserialize)
        {
            entityString = stringToDeserialize;
            entityObject = Deserialize();
            return entityObject;
        }
    }
}
