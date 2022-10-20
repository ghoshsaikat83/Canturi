using Canturi.Models.BusinessEntity.Admin;
using Canturi.Models.BusinessHelper.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using Canturi.Web.App_Start;
using Canturi.Models.BusinessHelper.CommonHelper;

namespace Canturi.Web.Areas.SecureAdmin.Controllers
{
     [AdminSessionExpire]
    public class UserController : Controller
    {
        //
        // GET: /Admin/User/

        public ActionResult Index()
        {
            return View();
        }


        List<UserModels> ActiveUserList = new List<UserModels>();
        List<UserModels> InActiveUserList = new List<UserModels>();

        public ActionResult List()
        {
            UserModels model = new UserModels();
            return BindUserList(model);
        }

        [HttpPost]
        public ActionResult List([Bind(Prefix = "Item3")] UserModels model)
        {
            return BindUserList(model);
        }

        private ActionResult BindUserList(UserModels model)
        {
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

            //get grid parameters from URL/POST (if any)
            var activeGridParameters = ASPRazorWebGridSample.UI.GridParameters.GetGridParameters();
            int pageSize = 10;   //displayed rows per page


            model.Flag = 1;
            model.Status = 1;
            AdminUserHelper objAdminUserHelper = new AdminUserHelper();



            ActiveUserList = objAdminUserHelper.GetAdminUserByStatus(model);
            var ActiveUser = GetUserDataUsingLINQ(activeGridParameters.Sort,       //order by column
                                        activeGridParameters.SortDirection,   //order by direction
                                        activeGridParameters.Page ?? 1,       //returned page
                                        pageSize, ActiveUserList);               //displayed rows per page


            model.Status = 0;
            var InActiveGridParameters = ASPRazorWebGridSample.UI.GridParameters.GetGridParameters();
            InActiveUserList = objAdminUserHelper.GetAdminUserByStatus(model);
            var InActiveUser = GetUserDataUsingLINQ(InActiveGridParameters.Sort,       //order by column
                                        InActiveGridParameters.SortDirection,   //order by direction
                                        InActiveGridParameters.Page ?? 1,       //returned page
                                        pageSize, InActiveUserList);               //displayed rows per page


            //set record count for use in view


            ViewBag.ActiveGridRecordCount = ActiveUserList.Count;
            ViewBag.InActiveGridRecordCount = InActiveUserList.Count;

            return View(Tuple.Create(ActiveUser, InActiveUser, model));
        }


        //get data from datasource using LINQ (sample data access layer)
        private IEnumerable<UserModels> GetUserDataUsingLINQ(string sort, string sortDir, int page, int numRows, List<UserModels> UsersList)
        {
            //List<UserModels> list = UsersList.AsQueryable().Skip((page - 1) * numRows).Take(numRows).ToList();
            List<UserModels> list = UsersList.AsQueryable().ToList();

            int tempSerialStart = 0;// (page - 1) * numRows;

            for (int i = 0; i < list.Count; i++)
            {
                list[i].SerialNumber = tempSerialStart + i + 1;
            }

            if (!string.IsNullOrEmpty(sort))
                return list.AsQueryable().OrderBy(sort + " " + sortDir).ToList();
            else
            {
                return list.AsQueryable().ToList();
            }
        }



        public ActionResult Add()
        {

            UserModels objUserModel = new UserModels();

            RoleModels objRoleModel = new RoleModels();
            objRoleModel.Flag = 3;
            objRoleModel.Status = 1;
            RoleHelper objRoleHelper = new RoleHelper();
            objUserModel.ListRole = objRoleHelper.GetRoleByStatus(objRoleModel);
            objUserModel.ListStatus = GetStatus();

            if (TempData["message"] != null)
            {
                objUserModel.MessageClass = (string)TempData["MessageClass"];
                objUserModel.IsShowMessage = 1;
                objUserModel.Message = (string)TempData["message"];
            }
            else
            {
                objUserModel.MessageClass = "";
                objUserModel.IsShowMessage = 0;
                objUserModel.Message = "";
            }
            objUserModel.Status = 1;
            return View(objUserModel);
        }

        public List<SelectListItem> GetStatus()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem { Text = "Active", Value = "1" });
            itemList.Add(new SelectListItem { Text = "In-Active", Value = "0" });
            return itemList;
        }

        [HttpPost]
        public ActionResult Add(UserModels model)
        {

            if (ModelState.IsValid)
            {
                model.IsShowMessage = 0;
                model.Flag = 1;
                model.CreatedBy = AdminSessionData.AdminUserId;
                model.CreatedFromIp = Request.UserHostAddress;

                AdminUserHelper objAdminUserHelper = new AdminUserHelper();
                int result = objAdminUserHelper.PerformActionOnUser(model);
                if (result > 0)
                {
                    TempData["MessageClass"] = "MsgGreen";
                    TempData["message"] = StringResource.GetStringResourceFile("admin.AdminInsert");
                    return RedirectToAction("List", "User");
                }
                else if (result == -5)
                {
                    model.MessageClass = "MsgRed";
                    model.IsShowMessage = 1;
                    model.Message = StringResource.GetStringResourceFile("admin.AdminUserNamePasswordSame");
                }
                else if (result == -4)
                {
                    model.MessageClass = "MsgRed";
                    model.IsShowMessage = 1;
                    model.Message = StringResource.GetStringResourceFile("admin.AdminEmailExist");
                }
                else if (result == -3)
                {
                    model.MessageClass = "MsgRed";
                    model.IsShowMessage = 1;
                    model.Message = StringResource.GetStringResourceFile("admin.AdminUserNameExist");
                }
                else
                {
                    model.MessageClass = "MsgRed";
                    model.IsShowMessage = 1;
                    model.Message = StringResource.GetStringResourceFile("admin.AdminError");
                }
            }

            RoleModels objRoleModel = new RoleModels();
            objRoleModel.Flag = 3;
            objRoleModel.Status = 1;
            RoleHelper objRoleHelper = new RoleHelper();
            model.ListRole = objRoleHelper.GetRoleByStatus(objRoleModel);
            model.ListStatus = GetStatus();

            return View(model);

        }




        [HttpGet]
        public ActionResult Edit(int? userId, UserModels objUserModel)
        {


            RoleModels objRoleModel = new RoleModels();
            objRoleModel.Flag = 3;
            objRoleModel.Status = 1;
            RoleHelper objRoleHelper = new RoleHelper();
            objUserModel.ListRole = objRoleHelper.GetRoleByStatus(objRoleModel);
            objUserModel.ListStatus = GetStatus();


            objUserModel.Flag = 2;
            objUserModel.UserId = (int)userId;
            AdminUserHelper objHelper = new AdminUserHelper();
            objUserModel = objHelper.GetUserById(objUserModel);

            // for clear of the validation class
            ModelState.Clear();

            return View(objUserModel);
        }

        [HttpPost]
        public ActionResult Edit(UserModels model)
        {

            if (ModelState.IsValid)
            {
                model.IsShowMessage = 0;
                model.Flag = 2;
                model.CreatedBy = AdminSessionData.AdminUserId;
                model.CreatedFromIp = Request.UserHostAddress;

                AdminUserHelper objHelper = new AdminUserHelper();
                int result = objHelper.PerformActionOnUser(model);
                if (result == 0)
                {
                    TempData["MessageClass"] = "MsgGreen";
                    TempData["message"] = StringResource.GetStringResourceFile("admin.AdminUserUpdate");
                    return RedirectToAction("List", "User");
                }
                else if (result == -5)
                {
                    TempData["MessageClass"] = "MsgRed";
                    TempData["message"] = StringResource.GetStringResourceFile("admin.AdminUserNamePasswordSame");
                }
                else if (result == -4)
                {
                    TempData["MessageClass"] = "MsgRed";
                    TempData["message"] = StringResource.GetStringResourceFile("admin.AdminEmailExist");
                }
                else if (result == -3)
                {
                    TempData["MessageClass"] = "MsgRed";
                    TempData["message"] = StringResource.GetStringResourceFile("admin.AdminUserNameExist");
                }
                else
                {
                    TempData["MessageClass"] = "MsgRed";
                    TempData["message"] = StringResource.GetStringResourceFile("admin.AdminError");
                }


                return RedirectToAction("List", "User");
            }
            RoleModels objRoleModel = new RoleModels();
            objRoleModel.Flag = 3;
            objRoleModel.Status = 1;
            RoleHelper objRoleHelper = new RoleHelper();
            model.ListRole = objRoleHelper.GetRoleByStatus(objRoleModel);
            model.ListStatus = GetStatus();
            return View(model);

        }


        [HttpGet]
        public ActionResult Delete(int? userId, UserModels model)
        {
            model.Flag = 3;
            model.UserId = (int)userId;
            AdminUserHelper objHelper = new AdminUserHelper();
            int result = objHelper.PerformActionOnUser(model);
            if (result == 0)
            {
                TempData["MessageClass"] = "MsgGreen";
                TempData["message"] = StringResource.GetStringResourceFile("admin.AdminDelete");

            }
            else
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["message"] = StringResource.GetStringResourceFile("admin.AdminNotDelete");
            }
            return RedirectToAction("List", "User");

        }

        [HttpGet]
        public ActionResult InActivate(int? userId, UserModels model)
        {
            model.Flag = 4;
            model.UserId = (int)userId;
            model.Status = 0;
            AdminUserHelper objHelper = new AdminUserHelper();
            int result = objHelper.PerformActionOnUser(model);
            if (result == 0)
            {
                TempData["MessageClass"] = "MsgGreen";
                TempData["message"] = StringResource.GetStringResourceFile("admin.AdminInactivate");

            }
            else
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["message"] = StringResource.GetStringResourceFile("admin.AdminNotInactivate");
            }
            return RedirectToAction("List", "User");

        }

        [HttpGet]
        public ActionResult Activate(int? userId, UserModels model)
        {
            model.Flag = 4;
            model.UserId = (int)userId;
            model.Status = 1;
            AdminUserHelper objHelper = new AdminUserHelper();
            int result = objHelper.PerformActionOnUser(model);
            if (result == 0)
            {
                TempData["MessageClass"] = "MsgGreen";
                TempData["message"] = StringResource.GetStringResourceFile("admin.AdminActivate");
            }
            else
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["message"] = StringResource.GetStringResourceFile("admin.AdminNotActivate");
            }
            return RedirectToAction("List", "User", new { @At = "InActiveUser" });

        }


    }
}
