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
    public class CaratSizeSettingController : Controller
    {
        // GET: SecureAdmin/CaratSizeSetting
        public ActionResult Index()
        {
            CaratSizeSettingModel model = new CaratSizeSettingModel();
            model = FnBindSetting(model);

            return View(model);
        }

        private static CaratSizeSettingModel FnBindSetting(CaratSizeSettingModel model)
        {
            CaratSizeSettingHelper objCaratSizeSettingHelper = new CaratSizeSettingHelper();
            model.Flag = 1;
            model = objCaratSizeSettingHelper.GetCaratSizeSettingById(model);
            return model;
        }

        [HttpPost]
        public ActionResult Index(CaratSizeSettingModel model)
        {
            if (ModelState.IsValid)
            {
                CaratSizeSettingHelper objCaratSizeSettingHelper = new CaratSizeSettingHelper();
                if (model.MinimumCaratSize < model.MaximumCaratSize)
                {
                    
                    model.Flag = 1;
                    if (objCaratSizeSettingHelper.AddUpdCaratSizeSetting(model) == 0)
                    {
                        model = FnBindSetting(model);
                        model.IsShowMessage = 1;
                        model.MessageClass = "MsgGreen";
                        model.Message = "Carat size setting has been save successfully…!";
                    }
                    else
                    {
                        model = FnBindSetting(model);
                        model.IsShowMessage = 1;
                        model.MessageClass = "MsgRed";
                        model.Message = "Error, please try again…!";
                    }
                }
                else
                {
                    ModelState.Clear();
                    model = FnBindSetting(model);
                    model.IsShowMessage = 1;
                    model.MessageClass = "MsgRed";
                    model.Message = "Error, Maximum carat size should be less then maximum carat size.";
                }
            }
            else
            {
                ModelState.Clear();
                model = FnBindSetting(model);
                model.IsShowMessage = 1;
                model.MessageClass = "MsgRed";
                model.Message = "Error, please try again…!";
            }
            return View(model);
        }


    }
}