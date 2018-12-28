using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace APIEmbedded.Config
{
    public interface IConfiguration
    {
        T Get<T>(string key);
        IConfigurationSection GetSection(string key);
        string GetSectionArray(string key);

        IEnumerable<IConfigurationSection> GetChildren(string key);
    }

    public class Configuration : IConfiguration
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public Configuration(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T Get<T>(string key)
        {
            return (_configuration.GetValue<T>(key));
        }

        public IConfigurationSection GetSection(string key)
        {
            return (_configuration.GetSection(key));
        }

        public string GetSectionArray(string key)
        {
            return (_configuration[key]);
        }
        public IEnumerable<IConfigurationSection> GetChildren(string key)
        {
            return (_configuration.GetChildren());
        }
    }
}
