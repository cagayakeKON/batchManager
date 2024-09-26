using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Primitives;
using BatchManager.Models;

namespace BatchManager.Common
{
    public class DynamicApiFilterAttribute : ActionFilterAttribute
    {
        private readonly List<BatchConfig> _configs;

        public DynamicApiFilterAttribute(List<BatchConfig> configs)
        {
            _configs = configs;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;

            var commandExecuteService = new LocalCommandExecute();
            var matchedConfig = _configs.FirstOrDefault(c => $"/{c.ControllerName}".Equals(request.Path, StringComparison.OrdinalIgnoreCase));

            if (matchedConfig != null)
            {
                var lines = 0;
                string body = null;

                using (var reader = new StreamReader(request.InputStream)) // Synchronous version
                {
                    body = reader.ReadToEnd();
                }

                if (!string.IsNullOrEmpty(body))
                {
                    var bodyJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(body);
                    if (bodyJson != null && bodyJson.ContainsKey("lines"))
                    {
                        var value = bodyJson["lines"];

                        request.QueryString.Add("lines", value.ToString());

                        try
                        {
                            lines = Convert.ToInt32(value);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Invalid lines value", ex);
                        }
                    }
                }

                var cmd = matchedConfig.FilePath + " " + string.Join(" ", matchedConfig.Parameters.Select(p => p.Name));
                var result = commandExecuteService.Execute(cmd, matchedConfig.LogPath, 10);

                response.ContentType = "application/json";
                var jsonResponse = JsonConvert.SerializeObject(result);
                response.Write(jsonResponse);
                filterContext.Result = new EmptyResult(); 
            }
            else
            {
                base.OnActionExecuting(filterContext); 
            }
        }
    }
}
