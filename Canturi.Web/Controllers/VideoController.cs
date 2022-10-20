using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Controllers
{
    public class VideoController : Controller
    {
        // GET: Video
        //
        public ActionResult Index(string id)
        {
            try
            {
                ViewBag.URL = id;
            }
            catch { }
            return View();
        }
    }
}