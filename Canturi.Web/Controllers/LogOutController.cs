using Canturi.Models.BusinessEntity.FrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Controllers
{
    public class LogOutController : Controller
    {
        //
        // GET: /LogOut/

        public ActionResult Index()
        {
            UserSessionData.UserId = 0;
            UserSessionData.UserName ="";
            UserSessionData.Name = "";
            UserSessionData.Currency = "AUD";
            return Redirect(Url.Content("~/"));
        }

    }
}
