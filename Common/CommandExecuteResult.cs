using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BatchManager.Common
{
    public sealed class CommandExecuteResult
    {
        public string Result { get; }
        public string Logs { get; }

        public CommandExecuteResult(string result, string logs)
        {
            Result = result;
            Logs = logs;
        }

        public override string ToString() => $"{nameof(Result)}: {Result}, {nameof(Logs)}: {Logs}";
    }

}