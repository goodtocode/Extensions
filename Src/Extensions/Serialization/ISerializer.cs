using GoodToCode.Extensions.Collections;
using System;

namespace GoodToCode.Extensions.Serialization
{
    /// <summary>
    /// Strongly typed serializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISerializer<T>
    {
        /// <summary>
        /// List of types that allow serializer to use a type not explicitly defined. 
        ///   Primarily used to define ISerialier Of IMyType, but pass in object of MyType 
        ///   (serializer blows up on now knowing that MyType exists, only knows about IMyType)
        /// </summary>
        IListSafe<Type> KnownTypes { get; set; }

        /// <summary>
        /// Setting to throw exception
        /// </summary>
        bool ThrowException { get; set; }

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        string Serialize(T objectToSerialize);

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="stringToDeserialize"></param>
        /// <returns></returns>
        T Deserialize(string stringToDeserialize);
    }
}
