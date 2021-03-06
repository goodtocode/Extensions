using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using GoodToCode.Extensions;
using GoodToCode.Extensions.Text;

namespace GoodToCode.Extensions.Configuration
{
    /// <summary>
    /// Generic list of connection strings
    /// </summary>
    
    public class ConnectionStringList : List<ConnectionStringSafe>
    {
        private XDocument configStringsXDocField = new XDocument();

        /// <summary>
        /// Name of the Xml node element
        /// </summary>
        public const string ElementName = "add";
        /// <summary>
        /// Name of the attribute key inside Xml node
        /// </summary>
        public const string ElementKeyName = "name";
        /// <summary>
        /// Name of the attribute value inside Xml node
        /// </summary>
        public const string ElementValueName = "connectionString";
        
        /// <summary>
        /// File that contains these configurations
        /// </summary>
        public string ConfigFile { get; private set; } = string.Empty;
        /// <summary>
        /// Raw XML document
        /// </summary>
        public XDocument RawXml { get { return configStringsXDocField; } }
        /// <summary>
        /// StatusMessage
        /// </summary>
        public string StatusMessage { get; set; } = string.Empty;
        /// <summary>
        ///      AllowDuplicates
        /// </summary>
        public bool AllowDuplicates { get; set; } = false;
        /// <summary>
        /// ThrowException
        /// </summary>
        public bool ThrowException { get; set; } = false;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionStringList() : base()
        {
            StatusMessage = "No data loaded.";
#if (DEBUG)
            ThrowException = true;
#endif
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xmlRaw"></param>
        /// <param name="configFile"></param>
        public ConnectionStringList(XDocument xmlRaw, string configFile) : this()
        {
            ConfigFile = configFile;
            configStringsXDocField = xmlRaw;
            Load();
        }
        
        /// <summary>
        /// Loads all values from configuration file
        /// </summary>
        private void Load()
        {
            IEnumerable<XElement> elements = new List<XElement>();
            IEnumerable<XAttribute> attributes = new List<XAttribute>();
            try
            {
                // Only act if there is Xml to parse
                if (configStringsXDocField.Nodes().Count() > 0)
                {
                    // Extract raw data
                    elements = configStringsXDocField.Descendants(ConnectionStringList.ElementName);
                    var KVPs = elements.Select(x => new {
                            Key = x.Attribute(ConnectionStringList.ElementKeyName).Value,
                            Value = x.Attribute(ConnectionStringList.ElementValueName).Value
                        }).ToList();
                    // Fill
                    foreach (var Item in KVPs) { Add(new ConnectionStringSafe(Item.Key, Item.Value)); }
                }
            }
            catch (NullReferenceException)
            {
              if (ThrowException == false)
                { StatusMessage = String.Format("Cannot load. Required elements are not present."); } 
              else { throw; }
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
        public StringMutable GetValue(StringMutable key)
        {
            return this.FindSafe(x => x.Key == key).Value;
        }
        
        /// <summary>
        /// Finds the index
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int FindIndex(StringMutable key)
        {
            var returnValue = -1;
            for (var Count = 0; Count < this.Count - 1; Count++)
            {
                if (this[Count].Key == key)
                {
                    returnValue = Count;
                    break;
                }
            }
            return returnValue;
        }
        
        /// <summary>
        /// Adds float item, maintaining identity key
        /// </summary>
        /// <param name="itemToAdd"></param>
        public new void Add(ConnectionStringSafe itemToAdd)
        {
            // Check for Id clear-age
            List<ConnectionStringSafe> ConflictingItems = FindAll(x => x.Key == itemToAdd.Key);
            if (AllowDuplicates == false == true && ConflictingItems.Count > 0)
                throw new System.IndexOutOfRangeException("Unable to add new items, Identity Key conflict.");
            base.Add(itemToAdd);
        }

        /// <summary>
        /// Normalizes and Adds a new member to the list
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public void Add(StringMutable key, StringMutable value)
        {
            // Self-normalize based on AllowDuplicates and ThrowException
            if (AllowDuplicates == false == false && this.GetValue(key) != string.Empty)
            {
                RemoveAt(FindIndex(key));
            }
            base.Add(new ConnectionStringSafe() { Key = key, Value = value });
        }
        
        /// <summary>
        /// Remove a member from the list
        /// </summary>
        /// <param name="key"></param>
        /// <remarks></remarks>
        public void Remove(StringMutable key)
        {
            if (this.GetValue(key).ToStringSafe() != string.Empty)
                RemoveAt(FindIndex(key));
        }
    }
}
