//-----------------------------------------------------------------------
// <copyright file="UrlBuilder.cs" company="GoodToCode">
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
using GoodToCode.Extensions;
using GoodToCode.Extensions.Text.Encoding;
using System.Collections.Generic;
using GoodToCode.Extensions.Collections;

namespace GoodToCode.Extensions.Net
{
    /// <summary>
    /// Builds URLs similary to and dependent on the UriBuilder class
    /// Uri Layout: [scheme]://[user]:[password]@[host/authority]:[port]/[path];[params]?[querystring]#[fragment]
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>    
    public class UrlBuilder : UriBuilder, IFormattable
    {
        /// <summary>
        /// [scheme]://[user]:[password]@[host/authority]:[port]/[path];[params]?[querystring]#[fragment]
        /// </summary>
        public struct UriMasks
        {
            /// <summary>
            /// [root]/[path]
            /// </summary>
            public static string Root { get; } = @"{0}/{1}";

            /// <summary>
            /// [root]/[path]?[querystring]
            /// </summary>
            public static string RootWithQuerystring { get; } = @"{0}/{0}?{0}";

            /// <summary>
            /// [root]/[path]#[fragment]
            /// </summary>
            public static string RootWithFragment { get; } = @"{0}/{1}#{2}";

            /// <summary>
            /// [root]/[path]/param-1/.../param-n
            /// </summary>
            public static string RootWithParameterBinding { get; } = @"[root]/[path]/param-1/.../param-n";

            /// <summary>
            /// [scheme]://[user]:[password]@[host/authority]:[port]/[path];[params]?[querystring]#[fragment]
            /// </summary>
            public static string Full { get; } = @"[scheme]://[user]:[password]@[host/authority]:[port]/[path];[params]?[querystring]#[fragment]";
        }
            

        /// <summary>
        /// Constructor
        /// </summary>
        public UrlBuilder() 
            : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public UrlBuilder(string fullUrl) 
            : base(fullUrl) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public UrlBuilder(string rootUrl, string path) 
            : this(rootUrl.AddLast("/") + path.RemoveLast("/")) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public UrlBuilder(string rootUrl, string controller, string action) 
            : base(rootUrl.AddLast("/") + controller.AddLast("/") + action.AddLast("/")) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public UrlBuilder(string rootUrl, string controller, string action, string parametersWithNoLeadingQuestionMark) 
            : this(rootUrl, controller, action)
        {
            Query = parametersWithNoLeadingQuestionMark;
        }

        /// <summary>
        /// Constructor that formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH
        /// No trailing slash.
        /// </summary>
        /// <param name="protocol">Protocol for Url. I.e. http</param>
        /// <param name="serverName">Server name for Url. I.e. www.YourDomain.com</param>
        /// <param name="port">Port for Url. I.e. 80</param>
        /// <param name="applicationPath">Application path for Url. I.e. /Home/Index</param>
        /// <returns>Constructed url</returns>
        public UrlBuilder(string protocol, string serverName, int port, string applicationPath) 
            : base(protocol, serverName, port, applicationPath)
        {
        }

        /// <summary>
        /// Constructor that formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH/Parameter1/Parameter2/.../ParameterN/
        /// </summary>
        /// <param name="urlNoQuerystring">Url with everything but parameters and punctuation</param>
        /// <param name="parameters">Collection of parameters to add to Url</param>
        /// <returns>Constructed url</returns>
        public UrlBuilder(string urlNoQuerystring, IEnumerable<string> parameters) 
            : base(urlNoQuerystring.AddLast("/") + String.Join("|", parameters).Replace("||", "|%20|").Replace("|", "/"))
        {
        }

        /// <summary>
        /// Constructor that formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH/Parameter1/Parameter2/.../ParameterN/
        /// </summary>
        /// <param name="urlNoQuerystring">Url with everything but parameters and punctuation</param>
        /// <param name="parameters">Collection of parameters to add to Url</param>
        /// <returns>Constructed url</returns>
        public UrlBuilder(string urlNoQuerystring, KeyValueListString parameters) 
            : base(urlNoQuerystring.AddLast("/") +parameters.ToString("QS"))
        {
        }
        
        /// <summary>
        /// Encodes to URL friendly
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string Encode(string originalString)
        {
            return UrlEncoder.Encode(originalString);
        }

        /// <summary>
        /// Decodes to URL friendly
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string Decode(string originalString)
        {
            return UrlEncoder.Decode(originalString);
        }

        /// <summary>
        /// Forms url in G format, which is PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToString("G");
        }

        /// <summary>
        /// Formats data according to requesting format
        /// </summary>
        /// <param name="format">G: PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH</param>
        /// <param name="formatProvider">ICustomFormatter compatible class</param>
        /// <returns>Key and/or Value formatted</returns>
        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            var returnValue = base.ToString();
            if (formatProvider != null && formatProvider.GetFormat(GetType()) is ICustomFormatter fmt)
            { return fmt.Format(format, this, formatProvider); }
            switch (format)
            {
                case "G":
                    returnValue = base.ToString();
                    break;
                default:
                    returnValue = base.ToString();
                    break;
            }
            return returnValue.RemoveLast("/");
        }
    }
}
