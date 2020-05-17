using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new Result
            {
                Controller = nameof(HomeController),
                Action = nameof(Index)
            };

            return View("Result", model);
        }

        public IActionResult CustomVariable1()
        {
            var model = new Result
            {
                Controller = nameof(HomeController),
                Action = nameof(CustomVariable1)
            };
            model.Data["Id"] = RouteData.Values["Id"];

            return View("Result", model);
        }

        public IActionResult CustomVariable2(string id)
        {
            var model = new Result
            {
                Controller = nameof(HomeController),
                Action = nameof(CustomVariable2)
            };
            model.Data["Id"] = id ?? "<no value>";

            return View("Result", model);
        }
    }
}