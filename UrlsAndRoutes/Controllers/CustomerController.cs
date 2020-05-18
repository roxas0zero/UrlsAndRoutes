using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
    [Route("app/[controller]/actions/[action]/{id:weekday?}")]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            var model = new Result
            {
                Controller = nameof(CustomerController),
                Action = nameof(Index)
            };

            return View("Result", model);
        }

        public IActionResult List(string id)
        {
            var model = new Result
            {
                Controller = nameof(CustomerController),
                Action = nameof(List)
            };
            model.Data["Id"] = id ?? "<no value>";
            model.Data["catchall"] = RouteData.Values["catchall"];

            return View("Result", model);
        }
    }
}