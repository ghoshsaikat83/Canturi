using Canturi.Models.BusinessHelper.Admin;
using Canturi.Models.BusinessHelper.CommonHelper;
using Canturi.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Areas.SecureAdmin.Controllers
{
    [AdminSessionExpire]
    public class CurrencySettingController : Controller
    {
        //
        // GET: /SecureAdmin/CurrencySetting/

        public ActionResult Index()
        {
            BindSettingValues();

            return View();
        }

        private void BindSettingValues()
        {
            SettingHelper objSettingHelper = new SettingHelper();
            DataTable dtSetting = objSettingHelper.GetSettings(1);
            foreach (DataRow item in dtSetting.Rows)
            {
                if (item["SettingName"].ToString() == "CurrancyAud")
                    ViewBag.InAud = item["SettingValue"];

                if (item["SettingName"].ToString() == "CurrancyEuro")
                    ViewBag.InEuro = item["SettingValue"];
            }
        }

        [HttpPost]
        public ActionResult Index(string id)
        {
            try
            {
                string strInAud = "";
                string strInEuro = "";
                strInAud = Request.Form["InAud"].ToString();
                strInEuro = Request.Form["InEuro"].ToString();
                ViewBag.InEuro = strInEuro;
                ViewBag.InAud = strInAud;
                if (String.IsNullOrEmpty(strInAud))
                {

                    TempData["CommonMessage"] = CommonData.GetMessage("Please input value In AUD.", 0);
                    return View();
                }
                if (String.IsNullOrEmpty(strInEuro))
                {
                    TempData["CommonMessage"] = CommonData.GetMessage("Please input value In Euro.", 0);
                    return View();
                }
                try
                {
                    double dblInAud = Convert.ToDouble(strInAud);
                    strInAud = dblInAud.ToString();
                }
                catch (Exception ex)
                {
                    TempData["CommonMessage"] = CommonData.GetMessage("Invalid In AUD value", 0);
                    return View();
                }
                try
                {
                    double dblInAud = Convert.ToDouble(strInEuro);
                    strInEuro = dblInAud.ToString();
                }
                catch (Exception ex)
                {
                    TempData["CommonMessage"] = CommonData.GetMessage("Invalid In Euro value", 0);
                    return View();
                }

                SettingHelper objSettingHelper = new SettingHelper();
                int result = objSettingHelper.AddUpdSettings("CurrancyAud", strInAud, 1);
                int result1 = objSettingHelper.AddUpdSettings("CurrancyEuro", strInEuro, 1);
                if (result == 0 && result1 == 0)
                {
                    TempData["CommonMessage"] = CommonData.GetMessage("Record Updated successfully!", 1);
                }
                else
                {
                    TempData["CommonMessage"] = CommonData.GetMessage("Error, Please try again", 0);
                }

            }
            catch (Exception ex)
            {

                TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
            }

            BindSettingValues();
            return View();
        }

    }
}
