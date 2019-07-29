//-----------------------------------------------------------------------
// <copyright file="ConfigurationManagerFull.cs" company="GoodToCode">
//      Copyright (c) 2017-2020 GoodToCode. All rights reserved.
// 
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
using GoodToCode.Extensions;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace GoodToCode.Extras.Configuration
{
    /// <summary>
    /// Simulates the System.Configuration.ConfigurationManager class for local XML files with <appSettings></appSettings> nodes.
    /// Profile 1 (.NET Framework MVC/WCF/WebForms):
    ///  RootConfigFile: ..\..\..\web.config
    ///  AppSettingsConfigFile: ..\..\App_Data\appsettings.config
    ///  ConnectionStringsConfigFile: ..\..\App_Data\connectionstrings.config
    /// Profile 2 (.NET Framework WPF/Console/WinForms):
    ///  RootConfigFile: ..\..\..\app.config
    ///  AppSettingsConfigFile: ..\..\App_Data\appsettings.config
    ///  ConnectionStringsConfigFile: ..\..\App_Data\connectionstrings.config
    /// </summary>
    public class ConfigurationManagerFull : ConfigurationManagerSafe
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ConfigurationManagerFull() : this(ApplicationTypes.Web)
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        public ConfigurationManagerFull(ApplicationTypes applicationType) : base()
        {
#if (DEBUG)
            ThrowException = true;
#endif
            ApplicationType = applicationType;
            Load();
        }

        /// <summary>
        /// Constructor that accepts ConfigurationManager.AppSettings and ConfigurationManager.ConnectionStrings
        /// </summary>
        /// <param name="appSettings">ConfigurationManager.AppSettings</param>
        /// /// <param name="connectionStrings">ConfigurationManager.ConnectionStrings</param>
        public ConfigurationManagerFull(NameValueCollection appSettings, ConnectionStringSettingsCollection connectionStrings)
            : base(appSettings.ToArraySafe(), null)
        {
            // ConnectionStringSettingsCollection is difficult to work with and is sealed, so cant extend. Just convert to array manually.
            foreach (ConnectionStringSettings connection in connectionStrings ?? new ConnectionStringSettingsCollection())
            {
                base.ConnectionStrings.Add(connection.Name, connection.ConnectionString);
            }
        }

        /// <summary>
        /// Loads from XML data
        /// </summary>
        public void Load()
        {
            var appSettings = new NameValueCollection();
            var connectionStrings = new ConnectionStringSettingsCollection();

            try { appSettings = System.Configuration.ConfigurationManager.AppSettings; }
            catch (NullReferenceException) { throw; }
            try { connectionStrings = System.Configuration.ConfigurationManager.ConnectionStrings; }
            catch (NullReferenceException) { throw; }
            foreach (string Item in appSettings)
            {
                base.AppSettings.Add(new AppSettingSafe(Item, appSettings.GetValues(Item).FirstOrDefault()));
            }
            foreach (System.Configuration.ConnectionStringSettings Item in connectionStrings)
            {
                base.ConnectionStrings.Add(new ConnectionStringSafe(Item.Name, Item.ConnectionString));
            }
        }
    }
}




