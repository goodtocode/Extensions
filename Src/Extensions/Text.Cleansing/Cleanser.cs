using System;
using System.Collections.Generic;
using System.Reflection;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Text.Cleansing
{
    /// <summary>
    /// Text Cleanser interface
    /// </summary>
    
    public abstract class Cleanser
    {
        /// <summary>
        /// Id of the target of this cleanser
        /// </summary>
        public abstract CleanserIds CleanserId { get; }

        /// <summary>
        /// Item to cleanse
        /// </summary>
        public string TextToCleanse { get; set; }

        /// <summary>
        /// Result after cleanse
        /// </summary>
        public string TextCleansed { get; protected set; }

        /// <summary>
        /// Worker that cleanses the text
        /// </summary>
        /// <returns></returns>
        public abstract string Cleanse();

        /// <summary>
        /// Cleanses all properties marked with CleanseFor attribute
        /// </summary>
        /// <param name="classToCleanse"></param>
        public static void CleanseAll(object classToCleanse)
        {
            // Get properties with CleanseFor() attribute
            IEnumerable<PropertyInfo> props = classToCleanse.GetPropertiesByAttribute(typeof(CleanseFor));
            foreach (var item in props)
            {
                var ValueToSet = item.GetValue(classToCleanse, null).ToStringSafe();
                Cleanser cleanserWorker = CleanserFactory.Construct(item.GetAttributeValue<CleanseFor, CleanserIds>(CleanserIds.Default), ValueToSet);
                ValueToSet = cleanserWorker.Cleanse();
                item.SetValue(classToCleanse, ValueToSet);
            }
        }
    }
}
