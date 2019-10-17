using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// object Extensions
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// Returns name of run-time type
        /// </summary>
        /// <param name="item">Object with name to get</param>
        /// <returns>Run-time name of object</returns>
        public static string GetName(this object item)
        {
            return item.GetType().Name;
        }

        /// <summary>
        /// Returns name of compile-time type
        /// </summary>
        /// <param name="item">Type with name to get</param>
        /// <returns>Compile-time name of object</returns>
        public static string GetName<T>(this T item)
        {
            return typeof(T).Name;
        }

        /// <summary>
        /// Gets the string value of an attribute that implements IAttributeValue
        /// Overload for int
        /// </summary>
        /// <typeparam name="TAttribute">Attribute to get the value</typeparam>
        /// <param name="item">Object containing the attribute</param>
        /// <param name="notFoundValue">Will use this string if no attribute is found</param>
        /// <returns>Value, or passed notFoundValue if not found</returns>
        public static int GetAttributeValue<TAttribute>(this object item, int notFoundValue) where TAttribute : Attribute, IAttributeValue<int>
        {
            return item.GetAttributeValue<TAttribute, int>(notFoundValue);
        }

        /// <summary>
        /// Gets the string value of an attribute that implements IAttributeValue
        /// Overload for string
        /// </summary>
        /// <typeparam name="TAttribute">Attribute to get the value</typeparam>
        /// <param name="item">Object containing the attribute</param>
        /// <param name="notFoundValue">Will use this string if no attribute is found</param>
        /// <returns>Value, or passed notFoundValue if not found</returns>
        public static string GetAttributeValue<TAttribute>(this object item, string notFoundValue) where TAttribute : Attribute, IAttributeValue<string>
        {
            return item.GetAttributeValue<TAttribute, string>(notFoundValue);
        }

        /// <summary>
        /// Gets the value of an attribute that implements IAttributeValue
        /// </summary>
        /// <typeparam name="TAttribute">Attribute to get the value</typeparam>
        /// <typeparam name="TValue">Type of the value to be returned</typeparam>
        /// <param name="item">Object containing the attribute</param>
        /// <param name="notFoundValue">Will use this string if no attribute is found</param>
        /// <returns>Value, or passed notFoundValue if not found</returns>
        public static TValue GetAttributeValue<TAttribute, TValue>(this object item, TValue notFoundValue) where TAttribute : Attribute, IAttributeValue<TValue>
        {
            TypeInfo itemType = item.GetType().GetTypeInfo();
            TValue returnValue = notFoundValue;

            foreach (object attribute in itemType.GetCustomAttributes(false))
            {
                if (attribute is TAttribute)
                {
                    returnValue = ((TAttribute)attribute).Value;
                    break;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Get list of properties decorated with the passed attribute
        /// </summary>
        /// <param name="item"></param>
        /// <param name="myAttribute"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesByAttribute(this object item, Type myAttribute)
        {
            TypeInfo itemType = item.GetType().GetTypeInfo();

            var returnValue = itemType.DeclaredProperties.Where(
                p => p.GetCustomAttributes(myAttribute, false).Any());

            return returnValue;
        }

        /// <summary>
        /// Safe Type Casting based on .NET default() method
        /// </summary>
        /// <typeparam name="TDestination">default(DestinationType)</typeparam>
        /// <param name="item">Item to default.</param>
        /// <returns>default(DestinationType)</returns>
        public static TDestination DefaultSafe<TDestination>(this object item)
        {
            var returnValue = TypeExtension.InvokeConstructorOrDefault<TDestination>();

            try
            {
                if (item != null)
                {
                    returnValue = (TDestination)item;
                }
            }
            catch
            {
                returnValue = TypeExtension.InvokeConstructorOrDefault<TDestination>();
            }

            return returnValue;
        }

        /// <summary>
        /// Safe type casting via (TDestination)item method.
        /// If cast fails, will return constructed object
        /// </summary>
        /// <typeparam name="TDestination">Type to default, or create new()</typeparam>
        /// <param name="item">Item to cast</param>
        /// <returns>Cast result via (TDestination)item, or item.Fill(), or new TDestination().</returns>
        public static TDestination CastSafe<TDestination>(this object item) where TDestination : new()
        {
            var returnValue = new TDestination();

            try
            {
                returnValue = item != null ? (TDestination)item : returnValue;
            }
            catch (InvalidCastException)
            {
                returnValue = new TDestination();
            }

            return returnValue;
        }

        /// <summary>
        /// Safe Type Casting based on Default.{Type} conventions.
        /// If cast fails, will attempt the slower Fill() of data via reflection
        /// </summary>
        /// <typeparam name="TDestination">Type to default, or create new()</typeparam>
        /// <param name="item">Item to cast</param>
        /// <returns>Defaulted type, or created new()</returns>
        public static TDestination CastOrFill<TDestination>(this object item) where TDestination : new()
        {
            var returnValue = new TDestination();

            try
            {
                returnValue = item != null ? (TDestination)item : returnValue;
            }
            catch (InvalidCastException)
            {
                returnValue.Fill(item);
            }

            return returnValue;
        }

        /// <summary>
        /// Item to exception-safe cast to string
        /// </summary>
        /// <param name="item">Item to cast</param>
        /// <returns>Converted string, or ""</returns>
        public static string ToStringSafe(this object item)
        {
            var returnValue = Defaults.String;

            if (item == null == false)
            {
                returnValue = item.ToString();
            }

            return returnValue;
        }


        /// <summary>
        /// Fills this object with another object's data, by matching property names
        /// </summary>
        /// <typeparam name="T">Type of original object.</typeparam>
        /// <param name="item">Destination object to fill</param>
        /// <param name="sourceItem">Source object</param>
        public static void Fill<T>(this T item, object sourceItem)
        {
            var sourceType = sourceItem.GetType();

            foreach (PropertyInfo sourceProperty in sourceType.GetRuntimeProperties())
            {
                PropertyInfo destinationProperty = typeof(T).GetRuntimeProperty(sourceProperty.Name);
                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    // Copy data only for Primitive-ish types including Value types, Guid, String, etc.
                    Type destinationPropertyType = destinationProperty.PropertyType;
                    if (destinationPropertyType.GetTypeInfo().IsPrimitive || destinationPropertyType.GetTypeInfo().IsValueType
                        || (destinationPropertyType == typeof(string)) || (destinationPropertyType == typeof(Guid)))
                    {
                        destinationProperty.SetValue(item, sourceProperty.GetValue(sourceItem, null), null);
                    }
                }
            }
        }
    }
}
