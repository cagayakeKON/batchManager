using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BatchManager.Models;

namespace BatchManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var batchConfigs = BatchConfig.ReadYamlConfig();

            return View(batchConfigs);
        }
    }
}
