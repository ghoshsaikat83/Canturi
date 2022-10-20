using Canturi.Models.BusinessEntity.FrontEnd;
using Canturi.Models.BusinessHelper.CommonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Controllers
{
    public class SmtpController : Controller
    {
        // GET: Smtp
        public ActionResult Index()
        {
            EmailServerModel emailServerModel = new EmailServerModel();
            return View(emailServerModel);
        }

        [HttpPost]
        public ActionResult Index(EmailServerModel emailServerModel)
        {


            string mailSuccess = EmailSender.SendTestMailFromAdmin(emailServerModel);
            TempData["CommonMessage"] = mailSuccess;
            //if (mailSuccess == "Success")
            //{
            //    TempData["CommonMessage"] = "Email is tested successfully.";
            //    return View(emailServerModel);
            //}
            //else
            //{
            //    TempData["CommonMessage"] = mailSuccess;
            //    return View(emailServerModel);
            //}

            return View(emailServerModel);
        }
    }
}