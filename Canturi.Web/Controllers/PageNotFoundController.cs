using Canturi.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Controllers
{
    //[FrontEndSessionExpire]
    public class PageNotFoundController : Controller
    {
        //
        // GET: /PageNotFound/

        public ActionResult Index()
        {
            return View();
        }

    }
}
