//-----------------------------------------------------------------------
// <copyright file="HttpClientBuilder.cs" company="GoodToCode">
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

namespace GoodToCode.Extensions.Media
{
    /// <summary>
    /// Color info in RGB converted format
    /// </summary>
    
    public class RGBStandardInfo
    {
        /// <summary>
        /// Alpha channel (transparency)
        /// </summary>
        public float Alpha { get; set; } = Defaults.Single;
        /// <summary>
        /// Blue channel
        /// </summary>
        public float Blue { get; set; } = Defaults.Single;
        /// <summary>
        /// Green channel
        /// </summary>
        public float Green { get; set; } = Defaults.Single;
        /// <summary>
        /// Red channel
        /// </summary>
        public float Red { get; set; } = Defaults.Single;
        
        /// <summary>
        /// Inverses the current RGB values
        /// </summary>
        /// <returns></returns>
        public RGBStandardInfo Inverse()
        {
            RGBStandardInfo returnValue = new RGBStandardInfo();
            returnValue.Red = (1.0f - this.Red);
            returnValue.Green = (1.0f - this.Green);
            returnValue.Blue = (1.0f - this.Blue);
            return returnValue;
        }
    }
}
