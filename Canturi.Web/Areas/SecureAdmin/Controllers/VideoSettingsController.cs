using Canturi.Models.BusinessEntity.Admin;
using Canturi.Models.BusinessHelper.Admin;
using Canturi.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Areas.SecureAdmin.Controllers
{
    [AdminSessionExpire]
    public class VideoSettingsController : Controller
    {
        // GET: SecureAdmin/VideoSettings
        public ActionResult Index()
        {
            SupplierHelper helper = new SupplierHelper();
            List<SuppliersVideo> list = helper.GetSuppliersVideoSetting(1);
            ViewBag.SupplierList = list;
            return View();
        }

        [HttpPost]
        public ActionResult Update(string ids)
        {
            SupplierHelper helper = new SupplierHelper();
            string strMsg = "ok";
            string strData = "";
            int res = helper.SaveSupplierVideoSettings(1,ids);
            if (res < 0)
            {
                strMsg = "notok";
                strData = "Error! Record not updated.";
            }
            return Json(new { msg = strMsg, data = strData });
        }
    }
}