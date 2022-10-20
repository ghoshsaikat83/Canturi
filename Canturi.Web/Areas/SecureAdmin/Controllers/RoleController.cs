using Canturi.Models.BusinessEntity.Admin;
using Canturi.Models.BusinessHelper.Admin;
using Canturi.Models.BusinessHelper.CommonHelper;
using Canturi.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace Canturi.Web.Areas.SecureAdmin.Controllers
{
    [AdminSessionExpire]
    public class RoleController : Controller
    {
        //
        // GET: /Admin/Role/

        public ActionResult Index()
        {
            return View();
        }


        //Role Section

        public ActionResult Add()
        {
            RoleModels roleModel = new RoleModels();
            return View(roleModel);
        }

        [HttpPost]
        public ActionResult Add(RoleModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.IsShowPrivillage = 0;
                    model.IsShowMessage = 0;
                    model.Flag = 1;
                    model.CreatedBy = AdminSessionData.AdminUserId;
                    model.CreatedFromIp = Request.UserHostAddress;

                    RoleHelper objRole = new RoleHelper();
                    int result = objRole.InsertRole(model);
                    if (result > 0)
                    {
                        //return RedirectToAction("ViewRole", "Admin");
                        model.RoleId = result;
                        model.RoleName = "";
                        model.RoleDescription = "";
                        model.MenuHTML = MenuHelper.GetNewEditModuleForRoles(0);
                        model.IsShowMessage = 1;
                        model.IsShowPrivillage = 1;
                        model.MessageClass = "MsgGreen";
                        //model.Message = "Role successfully added. You can now add privileges to this role.";
                        model.Message = StringResource.GetStringResourceFile("admin.RoleInsert");
                    }
                    else if (result == -2)
                    {
                        model.MessageClass = "MsgRed";
                        model.IsShowMessage = 1;
                        model.Message = StringResource.GetStringResourceFile("admin.RoleAlreadyExist");
                    }
                    else
                    {
                        model.MessageClass = "MsgRed";
                        model.IsShowMessage = 1;
                        model.Message = StringResource.GetStringResourceFile("admin.RoleError");
                    }
                }
                return View(model);
            }
            catch(Exception ex)
            {
                model.MessageClass = "MsgRed";
                model.IsShowMessage = 1;
                model.Message = ex.Message;
                return View(model);
            }
        }


        [HttpGet]
        public ActionResult Edit(int? RoleId, RoleModels model)
        {
            model.Flag = 2;
            model.RoleId = (int)RoleId;
            model.MenuHTML = MenuHelper.GetNewEditModuleForRoles((int)RoleId);
            RoleHelper objHelper = new RoleHelper();
            model = objHelper.GetRoleById(model);

            // for clear of the validation class
            ModelState.Clear();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RoleModels model)
        {
            if (ModelState.IsValid)
            {
                model.IsShowMessage = 0;
                model.Flag = 2;
                model.CreatedBy = AdminSessionData.AdminUserId;
                model.CreatedFromIp = Request.UserHostAddress;

                RoleHelper objRole = new RoleHelper();
                int result = objRole.UpdateRole(model);
                if (result == 0)
                {
                    TempData["MessageClass"] = "MsgGreen";
                    TempData["message"] = StringResource.GetStringResourceFile("admin.RoleUpdate");
                    return RedirectToAction("List", "Role");
                }
                else if (result == -2)
                {
                    TempData["MessageClass"] = "MsgRed";
                    TempData["message"] = StringResource.GetStringResourceFile("admin.RoleAlreadyExist");
                }
                else
                {
                    TempData["MessageClass"] = "MsgRed";
                    TempData["message"] = StringResource.GetStringResourceFile("admin.RoleError");
                }
                return RedirectToAction("List", "Role");
            }
            return View(model);

        }



        List<RoleModels> ActiveRolesList = new List<RoleModels>();
        List<RoleModels> InActiveRolesList = new List<RoleModels>();

        public ActionResult List(string msg)
        {
            RoleModels model = new RoleModels();
            if (TempData["message"] != null)
            {
                model.MessageClass = (string)TempData["MessageClass"];
                model.IsShowMessage = 1;
                model.Message = (string)TempData["message"];
            }
            else if (msg != null)
            {
                model.MessageClass = "MsgGreen";
                model.IsShowMessage = 1;
                //model.Message = "Role successfully added/updated.";
                model.Message = StringResource.GetStringResourceFile("admin.RoleUpdate");
            }
            else
            {
                model.MessageClass = "";
                model.IsShowMessage = 0;
                model.Message = "";
            }

            //get grid parameters from URL/POST (if any)
            var activeGridParameters = ASPRazorWebGridSample.UI.GridParameters.GetGridParameters();
            int pageSize = 10;   //displayed rows per page


            model.Flag = 1;
            model.Status = 1;
            RoleHelper objHelper = new RoleHelper();



            ActiveRolesList = objHelper.GetRoleByStatus(model);
            var ActiveRole = GetDataUsingLINQ(activeGridParameters.Sort,       //order by column
                                        activeGridParameters.SortDirection,   //order by direction
                                        activeGridParameters.Page ?? 1,       //returned page
                                        pageSize, ActiveRolesList);               //displayed rows per page


            model.Status = 0;
            var InActiveGridParameters = ASPRazorWebGridSample.UI.GridParameters.GetGridParameters();
            InActiveRolesList = objHelper.GetRoleByStatus(model);
            var InActiveRole = GetDataUsingLINQ(InActiveGridParameters.Sort,       //order by column
                                        InActiveGridParameters.SortDirection,   //order by direction
                                        InActiveGridParameters.Page ?? 1,       //returned page
                                        pageSize, InActiveRolesList);               //displayed rows per page


            //set record count for use in view


            ViewBag.ActiveGridRecordCount = ActiveRolesList.Count;
            ViewBag.InActiveGridRecordCount = InActiveRolesList.Count;

            return View(Tuple.Create(ActiveRole, InActiveRole, model));
        }

        //get data from datasource using LINQ (sample data access layer)
        private IEnumerable<RoleModels> GetDataUsingLINQ(string sort, string sortDir, int page, int numRows, List<RoleModels> RolesList)
        {

            //List<RoleModels> list = RolesList.AsQueryable().Skip((page - 1) * numRows).Take(numRows).ToList();
            List<RoleModels> list = RolesList.AsQueryable().ToList();


            int tempSearilStart = 0;// (page - 1) * numRows;

            for (int i = 0; i < list.Count; i++)
            {
                list[i].SerialNumber = tempSearilStart + i + 1;
            }

            if (!string.IsNullOrEmpty(sort))
                return list.AsQueryable().OrderBy(sort + " " + sortDir).ToList();
            else
            {
                return list.AsQueryable().ToList();
            }
        }


        [HttpGet]
        public ActionResult Delete(int? roleId, RoleModels model)
        {
            model.Flag = 3;
            model.RoleId = (int)roleId;
            RoleHelper objHelper = new RoleHelper();
            int result = objHelper.DeleteRole(model);
            if (result == 0)
            {
                TempData["MessageClass"] = "MsgGreen";
                TempData["message"] = StringResource.GetStringResourceFile("admin.RoleDelete");
            }
            else
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["message"] = StringResource.GetStringResourceFile("admin.RoleNotDelete");
            }
            return RedirectToAction("List", "Role");

        }

        [HttpGet]
        public ActionResult InActivate(int? roleId, RoleModels model)
        {
            model.Flag = 4;
            model.RoleId = (int)roleId;
            model.Status = 0;
            RoleHelper objHelper = new RoleHelper();
            int result = objHelper.ChangeStatusRole(model);
            if (result == 0)
            {
                TempData["MessageClass"] = "MsgGreen";
                TempData["message"] = StringResource.GetStringResourceFile("admin.RoleInactivate");

            }
            else
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["message"] = StringResource.GetStringResourceFile("admin.RoleNotInactivate");
            }
            return RedirectToAction("List", "Role");

        }

        [HttpGet]
        public ActionResult Activate(int? roleId, RoleModels model)
        {
            model.Flag = 4;
            model.RoleId = (int)roleId;
            model.Status = 1;
            RoleHelper objHelper = new RoleHelper();
            int result = objHelper.ChangeStatusRole(model);
            if (result == 0)
            {
                TempData["MessageClass"] = "MsgGreen";
                TempData["message"] = StringResource.GetStringResourceFile("admin.RoleActivate");

            }
            else
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["message"] = StringResource.GetStringResourceFile("admin.RoleNotActivate");
            }
            return RedirectToAction("List", "Role", new { @At = "InActiveRoles" });

        }



        [HttpPost]
        public ActionResult AddPrivillage(string ModuleId, int RoleId)
        {            
            try
            {
                int result = -1;
                RoleModels objRoleModel = new RoleModels();
                objRoleModel.Flag = 1;
                objRoleModel.RoleId = RoleId;
                objRoleModel.ModuleIds = ModuleId;
                objRoleModel.CreatedBy = AdminSessionData.AdminUserId;
                objRoleModel.CreatedFromIp = Request.UserHostAddress;

                RoleHelper objRoleHelper = new RoleHelper();
                result = objRoleHelper.InsertModuleOnRole(objRoleModel);
                if (result == 0)
                {
                    TempData["message"] = StringResource.GetStringResourceFile("admin.RolePrivilegesAdded");
                    TempData["MessageClass"] = "MsgGreen";
                    return Json(new { ok = true, data = "", message = StringResource.GetStringResourceFile("admin.PrivilegesAdded") });
                }
                else
                {
                    TempData["message"] = StringResource.GetStringResourceFile("admin.RolePrivilegesNotAdded");
                    TempData["MessageClass"] = "MsgGreen";
                    return Json(new { ok = false, data = "", message = StringResource.GetStringResourceFile("admin.PrivilegesNotAdded") });
                }
            }
            catch
            {
                return Json(new { ok = false, data = "", message = "" });
            }
        }

    }
}
