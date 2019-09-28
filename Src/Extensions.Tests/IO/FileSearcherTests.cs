using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GoodToCode.Extensions.IO;
using GoodToCode.Extensions;
using System;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class FileSearcherTests
    {
        /// <summary>
        /// Searcher worker
        /// </summary>
        [TestMethod()]
        public void Core_IO_FileSearcher()
        {
            var pathsToSearch = new List<string>() { Directory.GetCurrentDirectory() };
            var maskToSearch = @"*.*";
            var searcher = new FileSearcher(pathsToSearch, maskToSearch, 2);
            Assert.IsTrue(searcher.FoundFiles.Count() == 0);
            var files = searcher.Search();
            Assert.IsTrue(searcher.FoundFiles.Count() > 0);
            Assert.IsTrue(files.Count() > 0);
        }


        /// <summary>
        /// Read appsettings.{environment}.json
        /// </summary>
        [TestMethod()]
        public void Core_IO_FileSearcher_Configs()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var pathsToSearch = new List<string>() { Directory.GetCurrentDirectory() };
            var maskToSearch = $"appsettings.{env}.json";
            var searcher = new FileSearcher(pathsToSearch, maskToSearch, 3);
            Assert.IsTrue(searcher.FoundFiles.Count() == 0);
            var files = searcher.Search();
            Assert.IsTrue(searcher.FoundFiles.Count() > 0);
            Assert.IsTrue(files.Count() > 0);
        }
    }
}