using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class YamlConfigReader
{
    public static BatchConfig[] ReadConfig(string filePath)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var yaml = File.ReadAllText(filePath);
        return deserializer.Deserialize<BatchConfig[]>(yaml);
    }
}

public class BatchConfig
{
    public string Name { get; set; }
    public string FilePath { get; set; }
    public string LogPath { get; set; }
    public string Description { get; set; }
    public string ControllerName { get; set; }
    public Parameter[] Parameters { get; set; }
}

public class Parameter
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
}