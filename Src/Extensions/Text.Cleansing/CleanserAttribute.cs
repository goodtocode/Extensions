using System;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Text.Cleansing
{
    /// <summary>
    /// Connection string Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CleanseFor : Attribute, IAttributeValue<CleanserIds>
    {
        private static CleanserIds defaultValue = CleanserIds.Default;

        /// <summary>
        /// Value of attribute
        /// </summary>
        public CleanserIds Default { get; set; } = defaultValue;

        /// <summary>
        /// Value of attribute
        /// </summary>
        public CleanserIds Value { get; set; } = defaultValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value to hydrate</param>
        public CleanseFor(CleanserIds value)
        {
            Value = value;
        }
    }
}
