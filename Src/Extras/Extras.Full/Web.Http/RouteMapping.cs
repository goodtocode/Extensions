//-----------------------------------------------------------------------
// <copyright file="RouteMapping.cs" company="GoodToCode">
//      Copyright (c) GoodToCode. All rights reserved.
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
using System.Web.Http;

namespace GoodToCode.Extras.Web.Http
{
    /// <summary>
    /// Purpose is to support Mvc and Web API route mapping
    /// Routes.MapRoute and Routes.MapHttpRoute
    /// </summary>
    public struct RouteMapping
    {
        /// <summary>
        /// asdf
        /// </summary>
        public struct Mappings
        {
            /// <summary>
            /// DefaulApi: api/{controller}/{id}
            /// </summary>
            public static RouteMapping WebApiDefault = new RouteMapping("DefaulApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            /// <summary>
            /// DefaulApiV1: v1/{controller}/{id}
            /// </summary>
            public static RouteMapping WebApiV1 = new RouteMapping("DefaulV1", "v1/{controller}/{id}", new { id = RouteParameter.Optional });

            /// <summary>
            /// DefaultV1Naked: v1/{controller}/{id}
            /// </summary>
            public static RouteMapping WebApiV1Naked = new RouteMapping("DefaultV1Naked", "v1", new { controller = "HomeApi", action = "Index" });

            /// <summary>
            /// DefaulApi: api/{controller}/{id}
            /// </summary>
            public static RouteMapping MvcDefault = new RouteMapping("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = RouteParameter.Optional });
        }

        /// <summary>
        /// Name of route
        /// Example: "DefaulApi"
        /// </summary>
        public string Name;

        /// <summary>
        /// Route template/mask
        /// Example: "api/{controller}/{id}"
        /// </summary>
        public string RouteTemplate;

        /// <summary>
        /// Default properties
        /// Example: new { id = RouteParameter.Optional })
        /// </summary>
        public object Defaults;

        /// <summary>
        /// Constructor that fully hydrates
        /// </summary>
        /// <param name="name">Name of route mapping: "DefaulApi"</param>
        /// <param name="routeTemplate">Route template/mask</param>
        /// <param name="defaults">Default properties: new { id = RouteParameter.Optional })</param>
        public RouteMapping(string name, string routeTemplate, object defaults)
        {
            this.Name = name;
            this.RouteTemplate = routeTemplate;
            Defaults = defaults;
        }
    }
}
