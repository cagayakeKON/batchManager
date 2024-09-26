using System.IO;
using System;
using System.Web;
using System.Web.Mvc;
using BatchManager.Models;
using BatchManager.Common;

namespace BatchManager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            var batchConfigs = BatchConfig.ReadYamlConfig();
            filters.Add(new DynamicApiFilterAttribute(batchConfigs));
        }
    }
}
