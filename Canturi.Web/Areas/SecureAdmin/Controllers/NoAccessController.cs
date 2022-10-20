using Canturi.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Areas.SecureAdmin.Controllers
{
    [AdminSessionExpire]
    public class NoAccessController : Controller
    {
        //
        // GET: /SecureAdmin/NoAccess/

        public ActionResult Index()
        {
            return View();
        }

    }
}
