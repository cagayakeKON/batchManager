﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BatchManager.Models;

namespace BatchManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes, List<BatchConfig> batchConfigs)
        {
            foreach (var config in batchConfigs)
            {
                routes.MapRoute(
                    name: config.ControllerName,
                    url: $"{config.ControllerName}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            }
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
       

        }
    }
}
