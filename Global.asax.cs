using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using BatchManager.Common;
using BatchManager.Models;

namespace BatchManager
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            var yamlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "batchConfigs.yaml");
            var batchConfigs = BatchConfig.ReadYamlConfig();     
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes,batchConfigs);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            builder.Build();

        }
    }
}
