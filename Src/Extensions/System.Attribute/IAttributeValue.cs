namespace GoodToCode.Extensions
{
    /// <summary>
    /// An attribute that has a string Value {get; set;} property
    /// </summary>
    public interface IAttributeValue<TValue>
    {
        /// <summary>
        /// string value of the attribute
        /// </summary>
        TValue Value { get; set; }
    }
}
