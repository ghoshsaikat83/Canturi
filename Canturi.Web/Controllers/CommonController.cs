using Canturi.Models.BusinessEntity.FrontEnd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult FnChangeCurrancy(string selectedCurrancy)
        {
            string strMsg = "ok";
            string strData = "";
            try
            {
                if (selectedCurrancy.ToLower()=="aud")
                {
                    UserSessionData.Currency = "AUD";
                }
                if (selectedCurrancy.ToLower() == "euro")
                {
                    UserSessionData.Currency = "EURO";
                }
                if (selectedCurrancy.ToLower() == "usd")
                {
                    UserSessionData.Currency = "USD";
                }
            }
            catch (Exception ex)
            {
                strData = "";
            }

            return Json(new { msg = strMsg, data = strData });
        }

        public ActionResult DeleteOldCsv()
        {
            var files = new DirectoryInfo(System.Configuration.ConfigurationManager.AppSettings["RapnetDlsPath"]).GetFiles();
            foreach (var file in files.Where(file => DateTime.UtcNow - file.CreationTimeUtc > TimeSpan.FromDays(2)))
            {
                file.Delete();
            }
            return View();
        }
    }
}
