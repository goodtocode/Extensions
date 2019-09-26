//-----------------------------------------------------------------------
// <copyright file="ConfigurationManagerSafe.cs" company="GoodToCode">
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
using GoodToCode.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace GoodToCode.Extensions.Configuration
{
    /// <summary>
    /// Types of applications that can consume a configuration file
    /// </summary>
    public enum ApplicationTypes
    {
        /// <summary>
        /// Http request/response type application
        /// </summary>
        Web = 0,
        /// <summary>
        /// Native application, mobile, desktop, service, etc.
        /// </summary>
        Native = 1
    }

    /// <summary>
    /// Types of .NET Framework that can consume a configuration file
    /// </summary>
    public enum FrameworkTypes
    {
        /// <summary>
        /// Http request/response type application
        /// </summary>
        Full = 0,
        /// <summary>
        /// Native application, mobile, desktop, service, etc.
        /// </summary>
        Core = 1
    }

    /// <summary>
    /// Cross-platform emulator of System.Configuration.ConfigurationManager, for local .config files
    /// Target Projects: Universal Windows 10/8.1, Xamarin Forms, Windows Phone, Portable Class Library
    /// Usage: From the Application project, 1. Read .config XML from filesystem, 2. Pass xml into constructor as strings
    ///     var localFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
    ///     localFolder = await localFolder.GetFolderAsync("App_Data");
    ///     var appSettingsFile = await localFolder.GetFileAsync("AppSettings.config");
    ///     var appSettingsXml = await Windows.Storage.FileIO.ReadTextAsync(appSettingsFile);
    ///     var connectStringsFile = await localFolder.GetFileAsync("ConnectionStrings.config");
    ///     var connectStringsXml = await Windows.Storage.FileIO.ReadTextAsync(connectStringsFile);
    ///     var configManager = new ConfigurationManagerSafe(appSettingsXml, connectStringsXml);
    /// </summary>
    public class ConfigurationManagerSafe : IConfigurationManager
    {
        /// <summary>
        /// Friendly names of common configuration filenames
        /// </summary>
        public struct FileNames
        {
            /// <summary>
            /// ConfigFileWeb
            /// </summary>
            public const string WebConfig = "Web.config";

            /// <summary>
            /// ConfigFileNative
            /// </summary>
            public const string AppConfig = "App.config";

            /// <summary>
            /// Config file containing application settings
            /// </summary>
            public const string AppSettingsConfig = "AppSettings.config";

            /// <summary>
            /// Config file containing connection strings
            /// </summary>
            public const string ConnectionStringsConfig = "ConnectionStrings.config";

            /// <summary>
            /// ConfigFileWeb
            /// </summary>
            public const string WebJson = "Web.json";

            /// <summary>
            /// ConfigFileNative
            /// </summary>
            public const string AppJson = "App.json";

            /// <summary>
            /// Config file containing application settings
            /// </summary>
            public const string AppSettingsJson = "AppSettings.json";

            /// <summary>
            /// Config file containing application settings
            /// </summary>
            public const string AppSettingsEnvironmentJson = "AppSettings.{0}.json";

            /// <summary>
            /// Config file containing connection strings
            /// </summary>
            public const string ConnectionStringsJson = "ConnectionStrings.json";
        }

        /// <summary>
        /// ApplicationType
        /// </summary>
        public ApplicationTypes ApplicationType { get; protected set; } = ApplicationTypes.Web;

        /// <summary>
        /// FrameworkType
        /// </summary>
        public FrameworkTypes FrameworkType { get; protected set; } = FrameworkTypes.Full;

        /// <summary>
        /// Full path to the App_Data folder, or the current OS equivilent
        /// </summary>
        public string CurrentPath { get; } = Defaults.String;

        /// <summary>
        /// Full path to the App_Data folder, or the current OS equivilent
        /// </summary>
        public string RootPath { get; set; } = Defaults.String;

        /// <summary>
        /// Full path to the App_Data folder, or the current OS equivilent
        /// </summary>
        public string AppDataFolder { get { return $"{RootPath.AddLast(@"\")}App_Data"; } }

        /// <summary>
        /// All application settings in the referenced config file
        /// </summary>
        public AppSettingList AppSettings { get; } = new AppSettingList();

        /// <summary>
        /// All connection strings in the referenced config file
        /// </summary>
        public ConnectionStringList ConnectionStrings { get; } = new ConnectionStringList();

        /// <summary>
        /// ThrowException
        /// </summary>
        public bool ThrowException { get; set; } = Defaults.Boolean;

        /// <summary>
        /// Constructor
        /// </summary>
        protected ConfigurationManagerSafe() : base() { ThrowException = true; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ConfigurationManagerSafe(string path) : this() { CurrentPath = path; RootPath = path; }

        /// <summary>
        /// Constructor that accepts AppSettings and ConnectionStrings XML data in string form
        /// Usage: From the Application project, 1. Read .config XML from filesystem, 2. Pass xml into constructor as strings
        ///  - .NET Core:
        ///     var configManager = new ConfigurationManagerSafe(System.Configuration.ConfigurationManager.AppSettings, 
        ///                                                 System.Configuration.ConfigurationManager.ConnectionStrings);
        ///  - .NET 4.x Full:
        ///     var configManager = new ConfigurationManagerSafe(System.Configuration.ConfigurationManager.AppSettings, 
        ///                                                 System.Configuration.ConfigurationManager.ConnectionStrings);
        ///  - Desktop and UWP:
        ///     var localFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
        ///     localFolder = await localFolder.GetFolderAsync("App_Data");
        ///     var appSettingsFile = await localFolder.GetFileAsync("AppSettings.config");
        ///     var connectStringsFile = await localFolder.GetFileAsync("ConnectionStrings.config");
        ///     var configManager = new ConfigurationManagerSafe(appSettingsXml, connectStringsXml);
        ///  - UWP:
        ///     var appSettingsXml = await Windows.Storage.FileIO.ReadTextAsync(appSettingsFile);        
        ///     var connectStringsXml = await Windows.Storage.FileIO.ReadTextAsync(connectStringsFile);
        /// </summary>
        /// <param name="appSettings">Raw XML from AppSettings.config</param>
        /// <param name="appSettingsPathFile">Path\File containing AppSettings</param>
        /// <param name="connectionStrings">Raw XML from ConnectionStrings.config</param>
        /// <param name="connectionStringsPathFile">Path\File containing ConnectionStrings</param>
        public ConfigurationManagerSafe(XDocument appSettings, string appSettingsPathFile, XDocument connectionStrings, string connectionStringsPathFile) : this()
        {

            AppSettings = new AppSettingList(appSettings, appSettingsPathFile);
            ConnectionStrings = new ConnectionStringList(connectionStrings, connectionStringsPathFile);
        }

        /// <summary>
        /// Constructor that accepts Stream, for .NET Core/PCL/Universal apps
        /// </summary>
        /// <param name="configContents">Raw XML from Web.config, AppSettings.config and ConnectionStrings.config</param>
        /// <param name="configPathFile">File containing app settings and connection strings</param>
        public ConfigurationManagerSafe(Stream configContents, string configPathFile) : this()
        {
            XDocument xdoc = configContents.ToXDocument();

            AppSettings = new AppSettingList(xdoc, configPathFile);            
            ConnectionStrings = new ConnectionStringList(xdoc, configPathFile);
        }

        /// <summary>
        /// Constructor that accepts AppSettings and ConnectionStrings XML data in array form
        /// This method is to support ConfigurationManagerFull() construction
        /// </summary>
        /// <param name="appSettings">ConfigurationManager.AppSettings.ToArraySafe()</param>
        /// <param name="connectionStrings">ConfigurationManager.ConnectionStrings.ToArraySafe()</param>
        public ConfigurationManagerSafe(string[,] appSettings, string[,] connectionStrings) : this()
        {
            connectionStrings = connectionStrings ?? new string[0, 2];
            for (var itemCount = 0; itemCount < connectionStrings.GetLength(0); itemCount++)
            {
                ConnectionStrings.Add(connectionStrings[itemCount, 0], connectionStrings[itemCount, 1]);
            }
            appSettings = appSettings ?? new string[0, 2];
            for (var itemCount = 0; itemCount < appSettings.GetLength(0); itemCount++)
            {
                AppSettings.Add(appSettings[itemCount, 0], appSettings[itemCount, 1]);
            }
        }

        /// <summary>
        /// Allows for return of setting when instantiated
        /// </summary>
        /// <param name="key">Key of app setting to retrieve</param>
        /// <returns>App setting that matches the key</returns>
        public AppSettingSafe AppSetting(string key)
        {
            AppSettingSafe ReturnData = AppSettings.Find(x => x.Key == key).CastSafe<AppSettingSafe>();

            if (ThrowException && ReturnData.Value == Defaults.String)
            {
                throw new System.DataMisalignedException(String.Format("App Setting is missing or has an empty value. ({0})", key));
            }

            return ReturnData;
        }

        /// <summary>
        /// Allows for return of setting when instantiated
        /// </summary>
        /// <param name="key">Key of item to retrieve the value</param>
        /// <returns>Value contents</returns>
        public string AppSettingValue(string key)
        {
            return AppSetting(key).Value;
        }

        /// <summary>
        /// Allows for return of setting when instantiated
        /// </summary>
        /// <param name="key">Key of item to retrieve the value</param>
        /// <returns>Value contents</returns>
        public ConnectionStringSafe ConnectionString(string key)
        {
            ConnectionStringSafe ReturnData = ConnectionStrings.Find(x => x.Key == key).CastSafe<ConnectionStringSafe>();

            if (ThrowException && ReturnData.Value == Defaults.String)
            {
                throw new System.DataMisalignedException(String.Format("Connection string is missing or has an empty value. ({0})", key));
            }

            return ReturnData;
        }

        /// <summary>
        /// Allows for return of setting when instantiated
        /// </summary>
        /// <param name="key">Key of item to retrieve the value</param>
        /// <returns>Value contents</returns>
        public string ConnectionStringValue(string key)
        {
            return ConnectionString(key).Value;
        }
    }
}
