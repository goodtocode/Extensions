using System;
using GoodToCode.Extensions.Collections;
using GoodToCode.Extensions;
using System.Reflection;
using System.Linq;
using GoodToCode.Extensions.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace GoodToCode.Extensions.Configuration
{
    /// <summary>
    /// Container class for connection strings
    /// </summary>
    
    public class ConnectionStringSafe : KeyValuePairString, IFormattable, INotifyPropertyChanged
    {
        /// <summary>
        /// Element names
        /// </summary>
        public struct XmlElements
        {
            /// <summary>
            /// appSettings element
            /// </summary>
            public const string ConnectionStrings = "connectionStrings";
            /// <summary>
            /// Add element
            /// </summary>
            public const string Add = "add";
            /// <summary>
            /// Clear element
            /// </summary>
            public const string Clear = "clear";
            /// <summary>
            /// Remove element
            /// </summary>
            public const string Remove = "remove";
        }

        /// <summary>
        /// Element names
        /// </summary>
        public struct XmlAttributes
        {
            /// <summary>
            /// appSettings element
            /// </summary>
            public const string Key = "name";
            /// <summary>
            /// Add element
            /// </summary>
            public const string Value = "connectionString";
        }

        /// <summary>
        /// Types of apps that can consume a .config file
        /// </summary>
        public enum ConnectionStringTypes
        {
            /// <summary>
            /// Empty
            /// </summary>
            Empty = 0x0,
            /// <summary>
            /// ADO Data access
            /// </summary>
            ADO = 0x1,
            /// <summary>
            /// Entity Framework data access
            /// </summary>
            EF = 0x2,
            /// <summary>
            /// Invalid string
            /// </summary>
            Invalid = 0x4
        }

        /// <summary>
        /// Mask of EF connection string
        /// </summary>
        private const string maskSQLExpress = @"Data Source=.\SQLEXPRESS;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|{0}.mdf;User Instance=true";

        /// <summary>
        /// Mask of EF connection string
        /// </summary>
        private const string maskEF = @"metadata=res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl;provider=System.Data.SqlClient;provider connection string='{1}'";

        /// <summary>
        /// Mask of EF connection string
        /// </summary>
        private const string maskADO = @"{0}";

        /// <summary>
        /// EDMXFileName
        /// </summary>
        public string EDMXFileName { get; set; } = Defaults.String;

        /// <summary>
        /// Determines type of connection string: ADO or EF
        /// </summary>
        /// <returns></returns>
        public ConnectionStringTypes ConnectionStringType { get; private set; } = ConnectionStringTypes.Empty;

        /// <summary>
        /// key of this pair
        /// </summary>
        public new StringMutable Key { get { return keyField; } set { SetField(ref keyField, value); } }

        /// <summary>
        /// Value of this pair
        /// </summary>
        public new StringMutable Value { get { return valueField; } set { SetField(ref valueField, value); } }

        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionStringSafe() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionStringSafe(KeyValuePairString item) : this(item.Key, item.Value) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public ConnectionStringSafe(string key, string value) : base()
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Property changed event handler for INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Property changed event for INotifyPropertyChanged
        /// </summary>
        /// <param name="propertyName">String name of property</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            ConnectionStringValueChanged();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the property data as well as fired OnPropertyChanged() for INotifyPropertyChanged
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetField<TField>(ref TField field, TField value,
        [CallerMemberName] string propertyName = null)
        {
            var returnValue = Defaults.Boolean;
            if (EqualityComparer<TField>.Default.Equals(field, value) == false)
            {
                field = value;
                OnPropertyChanged(propertyName);
                returnValue = true;
            }
            return returnValue;
        }

        /// <summary>
        /// Determine time and validity of new conneciton string data
        /// </summary>
        private void ConnectionStringValueChanged()
        {
            string[] metadataKeys = { ".csdl", ".ssdl", ".msl" }; // EF  Only
            string[] serverKeys = { "data source", "server", "address", "addr", "network address" }; // EF and ADO
            string[] databaseKeys = { "initial catalog", "database" }; // EF and ADO
            string[] fileKeys = { "attachdbfilename", "extended properties", "initial file name" }; // EF and ADO 
            string compareValue = this.Value.ToLowerInvariant();

            if (metadataKeys.Any(compareValue.Contains)) ConnectionStringType = ConnectionStringTypes.EF;
            else if (serverKeys.Any(compareValue.Contains) && (databaseKeys.Any(compareValue.Contains) || fileKeys.Any(compareValue.Contains))) ConnectionStringType = ConnectionStringTypes.ADO;
            else ConnectionStringType = ConnectionStringTypes.Invalid;
        }

        /// <summary>
        /// Is an ADO connection string?
        /// </summary>
        /// <returns></returns>
        public bool IsADO { get { return (ConnectionStringType == ConnectionStringTypes.ADO); } }

        /// <summary>
        /// Is an EF connection string?
        /// </summary>
        /// <returns>True if this is an ADO connection string</returns>
        public bool IsEF { get { return (ConnectionStringType == ConnectionStringTypes.EF); } }

        /// <summary>
        /// Tests the connection string to see if is valid
        ///  Does not test the database existence. Only string keywords
        /// </summary>
        public bool IsValid { get { return (ConnectionStringType != ConnectionStringTypes.Empty && ConnectionStringType != ConnectionStringTypes.Invalid); } }

        /// <summary>
        /// returns currently loaded connection string
        /// </summary>
        /// <returns>Connection string in default (current value) format, not converted.</returns>
        public override string ToString()
        {
            return this.ToString("G");
        }

        /// <summary>
        /// Formats data according to requesting format
        /// For returning ADO-style connection string from EF format: Assumes "provider connection string=" is the last key in the "connectionString=" value
        /// </summary>
        /// <param name="format">G (as-is), EF (EF format), ADO (ADO format)</param>
        /// <param name="formatProvider">ICustomFormatter compatible class</param>
        /// <returns>Name field formatted in common combinations (EF, ADO)</returns>
        public new string ToString(string format, IFormatProvider formatProvider = null)
        {
            var returnValue = Value;
            if (formatProvider != null)
            {
                ICustomFormatter fmt = formatProvider.GetFormat(this.GetType()) as ICustomFormatter;
                if (fmt != null) { return fmt.Format(format, this, formatProvider); }
            }
            switch (format.ToUpperInvariant())
            {
                case "EF":
                    if (IsEF)
                    { returnValue = Value; } else { returnValue = String.Format(maskEF, EDMXFileName, Value).Replace("&quot;", "'"); }
                    break;
                case "ADO":
                    if (IsADO)
                    { returnValue = Value; } else
                    {
                        var cleansed = String.Format("{0}{1}", valueField.Value.Replace("\"", "").Replace("'", "").RemoveLast(";"), ";");
                        var beginPhrase = "provider connection string=";
                        returnValue = cleansed.SubstringRight(cleansed.Length - (cleansed.IndexOf(beginPhrase) + beginPhrase.Length));
                    }
                    break;
                default:
                    returnValue = Value;
                    break;
            }
            return returnValue;
        }

        /// <summary>
        /// Converts from ADO or EF raw connection strings to EF format
        /// </summary>
        /// <param name="dataAccessObject">Defines {namespace}.edmx EF connection string configuration. Namespace string generated from: dataAccessObject.GetTypeInfo().Namespace.Replace(".", Default.String)</param>
        /// <returns>{namespace}.edmx connection string configuration based on Type dataAccessObject's namespace. </returns>
        public string ToEF(Type dataAccessObject)
        {
            var returnValue = this.Value;

            if (IsEF == false)
            {
                EDMXFileName = dataAccessObject.GetTypeInfo().Namespace.Replace(".", Defaults.String);
                returnValue = this.ToString("EF");
            }

            return returnValue;
        }

        /// <summary>
        /// Converts from EF or ADO raw connection strings to ADO format
        /// </summary>
        /// <returns></returns>
        public string ToADO()
        {
            var returnValue = this.Value;

            if (this.IsADO == false)
            {
                returnValue = this.ToString("ADO");
            }

            return returnValue;
        }
    }
}
