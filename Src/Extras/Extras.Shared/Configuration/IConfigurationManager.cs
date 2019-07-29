//-----------------------------------------------------------------------
// <copyright file="IConfigurationManager.cs" company="GoodToCode">
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

namespace GoodToCode.Extras.Configuration
{
    /// <summary>
    /// Cross-platform emulator of System.Configuration.ConfigurationManager, for local .config files
    /// Target Projects: Universal Windows 10/8.1, Xamarin Forms, Windows Phone, Portable Class Library
    /// Usage: From the Application project, 1. Read .config XML from filesystem, 2. Pass xml into constructor as strings
    /// </summary>
    
    public interface IConfigurationManager
    {
        /// <summary>
        /// All application settings in the referenced config file
        /// </summary>
        AppSettingList AppSettings { get; }

        /// <summary>
        /// All connection strings in the referenced config file
        /// </summary>
        ConnectionStringList ConnectionStrings { get; }

        /// <summary>
        /// Allows for return of setting when instantiated
        /// </summary>
        /// <param name="key">Key of app setting to retrieve</param>
        /// <returns>App setting that matches the key</returns>
        AppSettingSafe AppSetting(string key);

        /// <summary>
        /// Allows for return of setting when instantiated
        /// </summary>
        /// <param name="key">Key of item to retrieve the value</param>
        /// <returns>Value contents</returns>
        string AppSettingValue(string key);

        /// <summary>
        /// Allows for return of setting when instantiated
        /// </summary>
        /// <param name="key">Key of item to retrieve the value</param>
        /// <returns>Value contents</returns>
        ConnectionStringSafe ConnectionString(string key);

        /// <summary>
        /// Allows for return of setting when instantiated
        /// </summary>
        /// <param name="key">Key of item to retrieve the value</param>
        /// <returns>Value contents</returns>
        string ConnectionStringValue(string key);
    }
}
