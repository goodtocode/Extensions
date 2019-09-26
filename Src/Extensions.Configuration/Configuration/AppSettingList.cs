//-----------------------------------------------------------------------
// <copyright file="AppSettingList.cs" company="GoodToCode">
//      Copyright (c) 2017-2020 GoodToCode. All rights reserved.
//      Licensed to the Apache Software Foundation (ASF) under one or more 
//      contributor license agreements.  See the NOTICE file distributed with 
//      this work for additional information regarding copyright ownership.
//      The ASF licenses this file to You under the Apache License, Version 2.0 
//      (the 'License'); you may not use this file except in compliance with 
//      the License.  You may obtain a copy of the License at 
//       
//        http://www.apache.org/licenses/LICENSE-2.0 
//       
//       Unless required by applicable law or agreed to in writing, software  
//       distributed under the License is distributed on an 'AS IS' BASIS, 
//       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  
//       See the License for the specific language governing permissions and  
//       limitations under the License. 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Configuration
{
    /// <summary>
    /// Generic list of application settings from .config file
    /// </summary>
    
    public class AppSettingList : List<AppSettingSafe>
    {
        private XDocument appSettingsXDocField = new XDocument();

        /// <summary>
        /// AppSettings.config key name for MyWebService root url
        /// </summary>
        public const string MyWebServiceKeyName = "MyWebService";

        /// <summary>
        /// File that contains these configurations
        /// </summary>
        public string ConfigFile { get; private set; } = Defaults.String;

        /// <summary>
        /// Name of the Xml node element
        /// </summary>
        public const string ElementName = "add";
        
        /// <summary>
        /// Name of the attribute key inside Xml node
        /// </summary>
        public const string ElementKeyName = "key";
        
        /// <summary>
        /// Name of the attribute value inside Xml node
        /// </summary>
        public const string ElementValueName = "value";
       
        /// <summary>
        /// Raw XML document
        /// </summary>
        public XDocument AppSettingsXDoc { get { return appSettingsXDocField; } }
        
        /// <summary>
        /// Status message
        /// </summary>
        public string StatusMessage { get; set; } = Defaults.String;

        /// <summary>
        /// Setting to allow duplicates
        /// </summary>
        public bool AllowDuplicates { get; set; } = Defaults.Boolean;

        /// <summary>
        /// Setting to throw exception if rules are broken
        /// </summary>
        public bool ThrowException { get; set; } = Defaults.Boolean;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public AppSettingList() : base()
        {
            StatusMessage = "No data loaded.";
#if (DEBUG)
            ThrowException = true;
#endif
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xmlRaw">Raw Xml string containing all data</param>
        /// <param name="configFile">Path\File of the source configuration file</param>
        public AppSettingList(XDocument xmlRaw, string configFile) : this()
        {
            ConfigFile = configFile;
            appSettingsXDocField = xmlRaw;
            Load();
        }
        
        /// <summary>
        /// Loads all values from config file
        /// </summary>
        private void Load()
        {
            IEnumerable<XElement> elements = new List<XElement>();
            IEnumerable<XAttribute> attributes = new List<XAttribute>();
            try
            {
                // Only act if there is Xml to parse
                if (appSettingsXDocField.Nodes().Count() > 0)
                {
                    // Extract raw data
                    elements = appSettingsXDocField.Descendants(AppSettingList.ElementName);
                    var KVPs = elements.Select(x => new {
                        Key = x.Attribute(ElementKeyName).Value,
                        Value = x.Attribute(ElementValueName).Value
                    }).ToList();
                    // Fill data structure
                    foreach (var Item in KVPs) { Add(new AppSettingSafe(Item.Key, Item.Value)); }
                }
            }
            catch (NullReferenceException)
            {
                if (ThrowException == false)
                { StatusMessage = "Cannot load. Required elements are not in the file"; } else { throw; }
            }
            finally
            {
                StatusMessage = String.Format("{0} records loaded.", this.Count);
            }
        }
        
        /// <summary>
        /// Gets value for a key
        /// </summary>
        /// <param name="key">Key of the element</param>
        /// <remarks></remarks>
        public string GetValue(string key)
        {
            var returnValue = Defaults.String;
            returnValue = this.FindSafe(x => x.Key == key).Value;
            return returnValue;
        }
        
        /// <summary>
        /// Finds the index
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int FindIndex(string key)
        {
            var returnValue = Defaults.Integer;
            for (var count = 0; count < this.Count - 1; count++)
            {
                if (this[count].Key == key)
                {
                    returnValue = count;
                    break;
                }
            }
            return returnValue;
        }
        
        /// <summary>
        /// Adds float item, maintaining identity key
        /// </summary>
        /// <param name="itemToAdd"></param>
        public new void Add(AppSettingSafe itemToAdd)
        {
            // Check for Id
            List<AppSettingSafe> conflictingItems = FindAll(x => x.Key == itemToAdd.Key);
            if (AllowDuplicates == false == true && conflictingItems.Count > 0)
                throw new System.IndexOutOfRangeException("Unable to add new items, Identity Key conflict.");
            base.Add(itemToAdd);
        }

        /// <summary>
        /// Normalizes and Adds a new member to the list
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public void Add(string key, string value)
        {
            // Self-normalize based on AllowDuplicates and ThrowException
            if (AllowDuplicates == false == false && this.GetValue(key) != Defaults.String)
            {
                RemoveAt(FindIndex(key));
            }
            base.Add(new AppSettingSafe(key, value));
        }
        
        /// <summary>
        /// Remove a member from the list
        /// </summary>
        /// <param name="key"></param>
        /// <remarks></remarks>
        public void Remove(string key)
        {
            if (this.GetValue(key).ToStringSafe() != Defaults.String)
                RemoveAt(FindIndex(key));
        }
    }
}
