namespace GoodToCode.Extensions.Serialization
{
    /// <summary>
    /// Data access entity that can only read
    /// </summary>
    public interface ISerializable<T>
    {
        /// <summary>
        /// Serializes this object into a string
        /// </summary>
        /// <returns></returns>
        string Serialize();

        /// <summary>
        /// De-serializes a string into this object
        /// </summary>
        /// <returns></returns>
        T Deserialize(string entityString);
    }
}
