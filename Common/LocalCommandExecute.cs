using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace BatchManager.Common
{
    public class LocalCommandExecute 
    {
        public CommandExecuteResult Execute(string cmd, string logPath = null, int lines = 10)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {cmd}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // Get the last lines and return
            if (lines <= 0 || string.IsNullOrEmpty(logPath) || !File.Exists(logPath))
            {
                return new CommandExecuteResult(result, "");
            }

            var logs = File.ReadAllLines(logPath);
            return new CommandExecuteResult(result, string.Join(Environment.NewLine, logs.Skip(Math.Max(0, logs.Length - lines))));
        }
    }
}