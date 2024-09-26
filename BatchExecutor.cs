using System.Diagnostics;

public class BatchExecutor
{
    public static void ExecuteBatch(BatchConfig config, Dictionary<string, string> parameters)
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = config.FilePath,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            Arguments = string.Join(" ", parameters.Select(p => $"{p.Key}={p.Value}"))
        };

        using (var process = new Process { StartInfo = processInfo })
        {
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            // 处理输出，例如写入日志文件
        }
    }
}