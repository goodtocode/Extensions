//-----------------------------------------------------------------------
// <copyright file="ContentTypes.cs" company="GoodToCode">
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

namespace GoodToCode.Extensions.Net
{
    /// <summary>
    /// Extends Multipurpose Internet Mail Exchange (MIME) headers
    /// HTML content types
    /// </summary>
    /// <remarks></remarks>
    
    public class ContentTypes : List<ContentType>
    {       
        /// <summary>
        /// Constructor
        /// </summary>
        public ContentTypes()
            : base()
        {
            loadData();
        }
        
        /// <summary>
        /// Loads content types into this object
        /// </summary>
        protected void loadData()
        {
            // Unknown (similar to application/octet-stream, but avoids chrome throwing exception)
            Add(MimeTypes.ApplicationUnknown);
            // Image
            Add(MimeTypes.ImageUnknown);
            Add(MimeTypes.Bmp, ".bmp");
            Add(MimeTypes.Gif, ".gif");
            Add(MimeTypes.Jpg, ".jpg");
            Add(MimeTypes.Png, ".png");
            Add(MimeTypes.Tif, ".tif");
            //Documents
            Add(MimeTypes.Doc, ".doc");
            Add(MimeTypes.Docx, ".docx");
            Add(MimeTypes.Pdf, ".pdf");
            //Slide shows
            Add(MimeTypes.Ppt, ".ppt");
            Add(MimeTypes.Pptx, ".pptx");
            //Data
            Add(MimeTypes.Xls, ".xls");
            Add(MimeTypes.Xlsx, ".xlsx");            
            Add(MimeTypes.Csv, ".csv");
            Add(MimeTypes.Json, ".json");
            Add(MimeTypes.Xml, ".xml");
            Add(MimeTypes.Txt, ".txt");
            //Compressed Folders
            Add(MimeTypes.Zip, ".zip");
            //Audio
            Add(MimeTypes.Ogg, ".ogg");
            Add(MimeTypes.Mp3, ".mp3");
            Add(MimeTypes.Wma, ".wma");
            Add(MimeTypes.Wav, ".wav");
            //Video;
            Add(MimeTypes.Wmv, ".wmv");
            Add(MimeTypes.FlashSwf, ".swf");
            Add(MimeTypes.Avi, ".avi");
            Add(MimeTypes.Mp4, ".mp4");
            Add(MimeTypes.Mpeg, ".mpeg");
            Add(MimeTypes.Qt, ".qt");
        }
        
        /// <summary>
        /// Adds new content type
        /// </summary>
        /// <param name="name"></param>
        public void Add(string name)
        {
            Add(new ContentType(name));
        }

        /// <summary>
        /// Adds new content type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="extension"></param>
        public void Add(string name, string extension)
        {
            Add(new ContentType(name, extension));
        }
        
    }
}
