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
    public class ChangePasswordController : Controller
    {
        //
        // GET: /SecureAdmin/ChangePassword/

        public ActionResult Index()
        {
            ChangePasswordModel model = new ChangePasswordModel();
            if (TempData["message"] != null)
            {
                model.MessageClass = (string)TempData["MessageClass"];
                model.IsShowMessage = 1;
                model.Message = (string)TempData["message"];
            }
            else
            {
                model.MessageClass = "";
                model.IsShowMessage = 0;
                model.Message = "";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ChangePasswordModel model)
        {
            try
            {
                AdminUserHelper objUserHelper = new AdminUserHelper();
                int val = objUserHelper.ChangeAdminPassword(ref model);
                if (val == 1)
                {
                    model.IsShowMessage = 1;
                    model.Message = "Please enter correct old password.";
                    model.MessageClass = "MsgRed";
                }
                else
                {
                    model.IsShowMessage = 1;
                    model.Message = model.Message;
                    model.MessageClass = "MsgGreen";
                }
            }
            catch (Exception ex)
            {

                model.IsShowMessage = 1;
                model.Message = ex.Message;
                model.MessageClass = "MsgRed";
            }
            return View(model);
        }

    }
}
