using Microsoft.Extensions.Configuration;
using System;

namespace GoodToCode.Extensions.Test
{
    public class AppSettingFactory
    {
        private IConfiguration _config;

        public string DefaultConnection { get { return _config["AppSettings:MyWebService"]; } }
        
        public AppSettingFactory()
        {
            _config = new ConfigurationBuilder()
              .AddJsonFile($"appsettings.{(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT ") ?? "Development")}.json")
              .AddJsonFile("appsettings.json")
              .Build();
        }

        public AppSettingFactory(IConfiguration config)
        {
            _config = config;
        }

        public string GetDefaultConnection()
        {
            return DefaultConnection;
        }
    }
}
