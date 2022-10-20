using Canturi.Models.BusinessEntity.Admin;
using Canturi.Models.BusinessHelper.Admin;
using Canturi.Models.BusinessHelper.CommonHelper;
using Canturi.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Areas.SecureAdmin.Controllers
{
    
    public class LogOnController : Controller
    {
        //
        // GET: /Admin/Dashboard/
        [HttpGet]
        public ActionResult Index()
        {
            LogOnModels ObjModel = new LogOnModels();
            if (AdminSessionData.AdminUserId != 0)
            {
                return Redirect(Url.Content("~/SecureAdmin/DashBoard"));
            }
            return View(ObjModel);
        }

        [HttpPost]
        public ActionResult Index(LogOnModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string message = string.Empty;
                    int result = -1;
                    DataTable dt = model.AuthenticateUser(model, out result);
                    if (result == 0)
                    {
                        AdminSessionData.AdminName = dt.Rows[0]["Name"].ToString();
                        AdminSessionData.AdminUserId = Convert.ToInt32(dt.Rows[0]["userId"].ToString());
                        //AdminSessionData.AdminStoreId = Convert.ToInt32(dt.Rows[0]["StoreId"].ToString());
                        AdminSessionData.AdminRoleId = Convert.ToInt32(dt.Rows[0]["roleId"].ToString());
                        AdminSessionData.AdminCreatedOn = dt.Rows[0]["createdOn"].ToString();
                        AdminSessionData.AdminLastLoginOn = dt.Rows[0]["LastLogin"].ToString();
                        AdminSessionData.AdminRoleName = dt.Rows[0]["RoleName"].ToString();

                        AdminSessionData.AdminThemeName = dt.Rows[0]["ThemeName"].ToString();
                        AdminSessionData.AdminThemeFixedLayout = dt.Rows[0]["IsFixedLayout"].ToString();
                        return RedirectToAction("Index", "DashBoard");
                    }
                    else if (result == -1)
                    {
                        model.Message = "Error occured! Try Again";
                    }
                    else if (result == -2)
                    {
                        model.Message = "Please fill correct username and password";
                    }
                    else if (result == -3)
                    {
                        model.Message = "Your account is not activated";
                    }
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        [AdminSessionExpire]
        public ActionResult UpdateTheme(string themeName)
        {
            try
            {
                AdminSessionData.AdminThemeName = themeName;
                AdminUserHelper objAdminUserHelper = new AdminUserHelper();
                int result = objAdminUserHelper.UpdateAdminTheme(6, themeName, AdminSessionData.AdminUserId);
            }
            catch
            {
            }
            return Json(new { ok = false, data = "", message = "" });
        }

        public ActionResult ForgetPassword()
        {
            ForgetPasswordModel ObjModel = new ForgetPasswordModel();
            return View(ObjModel);
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordModel objForgetPasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string message = string.Empty;
                    objForgetPasswordModel.Flag = 4;
                    DataTable dt = objForgetPasswordModel.AdminPasswordReminder(objForgetPasswordModel);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int sucess = EmailSender.FncSendAdminPasswordReminderMail(dt.Rows[0]["emailid"].ToString(), dt.Rows[0]["password"].ToString(), dt.Rows[0]["loginid"].ToString(), dt.Rows[0]["username"].ToString(), dt.Rows[0]["VerticalId"].ToString());
                        if (sucess == 1)
                        {
                            objForgetPasswordModel.Message = "Your password has been sent. Please check your inbox.It usually takes a few minutes but when we're busy.it may take longer.";
                        }
                        else
                        {
                            objForgetPasswordModel.Message = "Please try again.";
                        }
                    }
                    else
                    {
                        objForgetPasswordModel.Message = "Email address does not exists. Please try again.";
                    }

                }
                return View(objForgetPasswordModel);
            }
            catch
            {
                return View();
            }
        }

    }
}
