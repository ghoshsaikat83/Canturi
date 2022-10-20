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
    public class ConsultantController : Controller
    {
        //
        // GET: /SecureAdmin/Consultant/

        List<ConsultantModels> ActiveConsultantList = new List<ConsultantModels>();
        List<ConsultantModels> InActiveConsultantList = new List<ConsultantModels>();

        public ActionResult Index(string msg)
        {
            ConsultantModels model = new ConsultantModels();

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
                model.Message = "Consultant added/updated successfully.";
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


            model.Flag = 2;
            model.Status = 1;
            ConsultantHelper objHelper = new ConsultantHelper();



            ActiveConsultantList = objHelper.GetConsultants(model);
            var ActiveConsultant = GetDataUsingLINQ(activeGridParameters.Sort,       //order by column
                                        activeGridParameters.SortDirection,   //order by direction
                                        activeGridParameters.Page ?? 1,       //returned page
                                        pageSize, ActiveConsultantList);               //displayed rows per page


            model.Status = 0;
            var InActiveGridParameters = ASPRazorWebGridSample.UI.GridParameters.GetGridParameters();
            InActiveConsultantList = objHelper.GetConsultants(model);
            var InActiveConsultant = GetDataUsingLINQ(InActiveGridParameters.Sort,       //order by column
                                        InActiveGridParameters.SortDirection,   //order by direction
                                        InActiveGridParameters.Page ?? 1,       //returned page
                                        pageSize, InActiveConsultantList);               //displayed rows per page


            //set record count for use in view

            ViewBag.ActiveGridRecordCount = ActiveConsultantList.Count;
            ViewBag.InActiveGridRecordCount = InActiveConsultantList.Count;

            return View(Tuple.Create(ActiveConsultant, InActiveConsultant, model));
        }

        //get data from datasource using LINQ (sample data access layer)
        private IEnumerable<ConsultantModels> GetDataUsingLINQ(string sort, string sortDir, int page, int numRows, List<ConsultantModels> RolesList)
        {

            //List<RoleModels> list = RolesList.AsQueryable().Skip((page - 1) * numRows).Take(numRows).ToList();
            List<ConsultantModels> list = RolesList.AsQueryable().ToList();


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

        public ActionResult Add()
        {
            ConsultantModels model = new ConsultantModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ConsultantModels model)
        {
            ConsultantHelper objConsultantHelper = new ConsultantHelper();
            model.Flag = 1;

            int result = objConsultantHelper.AddUpdConsultants(model);

            if (result == -2)
            {

                TempData["MessageClass"] = "MsgRed";
                TempData["CommonMessage"] = CommonData.GetMessage(StringResource.GetStringResourceFile("admin.Consultant.name.already.exists"), 0);
                //TempData["message"] = "Record Updated successfully!";  
            }
            else if (result == -1)
            {

                TempData["MessageClass"] = "MsgRed";
                TempData["CommonMessage"] = CommonData.GetMessage(StringResource.GetStringResourceFile("admin.Consultant.add.error"), 0);
                //TempData["message"] = "Record Updated successfully!"; 
            }
            else
            {
                TempData["MessageClass"] = "MsgGreen";

                TempData["CommonMessage"] = CommonData.GetMessage(StringResource.GetStringResourceFile("admin.Consultant.add"), 1);// "Record Updated successfully!";
                return Redirect(Url.Content("~/SecureAdmin/consultant"));
            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            ConsultantModels model = new ConsultantModels();
            ConsultantHelper objConsultantHelper = new ConsultantHelper();
            model.Flag = 1;
            model.ConsultantId = Convert.ToInt64(id);
            model = objConsultantHelper.GetConsultantById(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ConsultantModels model)
        {
            ConsultantHelper objConsultantHelper = new ConsultantHelper();
            model.Flag = 2;
            int result = objConsultantHelper.AddUpdConsultants(model);
            if (result == -2)
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["CommonMessage"] = CommonData.GetMessage(StringResource.GetStringResourceFile("admin.Consultant.name.already.exists"), 0);
            }
            else if (result == -1)
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["CommonMessage"] = CommonData.GetMessage(StringResource.GetStringResourceFile("admin.Consultant.update.error"), 0);
            }
            else
            {
                TempData["MessageClass"] = "MsgGreen";
                TempData["CommonMessage"] = CommonData.GetMessage(StringResource.GetStringResourceFile("admin.Consultant.update"), 1);// "Record Updated successfully!";
                return Redirect(Url.Content("~/SecureAdmin/consultant"));
            }
            model.Flag = 1;
            model = objConsultantHelper.GetConsultantById(model);
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            try
            {

                ConsultantModels model = new ConsultantModels();
                model.ConsultantId = Convert.ToInt64(id);
                model.Flag = 4;
                model.Status = 3;
                ConsultantHelper objConsultantHelper = new ConsultantHelper();
                objConsultantHelper.AddUpdConsultants(model);
                TempData["MessageClass"] = "MsgGreen";
                TempData["CommonMessage"] = CommonData.GetMessage(StringResource.GetStringResourceFile("admin.Consultant.Delete"), 1);// "Record Updated successfully!";
            }
            catch (Exception ex)
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
            }
            return Redirect(Url.Content("~/SecureAdmin/consultant"));
        }

        public ActionResult InActivate(string id)
        {
            try
            {
                ConsultantModels model = new ConsultantModels();
                model.ConsultantId = Convert.ToInt64(id);
                model.Flag = 3;
                model.Status = 0;
                ConsultantHelper objConsultantHelper = new ConsultantHelper();
                objConsultantHelper.AddUpdConsultants(model);
                TempData["MessageClass"] = "MsgGreen";
                TempData["CommonMessage"] = CommonData.GetMessage(StringResource.GetStringResourceFile("admin.Consultant.InActivate"), 1);
            }
            catch (Exception ex)
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
            }
            return Redirect(Url.Content("~/SecureAdmin/consultant"));
        }

        public ActionResult Activate(string id)
        {
            try
            {
                ConsultantModels model = new ConsultantModels();
                model.ConsultantId = Convert.ToInt64(id);
                model.Flag = 3;
                model.Status = 1;
                ConsultantHelper objConsultantHelper = new ConsultantHelper();
                objConsultantHelper.AddUpdConsultants(model);
                TempData["MessageClass"] = "MsgGreen";
                TempData["CommonMessage"] = CommonData.GetMessage(StringResource.GetStringResourceFile("admin.Consultant.Activate"), 1);
            }
            catch (Exception ex)
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
            }
            return Redirect(Url.Content("~/SecureAdmin/consultant"));
        }
    }
}
