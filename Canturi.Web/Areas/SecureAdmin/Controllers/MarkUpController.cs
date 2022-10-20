using Canturi.Models.BusinessEntity.Admin;
using Canturi.Models.BusinessHelper.Admin;
using Canturi.Models.BusinessHelper.CommonHelper;
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
    [AdminSessionExpire]
    public class MarkUpController : Controller
    {
        //
        // GET: /SecureAdmin/MarkUp/

        public ActionResult Index()
        {
            MarkUpModels model = new MarkUpModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MarkUpModels model)
        {
            string strMsg = "ok";
            string strData = "";
            try
            {

                if (ModelState.IsValid)
                {
                    MarkUpHelper objMarkUpHelper = new MarkUpHelper();
                    model.Flag = 1;
                    if (!String.IsNullOrEmpty(model.MarkUpId))
                    {
                        model.Flag = 2;
                    }

                    if (Convert.ToInt64(model.PriceFrom) >= Convert.ToInt64(model.PriceTo))
                    {
                        strMsg = "NotOk";
                        strData = "Price To should be greater then Price From...!";
                    }
                    else
                    {
                        int result = objMarkUpHelper.AddUpdMarkUp(model);
                        if (result == 0)
                        {
                            if (model.Flag == 1)
                            {
                                strData = "Mark up added successfully...!";
                            }
                            else
                            {
                                strData = "Mark up updated successfully...!";
                            }
                        }
                        else if (result == -1)
                        {
                            strMsg = "NotOk";
                            strData = "Error, While saving data...!";
                        }
                        else if (result == -2)
                        {
                            strMsg = "NotOk";
                            strData = "Error, Input price range already exists...!";
                        }
                        else
                        {
                            strMsg = "NotOk";
                            strData = "Please input required information...!";
                        }
                    }
                }
                else
                {
                    strMsg = "NotOk";
                    strData = "Please input required information...!";
                }

            }
            catch (Exception ex)
            {
                strMsg = "NotOk";
                strData = ex.Message;
                new AppError().LogMe(ex);
            }
            return Json(new { msg = strMsg, data = strData, html = GetMarkUpHtml() });
        }


        public static string GetMarkUpHtml()
        {
            StringBuilder sbMarkUp = new StringBuilder();
            try
            {
                MarkUpHelper objMarkUpHelper = new MarkUpHelper();
                MarkUpModels model = new MarkUpModels();
                model.Flag = 1;
                DataSet dsMarkUp = objMarkUpHelper.GetMarkUp(model);

                if (dsMarkUp != null)
                {
                    if (dsMarkUp.Tables[0].Rows.Count != 0)
                    {
                        sbMarkUp.Append("<table class=\"table table-bordered table-hover\" id=\"activeTable\">");
                        sbMarkUp.Append("<thead>");
                        sbMarkUp.Append("<tr class=\"head\">");
                        sbMarkUp.Append("<th >Price From</th>");
                        sbMarkUp.Append("<th >Price To</th>");
                        sbMarkUp.Append("<th >% Mark up</th>");
                        sbMarkUp.Append("<th >$ Mark up</th>");
                        sbMarkUp.Append("<th >Mark up Tax</th>");
                        sbMarkUp.Append("<th >Action</th>");
                        sbMarkUp.Append("</tr>");
                        sbMarkUp.Append("</thead>");
                        sbMarkUp.Append("<tbody>");
                        foreach (DataRow item in dsMarkUp.Tables[0].Rows)
                        {
                            sbMarkUp.Append("<tr>");
                            sbMarkUp.Append("<td >" + item["PriceFrom"] + "</td>");
                            sbMarkUp.Append("<td >" + item["PriceTo"] + "</td>");
                            sbMarkUp.Append("<td >" + item["MarkUpPercentage"] + "</td>");
                            sbMarkUp.Append("<td >" + item["MarkUpAmount"] + "</td>");
                            sbMarkUp.Append("<td >" + item["MarkUpTax"] + "</td>");
                            sbMarkUp.Append("<td >");
                            sbMarkUp.Append("<a title=\"Edit\" onclick=\"FnMarkupEdit('" + item["MarkUpId"] + "');\" href=\"javascript:void(0)\" class=\"fa fa-fw fa-edit\"></a>");
                            sbMarkUp.Append("&nbsp;| &nbsp;<a title=\"Delete\" onclick=\"FnMarkupDelete('" + item["MarkUpId"] + "');\" href=\"javascript:void(0)\" class=\"fa fa-fw fa-trash-o\"></a>");
                            sbMarkUp.Append("</td>");
                            sbMarkUp.Append("</tr>");
                        }

                        sbMarkUp.Append("</tbody>");
                        sbMarkUp.Append("</table>");

                    }
                }
            }
            catch (Exception ex)
            {
                new AppError().LogMe(ex);
            }
            return sbMarkUp.ToString();
        }


        public JsonResult FnMarkupDelete(string mId)
        {
            string strMsg = "ok";
            string strData = "";
            try
            {

                MarkUpHelper objMarkUpHelper = new MarkUpHelper();
                MarkUpModels model = new MarkUpModels();
                model.Flag = 3;
                model.MarkUpId = mId;

                int result = objMarkUpHelper.AddUpdMarkUp(model);
                if (result == 0)
                {
                    strData = GetMarkUpHtml();
                }
                else
                {
                    strMsg = "NotOk";
                    strData = "Error, Please try again...!";
                }
            }
            catch (Exception ex)
            {
                strMsg = "NotOk";
                strData = ex.Message;
            }
            return Json(new { msg = strMsg, data = strData });
        }

        public JsonResult FnGetMarkupById(string mId)
        {
            string strMsg = "ok";
            string strData = "";
            MarkUpModels model = new MarkUpModels();
            try
            {
                MarkUpHelper objMarkUpHelper = new MarkUpHelper();
                model.Flag = 2;
                model.MarkUpId = mId;
                model = objMarkUpHelper.GetMarkUpById(model);
            }
            catch (Exception ex)
            {
                strMsg = "NotOk";
                strData = ex.Message;
            }
            return Json(new { msg = strMsg, data = strData, model = model });
        }
    }
}
