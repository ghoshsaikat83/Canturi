using Canturi.Models.BusinessEntity.FrontEnd;
using Canturi.Models.BusinessHelper.CommonHelper;
using Canturi.Models.BusinessHelper.FrontEnd;
using Canturi.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Controllers
{
    [FrontEndSessionExpire]
    public class DiamondDetailsController : Controller
    {
        //
        // GET: /Diamond/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(string type, string id)
        {
            DiamondDetailModels model = new DiamondDetailModels();
            string strQuery = string.Empty;
            try
            {
                int diamondId = Convert.ToInt32(id);
            }
            catch (Exception ex)
            {
                //invalid request
            }
            if (!String.IsNullOrEmpty(type))
            {
                if (type.ToUpper().Contains("IN SALON"))
                {
                    type = "4";
                }
                if (type.ToLower() == "1" || type.ToLower() == "11" || type.ToLower() == "12")
                {
                    strQuery = "SELECT *,[dbo].[Udf_IsDiamondOrderd] (tblRapnetDiamond.LotNumber,'R') AS IsDiamondOrderd,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblRapnetDiamond WHERE DiamondID=" + id;
                }

                else if (type.ToLower() == "2")
                {
                    strQuery = "SELECT *,[dbo].[Udf_IsDiamondOrderd] (tblJBBrosDiamond.LotNumber,'J') AS IsDiamondOrderd,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblJBBrosDiamond WHERE DiamondID=" + id;
                }
                else if (type.ToLower() == "3")
                {
                    strQuery = "SELECT *,[dbo].[Udf_IsDiamondOrderd] (tblVenusJewelsDiamond.LotNumber,'V') AS IsDiamondOrderd,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblVenusJewelsDiamond WHERE DiamondID=" + id;
                }
                else if (type.ToLower() == "4")
                {
                    strQuery = "SELECT " + DiamondHelper.GetCanturiCoulmns() + ",[dbo].[Udf_IsDiamondOrderd] (tblCanturiDiamond.LotNumber,'C') AS IsDiamondOrderd,dbo.Udf_GetCanturiPrice(Price,'" + UserSessionData.Currency.ToUpper() + "','CANTURI',DiamondID) AS Amount FROM tblCanturiDiamond WHERE DiamondID=" + id;
                }
                //else if (type.ToLower() == "5")
                //{
                //    strQuery = "SELECT *,[dbo].[Udf_IsDiamondOrderd] (tblPanacheDiamond.LotNumber,'P') AS IsDiamondOrderd,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblPanacheDiamond WHERE DiamondID=" + id;
                //}
                else if (type.ToLower() == "5")
                {
                    strQuery = "SELECT *,[dbo].[Udf_IsDiamondOrderd] (tblYDVashDiamond.LotNumber,'YD') AS IsDiamondOrderd,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblYDVashDiamond WHERE DiamondID=" + id;
                }
                else if (type.ToLower() == "6")
                {
                    strQuery = "SELECT *,[dbo].[Udf_IsDiamondOrderd] (tblHariKrishnaDiamond.LotNumber,'H') AS IsDiamondOrderd,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblHariKrishnaDiamond WHERE DiamondID=" + id;
                }
                else if (type.ToLower() == "7")
                {
                    strQuery = "SELECT *,[dbo].[Udf_IsDiamondOrderd] (tblFineStarDiamond.LotNumber,'F') AS IsDiamondOrderd,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblFineStarDiamond WHERE DiamondID=" + id;
                }
                else if (type.ToLower() == "8")
                {
                    strQuery = "SELECT *,[dbo].[Udf_IsDiamondOrderd] (tblCDINESHDiamond.LotNumber,'CD') AS IsDiamondOrderd,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblCDINESHDiamond WHERE DiamondID=" + id;
                }
                //Replaced by Prashant on 20 Nov 2020
                //else if (type.ToLower() == "9")
                //{
                //    strQuery = "SELECT *,[dbo].[Udf_IsDiamondOrderd] (tblKapuGemsDiamond.LotNumber,'KG') AS IsDiamondOrderd,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKapuGemsDiamond WHERE DiamondID=" + id;
                //}
                else if (type.ToLower() == "9")
                {
                    strQuery = "SELECT *,[dbo].[Udf_IsDiamondOrderd] (tblGlowstarDiamond.LotNumber,'GS') AS IsDiamondOrderd,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblGlowstarDiamond WHERE DiamondID=" + id;
                }

                // Add this for Client SRK
                else if (type.ToLower() == "10")
                {
                    strQuery = "SELECT *,[dbo].[Udf_IsDiamondOrderd] (tblSrkDiamond.LotNumber,'CD') AS IsDiamondOrderd, dbo.Udf_GetPriceCalculation(Price,'" + UserSessionData.Currency.ToUpper() + "',Carat) AS Amount FROM tblSrkDiamond WHERE DiamondID=" + id;
                }
                else if (type.ToLower() == "13")
                {
                    strQuery = "SELECT *,[dbo].[Udf_IsDiamondOrderd] (tblKiranGemsDiamond.LotNumber,'KD') AS IsDiamondOrderd,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKiranGemsDiamond WHERE DiamondID=" + id;
                }
                else if (type.ToLower() == "14")
                {
                    strQuery = "SELECT *,[dbo].[Udf_IsDiamondOrderd] (tblDharmDiamond.LotNumber,'DH') AS IsDiamondOrderd,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblDharmDiamond WHERE DiamondID=" + id;
                }

                else
                {
                    //invalid request
                }
            }
            else
            {
                //invalid request   
            }

            if (!String.IsNullOrEmpty(strQuery))
            {
                model = GetDiamondDetails(type, strQuery);
            }
            else
            {
                //invalid request   
            }

            if (Request.QueryString["r"] != null)
            {
                string strReq = Request.QueryString["r"].ToString();
                if (!String.IsNullOrEmpty(strReq))
                {
                    if (strReq.ToLower() == "order")
                    {
                        model.IsRequestAvailability = false;
                        model.IsRequestOrder = true;
                    }
                    else if (strReq.ToLower() == "request")
                    {
                        model.IsRequestAvailability = true;
                        model.IsRequestOrder = false;
                    }

                }
            }
            model.UrlCode = type;
            return View(model);
        }


        private static DiamondDetailModels GetDiamondDetails(string type, string strQuery)
        {
            DiamondDetailModels model = new DiamondDetailModels();
            DiamondHelper objDiamondHelper = new DiamondHelper();
            DataSet ds = objDiamondHelper.GetDiamondById(strQuery);
            string[] strShapes = { "CUSHION", "PRINCESS", "RADIANT" };
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    model.DiamondId = ds.Tables[0].Rows[0]["DiamondID"].ToString();
                    if (ds.Tables[0].Rows[0]["Shape"].ToString().ToUpper().Contains("CUSHION"))
                    {
                        model.Shape = "CUSHION";
                    }
                    else
                    {
                        model.Shape = ds.Tables[0].Rows[0]["Shape"].ToString();
                    }
                    //Caret-color-clarity-shape-Diamond
                    model.DiamondName = ds.Tables[0].Rows[0]["Carat"].ToString() + "-" + ds.Tables[0].Rows[0]["Color"].ToString().Replace("+", "").Replace("-", "") + "-" + ds.Tables[0].Rows[0]["Clarity"].ToString().Replace("+", "").Replace("-", "") + "-" + model.Shape + " Diamond";
                    //model.PriceCurrancy = ds.Tables[0].Rows[0][""].ToString();
                    model.Price = ds.Tables[0].Rows[0]["Price"].ToString();
                    model.TotalAmount = ds.Tables[0].Rows[0]["Amount"].ToString();
                    //model.IsRequestOrder = ds.Tables[0].Rows[0][""].ToString();
                    //model.IsRequestAvailability = ds.Tables[0].Rows[0][""].ToString();
                    model.LotNumber = ds.Tables[0].Rows[0]["LotNumber"].ToString();


                    //model.Shape = ds.Tables[0].Rows[0]["Shape"].ToString();
                    model.Carat = ds.Tables[0].Rows[0]["Carat"].ToString();
                    model.Colour = ds.Tables[0].Rows[0]["Color"].ToString().Replace("+", "").Replace("-", "");
                    model.Clarity = ds.Tables[0].Rows[0]["Clarity"].ToString().Replace("+", "").Replace("-", "");
                    model.Cut = ds.Tables[0].Rows[0]["Cut"].ToString().Replace("+", "").Replace("-", "");
                    model.Polish = ds.Tables[0].Rows[0]["Polish"].ToString();
                    model.Symmetry = ds.Tables[0].Rows[0]["Symmetry"].ToString();


                    if (String.IsNullOrEmpty(ds.Tables[0].Rows[0]["Fluorescence"].ToString()))
                    {
                        model.Flourescence = "Check Certificate";
                    }
                    else if (ds.Tables[0].Rows[0]["Fluorescence"].ToString().ToUpper() == "SLIGHT")
                    {

                        model.Flourescence = "Faint";
                    }
                    else if (ds.Tables[0].Rows[0]["Fluorescence"].ToString().ToUpper() == "VERY SLIGHT")
                    {
                        model.Flourescence = "Very Faint";
                    }
                    else
                    {
                        model.Flourescence = ds.Tables[0].Rows[0]["Fluorescence"].ToString();
                    }
                    //model.Flourescence = ds.Tables[0].Rows[0]["Fluorescence"].ToString();
                    model.Depth = ds.Tables[0].Rows[0]["Depth"].ToString();
                    model.Table = ds.Tables[0].Rows[0]["Table"].ToString();
                    model.Measurements = ds.Tables[0].Rows[0]["Measurements"].ToString().Replace("*", "x").Replace("-", "x");

                    model.Ratio = ds.Tables[0].Rows[0]["Ratio"].ToString();

                    if (model.Shape.ToUpper().Contains("ROUND"))
                    {
                        model.Ratio = ds.Tables[0].Rows[0]["Ratio"].ToString();
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(model.Ratio))
                        {
                            model.Ratio = CommonData.GetRatio(model.Measurements);
                        }
                    }
                    if (!String.IsNullOrEmpty(model.Ratio))
                    {
                        if (strShapes.Contains(model.Shape.ToUpper()))
                        {
                            model.Shape = model.Shape + " " + CommonData.GetCushionShape(model.Shape, model.Ratio);
                        }
                    }



                    //model.Ratio = ds.Tables[0].Rows[0]["Ratio"].ToString();
                    model.Lab = ds.Tables[0].Rows[0]["Lab"].ToString();
                    model.Cert = ds.Tables[0].Rows[0]["DiamondCertificate"].ToString();

                    // added because of Venus and CD dinesh
                    model.CertURL = ds.Tables[0].Rows[0]["CertURL"].ToString();

                    model.Type = type;

                    model.HasCertFile = false;

                    model.EyeClean = ds.Tables[0].Rows[0]["EyeClean"].ToString();

                    try
                    {
                        model.HasCertFile = Convert.ToBoolean(ds.Tables[0].Rows[0]["HasCertFile"].ToString());
                        /*if (type.ToLower() == "3")  // Venus Jewels
                        {
                            model.Cert = ds.Tables[0].Rows[0]["DiamondCertificate"].ToString();
                        }*/
                    }
                    catch { }
                    try
                    {
                        model.Supplier = ds.Tables[0].Rows[0]["Supplier"].ToString();
                    }
                    catch { }
                    model.IsDiamondOrderd = false;
                    try
                    {
                        model.IsDiamondOrderd = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDiamondOrderd"].ToString());
                    }
                    catch { }


                    

                    try
                    {
                        List<string> AllowedSupplierIds = DiamondHelper.GetVideoAllowedSuppliers();
                        bool ShowHideVideo = DiamondHelper.ShowHideDiamondVideo(type, AllowedSupplierIds);
                        if (ShowHideVideo)
                        {
                            model.DiamondVideo = Convert.ToString(ds.Tables[0].Rows[0]["VideoURL"]);
                            model.VideoURL = ds.Tables[0].Rows[0]["VideoURL"].ToString();
                            if (!string.IsNullOrEmpty(model.DiamondVideo))
                            {
                                //if (type.ToLower() == "4")
                                if (model.DiamondVideo.EndsWith(".mp4") || model.DiamondVideo.EndsWith(".mov") || model.DiamondVideo.EndsWith(".ogg") || model.DiamondVideo.EndsWith(".webm"))
                                {
                                    model.DiamondVideo = "../../Content/Uploads/DiamondVideo/" + model.DiamondVideo;
                                    
                                }

                                model.VideoURL = "";
                            }


                        }
                        else
                            model.DiamondVideo = "";
                        model.LotNumber = ds.Tables[0].Rows[0]["LotNumber"].ToString();
                    }
                    catch { }

                   
                }
            }
            model.UrlCode = type;
            return model;
        }

        [HttpPost]
        public ActionResult _RequestOrder(DiamondDetailModels model)
        {
            string strMsg = "ok";
            string strData = "";
            try
            {


                if (!String.IsNullOrEmpty(model.RequestOrderDetails.IsFullPayment))
                {
                    if (Convert.ToBoolean(model.RequestOrderDetails.IsFullPayment))
                    {
                        ModelState.Remove("RequestOrderDetails.AvailabilityDepositToken");
                        ModelState.Remove("RequestOrderDetails.BalanceDue");
                        ModelState.Remove("RequestOrderDetails.DatePaid");
                        ModelState.Remove("RequestOrderDetails.DateToBePaid");
                        ModelState.Remove("RequestOrderDetails.PaymentBalanceDueVia");
                        ModelState.Remove("RequestOrderDetails.DesignPrice");
                        ModelState.Remove("RequestOrderDetails.DateOrderPaidInFull");

                    }
                    else
                    {
                        ModelState.Remove("RequestOrderDetails.DateToBePaid");
                        ModelState.Remove("RequestOrderDetails.DesignPrice");

                        ModelState.Remove("RequestOrderDetails.DateDiamondPaidInFull");
                        ModelState.Remove("RequestOrderDetails.DateOrderPaidInFull");
                        ModelState.Remove("RequestOrderDetails.FullAmount");
                        ModelState.Remove("RequestOrderDetails.Comment");
                    }
                    if (ModelState.IsValid)
                    {
                        DiamondOrderHelper objDiamondOrderHelper = new DiamondOrderHelper();
                        DiamondDetailModels objDiamondDetailModels = new DiamondDetailModels();
                        string strQuery = string.Empty;
                        try
                        {
                            int diamondId = Convert.ToInt32(model.DiamondId);
                        }
                        catch (Exception ex)
                        {
                            //invalid request
                            strMsg = "NotOk";
                            strData = ex.Message;
                        }
                        if (!String.IsNullOrEmpty(model.Type))
                        {
                            if (model.Type.ToLower() == "1" || model.Type.ToLower() == "11" || model.Type.ToLower() == "12")//rapnet
                            {
                                strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblRapnetDiamond WHERE DiamondID=" + model.DiamondId;
                            }

                            else if (model.Type.ToLower() == "2")//jbbros
                            {
                                strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblJBBrosDiamond WHERE DiamondID=" + model.DiamondId;
                            }
                            else if (model.Type.ToLower() == "3")//venus
                            {
                                strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblVenusJewelsDiamond WHERE DiamondID=" + model.DiamondId;
                            }
                            else if (model.Type.ToLower() == "4")//canturi
                            {
                                strQuery = "SELECT " + DiamondHelper.GetCanturiCoulmns() + ",dbo.Udf_GetCanturiPrice(Price,'" + UserSessionData.Currency.ToUpper() + "','CANTURI',DiamondID) AS Amount FROM tblCanturiDiamond WHERE DiamondID=" + model.DiamondId;
                            }
                            //else if (model.Type.ToLower() == "5")//Panache
                            //{
                            //    strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblPanacheDiamond WHERE DiamondID=" + model.DiamondId;
                            //}
                            else if (model.Type.ToLower() == "5")//YDVASH
                            {
                                strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblYDVashDiamond  WHERE DiamondID=" + model.DiamondId;
                            }
                            else if (model.Type.ToLower() == "6")//Hari Krishna
                            {
                                strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblHariKrishnaDiamond WHERE DiamondID=" + model.DiamondId;
                            }
                            else if (model.Type.ToLower() == "7")//FineStar
                            {
                                strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblFineStarDiamond WHERE DiamondID=" + model.DiamondId;
                            }
                            else if (model.Type.ToLower() == "8")//CDINESH
                            {
                                strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblCDINESHDiamond WHERE DiamondID=" + model.DiamondId;
                            }
                            else if (model.Type.ToLower() == "9")//KAPU GEMS
                            {
                                strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKapuGemsDiamond  WHERE DiamondID=" + model.DiamondId;
                            }
                            /* else if (model.Type.ToLower() == "10")//SUNRISE
                            {
                                strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblSunriseDiamond  WHERE DiamondID=" + model.DiamondId;
                            }*/

                            // Add this for Client SRK

                            else if (model.Type.ToLower() == "10")//SRK
                            {
                                strQuery = "SELECT *, dbo.Udf_GetPriceCalculation(Price,'" + UserSessionData.Currency.ToUpper() + "',Carat) AS Amount FROM tblSrkDiamond  WHERE DiamondID=" + model.DiamondId;
                            }
                            else if (model.Type.ToLower() == "13")//KIRAN GEMS
                            {
                                strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKiranGemsDiamond  WHERE DiamondID=" + model.DiamondId;
                            }
                            else if (model.Type.ToLower() == "14")//DHARM
                            {
                                strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblDharmDiamond  WHERE DiamondID=" + model.DiamondId;
                            }

                            else
                            {
                                //invalid request
                                strMsg = "NotOk";
                                strData = "Invalid request type";
                            }
                        }
                        else
                        {
                            //invalid request   
                            strMsg = "NotOk";
                            strData = "Request type is empty";
                        }

                        if (!String.IsNullOrEmpty(strQuery))
                        {
                            string strUrlCode = model.Type;
                            objDiamondDetailModels = GetDiamondDetails(model.Type, strQuery);

                            model.Flag = 1;
                            model.CreatedBy = UserSessionData.UserId;
                            model.PriceCurrancy = UserSessionData.Currency;
                            string strType = CommonData.GetDiamondType(model.Type.ToLower());
                            if (!String.IsNullOrEmpty(strType))
                            {
                                model.Type = strType;
                            }
                            else
                            {
                                //invalid request
                                strMsg = "NotOk";
                                strData = "invalid request type, type is not correct";
                            }


                            model.ConsultantId = UserSessionData.UserId.ToString();
                            model.Shape = objDiamondDetailModels.Shape;
                            model.Colour = objDiamondDetailModels.Colour;
                            model.Clarity = objDiamondDetailModels.Clarity;
                            model.Cut = objDiamondDetailModels.Cut;
                            model.Polish = objDiamondDetailModels.Polish;
                            model.Flourescence = objDiamondDetailModels.Flourescence;
                            model.Measurements = objDiamondDetailModels.Measurements.Replace("*", "x");
                            model.Lab = objDiamondDetailModels.Lab;
                            model.Cert = objDiamondDetailModels.Cert;
                            model.LotNumber = objDiamondDetailModels.LotNumber;
                            model.Symmetry = objDiamondDetailModels.Symmetry;
                            model.RequestOrderDetails.DiamondPrice = objDiamondDetailModels.TotalAmount;
                            model.Carat = objDiamondDetailModels.Carat;
                            model.EyeClean = objDiamondDetailModels.EyeClean;
                            model.Depth = objDiamondDetailModels.Depth;
                            model.Ratio = objDiamondDetailModels.Ratio;
                            model.Table = objDiamondDetailModels.Table;
                            model.UrlCode = strUrlCode;
                            if (objDiamondOrderHelper.AddUpdRequestOrder(model) == 0)
                            {
                                FnSendRequestOrderEmailToAdmin(model, ref strMsg, ref strData);
                            }
                            else
                            {
                                //error
                                strMsg = "NotOk";
                                strData = "Please input required information...!";
                            }
                        }
                    }
                }
                else
                {
                    strMsg = "NotOk";
                    strData = "Please select payment option!";
                }
            }
            catch (Exception ex)
            {
                strMsg = "NotOk";
                strData = ex.Message;

            }
            return Json(new { msg = strMsg, data = strData });

            //return Redirect(Url.Content("~/Diamond/Rapnet/48722154?r=order"));
        }

        private void FnSendRequestOrderEmailToAdmin(DiamondDetailModels model, ref string strMsg, ref string strData)
        {
            //success
            string strMessage = "";

            System.Net.Mail.MailMessage objEmailMessage = new System.Net.Mail.MailMessage();

            System.IO.FileStream objFsContent = new System.IO.FileStream(Server.MapPath("~/Content/EmailTemplates/RequestOrderMailToAdmin.html"), System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.StreamReader objStreamContent = new System.IO.StreamReader(objFsContent);
            strMessage = objStreamContent.ReadToEnd();
            objFsContent.Close();
            objStreamContent.Close();
            objFsContent.Dispose();
            objStreamContent.Dispose();
            strMessage = strMessage.Replace("{0}", "Admin");

            //Consultant details
            strMessage = strMessage.Replace("{CONSULTANT_RNAME}", UserSessionData.UserName);

            //CUSTOMER DETAILS
            strMessage = strMessage.Replace("{CUSTOMER_NAME}", model.RequestOrderDetails.CustomerName);
            strMessage = strMessage.Replace("{CONSULTANT_EMAIL}", model.RequestOrderDetails.ConsultantEmail);
            strMessage = strMessage.Replace("{ORDER_NUMBER}", model.RequestOrderDetails.OrderNumber);
            strMessage = strMessage.Replace("{JCS_CUSTOMER_NUMBER}", model.RequestOrderDetails.JcsCustomerNumber);
            strMessage = strMessage.Replace("{DUE_DATE}", DateTime.ParseExact(model.RequestOrderDetails.DueDate, "dd/MM/yyyy", null).ToString("dd/MM/yyyy"));


            strMessage = strMessage.Replace("{SUPPLIER}", model.UrlCode);
            //DIAMOND DETAILS
            //if (model.Type.ToUpper() == "C")
            //{
            //    if (String.IsNullOrEmpty(model.Supplier))
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "Canturi");
            //    }
            //    else
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", model.Supplier);
            //    }
            //}
            //else
            //{
            //    if (model.Type.ToUpper() == "R")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "Rapnet");
            //    }
            //    else if (model.Type.ToUpper() == "J")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "Jb Bros");
            //    }
            //    else if (model.Type.ToUpper() == "P")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "Panache");
            //    }
            //    else if (model.Type.ToUpper() == "H")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "Hari Krishna");
            //    }
            //    else if (model.Type.ToUpper() == "F")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "FineStar");
            //    }
            //    else if (model.Type.ToUpper() == "CD")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "CDINESH");
            //    }
            //    else if (model.Type.ToUpper() == "KG")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "KAPUGEMS");
            //    }
            //    else if (model.Type.ToUpper() == "SD")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "SUNRISE");
            //    }
            //    else if (model.Type.ToUpper() == "V")
            //    {
            //        if (String.IsNullOrEmpty(model.Supplier))
            //        {
            //            strMessage = strMessage.Replace("{SUPPLIER}", "Venus Jewel");
            //        }
            //        else
            //        {
            //            strMessage = strMessage.Replace("{SUPPLIER}", model.Supplier);
            //        }
            //    }
            //}

            decimal tmpCarat = 0;
            try
            {
                tmpCarat = Convert.ToDecimal(model.Carat);
            }
            catch { }

            if (model.Shape.ToUpper().Contains("CUSHION"))
            {
                strMessage = strMessage.Replace("{SHAPE}", "CUSHION");
            }
            else
            {
                strMessage = strMessage.Replace("{SHAPE}", model.Shape);
            }


            // strMessage = strMessage.Replace("{SHAPE}", model.Shape);
            strMessage = strMessage.Replace("{CARAT}", CommonData.FormatDecimal(tmpCarat));
            strMessage = strMessage.Replace("{COLOUR}", model.Colour);
            strMessage = strMessage.Replace("{CLARITY}", model.Clarity);

            //strMessage = strMessage.Replace("{CUT}", model.Cut);
            if (model.Shape.ToUpper() == "ROUND")
            {
                string strCut = "<tr>" +
                            "<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 0 39px 15px 39px;" +
                     "font-family: arial;width:200px;\">" +
                                "Cut" +
                            "</td>" +
                            "<td width=\"3px\" align=\"center\">" +
                                "<span>:</span>" +
                            "</td>" +
                            "<td valign=\"top\" style=\"color: #545454; padding-left: 12px; font-family: arial;\">" +
                               model.Cut +
                            "</td>" +
                        "</tr>";
                strMessage = strMessage.Replace("{CUT}", strCut);
            }
            else
            {
                strMessage = strMessage.Replace("{CUT}", "");
            }


            strMessage = strMessage.Replace("{POLISH}", model.Polish);
            strMessage = strMessage.Replace("{SYMMETRY}", model.Symmetry);
            strMessage = strMessage.Replace("{FLOURESCENCE}", model.Flourescence);
            string strEyeClean = "No";
            if (model.Type.ToUpper() == "V")
            {
                if (model.EyeClean.ToUpper() == "YES")
                {
                    strEyeClean = "Yes";
                }
                else
                {
                    strEyeClean = "No";
                }
            }
            else if (model.Type.ToUpper() == "R")
            {
                strEyeClean = "N/A";
            }
            else
            {
                if (model.EyeClean.ToUpper() == "YES" || model.EyeClean.ToUpper() == "E1")
                {
                    strEyeClean = "Yes";
                }
            }

            string depth = "0";
            try
            {
                depth = Math.Round(Convert.ToDecimal(model.Depth), 2).ToString("G29"); ;
            }
            catch
            {

            }
            depth = depth + " %";


            string strTable = "0";
            try
            {
                strTable = Math.Round(Convert.ToDecimal(model.Table), 2).ToString("G29"); ;
            }
            catch
            {

            }
            strTable = strTable + " %";


            strMessage = strMessage.Replace("{EYECLEAN}", strEyeClean);
            strMessage = strMessage.Replace("{DEPTH}", depth);
            strMessage = strMessage.Replace("{TABLE}", strTable);
            strMessage = strMessage.Replace("{RATIO}", model.Ratio);

            strMessage = strMessage.Replace("{MEASUREMENTS}", model.Measurements.Replace("*", "x"));
            strMessage = strMessage.Replace("{LAB}", model.Lab);
            strMessage = strMessage.Replace("{CERTIFICATE_NUMBER}", model.Cert);
            strMessage = strMessage.Replace("{LOT_NUMBER}", model.LotNumber);


            //DIAMOND PRICE
            strMessage = strMessage.Replace("{DIAMOND_PRICE}", UserSessionData.Currency + " " + model.RequestOrderDetails.DiamondPrice);
            //strMessage = strMessage.Replace("{DESIGN_PRICE}", model.RequestOrderDetails.DesignPrice);
            if (!string.IsNullOrEmpty(model.RequestOrderDetails.Comment))
            {
                strMessage = strMessage.Replace("{COMMENT}", model.RequestOrderDetails.Comment);
            }
            else
            {
                strMessage = strMessage.Replace("{COMMENT}", "N/A");
            }




            if (Convert.ToBoolean(model.RequestOrderDetails.IsFullPayment))
            {

                StringBuilder sbFullPayment = new StringBuilder();

                sbFullPayment.Append(" <tr >");
                sbFullPayment.Append("<th valign=\"top\" align=\"center\" style=\"font-weight: bold; font-size: 15px; padding:8px 0px 8px 0px;font-family: arial; color:#fff; margin:0 0 10px; background:#797979\" colspan=\"3\">");
                sbFullPayment.Append("FULL PAYMENT");
                sbFullPayment.Append("</th>");
                sbFullPayment.Append("</tr>");
                sbFullPayment.Append("<tr >");
                sbFullPayment.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 5px 39px 15px 39px;font-family: arial;width:300px;\">");
                sbFullPayment.Append("Date Diamond Paid in Full");
                sbFullPayment.Append("</td>");
                sbFullPayment.Append("<td width=\"3px\" align=\"center\">");
                sbFullPayment.Append("<span>:</span>");
                sbFullPayment.Append("</td>");
                sbFullPayment.Append("<td valign=\"top\" style=\"color: #545454; padding:5px 0 0 12px; font-family: arial;\">");
                sbFullPayment.Append("{DATE_DIAMOND_PAID_IN_FULL}");
                sbFullPayment.Append("</td>");
                sbFullPayment.Append("</tr>");
                sbFullPayment.Append("<tr >");
                sbFullPayment.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 0 39px 15px 39px;font-family: arial;width:300px;\">");
                sbFullPayment.Append("Amount $");
                sbFullPayment.Append("</td>");
                sbFullPayment.Append("<td width=\"3px\" align=\"center\">");
                sbFullPayment.Append("<span>:</span>");
                sbFullPayment.Append("</td>");
                sbFullPayment.Append("<td valign=\"top\" style=\"color: #545454; padding-left: 12px; font-family: arial;\">");
                sbFullPayment.Append("AUD {FULL_PAYMENT_AMOUNT}");
                sbFullPayment.Append("</td>");
                sbFullPayment.Append("</tr>");
                //sbFullPayment.Append("<tr>");
                //sbFullPayment.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 0 39px 15px 39px;font-family: arial;width:300px;\">");
                //sbFullPayment.Append("Date Order Paid in Full");
                //sbFullPayment.Append("</td>");
                //sbFullPayment.Append("<td width=\"3px\" align=\"center\">");
                //sbFullPayment.Append("<span>:</span>");
                //sbFullPayment.Append("</td>");
                //sbFullPayment.Append("<td valign=\"top\" style=\"color: #545454; padding-left: 12px; font-family: arial;\">");
                //sbFullPayment.Append("{DATE_ORDER_PAID_IN_FULL}");
                //sbFullPayment.Append("</td>");
                //sbFullPayment.Append("</tr>");


                sbFullPayment.Replace("{is_full_payment}", "");
                sbFullPayment.Replace("{is_deposit_only_to_confirm_availability}", "style=\"display:none;\"");


                //FULL PAYMENT
                sbFullPayment.Replace("{DATE_DIAMOND_PAID_IN_FULL}", model.RequestOrderDetails.DateDiamondPaidInFull.ToString());
                sbFullPayment.Replace("{FULL_PAYMENT_AMOUNT}", model.RequestOrderDetails.FullAmount);
                //sbFullPayment.Replace("{DATE_ORDER_PAID_IN_FULL}", model.RequestOrderDetails.DateOrderPaidInFull.ToString());

                sbFullPayment.Replace("{COMMENT}", model.RequestOrderDetails.Comment);
                strMessage = FullPaymentPaidVia1(model, strMessage, sbFullPayment);

                // strMessage = strMessage.Replace("{PAYMENT_BLOCK}", sbFullPayment.ToString());

            }
            else
            {
                StringBuilder sbDepositOnlyToConfirmAvailability = new StringBuilder();


                sbDepositOnlyToConfirmAvailability.Append("<tr>");
                sbDepositOnlyToConfirmAvailability.Append("<th valign=\"top\" align=\"center\" style=\"font-weight: bold; font-size: 15px; padding:8px 0px 8px 0px;font-family: arial; color:#fff; margin:0 0 10px; background:#797979\" colspan=\"3\">");
                sbDepositOnlyToConfirmAvailability.Append("DEPOSIT ONLY TO CONFIRM AVAILABILITY");
                sbDepositOnlyToConfirmAvailability.Append("</th>");
                sbDepositOnlyToConfirmAvailability.Append("</tr>");
                sbDepositOnlyToConfirmAvailability.Append("<tr >");
                sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 5px 39px 15px 39px;font-family: arial;width:300px;\">");
                sbDepositOnlyToConfirmAvailability.Append("Availability Deposit Taken $");
                sbDepositOnlyToConfirmAvailability.Append("</td>");
                sbDepositOnlyToConfirmAvailability.Append("<td width=\"3px\" align=\"center\">");
                sbDepositOnlyToConfirmAvailability.Append("<span>:</span>");
                sbDepositOnlyToConfirmAvailability.Append("</td>");
                sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"color: #545454; padding:5px 0 0 12px; font-family: arial;\">");
                sbDepositOnlyToConfirmAvailability.Append("AUD {AVAILABILITY_DEPOSIT_TAKEN}");
                sbDepositOnlyToConfirmAvailability.Append("</td>");
                sbDepositOnlyToConfirmAvailability.Append("</tr>");

                sbDepositOnlyToConfirmAvailability.Append("<tr >");
                sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 0 39px 15px 39px;font-family: arial;width:300px;\">");
                sbDepositOnlyToConfirmAvailability.Append("Date Paid");
                sbDepositOnlyToConfirmAvailability.Append("</td>");
                sbDepositOnlyToConfirmAvailability.Append("<td width=\"3px\" align=\"center\">");
                sbDepositOnlyToConfirmAvailability.Append("<span>:</span>");
                sbDepositOnlyToConfirmAvailability.Append("</td>");
                sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"color: #545454; padding-left: 12px; font-family: arial;\">");
                sbDepositOnlyToConfirmAvailability.Append("{DATE_PAID}");
                sbDepositOnlyToConfirmAvailability.Append("</td>");
                sbDepositOnlyToConfirmAvailability.Append("</tr>");


                sbDepositOnlyToConfirmAvailability.Append("<tr >");
                sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 0 39px 15px 39px;font-family: arial;width:300px;\">");
                sbDepositOnlyToConfirmAvailability.Append("Balance Due $");
                sbDepositOnlyToConfirmAvailability.Append("</td>");
                sbDepositOnlyToConfirmAvailability.Append("<td width=\"3px\" align=\"center\">");
                sbDepositOnlyToConfirmAvailability.Append("<span>:</span>");
                sbDepositOnlyToConfirmAvailability.Append("</td>");
                sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"color: #545454; padding-left: 12px; font-family: arial;\">");
                sbDepositOnlyToConfirmAvailability.Append("AUD {BALANCE_DUE}");
                sbDepositOnlyToConfirmAvailability.Append("</td>");
                sbDepositOnlyToConfirmAvailability.Append("</tr>");

                //sbDepositOnlyToConfirmAvailability.Append("<tr >");
                //sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 0 39px 15px 39px;font-family: arial;width:300px;\">");
                //sbDepositOnlyToConfirmAvailability.Append("Date to be Paid");
                //sbDepositOnlyToConfirmAvailability.Append("</td>");
                //sbDepositOnlyToConfirmAvailability.Append("<td width=\"3px\" align=\"center\">");
                //sbDepositOnlyToConfirmAvailability.Append("<span>:</span>");
                //sbDepositOnlyToConfirmAvailability.Append("</td>");
                //sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"color: #545454; padding-left: 12px; font-family: arial;\">");
                //sbDepositOnlyToConfirmAvailability.Append("{DATE_TO_BE_PAID}");
                //sbDepositOnlyToConfirmAvailability.Append("</td>");
                //sbDepositOnlyToConfirmAvailability.Append("</tr>"); ;



                strMessage = FullPaymentPaidVia(model, strMessage, sbDepositOnlyToConfirmAvailability);

            }




            //if (model.RequestOrderDetails.IsCertificateViewdByClient)
            if (!String.IsNullOrEmpty(model.RequestOrderDetails.IsCertificateViewdByClient))
            {
                strMessage = strMessage.Replace("{is_certificate_viewd_by_client}", "");
                if (model.RequestOrderDetails.IsCertificateViewdByClient == "true")
                {
                    strMessage = strMessage.Replace("{CERTIFICATE_VIEWED_BY_CLIENT}", "Yes");
                }
                else
                {
                    strMessage = strMessage.Replace("{CERTIFICATE_VIEWED_BY_CLIENT}", "Not Important");
                }
            }
            else
            {
                strMessage = strMessage.Replace("{is_certificate_viewd_by_client}", "style=\"display:none;\"");
            }
            if (model.RequestOrderDetails.IsClientViewing == "true")
            {
                strMessage = strMessage.Replace("{CLIENT_VIEWING}", "Yes");
            }
            else
            {
                strMessage = strMessage.Replace("{CLIENT_VIEWING}", "No");
            }
            objEmailMessage.Subject = ConfigurationManager.AppSettings["RequestOrderAdminSubject"].Replace("{CUSTOMER_NAME}", model.RequestOrderDetails.CustomerName);
            objEmailMessage.To.Add(ConfigurationManager.AppSettings["RequestOrderAdminToEmail"]);
            objEmailMessage.To.Add(model.RequestOrderDetails.ConsultantEmail.Replace(";", ",").Replace("|", ","));
            objEmailMessage.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["RequestOrderAdminFrom"].ToString(CultureInfo.InvariantCulture), ConfigurationManager.AppSettings["RequestOrderAdminFromName"]);
            objEmailMessage.IsBodyHtml = true;
            objEmailMessage.Body = strMessage;
            try
            {
                EmailSender.FnEmailSend(ref objEmailMessage);
            }
            catch (Exception ex)
            {
                strMsg = "NotOk";
                strData = ex.Message;
                TempData["CommonMessage"] = "<span class=\"MsgRed\" >" + ex.Message + "</span>";
                AppError apError = new AppError();
                apError.LogMe(ex);
            }
        }

        private static string FullPaymentPaidVia(DiamondDetailModels model, string strMessage, StringBuilder sbDepositOnlyToConfirmAvailability)
        {
            sbDepositOnlyToConfirmAvailability.Append("<tr >");
            sbDepositOnlyToConfirmAvailability.Append("<th valign=\"top\" align=\"center\" style=\"font-weight: bold; font-size: 15px; padding:8px 0px 8px 0px;font-family: arial; color:#fff; margin:0 0 10px; background:#797979\" colspan=\"3\">");
            sbDepositOnlyToConfirmAvailability.Append("    Payment of balance due via");
            sbDepositOnlyToConfirmAvailability.Append("</th>");
            sbDepositOnlyToConfirmAvailability.Append("</tr>");
            sbDepositOnlyToConfirmAvailability.Append("<tr>");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 5px 39px 15px 39px; font-family: arial;width:300px;\">");
            sbDepositOnlyToConfirmAvailability.Append("Bank Transfer");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td width=\"3px\" align=\"center\">");
            sbDepositOnlyToConfirmAvailability.Append("<span>:</span>");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"color: #545454; padding:5px 0 0 12px; font-family: arial;\">");
            sbDepositOnlyToConfirmAvailability.Append("{BANK_TRANSFER}");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("</tr>");
            sbDepositOnlyToConfirmAvailability.Append("<tr >");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 0 39px 15px 39px;font-family: arial;width:300px;\">");
            sbDepositOnlyToConfirmAvailability.Append("Credit Card");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td width=\"3px\" align=\"center\">");
            sbDepositOnlyToConfirmAvailability.Append("<span>:</span>");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"color: #545454; padding-left: 12px; font-family: arial;\">");
            sbDepositOnlyToConfirmAvailability.Append("{CREDIT_CARD}");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("</tr>");
            sbDepositOnlyToConfirmAvailability.Append("<tr >");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 0 39px 15px 39px;font-family: arial;width:300px;\">");
            sbDepositOnlyToConfirmAvailability.Append("Cash");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td width=\"3px\" align=\"center\">");
            sbDepositOnlyToConfirmAvailability.Append("<span>:</span>");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"color: #545454; padding-left: 12px; font-family: arial;\">");
            sbDepositOnlyToConfirmAvailability.Append("{CASH}");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("</tr>");
            sbDepositOnlyToConfirmAvailability.Append("<tr >");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 0 39px 15px 39px;                     font-family: arial;width:300px;\">");
            sbDepositOnlyToConfirmAvailability.Append("Other");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td width=\"3px\" align=\"center\">");
            sbDepositOnlyToConfirmAvailability.Append("<span>:</span>");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"color: #545454; padding-left: 12px; font-family: arial;\">");
            sbDepositOnlyToConfirmAvailability.Append("{OTHER}");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("</tr>");


            string strDepositOnlyToConfirmAvailability = sbDepositOnlyToConfirmAvailability.ToString();

            //strMessage = strMessage.Replace("{is_full_payment}", "style=\"display:none;\"");
            //strMessage = strMessage.Replace("{is_deposit_only_to_confirm_availability}", "");


            //DEPOSIT ONLY TO CONFIRM AVAILABILITY
            strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{AVAILABILITY_DEPOSIT_TAKEN}", model.RequestOrderDetails.AvailabilityDepositToken);
            strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{BALANCE_DUE}", model.RequestOrderDetails.BalanceDue);
            strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{DATE_PAID}", model.RequestOrderDetails.DatePaid.ToString());
            //strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{DATE_TO_BE_PAID}", model.RequestOrderDetails.DateToBePaid.ToString());

            //PAYMENT OF BALANCE DUE VIA
            if (model.RequestOrderDetails.IsBankTransfer)
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{BANK_TRANSFER}", "Yes");
            }
            else
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{BANK_TRANSFER}", "No");
            }
            if (model.RequestOrderDetails.IsCreditCard)
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{CREDIT_CARD}", "Yes");
            }
            else
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{CREDIT_CARD}", "No");
            }
            if (model.RequestOrderDetails.IsCash)
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{CASH}", "Yes");
            }
            else
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{CASH}", "No");
            }

            if (model.RequestOrderDetails.IsOther)
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{OTHER}", "Yes");
            }
            else
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{OTHER}", "No");
            }

            strMessage = strMessage.Replace("{PAYMENT_BLOCK}", strDepositOnlyToConfirmAvailability.ToString());
            return strMessage;
        }

        private static string FullPaymentPaidVia1(DiamondDetailModels model, string strMessage, StringBuilder sbDepositOnlyToConfirmAvailability)
        {
            sbDepositOnlyToConfirmAvailability.Append("<tr >");
            sbDepositOnlyToConfirmAvailability.Append("<th valign=\"top\" align=\"center\" style=\"font-weight: bold; font-size: 15px; padding:8px 0px 8px 0px;font-family: arial; color:#fff; margin:0 0 10px; background:#797979\" colspan=\"3\">");
            sbDepositOnlyToConfirmAvailability.Append("    FULL PAYMENT PAID VIA");
            sbDepositOnlyToConfirmAvailability.Append("</th>");
            sbDepositOnlyToConfirmAvailability.Append("</tr>");
            sbDepositOnlyToConfirmAvailability.Append("<tr>");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 5px 39px 15px 39px; font-family: arial;width:300px;\">");
            sbDepositOnlyToConfirmAvailability.Append("Bank Transfer");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td width=\"3px\" align=\"center\">");
            sbDepositOnlyToConfirmAvailability.Append("<span>:</span>");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"color: #545454; padding:5px 0 0 12px; font-family: arial;\">");
            sbDepositOnlyToConfirmAvailability.Append("{BANK_TRANSFER}");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("</tr>");
            sbDepositOnlyToConfirmAvailability.Append("<tr >");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 0 39px 15px 39px;font-family: arial;width:300px;\">");
            sbDepositOnlyToConfirmAvailability.Append("Credit Card");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td width=\"3px\" align=\"center\">");
            sbDepositOnlyToConfirmAvailability.Append("<span>:</span>");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"color: #545454; padding-left: 12px; font-family: arial;\">");
            sbDepositOnlyToConfirmAvailability.Append("{CREDIT_CARD}");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("</tr>");
            sbDepositOnlyToConfirmAvailability.Append("<tr >");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 0 39px 15px 39px;font-family: arial;width:300px;\">");
            sbDepositOnlyToConfirmAvailability.Append("Cash");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td width=\"3px\" align=\"center\">");
            sbDepositOnlyToConfirmAvailability.Append("<span>:</span>");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"color: #545454; padding-left: 12px; font-family: arial;\">");
            sbDepositOnlyToConfirmAvailability.Append("{CASH}");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("</tr>");
            sbDepositOnlyToConfirmAvailability.Append("<tr >");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 0 39px 15px 39px;                     font-family: arial;width:300px;\">");
            sbDepositOnlyToConfirmAvailability.Append("Other");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td width=\"3px\" align=\"center\">");
            sbDepositOnlyToConfirmAvailability.Append("<span>:</span>");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("<td valign=\"top\" style=\"color: #545454; padding-left: 12px; font-family: arial;\">");
            sbDepositOnlyToConfirmAvailability.Append("{OTHER}");
            sbDepositOnlyToConfirmAvailability.Append("</td>");
            sbDepositOnlyToConfirmAvailability.Append("</tr>");


            string strDepositOnlyToConfirmAvailability = sbDepositOnlyToConfirmAvailability.ToString();

            //strMessage = strMessage.Replace("{is_full_payment}", "style=\"display:none;\"");
            //strMessage = strMessage.Replace("{is_deposit_only_to_confirm_availability}", "");



            //PAYMENT OF BALANCE DUE VIA
            if (model.RequestOrderDetails.IsBankTransfer)
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{BANK_TRANSFER}", "Yes");
            }
            else
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{BANK_TRANSFER}", "No");
            }
            if (model.RequestOrderDetails.IsCreditCard)
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{CREDIT_CARD}", "Yes");
            }
            else
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{CREDIT_CARD}", "No");
            }
            if (model.RequestOrderDetails.IsCash)
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{CASH}", "Yes");
            }
            else
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{CASH}", "No");
            }

            if (model.RequestOrderDetails.IsOther)
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{OTHER}", "Yes");
            }
            else
            {
                strDepositOnlyToConfirmAvailability = strDepositOnlyToConfirmAvailability.Replace("{OTHER}", "No");
            }

            strMessage = strMessage.Replace("{PAYMENT_BLOCK}", strDepositOnlyToConfirmAvailability.ToString());
            return strMessage;
        }

        [HttpPost]
        public ActionResult _RequestAvailability(DiamondDetailModels model)
        {
            string strMsg = "ok";
            string strData = "";

            ModelState.Remove("RequestAvailabilityDetails.DesignPrice");


            if (ModelState.IsValid)
            {

                DiamondOrderHelper objDiamondOrderHelper = new DiamondOrderHelper();
                DiamondDetailModels objDiamondDetailModels = new DiamondDetailModels();
                string strQuery = string.Empty;
                try
                {
                    int diamondId = Convert.ToInt32(model.DiamondId);
                }
                catch (Exception ex)
                {
                    strMsg = "NotOk";
                    strData = ex.Message;
                    //invalid request
                }
                if (!String.IsNullOrEmpty(model.Type))
                {
                    if (model.Type.ToLower() == "1" || model.Type.ToLower() == "11" || model.Type.ToLower() == "12")//rapnet
                    {
                        strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblRapnetDiamond WHERE DiamondID=" + model.DiamondId;
                    }
                    else if (model.Type.ToLower() == "2")//jbbros
                    {
                        strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblJBBrosDiamond WHERE DiamondID=" + model.DiamondId;
                    }
                    else if (model.Type.ToLower() == "3")//venus
                    {
                        strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblVenusJewelsDiamond WHERE DiamondID=" + model.DiamondId;
                    }

                    else if (model.Type.ToLower() == "4")//canturi
                    {
                        strQuery = "SELECT " + DiamondHelper.GetCanturiCoulmns() + ",dbo.Udf_GetCanturiPrice(Price,'" + UserSessionData.Currency.ToUpper() + "','CANTURI',DiamondID) AS Amount FROM tblCanturiDiamond WHERE DiamondID=" + model.DiamondId;
                    }
                    //else if (model.Type.ToLower() == "5")//Panache
                    //{
                    //    strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblPanacheDiamond WHERE DiamondID=" + model.DiamondId;
                    //}
                    else if (model.Type.ToLower() == "5")//YDVASH
                    {
                        strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblYDVashDiamond WHERE DiamondID=" + model.DiamondId;
                    }
                    else if (model.Type.ToLower() == "6")//Hari Krishna
                    {
                        strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblHariKrishnaDiamond WHERE DiamondID=" + model.DiamondId;
                    }
                    else if (model.Type.ToLower() == "7")//FineStar
                    {
                        strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblFineStarDiamond WHERE DiamondID=" + model.DiamondId;
                    }
                    else if (model.Type.ToLower() == "8")//CDINESH
                    {
                        strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblCDINESHDiamond WHERE DiamondID=" + model.DiamondId;
                    }
                    else if (model.Type.ToLower() == "9")//KAPUGEMS
                    {
                        strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKapuGemsDiamond WHERE DiamondID=" + model.DiamondId;
                    }
                    /* else if (model.Type.ToLower() == "10")//SUNRISE
                    {
                        strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblSunriseDiamond WHERE DiamondID=" + model.DiamondId;
                    }*/

                    // Add this for Client SRK

                    else if (model.Type.ToLower() == "10")//SRK
                    {
                        strQuery = "SELECT *,'' as Ratio, dbo.Udf_GetPriceCalculation(Price,'" + UserSessionData.Currency.ToUpper() + "',Carat) AS Amount FROM tblSrkDiamond WHERE DiamondID=" + model.DiamondId;
                    }

                    else if (model.Type.ToLower() == "13")//KIRANGEMS
                    {
                        strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKiranGemsDiamond WHERE DiamondID=" + model.DiamondId;
                    }
                    else if (model.Type.ToLower() == "14")//DHARM
                    {
                        strQuery = "SELECT *,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblDharmDiamond WHERE DiamondID=" + model.DiamondId;
                    }

                    else
                    {
                        //invalid request
                        strMsg = "NotOk";
                        strData = "invalid request type";
                    }
                }
                else
                {
                    //invalid request   
                    strMsg = "NotOk";
                    strData = "invalid request type, type is empty";
                }

                if (!String.IsNullOrEmpty(strQuery))
                {
                    string strUrlCode = model.Type;
                    objDiamondDetailModels = GetDiamondDetails(model.Type, strQuery);

                    model.Flag = 1;
                    model.CreatedBy = UserSessionData.UserId;
                    model.PriceCurrancy = UserSessionData.Currency;

                    string strType = CommonData.GetDiamondType(model.Type.ToLower());
                    if (!String.IsNullOrEmpty(strType))
                    {
                        model.Type = strType;
                    }
                    else
                    {
                        //invalid request
                        strMsg = "NotOk";
                        strData = "invalid request type, type is not correct";
                    }
                    model.ConsultantId = UserSessionData.UserId.ToString();
                    model.Shape = objDiamondDetailModels.Shape;
                    model.Colour = objDiamondDetailModels.Colour;
                    model.Clarity = objDiamondDetailModels.Clarity;
                    model.Cut = objDiamondDetailModels.Cut;
                    model.Polish = objDiamondDetailModels.Polish;
                    model.Flourescence = objDiamondDetailModels.Flourescence;
                    model.Measurements = objDiamondDetailModels.Measurements.Replace("*", "x");
                    model.Lab = objDiamondDetailModels.Lab;
                    model.Cert = objDiamondDetailModels.Cert;
                    model.LotNumber = objDiamondDetailModels.LotNumber;
                    model.Symmetry = objDiamondDetailModels.Symmetry;
                    model.TotalAmount = objDiamondDetailModels.TotalAmount;
                    model.Carat = objDiamondDetailModels.Carat;
                    model.EyeClean = objDiamondDetailModels.EyeClean;
                    model.Depth = objDiamondDetailModels.Depth;
                    model.Ratio = objDiamondDetailModels.Ratio;
                    model.Table = objDiamondDetailModels.Table;
                    model.UrlCode = strUrlCode;
                    if (objDiamondOrderHelper.AddUpdRequestAvailability(model) == 0)
                    {
                        FnSendRequestAvailabilityMailToAdmin(model, ref strMsg, ref strData);
                    }
                    else
                    {
                        //error
                        strMsg = "NotOk";
                        strData = "Error while saving data, Please try again...!";
                    }
                }
                else
                {
                    //invalid request   
                    strMsg = "NotOk";
                    strData = "Please input required information...!";
                }


            }
            return Json(new { msg = strMsg, data = strData });
            //return Content("Thanks", "text/html");
            //return Redirect(Url.Content("~/Diamond/Rapnet/48722154?r=request"));
        }

        private void FnSendRequestAvailabilityMailToAdmin(DiamondDetailModels model, ref string strMsg, ref string strData)
        {
            //success

            string strMessage = "";

            System.Net.Mail.MailMessage objEmailMessage = new System.Net.Mail.MailMessage();

            System.IO.FileStream objFsContent = new System.IO.FileStream(Server.MapPath("~/Content/EmailTemplates/RequestAvailabilityMailToAdmin.html"), System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.StreamReader objStreamContent = new System.IO.StreamReader(objFsContent);
            strMessage = objStreamContent.ReadToEnd();
            objFsContent.Close();
            objStreamContent.Close();
            objFsContent.Dispose();
            objStreamContent.Dispose();
            strMessage = strMessage.Replace("{0}", "Admin");
            strMessage = strMessage.Replace("{CONSULTANT_RNAME}", UserSessionData.UserName);
            strMessage = strMessage.Replace("{CUSTOMER_NAME}", model.RequestAvailabilityDetails.CustomerName);
            strMessage = strMessage.Replace("{CONSULTANT_EMAIL}", model.RequestAvailabilityDetails.ConsultantEmail);
            strMessage = strMessage.Replace("{ORDER_NUMBER}", model.RequestAvailabilityDetails.OrderNumber);
            strMessage = strMessage.Replace("{JCS_CUSTOMER_NUMBER}", model.RequestAvailabilityDetails.JcsCustomerNumber);
            strMessage = strMessage.Replace("{DUE_DATE}", model.RequestAvailabilityDetails.DueDate);
            strMessage = strMessage.Replace("{SUPPLIER}", model.UrlCode);
            //if (model.Type.ToUpper() == "C")
            //{
            //    if (String.IsNullOrEmpty(model.Supplier))
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "Canturi");
            //    }
            //    else
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", model.Supplier);
            //    }
            //}
            //else
            //{
            //    if (model.Type.ToUpper() == "R")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "Rapnet");
            //    }
            //    else if (model.Type.ToUpper() == "J")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "Jb Bros");
            //    }
            //    else if (model.Type.ToUpper() == "P")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "Panache");
            //    }
            //    else if (model.Type.ToUpper() == "H")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "Hari Krishna");
            //    }
            //    else if (model.Type.ToUpper() == "F")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "FineStar");
            //    }
            //    else if (model.Type.ToUpper() == "CD")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "CDINESH");
            //    }
            //    else if (model.Type.ToUpper() == "KG")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "KAPUGEMS");
            //    }
            //    else if (model.Type.ToUpper() == "SD")
            //    {
            //        strMessage = strMessage.Replace("{SUPPLIER}", "SUNRISE");
            //    }
            //    else if (model.Type.ToUpper() == "V")
            //    {
            //        if (String.IsNullOrEmpty(model.Supplier))
            //        {
            //            strMessage = strMessage.Replace("{SUPPLIER}", "Venus Jewel");
            //        }
            //        else
            //        {
            //            strMessage = strMessage.Replace("{SUPPLIER}", model.Supplier);
            //        }
            //    }
            //}
            decimal tmpCarat = 0;
            try
            {
                tmpCarat = Convert.ToDecimal(model.Carat);
            }
            catch { }

            strMessage = strMessage.Replace("{CARAT}", CommonData.FormatDecimal(tmpCarat));
            strMessage = strMessage.Replace("{SHAPE}", model.Shape);
            strMessage = strMessage.Replace("{COLOUR}", model.Colour);
            strMessage = strMessage.Replace("{CLARITY}", model.Clarity);

            if (model.Shape.ToUpper() == "ROUND")
            {
                string strCut = "<tr>" +
                            "<td valign=\"top\" style=\"font-weight: bold; font-size: 15px; padding: 0 39px 15px 39px;" +
                     "font-family: arial;width:200px;\">" +
                                "Cut" +
                            "</td>" +
                            "<td width=\"3px\" align=\"center\">" +
                                "<span>:</span>" +
                            "</td>" +
                            "<td valign=\"top\" style=\"color: #545454; padding-left: 12px; font-family: arial;\">" +
                               model.Cut +
                            "</td>" +
                        "</tr>";
                strMessage = strMessage.Replace("{CUT}", strCut);
            }
            else
            {
                strMessage = strMessage.Replace("{CUT}", "");
            }

            strMessage = strMessage.Replace("{POLISH}", model.Polish);
            strMessage = strMessage.Replace("{FLOURESCENCE}", model.Flourescence);

            string strEyeClean = "No";
            if (model.Type.ToUpper() == "V")
            {
                if (model.EyeClean.ToUpper() == "YES")
                {
                    strEyeClean = "Yes";
                }
                else
                {
                    strEyeClean = "No";
                }
            }
            else if (model.Type.ToUpper() == "R")
            {
                strEyeClean = "N/A";
            }
            else
            {
                if (model.EyeClean.ToUpper() == "YES" || model.EyeClean.ToUpper() == "E1")
                {
                    strEyeClean = "Yes";
                }
            }

            string depth = "0";
            try
            {
                depth = Math.Round(Convert.ToDecimal(model.Depth), 2).ToString("G29"); ;
            }
            catch
            {

            }
            depth = depth + " %";


            string strTable = "0";
            try
            {
                strTable = Math.Round(Convert.ToDecimal(model.Table), 2).ToString("G29"); ;
            }
            catch
            {

            }
            strTable = strTable + " %";


            strMessage = strMessage.Replace("{EYECLEAN}", strEyeClean);
            strMessage = strMessage.Replace("{DEPTH}", depth);
            strMessage = strMessage.Replace("{TABLE}", strTable);
            strMessage = strMessage.Replace("{RATIO}", model.Ratio);

            strMessage = strMessage.Replace("{MEASUREMENTS}", model.Measurements.Replace("*", "x"));
            strMessage = strMessage.Replace("{LAB}", model.Lab);
            strMessage = strMessage.Replace("{CERTIFICATE_NUMBER}", model.Cert);
            strMessage = strMessage.Replace("{LOT_NUMBER}", model.LotNumber);
            strMessage = strMessage.Replace("{SYMMETRY}", model.Symmetry);
            strMessage = strMessage.Replace("{DIAMOND_PRICE}", UserSessionData.Currency + " " + model.TotalAmount);
            strMessage = strMessage.Replace("{DESIGN_PRICE}", model.RequestAvailabilityDetails.DesignPrice);
            if (!String.IsNullOrEmpty(model.RequestAvailabilityDetails.Comment))
            {
                strMessage = strMessage.Replace("{COMMENT}", model.RequestAvailabilityDetails.Comment);
            }
            else
            {
                strMessage = strMessage.Replace("{COMMENT}", "N/A");
            }



            objEmailMessage.Subject = ConfigurationManager.AppSettings["RequestAvailabilityAdminSubject"].Replace("{CUSTOMER_NAME}", model.RequestAvailabilityDetails.CustomerName);
            objEmailMessage.To.Add(ConfigurationManager.AppSettings["RequestAvailabilityAdminToEmail"]);
            objEmailMessage.To.Add(model.RequestAvailabilityDetails.ConsultantEmail.Replace(";", ",").Replace("|", ","));
            objEmailMessage.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["RequestAvailabilityAdminFrom"].ToString(CultureInfo.InvariantCulture), ConfigurationManager.AppSettings["RequestAvailabilityAdminFromName"]);
            objEmailMessage.IsBodyHtml = true;
            objEmailMessage.Body = strMessage;
            try
            {
                EmailSender.FnEmailSend(ref objEmailMessage);
            }
            catch (Exception ex)
            {
                TempData["CommonMessage"] = "<span class=\"MsgRed\" >" + ex.Message + "</span>";
                AppError apError = new AppError();
                apError.LogMe(ex);
                strMsg = "NotOk";
                strData = ex.Message;
            }
        }


        [HttpPost]
        public ActionResult SendVideoLink(string type, string lot, string email)
        {
            string strMsg = "ok";
            string strData = "";

            ModelState.Remove("RequestAvailabilityDetails.DesignPrice");


            if (ModelState.IsValid)
            {

                DiamondOrderHelper objDiamondOrderHelper = new DiamondOrderHelper();
                DiamondDetailModels model = new DiamondDetailModels();
                if (string.IsNullOrEmpty(lot))
                {
                    strMsg = "NotOk";
                    strData = "Lot # empty";
                    //invalid request
                }

                string strQuery = "SELECT * from Fn_AllDiamonds('" + UserSessionData.Currency.ToUpper() + "') WHERE LotNumber = '" + lot + "'";


                if (!String.IsNullOrEmpty(strQuery))
                {
                    DiamondDetailModels objDiamondDetailModels = GetDiamondDetails(type, strQuery);
                    model.Type = type;
                    model.Flag = 1;
                    model.CreatedBy = UserSessionData.UserId;
                    model.PriceCurrancy = UserSessionData.Currency;

                    string strType = CommonData.GetDiamondType(model.Type.ToLower());
                    if (!String.IsNullOrEmpty(strType))
                    {
                        model.Type = strType;
                    }
                    else
                    {
                        //invalid request
                        strMsg = "NotOk";
                        strData = "invalid request type, type is not correct";
                    }
                    model.ConsultantId = UserSessionData.UserId.ToString();
                    model.Shape = objDiamondDetailModels.Shape;
                    model.Colour = objDiamondDetailModels.Colour;
                    model.Clarity = objDiamondDetailModels.Clarity;
                    model.Cut = objDiamondDetailModels.Cut;
                    model.Polish = objDiamondDetailModels.Polish;
                    model.Flourescence = objDiamondDetailModels.Flourescence;
                    model.Measurements = objDiamondDetailModels.Measurements.Replace("*", "x");
                    model.Lab = objDiamondDetailModels.Lab;
                    model.Cert = objDiamondDetailModels.Cert;
                    model.LotNumber = objDiamondDetailModels.LotNumber;
                    model.Symmetry = objDiamondDetailModels.Symmetry;
                    model.TotalAmount = objDiamondDetailModels.TotalAmount;
                    model.Carat = objDiamondDetailModels.Carat;
                    model.EyeClean = objDiamondDetailModels.EyeClean;
                    model.Depth = objDiamondDetailModels.Depth;
                    model.Ratio = objDiamondDetailModels.Ratio;
                    model.Table = objDiamondDetailModels.Table;
                    model.UrlCode = type;
                    model.DiamondVideo = objDiamondDetailModels.DiamondVideo;
                    //model.DiamondVideo
                    if (!string.IsNullOrEmpty(model.DiamondVideo))
                    {
                        if (model.Type.ToLower() == "c")
                        //if (model.DiamondVideo.EndsWith(".mp4") || model.DiamondVideo.EndsWith(".mov") || model.DiamondVideo.EndsWith(".ogg") || model.DiamondVideo.EndsWith(".webm"))
                        {
                            model.DiamondVideo = Request.Url.Scheme + "://" + Request.Url.Authority + "/Canturi/Video?id=" + Request.Url.Scheme + "://" + Request.Url.Authority + "/" + model.DiamondVideo.Replace("../", "");
                        }
                        else
                        {
                            model.DiamondVideo = Request.Url.Scheme + "://" + Request.Url.Authority + "/Canturi/Video?id=" + model.DiamondVideo;
                        }
                    }

                    FnSendVideoLinkEmail(model, email, ref strMsg, ref strData);
                    if (strMsg == "NotOk")
                    {
                        strData = "Email not sent !";
                    }
                }
                else
                {
                    //invalid request   
                    strMsg = "NotOk";
                    strData = "Please input required information...!";
                }


            }
            return Json(new { msg = strMsg, data = strData });
        }

        private void FnSendVideoLinkEmail(DiamondDetailModels model, string ToEmail, ref string strMsg, ref string strData)
        {
            //success
            string Subject = "VIDEO - {CARAT} {SHAPE} {COLOUR} {CLARITY}";

            System.Net.Mail.MailMessage objEmailMessage = new System.Net.Mail.MailMessage();

            System.IO.FileStream objFsContent = new System.IO.FileStream(Server.MapPath("~/Content/EmailTemplates/SendVideoLink.html"), System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.StreamReader objStreamContent = new System.IO.StreamReader(objFsContent);
            string strMessage = objStreamContent.ReadToEnd();
            objFsContent.Close();
            objStreamContent.Close();
            objFsContent.Dispose();
            objStreamContent.Dispose();





            decimal tmpCarat = 0;
            try
            {
                tmpCarat = Convert.ToDecimal(model.Carat);
            }
            catch { }

            if (model.Shape.ToUpper().Contains("CUSHION"))
            {
                strMessage = strMessage.Replace("{SHAPE}", "CUSHION");
                Subject = Subject.Replace("{SHAPE}", "CUSHION");
            }
            else
            {
                strMessage = strMessage.Replace("{SHAPE}", model.Shape);
                Subject = Subject.Replace("{SHAPE}", model.Shape);
            }


            // strMessage = strMessage.Replace("{SHAPE}", model.Shape);
            string carat = CommonData.FormatDecimal(tmpCarat);
            strMessage = strMessage.Replace("{CARAT}", carat);
            strMessage = strMessage.Replace("{COLOUR}", model.Colour);
            strMessage = strMessage.Replace("{CLARITY}", model.Clarity);



            strMessage = strMessage.Replace("{Video_URL}", model.DiamondVideo);
            Subject = Subject.Replace("{CARAT}", carat);
            Subject = Subject.Replace("{COLOUR}", model.Colour);
            Subject = Subject.Replace("{CLARITY}", model.Clarity);

            //strMessage = strMessage.Replace("{CUT}", model.Cut);
            if (model.Shape.ToUpper() == "ROUND")
            {
                strMessage = strMessage.Replace("{CUT}", model.Cut);
            }
            else
            {
                strMessage = strMessage.Replace("{CUT}", "");
            }


            strMessage = strMessage.Replace("{POLISH}", model.Polish);
            strMessage = strMessage.Replace("{SYMMETRY}", model.Symmetry);
            strMessage = strMessage.Replace("{FLOURESCENCE}", model.Flourescence);
            /*
            string strEyeClean = "No";
            if (model.Type.ToUpper() == "V")
            {
                if (model.EyeClean.ToUpper() == "YES")
                {
                    strEyeClean = "Yes";
                }
                else
                {
                    strEyeClean = "No";
                }
            }
            else if (model.Type.ToUpper() == "R")
            {
                strEyeClean = "N/A";
            }
            else
            {
                if (model.EyeClean.ToUpper() == "YES" || model.EyeClean.ToUpper() == "E1")
                {
                    strEyeClean = "Yes";
                }
            }

            string depth = "0";
            try
            {
                depth = Math.Round(Convert.ToDecimal(model.Depth), 2).ToString("G29"); ;
            }
            catch
            {

            }
            depth = depth + " %";


            string strTable = "0";
            try
            {
                strTable = Math.Round(Convert.ToDecimal(model.Table), 2).ToString("G29"); ;
            }
            catch
            {

            }
            strTable = strTable + " %";
            

            strMessage = strMessage.Replace("{EYECLEAN}", strEyeClean);
            strMessage = strMessage.Replace("{DEPTH}", depth);
            strMessage = strMessage.Replace("{TABLE}", strTable);
            strMessage = strMessage.Replace("{RATIO}", model.Ratio);
            strMessage = strMessage.Replace("{LAB}", model.Lab);
            */
            strMessage = strMessage.Replace("{MEASUREMENTS}", model.Measurements.Replace("*", "x"));
            strMessage = strMessage.Replace("{CERTIFICATE_NUMBER}", model.Cert);
            strMessage = strMessage.Replace("{LOT_NUMBER}", model.LotNumber);

            // For Damond Shape Image
            //string[] strShapes = { "CUSHION", "PRINCESS", "RADIANT" };
            string strDiamondImg = Url.Content("~/Content/FrontEnd/images/Diamonds/large/no-image.png");

            string diamondShape = model.Shape.ToUpper();
            if (model.Shape.ToUpper().Contains("CUSHION"))
            {
                diamondShape = "CUSHION";
            }
            if (model.Shape.ToUpper().Contains("PRINCESS"))
            {
                diamondShape = "PRINCESS";
            }
            if (model.Shape.ToUpper().Contains("RADIANT"))
            {
                diamondShape = "RADIANT";
            }
            if (System.IO.File.Exists(Server.MapPath("~/Content/FrontEnd/images/Diamonds/small/") + diamondShape.ToLower().Trim() + ".png"))
            {
                strDiamondImg = Request.Url.Scheme + "://" + Request.Url.Authority + Url.Content("~/Content/FrontEnd/images/Diamonds/small/") + diamondShape.ToLower().Trim() + ".png";
            }            

            strMessage = strMessage.Replace("{SHAPE_IMAGE}", "<img src='" + strDiamondImg + "' border='0' id='imgDiamondShape' alt='Image just for illustration'>");

            //For DIAMOND PRICE
            
            string totalPrice = "";
            try
            {
                totalPrice = Math.Round(Convert.ToDecimal(model.TotalAmount), 2).ToString("G29");
            }
            catch
            {

            }




            /*if (UserSessionData.Currency.ToLower() == "aud")
            {
                totalPrice = " AUD $" + totalPrice + "<small>Inclusive of GST</small>";
            }
            else if (UserSessionData.Currency.ToLower() == "euro")
            {
                //+ "<small>Inclusive of GST</small>"
                totalPrice = " €" + totalPrice;
            }
            else if (UserSessionData.Currency.ToLower() == "usd")
            {
                //+ "<small>Inclusive of GST</small>"
                totalPrice = " USD $" + totalPrice;
            }
            else
            {
                totalPrice = " $" + totalPrice + "<small> (Inclusive of GST)</small>";
            }*/



            strMessage = strMessage.Replace("{PRICE}", "$" + totalPrice);
            strMessage = strMessage.Replace("{DESIGN_PRICE}", ""/*model.RequestOrderDetails.DesignPrice*/);
            strMessage = strMessage.Replace("{CODE}", model.UrlCode);


            objEmailMessage.Subject = Subject;
            objEmailMessage.To.Add(ToEmail);
            //objEmailMessage.To.Add(model.RequestOrderDetails.ConsultantEmail.Replace(";", ",").Replace("|", ","));
            objEmailMessage.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["RequestOrderAdminFrom"].ToString(CultureInfo.InvariantCulture), ConfigurationManager.AppSettings["RequestOrderAdminFromName"]);
            objEmailMessage.IsBodyHtml = true;
            objEmailMessage.Body = strMessage;

            strMsg = EmailSender.FnEmailSend(ref objEmailMessage);

        }
        // for {SHAPE_IMAGE} 
    }
}
