using Canturi.Models.BusinessEntity.Admin;
using Canturi.Models.BusinessHelper.Admin;
using Canturi.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Areas.SecureAdmin.Controllers
{
    public class DashBoardController : Controller
    {
        //
        // GET: /Admin/DashBoard/
        [AdminSessionExpire]
        public ActionResult Index()
        {
            DashBoardModels objDashBoardModels = new DashBoardModels();
            ViewBag.Top20NewOrderRequest = GetTop20NewOrderRequest();
            return View(objDashBoardModels);
        }


        private DashBoardModels getNumberOfOrders()
        {
            DashBoardModels model = new DashBoardModels();

            return model;
        }

        [HttpGet]
        public ActionResult NoAccess()
        {
            return View();
        }

        public string GetTop20NewOrderRequest()
        {
            StringBuilder sbOrderRequest = new StringBuilder();
            try
            {
                DiamondOrderHelper objDiamondOrderHelper = new DiamondOrderHelper();
                DiamondDetailModels model = new DiamondDetailModels();
                model.Flag = 1;
                DataSet dsDiamondOrders = objDiamondOrderHelper.GetRequestOrder(model);

                if (dsDiamondOrders != null)
                {
                    if (dsDiamondOrders.Tables[0].Rows.Count != 0)
                    {
                        sbOrderRequest.Append("<table id=\"example2\" class=\"table table-bordered table-hover dataTable\" role=\"grid\" aria-describedby=\"example2_info\">");
                        sbOrderRequest.Append("<thead>");
                        sbOrderRequest.Append("<tr role=\"row\">");
                        sbOrderRequest.Append("<th>Diamond Image</th>");
                        sbOrderRequest.Append("<th>Order #</th>");
                        sbOrderRequest.Append("<th>LOT #</th>");
                        sbOrderRequest.Append("<th>Consultant</th>");
                        sbOrderRequest.Append("<th>Color</th>");
                        sbOrderRequest.Append("</tr>");
                        sbOrderRequest.Append("</thead>");
                        sbOrderRequest.Append("<tbody>");
                    }
                    foreach (DataRow item in dsDiamondOrders.Tables[0].Rows)
                    {
                        sbOrderRequest.Append("<tr role=\"row\" class=\"odd\">");
                        sbOrderRequest.Append("<td>");
                        string strDiamondImg = Url.Content("~/Content/FrontEnd/images/Diamonds/small/no-image.png");
                        if (System.IO.File.Exists(Server.MapPath("~/Content/FrontEnd/images/Diamonds/small/") + item["Shape"].ToString().ToLower() + ".png"))
                        {
                            strDiamondImg = Url.Content("~/Content/FrontEnd/images/Diamonds/small/") + item["Shape"].ToString().ToLower() + ".png";
                        }
                        sbOrderRequest.Append("<img src=\"" + strDiamondImg + "\" align=\"top\" title=\"" + item["Shape"].ToString() + "\" />");

                        sbOrderRequest.Append("</td>");
                        sbOrderRequest.Append("<td>" + item["OrderNumber"].ToString() + "</td>");
                        sbOrderRequest.Append("<td>" + item["LotNumber"].ToString() + "</td>");
                        sbOrderRequest.Append("<td>" + item["ConsultantName"].ToString() + "</td>");
                        sbOrderRequest.Append("<td>" + item["Color"].ToString() + "</td>");
                        sbOrderRequest.Append("</tr>");
                    }
                    if (dsDiamondOrders.Tables[0].Rows.Count != 0)
                    {
                        sbOrderRequest.Append("</tbody>");
                        sbOrderRequest.Append("</table>");
                    }
                }
            }
            catch
            {
                sbOrderRequest.Clear();
            }
            return sbOrderRequest.ToString();
        }

    }
}
