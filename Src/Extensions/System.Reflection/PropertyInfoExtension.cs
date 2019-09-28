using System;
using System.Reflection;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// HttpRequestBaseExtension
    /// </summary>
    public static class PropertyInfoExtension
    {
        /// <summary>
        /// Gets the value of an attribute that implements IAttributeValue
        /// </summary>
        /// <param name="item">Object containing the attribute</param>
        /// <param name="notFoundValue">Will use this string if no attribute is found</param>
        /// <returns></returns>
        public static TValue GetAttributeValue<TAttribute, TValue>(this PropertyInfo item, TValue notFoundValue) where TAttribute : Attribute, IAttributeValue<TValue>
        {
            TValue returnValue = notFoundValue;

            foreach (object attribute in item.GetCustomAttributes(false))
            {
                if ((attribute is TAttribute))
                {
                    returnValue = ((TAttribute)attribute).Value;
                    break;
                }
            }

            return returnValue;
        }
    }
}
