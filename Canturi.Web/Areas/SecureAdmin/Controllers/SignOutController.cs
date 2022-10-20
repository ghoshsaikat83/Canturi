using Canturi.Models.BusinessHelper.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Areas.SecureAdmin.Controllers
{
    public class SignOutController : Controller
    {
        //
        // GET: /SecureAdmin/SignOut/

        public ActionResult Index()
        {
            AdminSessionData.AdminUserId = 0;
            AdminSessionData.AdminUserName = "";
            AdminSessionData.AdminName = "";
            //SessionData.AdminUserId= blLogin.LoginId;
            AdminSessionData.AdminRoleId = 0;

            AdminSessionData.AdminLastLoginOn = "";
            return Redirect(Url.Content("~/SecureAdmin/logon"));
        }

    }
}
