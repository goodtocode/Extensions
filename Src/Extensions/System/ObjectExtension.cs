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

        /// <summary>
        /// Initialize all Null properties of an object 
        ///  Primitives Default.* 
        /// </summary>
        /// <typeparam name="ObjectType">Type of object to initialize</typeparam>
        /// <param name="item">Item to initialize</param>
        /// <param name="nullsOnly">Initializes values of only null properties</param>
        /// <returns>Initialized object to Default.* conventions</returns>
        public static ObjectType Initialize<ObjectType>(this object item, bool nullsOnly = true)
        {
            // Initialize
            var CurrentObjectType = item.GetType();
            var propsToInit = nullsOnly ? CurrentObjectType.GetRuntimeProperties().Where(x => x.GetValue(item) == null) 
                : CurrentObjectType.GetRuntimeProperties();
            // Loop through all new item's properties
            foreach (var CurrentProperty in propsToInit)
            {
                // Copy the data using reflection
                if (CurrentProperty.CanWrite)
                {
                    if (CurrentProperty.PropertyType.Equals(typeof(Int32)) || CurrentProperty.PropertyType.Equals(typeof(int)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Int32>)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<int>)))
                    {
                        CurrentProperty.SetValue(item, Defaults.Int32, null);
                    } else if (CurrentProperty.PropertyType.Equals(typeof(Int64)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Int64>)) || CurrentProperty.PropertyType.Equals(typeof(long)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<long>)))
                    {
                        CurrentProperty.SetValue(item, Defaults.Double, null);
                    } else if (CurrentProperty.PropertyType.Equals(typeof(Double)) || CurrentProperty.PropertyType.Equals(typeof(double)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Double>)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<double>)))
                    {
                        CurrentProperty.SetValue(item, Defaults.Double, null);
                    } else if (CurrentProperty.PropertyType.Equals(typeof(Decimal)) || CurrentProperty.PropertyType.Equals(typeof(decimal)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Decimal>)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<decimal>)))
                    {
                        CurrentProperty.SetValue(item, Defaults.Decimal, null);
                    } else if (CurrentProperty.PropertyType.Equals(typeof(String)) || CurrentProperty.PropertyType.Equals(typeof(string)))
                    {
                        CurrentProperty.SetValue(item, Defaults.String, null);
                    } else if (CurrentProperty.PropertyType.Equals(typeof(Char)) || CurrentProperty.PropertyType.Equals(typeof(char)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Char>)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<char>)))
                    {
                        CurrentProperty.SetValue(item, Defaults.Char, null);
                    } else if (CurrentProperty.PropertyType.Equals(typeof(Guid)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Guid>)))
                    {
                        CurrentProperty.SetValue(item, Defaults.Guid, null);
                    } else if (CurrentProperty.PropertyType.Equals(typeof(Boolean)) || CurrentProperty.PropertyType.Equals(typeof(bool)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Boolean>)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<bool>)))
                    {
                        CurrentProperty.SetValue(item, Defaults.Boolean, null);
                    } else if (CurrentProperty.PropertyType.Equals(typeof(DateTime)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<DateTime>)))
                    {
                        CurrentProperty.SetValue(item, Defaults.Date, null);
                    } else if (CurrentProperty.PropertyType.Equals(typeof(TimeSpan)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<TimeSpan>)))
                    {
                        CurrentProperty.SetValue(item, Defaults.Date, null);
                    } else if (CurrentProperty.GetValue(item, null) == null)
                    {
                        Type PropType = CurrentProperty.PropertyType;
                        if (PropType.HasParameterlessConstructor())
                        {
                            object NewProp = Activator.CreateInstance(PropType);
                            CurrentProperty.SetValue(item, NewProp, null);
                        }
                    }
                }
            }

            return (ObjectType)item;
        }
    }
}
