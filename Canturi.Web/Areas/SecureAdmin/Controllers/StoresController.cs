using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Canturi.Models.BusinessHelper.Admin;
using Canturi.Models.BusinessEntity.Admin;
using Canturi.Models.BusinessHelper.CommonHelper;
using Canturi.Web.App_Start;

namespace Canturi.Web.Areas.SecureAdmin.Controllers
{
    [AdminSessionExpire]
    public class StoresController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            List<StoreModel> ActiveStoreModel = new List<StoreModel>();
            List<StoreModel> InActiveStoreModel = new List<StoreModel>();
            StoreHelper objStoreHelper = new StoreHelper();
            ActiveStoreModel= objStoreHelper.GetStores(1);
            InActiveStoreModel = objStoreHelper.GetStores(0);
            return View(Tuple.Create(ActiveStoreModel,InActiveStoreModel));
        }

        public ActionResult Activate(int id)
        {
            try
            {
                StoreHelper objStoreHelper = new StoreHelper();
                StoreModel model = new StoreModel();
                model.StoreId = id;
                model.Status = 1;
                int result=objStoreHelper.UpdateStoreStatus(model);
                if (result == 0)
                {
                    TempData["MessageClass"] = "MsgGreen";
                    TempData["CommonMessage"] = CommonData.GetMessage(StringResource.GetStringResourceFile("admin.Store.Activate"), 1);
                }
                else
                {
                    TempData["MessageClass"] = "MsgRed";
                    TempData["CommonMessage"] = CommonData.GetMessage("Failed to update", 0);
                }
            }
            catch (Exception ex)
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
            }
            return RedirectToAction("Index");
        }

        public ActionResult InActivate(int id)
        {
            try
            {
                StoreHelper objStoreHelper = new StoreHelper();
                StoreModel model = new StoreModel();
                model.StoreId = id;
                model.Status = 0;
                int result = objStoreHelper.UpdateStoreStatus(model);
                if (result == 0)
                {
                    TempData["MessageClass"] = "MsgGreen";
                    TempData["CommonMessage"] = CommonData.GetMessage(StringResource.GetStringResourceFile("admin.Store.InActivate"), 1);
                }
                else
                {
                    TempData["MessageClass"] = "MsgRed";
                    TempData["CommonMessage"] = CommonData.GetMessage("Failed to update", 0);
                }
            }
            catch (Exception ex)
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
            }
            return RedirectToAction("Index");
        }
    }
}