using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BatchManager.Models
{
    public class BatchConfig
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string LogPath { get; set; }
        public string Description { get; set; }
        public string ControllerName { get; set; }
        public List<Parameter> Parameters { get; set; }

        public BatchConfig()
        {
            Parameters = new List<Parameter>();
        }

        public static List<BatchConfig> ReadYamlConfig()
        {
            string yamlFilePath= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "batchConfigs.yaml");
            try
            {
                using (var reader = new StreamReader(yamlFilePath)) 
                {
                    var deserializer = new DeserializerBuilder()
                        .WithNamingConvention(CamelCaseNamingConvention.Instance)
                        .Build();

                    return deserializer.Deserialize<List<BatchConfig>>(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read YAML configuration: {ex.Message}");
                throw;
            }
        }
    }

    public class Parameter
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}