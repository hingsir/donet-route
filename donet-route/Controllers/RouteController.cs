using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace donet_route.Controllers
{
    public class RouteController : Controller
    {
        public ActionResult Index()
        {
            string viewName;
            try
            {
                viewName = HttpContext.Request.Url.Query.Replace("%2F", "/").Substring(6);
            }
            catch (Exception e)
            {
                viewName = "/Views/Route/Index.cshtml";
            }

            ViewBag.viewName = viewName;

            return View(viewName);
        }

	}
}