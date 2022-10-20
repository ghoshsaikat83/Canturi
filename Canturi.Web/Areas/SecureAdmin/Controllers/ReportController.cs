using Canturi.Models.BusinessEntity.Admin;
using Canturi.Models.BusinessHelper.Admin;
using Canturi.Models.BusinessHelper.CommonHelper;
using Canturi.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Areas.SecureAdmin.Controllers
{
    [AdminSessionExpire]
    public class ReportController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }

        #region Order

        public ActionResult Order()
        {
            DiamondModels model = new DiamondModels();
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

        public ActionResult DeleteOrder(long id)
        {
            try
            {
                ReportsHelper objReportsHelper = new ReportsHelper();
                objReportsHelper.RequestOrderId = id.ToString();
                objReportsHelper.CreatedBy = AdminSessionData.AdminUserId;
                objReportsHelper.CreatedFromIp = Request.UserHostAddress;
                objReportsHelper.Status = "0";
                objReportsHelper.AddUpdRequestOrder(2);
                TempData["MessageClass"] = "MsgGreen";
                TempData["message"] = "Deleted successfully...!";
            }
            catch (Exception ex)
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["message"] = ex.Message;
            }
            
            return Redirect(Url.Content("~/secureadmin/report/order/"));
        }

        public ActionResult CancelOrder(long id)
        {
            try
            {
                ReportsHelper objReportsHelper = new ReportsHelper();
                objReportsHelper.RequestOrderId = id.ToString();
                objReportsHelper.CreatedBy = AdminSessionData.AdminUserId;
                objReportsHelper.CreatedFromIp = Request.UserHostAddress;
                objReportsHelper.Status = "2";
                objReportsHelper.AddUpdRequestOrder(2);
                TempData["MessageClass"] = "MsgGreen";
                TempData["message"] = "Canceled successfully...!";
            }
            catch (Exception ex)
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["message"] = ex.Message;
            }

            return Redirect(Url.Content("~/secureadmin/report/order/"));
        }

        public static string FnGetOrders(string consultantId, string month, string year, int status)
        {
            // sbDiamonds
            StringBuilder sbOrders = new StringBuilder();
            try
            {
                DateTime dtFrom = new DateTime();
                DateTime dtTo = new DateTime();
                FnGetDates(month, year, ref dtFrom, ref dtTo);

                ReportsHelper objReportsHelper = new ReportsHelper();
                objReportsHelper.ConsultantId = consultantId;
                objReportsHelper.DateFrom = dtFrom.ToString();
                objReportsHelper.DateTo = dtTo.ToString();
                objReportsHelper.AdminUserId = AdminSessionData.AdminUserId;
                if (String.IsNullOrEmpty(month) && String.IsNullOrEmpty(year))
                {
                    objReportsHelper.DateFrom = "";
                    objReportsHelper.DateTo = "";
                }
                objReportsHelper.Status = status.ToString();
                DataSet dsOrders = objReportsHelper.GetOrders(2);
                if (dsOrders != null)
                {
                    //<table class="table table-bordered table-hover" id="activeTable">
                    if (status != 2)
                    {
                        sbOrders.Append("<table id=\"activeTable\" class=\"table table-bordered table-hover\" >");
                    }
                    else
                    {
                        sbOrders.Append("<table id=\"in-activeTable\" class=\"table table-bordered table-hover\" >");
                    }
                    if (dsOrders.Tables[0].Rows.Count != 0)
                    {
                       
                        
                        sbOrders.Append("<thead>");
                        sbOrders.Append("<tr role=\"row\">");
                        sbOrders.Append("<th>LOT #</th>");
                        sbOrders.Append("<th>Code #</th>");
                        sbOrders.Append("<th>Shape</th>"); 
                        sbOrders.Append("<th>Carat</th>");
                        sbOrders.Append("<th>Colour</th>");
                        sbOrders.Append("<th>Clarity</th>");
                        sbOrders.Append("<th>JCS #</th>");
                        sbOrders.Append("<th>Customer Name</th>");
                        sbOrders.Append("<th>Order #</th>");
                       
                       
                       
                        if (status != 2)
                        {
                            sbOrders.Append("<th>Due Date</th>");
                        }
                        else
                        {
                            sbOrders.Append("<th>Cancelled Date</th>");
                        }
                        
                        //sbOrders.Append("<th>Type</th>");
                        sbOrders.Append("<th>Consultant Name</th>");
                        sbOrders.Append("<th>Date Entered</th>");
                        if (status != 2)
                        {
                            sbOrders.Append("<th>Action</th>");
                            sbOrders.Append("<th>Cancel Order</th>");
                        }
                        
                        sbOrders.Append("</tr>");
                        sbOrders.Append("</thead>");
                        sbOrders.Append("<tbody>");
                    }
                    string[] strShapes = { "CUSHION", "PRINCESS", "RADIANT" };
                    foreach (DataRow item in dsOrders.Tables[0].Rows)
                    {
                        sbOrders.Append("<tr role=\"row\" class=\"odd\">");
                        sbOrders.Append("<td>" + item["LotNumber"].ToString() + "</td>");
                        sbOrders.Append("<td>" + item["SupplierCode"].ToString() + "</td>");
                        string strDiamondImg = ConfigurationManager.AppSettings["WebsiteRootPath"] + "Content/FrontEnd/images/Diamonds/small/no-image.png";

                        string diamondShape = item["Shape"].ToString().ToUpper();
                        if (diamondShape.Contains("CUSHION"))
                        {
                            diamondShape = "CUSHION";
                        }
                        if (diamondShape.Contains("PRINCESS"))
                        {
                            diamondShape = "PRINCESS";
                        }
                        if (diamondShape.Contains("RADIANT"))
                        {
                            diamondShape = "RADIANT";
                        }
                        if (System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/FrontEnd/images/Diamonds/small/") + diamondShape.ToLower() + ".png"))
                        {
                            strDiamondImg = ConfigurationManager.AppSettings["WebsiteRootPath"] + "Content/FrontEnd/images/Diamonds/small/" + diamondShape.ToLower() + ".png";
                        }
                        sbOrders.Append("<td><span><img src=\"" + strDiamondImg + "\" align=\"top\" title=\"" + diamondShape.ToString() + "\"></span></td>");

                        //sbOrders.Append("<td>" + item["Shape"].ToString() + "</td>");
                        sbOrders.Append("<td>" + item["Carat"].ToString() + "</td>");
                        sbOrders.Append("<td>" + item["Color"].ToString() + "</td>");
                        sbOrders.Append("<td>" + item["Clarity"].ToString() + "</td>");
                        sbOrders.Append("<td>" + item["JcsCustomerNumber"].ToString() + "</td>");
                        sbOrders.Append("<td>" + item["CustomerName"].ToString() + "</td>");
                        sbOrders.Append("<td>" + item["OrderNumber"].ToString() + "</td>");
                       
                        
                       
                        if (status != 2)
                        {
                            sbOrders.Append("<td>" + item["DueDate"].ToString() + "</td>");
                        }
                        else
                        {
                            sbOrders.Append("<td>" + item["ModifiedOn"].ToString() + "</td>");
                        }
                        string diamondFrom = item["Type"].ToString();
                        if (diamondFrom.ToUpper() == "V")
                        {
                            diamondFrom = "Venus Jewels";
                        }
                        else if (diamondFrom.ToUpper() == "C")
                        {
                            diamondFrom = "Canturi";
                        }
                        else if (diamondFrom.ToUpper() == "J")
                        {
                            diamondFrom = "JB Bros";
                        }
                        else if (diamondFrom.ToUpper() == "R")
                        {
                            diamondFrom = "Rapnet";
                        }
                        //sbOrders.Append("<td>" + diamondFrom + "</td>");
                        sbOrders.Append("<td>" + item["ConsultantName"].ToString() + "</td>");
                        sbOrders.Append("<td>" + item["CreatedOn"].ToString() + "</td>");
                        if (status != 2)
                        {
                            sbOrders.Append("<td style=\"width:150px;\"><a href=\"" + ConfigurationManager.AppSettings["WebsiteRootPath"] + "secureadmin/report/orderdetails/" + item["RequestOrderId"].ToString() + "\" class=\"fa fa-fw fa-object-ungroup fl\" title=\"Selected diamond details\"></a>");
                            sbOrders.Append("&nbsp;|&nbsp;<a title=\"Delete\" onclick=\"javascript:return ConfirmDelete();\" href=\"" + ConfigurationManager.AppSettings["WebsiteRootPath"] + "secureadmin/report/deleteorder/" + item["RequestOrderId"].ToString() + "\" class=\"fa fa-fw fa-trash-o\"></a>");                            
                            sbOrders.Append("</td>");
                            sbOrders.Append("<td style=\"width:150px;\"><a title=\"Cancel\" onclick=\"javascript:return ConfirmCancel();\" href=\"" + ConfigurationManager.AppSettings["WebsiteRootPath"] + "secureadmin/report/cancelorder/" + item["RequestOrderId"].ToString() + "\" class=\"fa fa-fw fa-remove\"></a>");
                            sbOrders.Append("</td>");
                        }
                        sbOrders.Append("</tr>");
                    }
                    if (dsOrders.Tables[0].Rows.Count == 0)
                    {
                        sbOrders.Append("<tr><td>No Data</td><tr>");
                    }
                    sbOrders.Append("</tbody>");
                    sbOrders.Append("</table>");
                    
                }
            }
            catch (Exception ex)
            {
                sbOrders.Clear();
                sbOrders.Append(ex.Message);
            }
            return sbOrders.ToString();
        }


        public static DataTable FnGetOrdersTable(string consultantId, string month, string year)
        {
            // sbDiamonds
            StringBuilder sbOrders = new StringBuilder();
            try
            {
                DateTime dtFrom = new DateTime();
                DateTime dtTo = new DateTime();
                FnGetDates(month, year, ref dtFrom, ref dtTo);

                ReportsHelper objReportsHelper = new ReportsHelper();
                objReportsHelper.ConsultantId = consultantId;
                objReportsHelper.DateFrom = dtFrom.ToString();
                objReportsHelper.DateTo = dtTo.ToString();
                objReportsHelper.AdminUserId = AdminSessionData.AdminUserId;
                if (String.IsNullOrEmpty(month) && String.IsNullOrEmpty(year))
                {
                    objReportsHelper.DateFrom = "";
                    objReportsHelper.DateTo = "";
                }

                return objReportsHelper.GetOrders(2).Tables[0];


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DiamondModels> ConvertDataTabletoString(DataTable dt)
        {
            List<DiamondModels> listDiamondModels = new List<DiamondModels>();
            foreach (DataRow dr in dt.Rows)
            {
                listDiamondModels.Add(new DiamondModels { Color = dr["Color"].ToString(), Shape = dr["Shape"].ToString(), DiamondId = dr["DiamondId"].ToString() });
            }
            return listDiamondModels;

        }


        public JsonResult FnGetFilterOrders(string consultantId, string month, string year, string status)
        {
            string strMsg = "ok";
            string strData = "";
            try
            {
                if (string.IsNullOrEmpty(status))
                {
                    status = "1";
                }
                strData = FnGetOrders(consultantId, month, year, Convert.ToInt32(status));
            }
            catch (Exception ex)
            {
                strMsg = "notok";
                strData = ex.Message;
            }
            return Json(new { msg = strMsg, data = strData, JsonRequestBehavior.AllowGet });
        }

        public ActionResult FnExportOrdersToCsv(string consultantId, string month, string year)
        {
            try
            {
                ConvertToCSV objCSV = new ConvertToCSV();

                string FilePath = "~/Content/Uploads/Reports/csvData.csv";
                objCSV.fncDeleteFile(FilePath);

                DateTime dtFrom = new DateTime();
                DateTime dtTo = new DateTime();
                FnGetDates(month, year, ref dtFrom, ref dtTo);

                ReportsHelper objReportsHelper = new ReportsHelper();
                objReportsHelper.ConsultantId = consultantId;
                objReportsHelper.DateFrom = dtFrom.ToString();
                objReportsHelper.DateTo = dtTo.ToString();
                objReportsHelper.AdminUserId = AdminSessionData.AdminUserId;
                if (String.IsNullOrEmpty(month) && String.IsNullOrEmpty(year))
                {
                    objReportsHelper.DateFrom = "";
                    objReportsHelper.DateTo = "";
                }
                DataSet dsOrders = objReportsHelper.GetOrders(3);
                DataTable DT = dsOrders.Tables[0];

                if (DT.Rows.Count > 0)
                {
                    objCSV.CreateCSVFile(DT, Server.MapPath("~/Content/Uploads/Reports/") + "csvData.csv");

                    objCSV.FncDownLoadFiles("Orders_" + DateTime.Now.ToShortDateString() + ".csv", FilePath);
                }


            }
            catch (Exception Ex)
            {
                string Err = Ex.Message.ToString();
            }



            return Redirect(Url.Content("~/SecureAdmin/report/RequestAvailability"));
        }


        public ActionResult OrderDetails(string id)
        {
            ViewBag.OrderDetails = new DataTable();
            try
            {
                ReportsHelper objReportsHelper = new ReportsHelper();
                objReportsHelper.RequestId = id;
                objReportsHelper.AdminUserId = AdminSessionData.AdminUserId;
                DataTable dtOrderDetails = objReportsHelper.GetOrders(4).Tables[0];
                ViewBag.OrderDetails = dtOrderDetails;

            }
            catch (Exception ex)
            {
                new AppError().LogMe(ex);
            }
            return View();
        }
        #endregion

        #region RequestAvailability

        public ActionResult RequestAvailability()
        {
            DiamondModels model = new DiamondModels();
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

        public ActionResult RequestOrderDelete(long id)
        {
            try
            {
                ReportsHelper objReportsHelper = new ReportsHelper();
                objReportsHelper.RequestId = id.ToString();
                objReportsHelper.CreatedBy = AdminSessionData.AdminUserId;
                objReportsHelper.CreatedFromIp = Request.UserHostAddress;
                objReportsHelper.Status = "0";
                objReportsHelper.AddUpdRequestAvailability(2);
                TempData["MessageClass"] = "MsgGreen";
                TempData["message"] = "Deleted successfully...!";
            }
            catch (Exception ex)
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["message"] = ex.Message;
            }
            return Redirect(Url.Content("~/secureadmin/report/requestavailability/"));
        }

        public static string FnGetRequestAvailability(string consultantId, string month, string year)
        {
            // sbDiamonds
            StringBuilder sbDiamonds = new StringBuilder();
            try
            {
                DateTime dtFrom = new DateTime();
                DateTime dtTo = new DateTime();
                FnGetDates(month, year, ref dtFrom, ref dtTo);

                ReportsHelper objReportsHelper = new ReportsHelper();
                objReportsHelper.ConsultantId = consultantId;
                objReportsHelper.DateFrom = dtFrom.ToString();
                objReportsHelper.DateTo = dtTo.ToString();
                objReportsHelper.AdminUserId = AdminSessionData.AdminUserId;
                if (String.IsNullOrEmpty(month) && String.IsNullOrEmpty(year))
                {
                    objReportsHelper.DateFrom = "";
                    objReportsHelper.DateTo = "";
                }
                DataSet dsRequestAvailability = objReportsHelper.GetRequestAvailability(1);
                if (dsRequestAvailability != null)
                {
                    if (dsRequestAvailability.Tables[0].Rows.Count != 0)
                    {
                        //<table class="table table-bordered table-hover" id="activeTable">
                        sbDiamonds.Append("<table id=\"activeTable\" class=\"table table-bordered table-hover\" >");
                        sbDiamonds.Append("<thead>");
                        sbDiamonds.Append("<tr role=\"row\">");
                        //sbDiamonds.Append("<th>Order #</th>");
                        //sbDiamonds.Append("<th>JCS #</th>");
                        sbDiamonds.Append("<th>LOT #</th>");
                        sbDiamonds.Append("<th>CODE #</th>");
                        sbDiamonds.Append("<th>Shape</th>");
                        sbDiamonds.Append("<th>Carat</th>");
                        sbDiamonds.Append("<th>Colour</th>");
                        sbDiamonds.Append("<th>Clarity</th>");
                        sbDiamonds.Append("<th>Customer Name</th>");
                        sbDiamonds.Append("<th>Consultant Name</th>");
                        sbDiamonds.Append("<th>Date Entered</th>");
                        //sbDiamonds.Append("<th>Due Date</th>");
                        //sbDiamonds.Append("<th>Type</th>");
                        sbDiamonds.Append("<th>Action</th>");
                        sbDiamonds.Append("</tr>");
                        sbDiamonds.Append("</thead>");
                        sbDiamonds.Append("<tbody>");
                    }
                    foreach (DataRow item in dsRequestAvailability.Tables[0].Rows)
                    {
                        sbDiamonds.Append("<tr role=\"row\" class=\"odd\">");
                        //sbDiamonds.Append("<td>" + item["OrderNumber"].ToString() + "</td>");
                        //sbDiamonds.Append("<td>" + item["JcsCustomerNumber"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["LotNumber"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["SupplierCode"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["Shape"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["Carat"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["Color"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["Clarity"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["CustomerName"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["ConsultantName"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["CreatedOn"].ToString() + "</td>");
                        //sbDiamonds.Append("<td>" + item["DueDate"].ToString() + "</td>");
                        //sbDiamonds.Append("<td>" + item["Type"].ToString() + "</td>");
                        
                        
                        sbDiamonds.Append("<td><a href=\"" + ConfigurationManager.AppSettings["WebsiteRootPath"] + "secureadmin/report/RequestOrderDetails/" + item["RequestId"].ToString() + "\" class=\"fa fa-fw fa-object-ungroup fl\" title=\"Selected diamond details\"></a>");
                        sbDiamonds.Append("&nbsp;| &nbsp;<a title=\"Delete\" onclick=\"javascript:return ConfirmDelete();\" href=\"" + ConfigurationManager.AppSettings["WebsiteRootPath"] + "secureadmin/report/RequestOrderDelete/" + item["RequestId"].ToString() + "\" class=\"fa fa-fw fa-trash-o\"></a>");
                        sbDiamonds.Append("</td>");
                        //sbDiamonds.Append("<td><a href=\"javascript:void(0)\" class=\"fa fa-fw fa-object-ungroup fl\" title=\"Selected diamond details\" onclick=\"FnShowCustomMessage('','')\"></a></td>");
                        sbDiamonds.Append("</tr>");
                    }
                    if (dsRequestAvailability.Tables[0].Rows.Count != 0)
                    {
                        sbDiamonds.Append("</tbody>");
                        sbDiamonds.Append("</table>");
                    }
                }
            }
            catch (Exception ex)
            {
                sbDiamonds.Clear();
                sbDiamonds.Append(ex.Message);
            }
            return sbDiamonds.ToString();
        }

        public JsonResult FnGetFilterRequestAvailability(string consultantId, string month, string year)
        {
            string strMsg = "ok";
            string strData = "";
            try
            {
                strData = FnGetRequestAvailability(consultantId, month, year);
            }
            catch (Exception ex)
            {
                strMsg = "notok";
                strData = ex.Message;
            }
            return Json(new { msg = strMsg, data = strData });
        }

        public ActionResult FnExportRequestAvailabilityToCsv(string consultantId, string month, string year)
        {
            try
            {

                DateTime dtFrom = new DateTime();
                DateTime dtTo = new DateTime();
                FnGetDates(month, year, ref dtFrom, ref dtTo);

                ConvertToCSV objCSV = new ConvertToCSV();

                string FilePath = "~/Content/Uploads/Reports/csvData.csv";
                objCSV.fncDeleteFile(FilePath);

                ReportsHelper objReportsHelper = new ReportsHelper();
                objReportsHelper.ConsultantId = consultantId;
                objReportsHelper.DateFrom = dtFrom.ToString();
                objReportsHelper.DateTo = dtTo.ToString();
                if (String.IsNullOrEmpty(month) && String.IsNullOrEmpty(year))
                {
                    objReportsHelper.DateFrom = "";
                    objReportsHelper.DateTo = "";
                }
                objReportsHelper.AdminUserId = AdminSessionData.AdminUserId;
                DataSet dsRequestAvailability = objReportsHelper.GetRequestAvailability(2);
                DataTable DT = dsRequestAvailability.Tables[0];

                if (DT.Rows.Count > 0)
                {
                    objCSV.CreateCSVFile(DT, Server.MapPath("~/Content/Uploads/Reports/") + "csvData.csv");

                    objCSV.FncDownLoadFiles("RequestAvailability_" + DateTime.Now.ToShortDateString() + ".csv", FilePath);
                }


            }
            catch (Exception Ex)
            {
                string Err = Ex.Message.ToString();
            }



            return Redirect(Url.Content("~/SecureAdmin/report/RequestAvailability"));
        }

        private static void FnGetDates(string month, string year, ref DateTime dtFrom, ref DateTime dtTo)
        {
            if (!String.IsNullOrEmpty(month) && !String.IsNullOrEmpty(year))
            {
                dtFrom = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);
                dtTo = new DateTime(dtFrom.Year, dtFrom.Month, DateTime.DaysInMonth(dtFrom.Year, dtFrom.Month));
            }

            if (!String.IsNullOrEmpty(month) && String.IsNullOrEmpty(year))
            {
                dtFrom = new DateTime(DateTime.Now.Year, Convert.ToInt32(month), 1);
                dtTo = new DateTime(dtFrom.Year, dtFrom.Month, DateTime.DaysInMonth(dtFrom.Year, dtFrom.Month));
            }


            if (String.IsNullOrEmpty(month) && !String.IsNullOrEmpty(year))
            {
                dtFrom = new DateTime(Convert.ToInt32(year), Convert.ToInt32(DateTime.Now.Month), 1);
                dtTo = new DateTime(dtFrom.Year, dtFrom.Month, DateTime.DaysInMonth(dtFrom.Year, dtFrom.Month));
            }
        }


        public ActionResult RequestOrderDetails(string id)
        {
            ViewBag.RequestOrderDetails = new DataTable();
            try
            {
                ReportsHelper objReportsHelper = new ReportsHelper();
                objReportsHelper.RequestId = id;
                objReportsHelper.AdminUserId = AdminSessionData.AdminUserId;
                DataTable dtRequestOrderDetails = objReportsHelper.GetRequestAvailability(3).Tables[0];
                ViewBag.RequestOrderDetails = dtRequestOrderDetails;

            }
            catch (Exception ex)
            {
                new AppError().LogMe(ex);
            }
            return View();
        }
        #endregion

        #region Salon

        public ActionResult Salon()
        {
            return View();
        }

        public static string FnGetSalon(string clientName, string phone, string consultantName)
        {
            //sbDiamonds
            StringBuilder sbDiamonds = new StringBuilder();
            try
            {
                SaveSelectonHelper objSaveSelectonHelper = new SaveSelectonHelper();
                objSaveSelectonHelper.ClientName = clientName;
                objSaveSelectonHelper.Phone = phone;
                objSaveSelectonHelper.ConsultantName = consultantName;
                objSaveSelectonHelper.AdminUserId = AdminSessionData.AdminUserId;
                DataSet dsSaveSelection = objSaveSelectonHelper.GetSaveSelecton(1);
                if (dsSaveSelection != null)
                {
                    if (dsSaveSelection.Tables[0].Rows.Count != 0)
                    {
                        //<table class="table table-bordered table-hover" id="activeTable">
                        sbDiamonds.Append("<table id=\"activeTable\" class=\"table table-bordered table-hover\" >");
                        sbDiamonds.Append("<thead>");
                        sbDiamonds.Append("<tr role=\"row\">");
                        sbDiamonds.Append("<th>Client Name</th>");
                        sbDiamonds.Append("<th>Phone</th>");
                        sbDiamonds.Append("<th>Date</th>");
                        sbDiamonds.Append("<th>Currency</th>");
                        sbDiamonds.Append("<th>Consultant Name</th>");
                        sbDiamonds.Append("<th>Date Entered</th>");
                        sbDiamonds.Append("<th>Action</th>");
                        sbDiamonds.Append("</tr>");
                        sbDiamonds.Append("</thead>");
                        sbDiamonds.Append("<tbody>");
                    }
                    foreach (DataRow item in dsSaveSelection.Tables[0].Rows)
                    {
                        // ConfigurationManager.AppSettings["WebsiteRootPath"]
                        sbDiamonds.Append("<tr role=\"row\" class=\"odd\">");
                        sbDiamonds.Append("<td>" + item["ClientName"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["Phone"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["Date"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["Currency"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["ConsultantName"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + String.Format("{0:ddd, MMM d, yyyy}", Convert.ToDateTime(item["CreatedOn"].ToString())) + "</td>");
                        sbDiamonds.Append("<td><a href=\"javascript:void(0)\" class=\"fa fa-fw fa-object-ungroup fl\" title=\"Selected diamond details\" onclick=\"FnShowCustomMessage('','')\"></a>");
                        //sbDiamonds.Append("&nbsp;| &nbsp;<a title=\"Delete\" onclick=\"javascript:return ConfirmDelete();\" href=\"" + ConfigurationManager.AppSettings["WebsiteRootPath"] + "secureadmin/report/DeleteSalon/" + item["RequestId"].ToString() + "\" class=\"fa fa-fw fa-trash-o\"></a>");
                        sbDiamonds.Append("</td>");
                        sbDiamonds.Append("</tr>");
                    }
                    if (dsSaveSelection.Tables[0].Rows.Count != 0)
                    {
                        sbDiamonds.Append("</tbody>");
                        sbDiamonds.Append("</table>");
                    }
                }
            }
            catch (Exception ex)
            {
                sbDiamonds.Clear();
                sbDiamonds.Append(ex.Message);
            }
            return sbDiamonds.ToString();
        }

        public JsonResult FnGetFilterSalon(string clientName, string phone, string consultantName)
        {
            string strMsg = "ok";
            string strData = "";
            try
            {
                strData = FnGetSalon(clientName, phone, consultantName);
            }
            catch (Exception ex)
            {
                strMsg = "notok";
                strData = ex.Message;
            }
            return Json(new { msg = strMsg, data = strData });
        }

        public ActionResult FnExportSalonToCsv(string clientName, string phone, string consultantName)
        {
            try
            {
                ConvertToCSV objCSV = new ConvertToCSV();

                string FilePath = "~/Content/Uploads/Reports/csvData.csv";
                objCSV.fncDeleteFile(FilePath);

                SaveSelectonHelper objSaveSelectonHelper = new SaveSelectonHelper();
                objSaveSelectonHelper.ClientName = clientName;
                objSaveSelectonHelper.Phone = phone;
                objSaveSelectonHelper.ConsultantName = consultantName;
                objSaveSelectonHelper.AdminUserId = AdminSessionData.AdminUserId;
                DataSet dsSaveSelection = objSaveSelectonHelper.GetSaveSelecton(2);
                DataTable DT = dsSaveSelection.Tables[0];

                if (DT.Rows.Count > 0)
                {
                    objCSV.CreateCSVFile(DT, Server.MapPath("~/Content/Uploads/Reports/") + "csvData.csv");

                    objCSV.FncDownLoadFiles("Salon_" + DateTime.Now.ToShortDateString() + ".csv", FilePath);
                }


            }
            catch (Exception Ex)
            {
                string Err = Ex.Message.ToString();
            }



            return Redirect(Url.Content("~/SecureAdmin/report/Salon"));
        }
        #endregion

        #region SaveSelection

        public ActionResult SaveSelection()
        {
            DiamondModels model = new DiamondModels();
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

        public ActionResult SaveSelectionDelete(long id)
        {
            try
            {
                SaveSelectonHelper objSaveSelectonHelper = new SaveSelectonHelper();
                objSaveSelectonHelper.SaveSelectionId = id.ToString();
                objSaveSelectonHelper.CreatedBy = AdminSessionData.AdminUserId;
                objSaveSelectonHelper.CreatedFromIp = Request.UserHostAddress;
                objSaveSelectonHelper.Status = "0";
                objSaveSelectonHelper.AddUpdSaveSelecton(2);
                TempData["MessageClass"] = "MsgGreen";
                TempData["message"] = "Deleted successfully...!";
            }
            catch (Exception ex)
            {
                TempData["MessageClass"] = "MsgRed";
                TempData["message"] = ex.Message;
            }
            return Redirect(Url.Content("~/secureadmin/report/saveselection/"));
        }

        public static string FnGetSaveSelection(string clientName, string phone, string consultantName)
        {
            // sbDiamonds
            StringBuilder sbDiamonds = new StringBuilder();
            try
            {
                SaveSelectonHelper objSaveSelectonHelper = new SaveSelectonHelper();
                objSaveSelectonHelper.ClientName = clientName;
                objSaveSelectonHelper.Phone = phone;
                objSaveSelectonHelper.ConsultantName = consultantName;
                objSaveSelectonHelper.AdminUserId = AdminSessionData.AdminUserId;
                DataSet dsSaveSelection = objSaveSelectonHelper.GetSaveSelecton(1);
                if (dsSaveSelection != null)
                {
                    if (dsSaveSelection.Tables[0].Rows.Count != 0)
                    {
                        //<table class="table table-bordered table-hover" id="activeTable">
                        sbDiamonds.Append("<table id=\"activeTable\" class=\"table table-bordered table-hover\" >");
                        sbDiamonds.Append("<thead>");
                        sbDiamonds.Append("<tr role=\"row\">");
                        sbDiamonds.Append("<th>Client Name</th>");
                        sbDiamonds.Append("<th>Phone</th>");
                        sbDiamonds.Append("<th>Date</th>");
                        sbDiamonds.Append("<th>Currency</th>");
                        sbDiamonds.Append("<th>Consultant Name</th>");
                        sbDiamonds.Append("<th>Date Entered</th>");
                        sbDiamonds.Append("<th>Action</th>");
                        sbDiamonds.Append("</tr>");
                        sbDiamonds.Append("</thead>");
                        sbDiamonds.Append("<tbody>");
                    }
                    foreach (DataRow item in dsSaveSelection.Tables[0].Rows)
                    {
                        // ConfigurationManager.AppSettings["WebsiteRootPath"]
                        sbDiamonds.Append("<tr role=\"row\" class=\"odd\">");
                        sbDiamonds.Append("<td>" + item["ClientName"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["Phone"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["Date"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["Currency"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["ConsultantName"].ToString() + "</td>");
                        sbDiamonds.Append("<td>" + item["CreatedOn"].ToString() + "</td>");
                        sbDiamonds.Append("<td><a href=\"javascript:void(0)\" class=\"fa fa-fw fa-object-ungroup fl\" title=\"Selected diamond details\" onclick=\"FnShowCustomMessage('','')\"></a></td>");
                        sbDiamonds.Append("</tr>");
                    }
                    if (dsSaveSelection.Tables[0].Rows.Count != 0)
                    {
                        sbDiamonds.Append("</tbody>");
                        sbDiamonds.Append("</table>");
                    }
                }
            }
            catch (Exception ex)
            {
                sbDiamonds.Clear();
                sbDiamonds.Append(ex.Message);
            }
            return sbDiamonds.ToString();
        }

        public JsonResult FnGetFilterSaveSelection(string clientName, string phone, string consultantName)
        {
            string strMsg = "ok";
            string strData = "";
            List<Canturi.Models.BusinessEntity.FrontEnd.SaveSelectonModels> svSelection = new List<Canturi.Models.BusinessEntity.FrontEnd.SaveSelectonModels>();
            try
            {
                //strData = FnGetSaveSelection(clientName, phone, consultantName);
                svSelection = GetSaveSelectionList(clientName, phone, consultantName);
            }
            catch (Exception ex)
            {
                strMsg = "notok";
                strData = ex.Message;
            }
            return Json(svSelection, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FnGetSelectedDiamonds(string id)
        {
            List<DiamondModels> listDiamondModels = new List<DiamondModels>();
            string strMsg = "ok";
            string strData = "";
            try
            {
                //strData = FnGetSaveSelection(clientName, phone, consultantName);
                SaveSelectonHelper objSaveSelectonHelper = new SaveSelectonHelper();
                objSaveSelectonHelper.SaveSelectionId = id;
                objSaveSelectonHelper.AdminUserId = AdminSessionData.AdminUserId;
                DataSet dsSaveSelection = objSaveSelectonHelper.GetSaveSelecton(3);
                foreach (DataRow item in dsSaveSelection.Tables[0].Rows)
                {
                    string strUrl = "1";
                    strUrl = item["Supplier"].ToString();
                    string supplier = item["Supplier"].ToString();
                    if (String.IsNullOrEmpty( supplier))
                    {
                        if (item["Type"].ToString().ToLower() == "c")
                        {
                            supplier = "Canturi";
                            strUrl = "4";
                        }
                        else if (item["Type"].ToString().ToLower() == "r")
                        {
                            supplier = "Rapnet";
                            strUrl = "1";
                            //string strSupplier = item["Supplier"].ToString().ToUpper();

                            if (supplier.ToUpper() == "N.E.R. JEWELRY INC.")
                            {
                                strUrl = "12";
                            }
                            else if (supplier.ToUpper() == "OFER MIZRAHI DIAMONDS, INC.")
                            {
                                strUrl = "11";
                            }
                        }
                        else if (item["Type"].ToString().ToLower() == "j")
                        {
                            supplier = "JBBros";
                            strUrl = "2";
                        }
                        else if (item["Type"].ToString().ToLower() == "v")
                        {
                            supplier = "Venus Jewel";
                            strUrl = "3";
                        }
                        else if (item["Type"].ToString().ToLower() == "cd")
                        {
                            supplier = "CDINESH";
                            strUrl = "8";
                        }
                        else if (item["Type"].ToString().ToLower() == "f")
                        {
                            supplier = "FineStar";
                            strUrl = "7";
                        }
                        else if (item["Type"].ToString().ToLower() == "h")
                        {
                            supplier = "HariKrishna";
                            strUrl = "6";
                        }
                        else if (item["Type"].ToString().ToLower() == "Kg")
                        {
                            supplier = "KapuGems";
                            strUrl = "9";
                        }
                        else if (item["Type"].ToString().ToLower() == "p")
                        {
                            supplier = "Panache";
                            strUrl = "5";
                        }
                        else if (item["Type"].ToString().ToLower() == "yd")
                        {
                            supplier = "YDVASH";
                            strUrl = "5";
                        }
                        else if (item["Type"].ToString().ToLower() == "sd")
                        {
                            supplier = "Sunrise";
                            strUrl = "10";
                        }
                    }

                    DiamondModels objDiamondModels = new DiamondModels
                    {

                        DiamondId = item["DiamondId"].ToString(),
                        Shape = item["Shape"].ToString(),
                        Carat = Convert.ToDouble(item["Carat"].ToString()),
                        Color = item["Color"].ToString(),
                        Clartiy = item["Clarity"].ToString(),
                        Cut = item["Cut"].ToString(),
                        Polish = item["Polish"].ToString(),
                        Symmetry = item["Symmetry"].ToString(),
                        Flourescence = item["Flourescence"].ToString(),
                        Price = Convert.ToDouble(item["Price"].ToString()),
                        LotNumber = item["LotNumber"].ToString(),
                        Measurements = item["Measurements"].ToString().Replace("*", "x"),
                        Ratio = item["Ratio"].ToString(),
                        DiamondCertificate = item["CertificateNumber"].ToString(),
                        Supplier = strUrl//supplier
                    };

                    if (objDiamondModels.Shape.ToString().ToUpper().Contains("CUSHION"))
                    {
                        objDiamondModels.Shape = "CUSHION";
                    }
                   
                    listDiamondModels.Add(objDiamondModels);
                }
            }
            catch (Exception ex)
            {
                strMsg = "notok";
                strData = ex.Message;
            }
            return Json(listDiamondModels, JsonRequestBehavior.AllowGet);
        }

        public List<Canturi.Models.BusinessEntity.FrontEnd.SaveSelectonModels> GetSaveSelectionList(string clientName, string phone, string consultantName)
        {
            SaveSelectonHelper objSaveSelectonHelper = new SaveSelectonHelper();
            objSaveSelectonHelper.ClientName = clientName;
            objSaveSelectonHelper.Phone = phone;
            objSaveSelectonHelper.ConsultantName = consultantName;
            objSaveSelectonHelper.AdminUserId = AdminSessionData.AdminUserId;
            DataSet dsSaveSelection = objSaveSelectonHelper.GetSaveSelecton(1);
            List<Canturi.Models.BusinessEntity.FrontEnd.SaveSelectonModels> listSaveSelectonModels = new List<Canturi.Models.BusinessEntity.FrontEnd.SaveSelectonModels>();
            foreach (DataRow item in dsSaveSelection.Tables[0].Rows)
            {
                listSaveSelectonModels.Add(new Canturi.Models.BusinessEntity.FrontEnd.SaveSelectonModels
                {
                    ClientName = item["ClientName"].ToString(),
                    Phone = item["Phone"].ToString(),
                    Date = item["Date"].ToString(),
                    Currency = item["Currency"].ToString(),
                    ConsultantName = item["ConsultantName"].ToString(),
                    CreatedOn = item["CreatedOn"].ToString(),
                    SaveSelectionId = item["SaveSelectionId"].ToString(),
                    ConsultantId = item["ConsultantId"].ToString()

                });
            }
            return listSaveSelectonModels;
        }

        public ActionResult FnExportSaveSelectionToCsv(string clientName, string phone, string consultantName)
        {
            try
            {
                ConvertToCSV objCSV = new ConvertToCSV();

                string FilePath = "~/Content/Uploads/Reports/csvData.csv";
                objCSV.fncDeleteFile(FilePath);

                SaveSelectonHelper objSaveSelectonHelper = new SaveSelectonHelper();
                objSaveSelectonHelper.ClientName = clientName;
                objSaveSelectonHelper.Phone = phone;
                objSaveSelectonHelper.ConsultantName = consultantName;
                objSaveSelectonHelper.AdminUserId = AdminSessionData.AdminUserId;
                DataSet dsSaveSelection = objSaveSelectonHelper.GetSaveSelecton(4);
                DataTable DT = dsSaveSelection.Tables[0];

                if (DT.Rows.Count > 0)
                {
                    objCSV.CreateCSVFile(DT, Server.MapPath("~/Content/Uploads/Reports/") + "csvData.csv");

                    objCSV.FncDownLoadFiles("SaveSelection_" + DateTime.Now.ToShortDateString() + ".csv", FilePath);
                }


            }
            catch (Exception Ex)
            {
                string Err = Ex.Message.ToString();
            }



            return Redirect(Url.Content("~/SecureAdmin/report/SaveSelection"));
        }

        #endregion
    }
}
