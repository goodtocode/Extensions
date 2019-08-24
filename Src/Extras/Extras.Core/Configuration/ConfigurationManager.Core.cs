//-----------------------------------------------------------------------
// <copyright file="ConfigurationManagerCore.cs" company="GoodToCode">
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
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace GoodToCode.Extras.Configuration
{
    /// <summary>
    /// Simulates the System.Configuration.ConfigurationManager class for local XML files with <appSettings></appSettings> nodes.
    /// </summary>
    public class ConfigurationManagerCore : ConfigurationManagerSafe
    {
        private const string pathMask = @"..\";
        private readonly int parentLevels = 10;

        /// <summary>
        /// System/user variable defining environment name
        ///  I.e. Development, Staging, Production
        /// </summary>
        public const string EnvironmentVariable = "ASPNETCORE_ENVIRONMENT";

        /// <summary>
        /// Current environment in system/user variable
        /// </summary>
        public string CurrentEnvironment { get; set; } = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        /// <summary>
        /// Loads AppSettings and ConnectionStrings to be consistent with .NET Framework ConfigurationManager class
        /// Profile 1 (.NET Core 2 and above):
        ///  RootConfigFile: ..\..\..\appsettings.{Environment}.json
        ///  AppSettingsConfigFile: ..\..\appsettings.{Environment}.json
        ///  ConnectionStringsConfigFile: ..\..\appsettings.{Environment}.json
        /// Profile 2 (.NET Core 1 and below):
        ///  RootConfigFile: ..\..\..\app.json
        ///  AppSettingsConfigFile: ..\..\App_Data\appsettings.json
        ///  ConnectionStringsConfigFile: ..\..\App_Data\connectionstrings.json
        /// File Format:
        /// "ConnectionStrings": {
        ///  "DefaultConnection": "Server=tcp:genesysframework.database.windows.net,1433;Initial Catalog=FrameworkData;user id=TestUser; password=57595709-9E9C-47EA-ABBF-4F3BAA1B0D37;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Application Name=GoodToCodeFramework;",
        ///  "Example-LocalFile": "Data Source=(LocalDb)\\MSSQLLocalDB;Database=App_Data\\FrameworkData_Primary.mdf;Trusted_Connection=True;MultipleActiveResultSets=true;Application Name=GoodToCodeFramework;",
        ///  "Example-LocalSQL": "data source=.\\;initial catalog=FrameworkData;Integrated Security=True;Multipleactiveresultsets=True;Application Name=GoodToCodeFramework;",
        ///  "Example-SQLServer": "data source=DatabaseServer.test.goodtocode.com;initial catalog=FrameworkData;user id=TestUser; password=57595709-9E9C-47EA-ABBF-4F3BAA1B0D37;Multipleactiveresultsets=True;Application Name=GoodToCodeFramework;",
        ///  "Example-EDMX": "metadata=res://*/GoodToCode.Framework.Test.csdl|res://*/GoodToCode.Framework.Test.ssdl|res://*/GoodToCode.Framework.Test.msl;provider=System.Data.SqlClient;provider connection string='data source=DatabaseServer.dev.GoodToCode.com;initial catalog=FrameworkData;integrated security=True;MultipleActiveResultSets=True;App=GoodToCodeExtensions'"
        /// },
        /// "AppSettings": {
        ///  "MyWebService": "https://framework-for-webapi.azurewebsites.net/v4",
        ///  "MyWebService-Local": "http://localhost:30004/v4",
        ///  "MyWebService-Public": "https://framework-for-webapi.azurewebsites.net/v4",
        ///  "TestAppSetting": "http://www.goodtocode.com"
        /// }
        /// </summary>
        public ConfigurationManagerCore(ApplicationTypes appType) : base(Directory.GetCurrentDirectory())
        {
#if (DEBUG)
            ThrowException = true;
#endif
            FrameworkType = FrameworkTypes.Core;
            ApplicationType = appType;
            Initialize();
        }

        /// <summary>
        /// Initializes config locations and setup
        /// </summary>
        private void Initialize()
        {
            var parentMask = pathMask;
            var searchPath = CurrentPath;
            var configFiles = new List<string>();

            // Find RootPath
            if (File.Exists($@"{CurrentPath.AddLast(@"\")}{FileNames.AppSettingsJson}"))
                RootPath = CurrentPath;
            else
                for (var count = 0; count < parentLevels; count++)
                {
                    searchPath = Path.GetFullPath(Path.Combine(CurrentPath, parentMask));
                    if (File.Exists($@"{searchPath.AddLast(@"\")}{FileNames.AppSettingsJson}"))
                    {
                        RootPath = searchPath;
                        break;
                    }
                    parentMask += pathMask;
                }
            // Add appsettings.json
            if (File.Exists($@"{RootPath.AddLast(@"\")}{FileNames.AppSettingsJson}"))
                configFiles.Add($@"{RootPath.AddLast(@"\")}{FileNames.AppSettingsJson}");
            // Add appsettings.{environment}.json 
            if (File.Exists($@"{RootPath.AddLast(@"\")}{string.Format(FileNames.AppSettingsEnvironmentJson, CurrentEnvironment)}"))
                configFiles.Add($@"{RootPath.AddLast(@"\")}{string.Format(FileNames.AppSettingsEnvironmentJson, CurrentEnvironment)}");
            // Add App_Data\AppSettings.json
            if (File.Exists($@"{AppDataFolder.AddLast(@"\")}{FileNames.AppSettingsJson}"))
                configFiles.Add($@"{AppDataFolder.AddLast(@"\")}{FileNames.AppSettingsJson}");
            // Add App_Data\ConnectionStrings.json
            if (File.Exists($@"{AppDataFolder.AddLast(@"\")}{FileNames.ConnectionStringsJson}"))
                configFiles.Add($@"{AppDataFolder.AddLast(@"\")}{FileNames.ConnectionStringsJson}");
            // Load
            Load(configFiles);
        }

        /// <summary>
        /// Loads from JSON data
        ///  Use: Configuration["subsection:suboption1"]
        /// </summary>
        private void Load(IEnumerable<string> configFiles)
        {
            IConfigurationRoot configuration;
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();

            // Load files
            foreach (var file in configFiles)
                configBuilder.AddJsonFile(file, optional: true);
            configuration = configBuilder.Build();
            // Add AppSettings node
            foreach (var Item in configuration?.GetSection("AppSettings").GetChildren())
            {
                AppSettings.Add(new AppSettingSafe(Item.Key, Item.Value));
            }
            // Add ConnectionStrings node
            foreach (var Item in configuration.GetSection("ConnectionStrings").GetChildren())
            {
                ConnectionStrings.Add(new ConnectionStringSafe(Item.Key, Item.Value));
            }
        }
    }
}