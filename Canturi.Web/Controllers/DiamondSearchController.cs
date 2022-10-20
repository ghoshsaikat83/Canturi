using Canturi.Models.BusinessEntity.FrontEnd;
using Canturi.Models.BusinessHelper.CommonHelper;
using Canturi.Models.BusinessHelper.FrontEnd;
using Canturi.Web.App_Start;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Canturi.Web.Controllers
{
    [FrontEndSessionExpire]
    public class DiamondSearchController : Controller
    {
        //
        // GET: /DiamondSearch/

        public ActionResult Index()
        {
            Session["SelectedDiamond"] = null;
            Session["SelectedDiamondSelection"] = null;
            ViewBag.SearchKeyWord = "";
            ViewBag.SearchBy = "lot";
            try
            {
                if (Request.QueryString["q"] != null)
                {
                    string strSearchBy = "";

                    if (Request.QueryString["s"] != null)
                    {
                        strSearchBy = Request.QueryString["q"];
                    }
                    ViewBag.SearchBy = strSearchBy;
                    ViewBag.SearchKeyWord = Request.QueryString["q"];
                    int totalRecords = 0;
                    DiamondHelper objDiamondHelper = new DiamondHelper();
                    DiamondModels model = new DiamondModels();
                    model.PageNumber = 1;

                    model.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPaging"]);
                    if (Request.Browser.IsMobileDevice)
                    {
                        model.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPagingMobile"]);
                    }
                    DataSet dsDiamonds = null;


                    model.Flag = 3;
                    model.LotNumber = Request.QueryString["q"];
                    dsDiamonds = objDiamondHelper.GetDiamondSearch(model);

                    ViewBag.SearchResult = GetSearchHtml("1", ref totalRecords, dsDiamonds, "", "","");
                }
            }
            catch { }



            Canturi.Models.BusinessHelper.Admin.SettingHelper objSettingHelper = new Canturi.Models.BusinessHelper.Admin.SettingHelper();
            DataTable dtLargestDiamondPercentage = objSettingHelper.GetSettings(2);
            //for carat size setting
            CaratSizeSettingHelper objCaratSizeSettingHelper = new CaratSizeSettingHelper();
            CaratSizeSettingModel objCaratSizeSettingModel = new CaratSizeSettingModel { Flag = 1 };
            objCaratSizeSettingModel = objCaratSizeSettingHelper.GetCaratSizeSettingById(objCaratSizeSettingModel);
            ViewBag.MinimumCaratSize = objCaratSizeSettingModel.MinimumCaratSize;
            ViewBag.MaximumCaratSize = objCaratSizeSettingModel.MaximumCaratSize;
            ViewBag.LargestDiamondPercentage = dtLargestDiamondPercentage.Rows[0]["SettingValue"];
            if (Request.Browser.IsMobileDevice)
            {
                return View("MobileSearch");
            }
            return View();
        }


        public JsonResult GetDiamondSearch(string diamondShape, string caratMin, string caratMax, string cutMin, string cutMax, string flourscenceMin, string flourscenceMax, string clarityMin, string clarityMax, string polishMin, string polishMax, string priceMin, string priceMax, string colorMin, string colorMax, string symmetryMin, string symmetryMax, string searchKeyWord, string searchBy, string eyeClean, string pgNo, string largePrice, string sortOrder, string sortByColumn)
        {
            string strMsg = "ok";
            string strData = "";
            int totalRecords = 0;
            try
            {
                DiamondHelper objDiamondHelper = new DiamondHelper();
                DiamondModels model = new DiamondModels();
                // model.sor
                model.Flag = 1;

                


                model.SortByColumn = sortByColumn;
                model.SortOrder = sortOrder;


                model.Shape = CommonData.GetShapeRange(diamondShape);


                model.CaratMin = caratMin;
                model.CaratMax = caratMax;

                model.Cut = CommonData.GetCutRange(cutMin, cutMax);

                //model.CutMin = CommonData.CutTypeList()[Convert.ToInt32(cutMin)].Value;
                //model.CutMax = CommonData.CutTypeList()[Convert.ToInt32(cutMax)].Value;


                model.Flourescence = CommonData.GetFlourescenceRange(flourscenceMin, flourscenceMax);

                //model.FlourescenceMin = CommonData.FlourescenceTypeList()[Convert.ToInt32(flourscenceMin)].Value;
                //model.FlourescenceMax = CommonData.FlourescenceTypeList()[Convert.ToInt32(flourscenceMax)].Value;
                model.Clartiy = CommonData.GetClartiyRange(clarityMin, clarityMax);

                if (4 > Convert.ToInt16(clarityMin))
                {
                    model.Clartiy = model.Clartiy + "," + CommonData.GetClartiyRange("5", "10");
                    model.ClartiyLeft = CommonData.GetClartiyRange(clarityMin, clarityMax);
                    model.ClartiyRight = CommonData.GetClartiyRange("5", "10");
                }


                model.ClartiyMin = clarityMin;
                model.ClartiyMax = clarityMax;

                model.Polish = CommonData.GetPolishRange(polishMin, polishMax);
                //model.PolishMin = CommonData.PolishTypeList()[Convert.ToInt32(polishMin)].Value;
                //model.PolishMax = CommonData.PolishTypeList()[Convert.ToInt32(polishMax)].Value;

                model.PriceMin = priceMin;
                model.PriceMax = priceMax;

                model.Color = CommonData.GetColorRange(colorMin, colorMax);
                //model.ColorMin = CommonData.ColorTypeList()[Convert.ToInt32(colorMin)].Value;
                //model.ColorMax = CommonData.ColorTypeList()[Convert.ToInt32(colorMax)].Value;

                model.Symmetry = CommonData.GetSymmetryRange(symmetryMin, symmetryMax);
                //model.SymmetryMin = CommonData.SymmetryTypeList()[Convert.ToInt32(symmetryMin)].Value;
                //model.SymmetryMax = CommonData.SymmetryTypeList()[Convert.ToInt32(symmetryMax)].Value;

                model.EyeClean = eyeClean;
                model.PageNumber = Convert.ToInt32(pgNo);

                model.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPaging"]);
                if (Request.Browser.IsMobileDevice)
                {
                    if (String.IsNullOrEmpty(largePrice))
                    {
                        model.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPagingMobile"]);
                    }
                    else
                    {
                        model.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPaging"]);
                    }

                }
                DataSet dsDiamonds = null;


                model.LargePrice = largePrice;

                model.SearchByData = "";
                try
                {
                    if (searchBy.TrimStart().TrimEnd().ToLower() == "code")
                    {
                        if (searchKeyWord.Trim().ToUpper() == "IN SALON")
                        {
                            searchKeyWord = "4";
                        }
                        int diamondCode = Convert.ToInt32(searchKeyWord.Trim());
                        model.SearchByData = diamondCode.ToString();

                        string[] validCodes = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14" };
                        if (!validCodes.Contains(model.SearchByData))
                        {
                            model.SearchByData = "";
                        }
                        searchKeyWord = "";
                    }
                }
                catch
                {

                }

                //if (!String.IsNullOrEmpty(model.LargePrice))
                //{
                //    model.CaratMin = "0.21";
                //    model.CaratMax = "22.74";
                //}
                if (String.IsNullOrEmpty(searchKeyWord))
                {
                    model.LotNumber = "";
                    model.Flag = 1;
                    dsDiamonds = objDiamondHelper.GetDiamondSearch1(model);
                    if (!string.IsNullOrEmpty(largePrice))
                    {
                        DataView dvDiamonds = dsDiamonds.Tables[0].DefaultView;
                        dvDiamonds.Sort = "" + model.SortByColumn + " " + model.SortOrder + "";
                        DataTable sortedDT = dvDiamonds.ToTable();
                        DataSet dsSortDiamonds = new DataSet();
                        dsSortDiamonds.Tables.Add(sortedDT);
                        strData = GetSearchHtml(pgNo, ref totalRecords, dsSortDiamonds, model.LargePrice, diamondShape, searchBy.TrimStart().TrimEnd().ToLower());
                    }
                    else
                    {
                        strData = GetSearchHtml(pgNo, ref totalRecords, dsDiamonds, model.LargePrice, diamondShape, searchBy.TrimStart().TrimEnd().ToLower());
                    }

                }
                else
                {

                    model.AdminUserId = UserSessionData.UserId;
                    model.Flag = 3;
                    model.LotNumber = searchKeyWord;
                    if (searchBy.TrimStart().TrimEnd().ToLower() == "cert")
                    {
                        model.LotNumber = "";
                        model.DiamondCertificate = searchKeyWord;
                    }
                    // model.LotNumber = searchKeyWord;
                    model.Currency = UserSessionData.Currency;
                    dsDiamonds = objDiamondHelper.GetDiamondSearch(model);
                    strData = GetSearchHtml(pgNo, ref totalRecords, dsDiamonds, model.LargePrice, "","");
                }


                //strData = GetSearchHtml(pgNo, ref totalRecords, dsDiamonds, model.LargePrice);

            }
            catch (Exception ex)
            {
                strMsg = "notok";
                strData = ConfigurationManager.AppSettings["NoSearchDataFound"];
                new AppError().LogMe(ex);
            }
            if (String.IsNullOrEmpty(strData))
            {
                strData = ConfigurationManager.AppSettings["NoSearchDataFound"];
            }
            return Json(new { msg = strMsg, data = strData, totalRecords = totalRecords });
        }

        private string GetSearchHtml(string pgNo, ref int totalRecords, DataSet dsDiamonds, string largePrice, string shape, string searchBy)
        {
            string strData = "";
            StringBuilder sbDiamonds = new StringBuilder();

            string[] strShapes = { "CUSHION", "PRINCESS", "RADIANT" };
            if (dsDiamonds.Tables.Count > 0)
            {
                if (dsDiamonds.Tables[0].Rows.Count != 0)
                {
                    sbDiamonds.Append("<div class=\"result-tablebox\">");
                    sbDiamonds.Append("<table class=\"result-table\">");
                    totalRecords = Convert.ToInt32(dsDiamonds.Tables[0].Rows[0]["TotalRecords"]);
                    if (!String.IsNullOrEmpty(largePrice))
                    {
                        if (totalRecords > 20)
                        {
                            totalRecords = 20;
                        }
                    }
                    sbDiamonds.Append("<thead>");
                    sbDiamonds.Append("<tr>");
                    sbDiamonds.Append("<th class=\"chk-box\">&nbsp;</th>");
                    sbDiamonds.Append("<th class=\"shapes\">shape</th>");
                    if (!String.IsNullOrEmpty(shape))
                    {

                        if (strShapes.Contains(shape.ToUpper()))
                        {
                            sbDiamonds.Append("<th ></th>");
                        }


                    }

                    sbDiamonds.Append("<th class=\"carat sorting\"><a href=\"javascript:void(0)\" onclick=\"FnSort('carat')\">carat</a> </th>");
                    sbDiamonds.Append("<th class=\"colour\">color</th>");
                    sbDiamonds.Append("<th class=\"clarity\">clarity</th>");

                    sbDiamonds.Append("<th class=\"cut\">cut</th>");
                    //if (!String.IsNullOrEmpty(shape))
                    //{

                    //    if (shape.ToUpper() == "ROUND")
                    //    {

                    //        sbDiamonds.Append("<th class=\"cut\">cut</th>");
                    //    }
                    //}
                    //else
                    //{
                    //    if(searchBy!="5" && String.IsNullOrEmpty(searchBy))
                    //    {
                    //        sbDiamonds.Append("<th class=\"cut\">cut</th>");
                    //    }

                    //}

                    sbDiamonds.Append("<th class=\"polish\">polish</th>");
                    sbDiamonds.Append("<th class=\"symmetry\">symmetry</th>");
                    //sbDiamonds.Append("<th class=\"flr\">FLUORESCENCE</th>");
                    sbDiamonds.Append("<th class=\"price sorting\"><a href=\"javascript:void(0)\" onclick=\"FnSort('amount')\">price</a></th>");
                    sbDiamonds.Append("<th class=\"price\">lot #</th>");

                    sbDiamonds.Append("<th class=\"view\">&nbsp;</th>");
                    sbDiamonds.Append("<th class=\"supplier-code\">&nbsp;</th>");
                    sbDiamonds.Append("<th>Video</th>");
                    sbDiamonds.Append(" </tr>");
                    sbDiamonds.Append(" </thead>");
                    sbDiamonds.Append("<tbody>");
                }

                // int count = 1;
                var AllowedSupplierIds = DiamondHelper.GetVideoAllowedSuppliers();

                foreach (DataRow item in dsDiamonds.Tables[0].Rows)
                {
                    sbDiamonds.Append("<tr>");
                    sbDiamonds.Append("<td class=\"chk-box\">");
                    //sbDiamonds.Append("<input type=\"checkbox\" id=\"c" + count + "\" name=\"cc\">");
                    //count++;

                    bool IsAlreadySelected = false;

                    IsAlreadySelected = FnGetAlreadySelectedDiamonds(item, IsAlreadySelected);

                    if (IsAlreadySelected)
                    {
                        sbDiamonds.Append("<input type=\"checkbox\" checked=\"checked\" id=\"chkDiamond" + item["DiamondID"].ToString() + "\" name=\"cc\" onclick=\"BindSelection('chkDiamond" + item["DiamondID"].ToString() + "','" + item["DiamondID"].ToString() + "','" + item["TableType"].ToString() + "')\">");
                    }
                    else
                    {
                        sbDiamonds.Append("<input type=\"checkbox\" id=\"chkDiamond" + item["DiamondID"].ToString() + "\" name=\"cc\" onclick=\"BindSelection('chkDiamond" + item["DiamondID"].ToString() + "','" + item["DiamondID"].ToString() + "','" + item["TableType"].ToString() + "')\">");
                    }
                    sbDiamonds.Append("<label for=\"chkDiamond" + item["DiamondID"].ToString() + "\"><span></span></label>");
                    sbDiamonds.Append("</td>");
                    sbDiamonds.Append("<td class=\"shapes\">");
                    sbDiamonds.Append("<span>");

                    string strDiamondImg = Url.Content("~/Content/FrontEnd/images/Diamonds/small/no-image.png");
                    if (System.IO.File.Exists(Server.MapPath("~/Content/FrontEnd/images/Diamonds/small/") + item["Shape"].ToString().ToLower().Trim() + ".png"))
                    {
                        strDiamondImg = Url.Content("~/Content/FrontEnd/images/Diamonds/small/") + item["Shape"].ToString().ToLower().Trim() + ".png";
                    }
                    sbDiamonds.Append("<img src=\"" + strDiamondImg + "\" align=\"top\" title=\"" + item["Shape"].ToString().ToLower().Trim() + "\" />");
                    sbDiamonds.Append("</span>");
                    //if (item["Shape"].ToString().ToUpper().Contains("CUSHION"))
                    //{
                    //    sbDiamonds.Append("<small> CUSHION</small>");
                    //}
                    //else
                    //{
                    //    sbDiamonds.Append("<small> " + item["Shape"].ToString().ToUpper() + "</small>");
                    //}

                    string DiamondShape = item["Shape"].ToString().ToLower().Trim();
                    if (DiamondShape.ToUpper().Contains("CUSHION") || DiamondShape.ToUpper().Contains("PRINCESS") || DiamondShape.ToUpper().Contains("RADIANT"))
                    {
                        string ratio;
                        if (!String.IsNullOrEmpty(item["Ratio"].ToString()))
                        {
                            ratio = item["Ratio"].ToString();
                        }
                        else
                        {
                            ratio = CommonData.GetRatio(item["Measurements"].ToString().Replace("*", "x").Replace("-", "x"));
                        }
                        if (!String.IsNullOrEmpty(ratio))
                        {
                            if (Convert.ToDecimal(ratio) >= 1 && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.059))
                            {
                                sbDiamonds.Append("<small>" + item["Shape"].ToString().ToUpper() + " SQUARE</small>");
                            }
                            else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.060) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.088))
                            {
                                sbDiamonds.Append("<small>" + item["Shape"].ToString().ToUpper() + " SQUARISH</small>");
                            }
                            else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.089) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.900))
                            {
                                sbDiamonds.Append("<small>" + item["Shape"].ToString().ToUpper() + " ELONGATED</small>");
                            }
                            else
                            {
                                sbDiamonds.Append("<small>" + item["Shape"].ToString().ToUpper() + "</small>");

                            }
                        }
                        else
                        {
                            sbDiamonds.Append("<small>" + item["Shape"].ToString().ToUpper() + "</small>");

                        }


                    }
                    else
                    {
                        sbDiamonds.Append("<small> " + item["Shape"].ToString().ToUpper() + "</small>");
                    }

                    sbDiamonds.Append("</td>");

                    if (!String.IsNullOrEmpty(shape))
                    {
                        if (strShapes.Contains(shape.ToUpper()))
                        {
                            string ratio = "0";
                            if (!String.IsNullOrEmpty(item["Ratio"].ToString()))
                            {
                                ratio = item["Ratio"].ToString();
                            }
                            else
                            {
                                ratio = CommonData.GetRatio(item["Measurements"].ToString().Replace("*", "x").Replace("-", "x"));
                            }
                            if (!String.IsNullOrEmpty(ratio))
                            {
                                if (Convert.ToDecimal(ratio) >= 1 && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.059))
                                {
                                    sbDiamonds.Append("<td class='SQUARE'>SQUARE</td>");
                                }
                                else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.060) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.088))
                                {
                                    sbDiamonds.Append("<td  class='SQUARE'>SQUARISH </td>");
                                }
                                else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.089) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.900))
                                {
                                    sbDiamonds.Append("<td  class='SQUARE'>ELONGATED </td>");
                                }
                                else
                                {
                                    sbDiamonds.Append("<td  class='SQUARE'> </td>");
                                }
                            }
                            else
                            {
                                sbDiamonds.Append("<td  class='SQUARE'> </td>");
                            }


                        }
                    }

                    decimal tmpCarat = 0;
                    try
                    {
                        tmpCarat = Convert.ToDecimal(item["Carat"].ToString());
                    }
                    catch { }
                    // sbDiamonds.Append("<td ></td>");
                    sbDiamonds.Append("<td class=\"carat\">" + CommonData.FormatDecimal(tmpCarat) + "</td>");
                    sbDiamonds.Append("<td class=\"colour\">" + item["Color"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                    sbDiamonds.Append("<td class=\"clarity\">" + item["Clarity"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");

                    //ALL diamonds except for round, please do not display anything in the CUT field – make it a blank entry.
                    if (item["Shape"].ToString().ToLower().Trim() == "round")
                    {
                        sbDiamonds.Append("<td class=\"cut\">" + item["Cut"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                    }
                    else
                    {
                        sbDiamonds.Append("<td class=\"cut\">&nbsp;</td>");
                        //if (!String.IsNullOrEmpty(shape))
                        //{

                        //    if (shape.ToUpper() == "ROUND")
                        //    {
                        //        sbDiamonds.Append("<td class=\"cut\">" + item["Cut"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                        //    }
                        //    //else
                        //    //{
                        //    //    sbDiamonds.Append("<td class=\"cut\">&nbsp;</td>");
                        //    //}


                        //}
                        //else
                        //{
                        //    sbDiamonds.Append("<td class=\"cut\">&nbsp;</td>");
                        //}
                    }
                    //else
                    //{
                    //    if (!Request.Browser.IsMobileDevice)
                    //    {
                    //        sbDiamonds.Append("<td class=\"cut\">&nbsp;</td>");
                    //    }
                    //}



                    sbDiamonds.Append("<td class=\"polish\">" + item["Polish"].ToString().ToUpper() + "</td>");
                    sbDiamonds.Append("<td class=\"symmetry\">" + item["Symmetry"].ToString().ToUpper() + "</td>");
                    //if (String.IsNullOrEmpty(item["Fluorescence"].ToString()))
                    //{
                    //    sbDiamonds.Append("<td class=\"flr\">CHECK CERTIFICATE</td>");
                    //}
                    //else if (item["Fluorescence"].ToString().ToUpper() == "SLIGHT")
                    //{
                    //    sbDiamonds.Append("<td class=\"flr\">FAINT</td>");
                    //}
                    //else if (item["Fluorescence"].ToString().ToUpper() == "VERY SLIGHT")
                    //{
                    //    sbDiamonds.Append("<td class=\"flr\">VERY FAINT</td>");
                    //}
                    //else
                    //{
                    //    sbDiamonds.Append("<td class=\"flr\">" + item["Fluorescence"].ToString().ToUpper() + "</td>");
                    //}

                    if (Request.Browser.IsMobileDevice)
                    {
                        sbDiamonds.Append("<td class=\"measurements\">" + item["Measurements"].ToString().Replace("*", "x").Replace("-", "x") + "</td>");

                        if (item["Shape"].ToString().ToUpper().Trim().Contains("ROUND"))
                        {
                            sbDiamonds.Append("<td class=\"ratio\">" + item["Ratio"].ToString() + "</td>");
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(item["Ratio"].ToString()))
                            {
                                sbDiamonds.Append("<td class=\"ratio\">" + item["Ratio"].ToString() + "</td>");
                            }
                            else
                            {
                                sbDiamonds.Append("<td class=\"ratio\">" + CommonData.GetRatio(item["Measurements"].ToString().Replace("*", "x").Replace("-", "x")) + "</td>");
                            }

                        }



                    }


                    sbDiamonds.Append("<td class=\"price\">");

                    if (UserSessionData.Currency.ToLower() == "aud")
                        sbDiamonds.Append("$");
                    else if (UserSessionData.Currency.ToLower() == "euro")
                        sbDiamonds.Append("€");
                    else if (UserSessionData.Currency.ToLower() == "usd")
                        sbDiamonds.Append("USD $");
                    else
                        sbDiamonds.Append("$");



                    string strUrl = "1";

                    if (item["TableType"].ToString().ToUpper() == "RAPNET")
                    {
                        strUrl = "1";
                        string strSupplier = item["Supplier"].ToString().ToUpper();

                        if (strSupplier.ToUpper() == "N.E.R. JEWELRY INC.")
                        {
                            strUrl = "12";
                        }
                        else if (strSupplier.ToUpper() == "OFER MIZRAHI DIAMONDS, INC.")
                        {
                            strUrl = "11";
                        }
                    }
                    else if (item["TableType"].ToString().ToUpper() == "JBBROS")
                    { strUrl = "2"; }
                    else if (item["TableType"].ToString().ToUpper() == "VENUS")
                    { strUrl = "3"; }
                    else if (item["TableType"].ToString().ToUpper() == "CANTURI")
                    {
                        strUrl = "4";
                    }
                    //else if (item["TableType"].ToString().ToUpper() == "PANACHE")
                    //{
                    //    strUrl = "5";
                    //}
                    else if (item["TableType"].ToString().ToUpper() == "YDVASH")
                    {
                        strUrl = "5";
                    }
                    else if (item["TableType"].ToString().ToUpper() == "HARIKRISHNA")
                    {
                        strUrl = "6";
                    }
                    else if (item["TableType"].ToString().ToUpper() == "FINESTAR")
                    {
                        strUrl = "7";
                    }
                    else if (item["TableType"].ToString().ToUpper() == "CDINESH")
                    {
                        strUrl = "8";
                    }
                    //Replaced by Prashant on 20 Nov 2020
                    //else if (item["TableType"].ToString().ToUpper() == "KAPUGEMS")
                    //{
                    //    strUrl = "9";
                    //}
                    else if (item["TableType"].ToString().ToUpper() == "GLOWSTAR")
                    {
                        strUrl = "9";
                    }
                    else if (item["TableType"].ToString().ToUpper() == "SRK")
                    {
                        strUrl = "10";
                    }
                    else if (item["TableType"].ToString().ToUpper() == "KIRANGEMS")
                    {
                        strUrl = "13";
                    }
                    else if (item["TableType"].ToString().ToUpper() == "DHARM")
                    {
                        strUrl = "14";
                    }



                    sbDiamonds.Append(item["Amount"].ToString() + "</td>");
                    sbDiamonds.Append("<td class=\"lot\">" + item["LotNumber"].ToString().ToUpper() + "</td>");
                    sbDiamonds.Append("<td class=\"view\">");

                    sbDiamonds.Append("<a href=\"" + Url.Content("~/Diamond/") + strUrl + "/" + item["DiamondID"].ToString() + "\" class=\"list-view-more\">More</a>");
                    sbDiamonds.Append("<input type=\"submit\" name=\"\" value=\"More\" />");
                    sbDiamonds.Append("<div id=\"listing-tab-box\">");
                    sbDiamonds.Append("<div class=\"list-tbox\">");
                    sbDiamonds.Append("<span>&nbsp;</span>");
                    sbDiamonds.Append("<div class=\"cl\"></div>");
                    sbDiamonds.Append("<ul>");
                    //sbDiamonds.Append("<li><strong>LOT# :" + item["LotNumber"].ToString() + "</strong></li>");
                    sbDiamonds.Append("<li><strong>MEASUREMENTS:  " + item["Measurements"].ToString().Replace("*", "x") + "</strong></li>");
                    if (item["TableType"].ToString() == "Canturi")
                    {
                        sbDiamonds.Append("<li><strong>CERT#:  " + Path.GetFileNameWithoutExtension(item["DiamondCertificate"].ToString()) + "</strong></li>");
                    }
                    else
                    {
                        sbDiamonds.Append("<li><strong>CERT#:  " + item["DiamondCertificate"].ToString() + "</strong></li>");
                    }


                    if (item["Shape"].ToString().ToUpper().Trim().Contains("ROUND"))
                    {
                        sbDiamonds.Append("<li><strong>RATIO:  " + item["Ratio"].ToString() + "</strong></li>");
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(item["Ratio"].ToString()))
                        {
                            sbDiamonds.Append("<li><strong>RATIO:  " + item["Ratio"].ToString() + "</strong></li>");
                        }
                        else
                        {
                            sbDiamonds.Append("<li><strong>RATIO:  " + CommonData.GetRatio(item["Measurements"].ToString().Replace("*", "x").Replace("-", "x")) + "</strong></li>");

                        }

                    }


                    //sbDiamonds.Append("<li><strong>RATIO:  " + item["Ratio"].ToString() + "</strong></li>");
                    sbDiamonds.Append("</ul>");
                    sbDiamonds.Append("<div class=\"cl\"></div>");
                    sbDiamonds.Append("</div>");
                    sbDiamonds.Append("</div>");
                    sbDiamonds.Append("</td>");
                    sbDiamonds.Append("<td class=\"supplier-code  " + (strUrl == "4" ? "on-hand-diamond" : "") + "\">" + (strUrl == "4" ? "IN SALON" : strUrl) + "</td>");

                    bool ShowHideVideo = DiamondHelper.ShowHideDiamondVideo(strUrl, AllowedSupplierIds);

                    if (!String.IsNullOrEmpty(item["VideoURL"].ToString()) && ShowHideVideo)
                    {
                        sbDiamonds.Append("<td><img src=\"Images/video-icon.png\" align=\"top\" style='height:18px;' title=\"Video available\" /></td>");
                    }
                    else
                    {
                        sbDiamonds.Append("<td><span>&nbsp;</span></td>");
                    }
                    sbDiamonds.Append("</tr>");

                }
                if (dsDiamonds.Tables[0].Rows.Count != 0)
                {
                    sbDiamonds.Append("</tbody>");
                    sbDiamonds.Append(" </table>");
                    sbDiamonds.Append("<div class=\"cl\"></div>");
                    sbDiamonds.Append("</div>");
                }
            }
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPaging"]);
            if (Request.Browser.IsMobileDevice)
            {
                //pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPagingMobile"]);
                if (String.IsNullOrEmpty(largePrice))
                {
                    pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPagingMobile"]);
                }
                else
                {
                    pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPaging"]);
                }

            }
            strData = sbDiamonds.ToString() + PagerHelper.DisplayPaging(totalRecords, pageSize, Convert.ToInt32(pgNo), "", 1);
            return strData;
        }

        private bool FnGetAlreadySelectedDiamonds(DataRow item, bool IsAlreadySelected)
        {
            if (Session["SelectedDiamond"] != null)
            {
                string strSelectedDiamond = Session["SelectedDiamond"].ToString();
                if (strSelectedDiamond.Contains("~"))
                {
                    string[] arrayDiamond = strSelectedDiamond.Split('~');
                    foreach (string itemDiamond in arrayDiamond)
                    {
                        if (!String.IsNullOrEmpty(itemDiamond))
                        {
                            string tableType = item["TableType"].ToString().ToLower();
                            if (tableType == "jbbros")
                            {
                                if (itemDiamond.ToLower().Replace("jbbros", "") == item["DiamondID"].ToString().ToLower())
                                {
                                    IsAlreadySelected = true;
                                }
                            }

                            if (tableType == "rapnet")
                            {
                                if (itemDiamond.ToLower().Replace("rapnet", "") == item["DiamondID"].ToString().ToLower())
                                {
                                    IsAlreadySelected = true;
                                }
                            }
                            if (tableType == "canturi")
                            {
                                if (itemDiamond.ToLower().Replace("canturi", "") == item["DiamondID"].ToString().ToLower())
                                {
                                    IsAlreadySelected = true;
                                }
                            }
                            if (tableType == "venus")
                            {
                                if (itemDiamond.ToLower().Replace("venus", "") == item["DiamondID"].ToString().ToLower())
                                {
                                    IsAlreadySelected = true;
                                }
                            }
                            if (tableType.ToLower() == "panache")
                            {
                                if (itemDiamond.ToLower().Replace("venus", "") == item["DiamondID"].ToString().ToLower())
                                {
                                    IsAlreadySelected = true;
                                }
                            }
                            if (tableType.ToLower() == "harikrishna")
                            {
                                if (itemDiamond.ToLower().Replace("venus", "") == item["DiamondID"].ToString().ToLower())
                                {
                                    IsAlreadySelected = true;
                                }
                            }

                            if (tableType.ToUpper() == "FINESTAR")
                            {
                                if (itemDiamond.ToLower().Replace("finestar", "") == item["DiamondID"].ToString().ToLower())
                                {
                                    IsAlreadySelected = true;
                                }
                            }

                            if (tableType.ToUpper() == "SRK")
                            {
                                if (itemDiamond.ToLower().Replace("srk", "") == item["DiamondID"].ToString().ToLower())
                                {
                                    IsAlreadySelected = true;
                                }
                            }

                            if (tableType.ToUpper() == "CDINESH")
                            {
                                if (itemDiamond.ToLower().Replace("cdinesh", "") == item["DiamondID"].ToString().ToLower())
                                {
                                    IsAlreadySelected = true;
                                }
                            }

                            if (tableType.ToUpper() == "KAPUGEMS")
                            {
                                if (itemDiamond.ToLower().Replace("kapugems", "") == item["DiamondID"].ToString().ToLower())
                                {
                                    IsAlreadySelected = true;
                                }
                            }

                            if (tableType.ToUpper() == "KIRANGEMS")
                            {
                                if (itemDiamond.ToLower().Replace("kirangems", "") == item["DiamondID"].ToString().ToLower())
                                {
                                    IsAlreadySelected = true;
                                }
                            }

                            if (tableType.ToUpper() == "SUNRISE")
                            {
                                if (itemDiamond.ToLower().Replace("sunrise", "") == item["DiamondID"].ToString().ToLower())
                                {
                                    IsAlreadySelected = true;
                                }
                            }

                        }
                    }
                }
            }
            return IsAlreadySelected;
        }


        public JsonResult GetDiamondBySelection(string dimondNumber, string type, string isChecked)
        {
            string strMsg = "ok";
            string strData = "";
            int totalRecords = 0;
            try
            {
                string strSelectedDiamond = "";

                if (Session["SelectedDiamond"] == null)
                {
                    if (!String.IsNullOrEmpty(dimondNumber))
                    {
                        Session["SelectedDiamond"] = dimondNumber + type + "~";
                        strSelectedDiamond = Session["SelectedDiamond"].ToString();
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(dimondNumber))
                    {
                        if (isChecked.ToLower() == "true")
                        {

                            Session["SelectedDiamond"] = Session["SelectedDiamond"] + dimondNumber + type + "~";
                        }
                        else
                        {
                            Session["SelectedDiamond"] = Session["SelectedDiamond"].ToString().Replace(dimondNumber + type + "~", "");
                        }
                    }
                    strSelectedDiamond = Session["SelectedDiamond"].ToString();
                }

                string strRapnetDiamondIds = "";
                string strCanturiDiamondIds = "";
                string strJBBrosDiamondIds = "";
                string strVenusDiamondIds = "";
                string strPanacheDiamondIds = "";
                string strHariKrishnaDiamondIds = "";
                string strFineStarDiamondIds = "";
                string strCDINESHDiamondIds = "";
                string strKapuGemsDiamondIds = "";
                string strSunriseDiamondIds = "";
                string strSrkDiamondIds = "";
                string strKiranGemsDiamondIds = "";

                //Newly Added Suppliers // 
                string strDharmDiamondIds = "";
                string strYDVashDiamondIds = "";
                //Newly Added Suppliers // 


                if (strSelectedDiamond.Contains("~"))
                {
                    string[] arrayDiamond = strSelectedDiamond.Split('~');
                    foreach (string item in arrayDiamond)
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            if (item.ToLower().Contains("rapnet"))
                            {
                                strRapnetDiamondIds += item.ToLower().Replace("rapnet", "") + ",";
                            }
                            else if (item.ToLower().Contains("canturi"))
                            {
                                strCanturiDiamondIds += item.ToLower().Replace("canturi", "") + ",";
                            }
                            else if (item.ToLower().Contains("jbbros"))
                            {
                                strJBBrosDiamondIds += item.ToLower().Replace("jbbros", "") + ",";
                            }
                            else if (item.ToLower().Contains("venus"))
                            {
                                strVenusDiamondIds += item.ToLower().Replace("venus", "") + ",";
                            }
                            //else if (item.ToLower().Contains("panache"))
                            //{
                            //    strPanacheDiamondIds += item.ToLower().Replace("panache", "") + ",";
                            //}
                            else if (item.ToLower().Contains("harikrishna"))
                            {
                                strHariKrishnaDiamondIds += item.ToLower().Replace("harikrishna", "") + ",";
                            }
                            else if (item.ToLower().Contains("finestar"))
                            {
                                strFineStarDiamondIds += item.ToLower().Replace("finestar", "") + ",";
                            }
                            else if (item.ToLower().Contains("cdinesh"))
                            {
                                strCDINESHDiamondIds += item.ToLower().Replace("cdinesh", "") + ",";
                            }
                            else if (item.ToLower().Contains("kapugems"))
                            {
                                strKapuGemsDiamondIds += item.ToLower().Replace("kapugems", "") + ",";
                            }
                            else if (item.ToLower().Contains("kirangems"))
                            {
                                strKiranGemsDiamondIds += item.ToLower().Replace("kirangems", "") + ",";
                            }
                            else if (item.ToLower().Contains("sunrise"))
                            {
                                strSunriseDiamondIds += item.ToLower().Replace("sunrise", "") + ",";
                            }
                            else if (item.ToLower().Contains("srk")) // Add this for Client SRK
                            {
                                strSrkDiamondIds += item.ToLower().Replace("srk", "") + ",";
                            }

                            //Newly Added Suppliers // 
                            else if (item.ToLower().Contains("dharm"))
                            {
                                strDharmDiamondIds += item.ToLower().Replace("dharm", "") + ",";
                            }
                            else if (item.ToLower().Contains("ydvash"))
                            {
                                strYDVashDiamondIds += item.ToLower().Replace("ydvash", "") + ",";
                            }
                            //Newly Added Suppliers // 
                        }
                    }
                }


                string sqlQuery = String.Empty;
                if (!String.IsNullOrEmpty(strCanturiDiamondIds))
                {
                    if (strCanturiDiamondIds.EndsWith(","))
                    {
                        strCanturiDiamondIds = strCanturiDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = "SELECT " + DiamondHelper.GetCanturiCoulmns() + ",'Canturi' AS TableType,dbo.Udf_GetCanturiPrice(Price,'" + UserSessionData.Currency.ToUpper() + "','CANTURI',DiamondID) AS Amount FROM tblCanturiDiamond WHERE DiamondID IN (" + strCanturiDiamondIds + ")";
                }
                if (!String.IsNullOrEmpty(strJBBrosDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strJBBrosDiamondIds.EndsWith(","))
                    {
                        strJBBrosDiamondIds = strJBBrosDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'JBBros' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblJBBrosDiamond WHERE DiamondID IN (" + strJBBrosDiamondIds + ")";
                }

                //if (!String.IsNullOrEmpty(strPanacheDiamondIds))
                //{
                //    if (!String.IsNullOrEmpty(sqlQuery))
                //    {
                //        sqlQuery = sqlQuery + "union all ";
                //    }
                //    if (strPanacheDiamondIds.EndsWith(","))
                //    {
                //        strPanacheDiamondIds = strPanacheDiamondIds.TrimEnd(',');
                //    }
                //    sqlQuery = sqlQuery + "SELECT *,'Panache' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblPanacheDiamond WHERE DiamondID IN (" + strPanacheDiamondIds + ")";
                //}
                if (!String.IsNullOrEmpty(strHariKrishnaDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strHariKrishnaDiamondIds.EndsWith(","))
                    {
                        strHariKrishnaDiamondIds = strHariKrishnaDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'HariKrishna' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblHariKrishnaDiamond WHERE DiamondID IN (" + strHariKrishnaDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(strFineStarDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strFineStarDiamondIds.EndsWith(","))
                    {
                        strFineStarDiamondIds = strFineStarDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'FineStar' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblFineStarDiamond WHERE DiamondID IN (" + strFineStarDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(strCDINESHDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strCDINESHDiamondIds.EndsWith(","))
                    {
                        strCDINESHDiamondIds = strCDINESHDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'CDINESH' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblCDINESHDiamond WHERE DiamondID IN (" + strCDINESHDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(strRapnetDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strRapnetDiamondIds.EndsWith(","))
                    {
                        strRapnetDiamondIds = strRapnetDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'Rapnet' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblRapnetDiamond " +
                                        "WHERE DiamondID IN (" + strRapnetDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(strKapuGemsDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strKapuGemsDiamondIds.EndsWith(","))
                    {
                        strKapuGemsDiamondIds = strKapuGemsDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'KAPUGEMS' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKapuGemsDiamond " +
                                        "WHERE DiamondID IN (" + strKapuGemsDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(strKiranGemsDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strKiranGemsDiamondIds.EndsWith(","))
                    {
                        strKiranGemsDiamondIds = strKiranGemsDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'KIRANGEMS' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKiranGemsDiamond " +
                                        "WHERE DiamondID IN (" + strKiranGemsDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(strSunriseDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strSunriseDiamondIds.EndsWith(","))
                    {
                        strSunriseDiamondIds = strSunriseDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'SUNRISE' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblSunriseDiamond " +
                                        "WHERE DiamondID IN (" + strSunriseDiamondIds + ")";
                }

                // Add this for Client SRK

                if (!String.IsNullOrEmpty(strSrkDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strSrkDiamondIds.EndsWith(","))
                    {
                        strSrkDiamondIds = strSrkDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'SRK' AS TableType,dbo.Udf_GetPriceCalculation(Price,'" + UserSessionData.Currency.ToUpper() + "',Carat) AS Amount FROM tblSrkDiamond " +
                                        "WHERE DiamondID IN (" + strSrkDiamondIds + ")";
                }


                if (!String.IsNullOrEmpty(strVenusDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strVenusDiamondIds.EndsWith(","))
                    {
                        strVenusDiamondIds = strVenusDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'Venus' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblVenusJewelsDiamond  " +
                                        "WHERE DiamondID IN (" + strVenusDiamondIds + ")";
                }

                //Newly Added Suppliers //
                if (!String.IsNullOrEmpty(strDharmDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strDharmDiamondIds.EndsWith(","))
                    {
                        strDharmDiamondIds = strDharmDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'Dharm' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblDharmDiamond  " +
                                        "WHERE DiamondID IN (" + strDharmDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(strYDVashDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strYDVashDiamondIds.EndsWith(","))
                    {
                        strYDVashDiamondIds = strYDVashDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'YDVash' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblYDVashDiamond  " +
                                        "WHERE DiamondID IN (" + strYDVashDiamondIds + ")";
                }
                //Newly Added Suppliers //


                if (!String.IsNullOrEmpty(sqlQuery))
                {
                    DiamondHelper objDiamondHelper = new DiamondHelper();
                    DataSet ds = objDiamondHelper.GetDiamondBySelection("SELECT * FROM (" + sqlQuery + ") T1  ORDER BY Amount,Shape");

                    StringBuilder sbDiamond = new StringBuilder();
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            var AllowedSupplierIds = DiamondHelper.GetVideoAllowedSuppliers();

                            sbDiamond.Append("<p class=\"result-view\">View selected</p>");

                            sbDiamond.Append("<div class=\"result-tablebox\">");
                            sbDiamond.Append("<table class=\"result-table\">");
                            sbDiamond.Append("<thead>");
                            sbDiamond.Append("<tr>");
                            sbDiamond.Append("<th class=\"chk-box\">&nbsp;</th>");
                            sbDiamond.Append("<th class=\"shapes\">shape</th>");

                            sbDiamond.Append("<th class=\"carat\">carat</th>");
                            sbDiamond.Append("<th class=\"colour\">color</th>");
                            sbDiamond.Append("<th class=\"clarity\">clarity</th>");
                            sbDiamond.Append("<th class=\"cut\">cut</th>");
                            sbDiamond.Append("<th class=\"polish\">polish</th>");
                            sbDiamond.Append("<th class=\"symmetry\">symmetry</th>");
                            //sbDiamond.Append("<th class=\"flr\">FLUORESCENCE</th>");
                            sbDiamond.Append("<th class=\"price\">price</th>");
                            sbDiamond.Append("<th class=\"lot\">lot #</th>");
                            sbDiamond.Append("<th class=\"view\">&nbsp;</th>");
                            sbDiamond.Append("<th class=\"supplier-code\">&nbsp;</th>");
                            sbDiamond.Append("<th>Video</th>");
                            sbDiamond.Append("</tr>");
                            sbDiamond.Append("</thead>");

                            sbDiamond.Append("<tbody>");
                            foreach (DataRow item in ds.Tables[0].Rows)
                            {
                                sbDiamond.Append("<tr>");
                                sbDiamond.Append("<td class=\"chk-box\">");
                                //sbDiamond.Append("<input type=\"checkbox\" id=\"chkSelectedDiamond" + item["DiamondID"].ToString() + "\" name=\"cc\" onclick=\"FnSelecedDiamond('chkSelectedDiamond" + item["DiamondID"].ToString() + "','" + item["DiamondID"].ToString() + "','" + item["TableType"].ToString() + "')\">");
                                bool IsAlreadySelected = false;

                                IsAlreadySelected = FnGetSelectionAlreadySelectedDiamonds(item, IsAlreadySelected);

                                if (IsAlreadySelected)
                                {
                                    sbDiamond.Append("<input type=\"checkbox\" checked=\"checked\" id=\"chkSelectedDiamond" + item["DiamondID"].ToString() + "\" name=\"cc\" onclick=\"FnSelecedDiamond('chkSelectedDiamond" + item["DiamondID"].ToString() + "','" + item["DiamondID"].ToString() + "','" + item["TableType"].ToString() + "')\">");
                                }
                                else
                                {
                                    sbDiamond.Append("<input type=\"checkbox\" id=\"chkSelectedDiamond" + item["DiamondID"].ToString() + "\" name=\"cc\" onclick=\"FnSelecedDiamond('chkSelectedDiamond" + item["DiamondID"].ToString() + "','" + item["DiamondID"].ToString() + "','" + item["TableType"].ToString() + "')\">");
                                }
                                sbDiamond.Append("<label for=\"chkSelectedDiamond" + item["DiamondID"].ToString() + "\"><span></span></label>");

                                //sbDiamond.Append("<input type=\"checkbox\" id=\"c1\" name=\"cc\">");
                                //sbDiamond.Append("<label for=\"c1\"><span></span></label>");
                                sbDiamond.Append("</td>");
                                sbDiamond.Append("<td class=\"shapes\">");
                                sbDiamond.Append("<span>");

                                string strDiamondImg = Url.Content("~/Content/FrontEnd/images/Diamonds/small/no-image.png");
                                string shape = item["Shape"].ToString().ToLower().Trim();
                                if (System.IO.File.Exists(Server.MapPath("~/Content/FrontEnd/images/Diamonds/small/") + item["Shape"].ToString().ToLower().Trim() + ".png"))
                                {
                                    strDiamondImg = Url.Content("~/Content/FrontEnd/images/Diamonds/small/") + item["Shape"].ToString().ToLower().Trim() + ".png";
                                }
                                sbDiamond.Append("<img src=\"" + strDiamondImg + "\" align=\"top\" title=\"" + item["Shape"].ToString().Trim() + "\" />");
                                sbDiamond.Append("</span>");

                                if (shape.ToUpper().Contains("CUSHION") || shape.ToUpper().Contains("PRINCESS") || shape.ToUpper().Contains("RADIANT"))
                                {
                                    string ratio = "0";
                                    if (!String.IsNullOrEmpty(item["Ratio"].ToString()))
                                    {
                                        ratio = item["Ratio"].ToString();
                                    }
                                    else
                                    {
                                        ratio = CommonData.GetRatio(item["Measurements"].ToString().Replace("*", "x").Replace("-", "x"));
                                    }
                                    if (!String.IsNullOrEmpty(ratio))
                                    {
                                        if (Convert.ToDecimal(ratio) >= 1 && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.059))
                                        {
                                            sbDiamond.Append("<small>" + item["Shape"].ToString().ToUpper() + " SQUARE</small>");
                                        }
                                        else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.060) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.088))
                                        {
                                            sbDiamond.Append("<small>" + item["Shape"].ToString().ToUpper() + " SQUARISH</small>");
                                        }
                                        else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.089) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.900))
                                        {
                                            sbDiamond.Append("<small>" + item["Shape"].ToString().ToUpper() + " ELONGATED</small>");
                                        }
                                        else
                                        {
                                            sbDiamond.Append("<small>" + item["Shape"].ToString().ToUpper() + "</small>");

                                        }
                                    }
                                    else
                                    {
                                        sbDiamond.Append("<small>" + item["Shape"].ToString().ToUpper() + "</small>");
                                    }

                                }



                                //if (item["Shape"].ToString().ToUpper().Contains("CUSHION"))
                                //{
                                //    sbDiamond.Append("<small> CUSHION</small>");
                                //}
                                else
                                {
                                    sbDiamond.Append("<small> " + item["Shape"].ToString().ToUpper() + "</small>");
                                }

                                //sbDiamond.Append("<small> " + item["Shape"].ToString().ToUpper() + "</small>");
                                sbDiamond.Append("</td>");
                                decimal tmpCarat = 0;
                                try
                                {
                                    tmpCarat = Convert.ToDecimal(item["Carat"].ToString());
                                }
                                catch { }

                                sbDiamond.Append("<td class=\"carat\">" + CommonData.FormatDecimal(tmpCarat) + "</td>");

                                //sbDiamond.Append("<td class=\"carat\">" + item["Carat"].ToString() + "</td>");
                                sbDiamond.Append("<td class=\"colour\">" + item["Color"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                                sbDiamond.Append("<td class=\"clarity\">" + item["Clarity"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                                //ALL diamonds except for round, please do not display anything in the CUT field – make it a blank entry.
                                if (item["Shape"].ToString().ToLower().Trim() == "round")
                                {
                                    sbDiamond.Append("<td class=\"cut\">" + item["Cut"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                                }
                                else
                                {
                                    sbDiamond.Append("<td class=\"cut\">&nbsp;</td>");
                                }

                                //sbDiamond.Append("<td class=\"cut\">" + item["Cut"].ToString() + "</td>");
                                sbDiamond.Append("<td class=\"polish\">" + item["Polish"].ToString().ToUpper() + "</td>");
                                sbDiamond.Append("<td class=\"symmetry\">" + item["Symmetry"].ToString().ToUpper() + "</td>");
                                //if (String.IsNullOrEmpty(item["Fluorescence"].ToString()))
                                //{
                                //    sbDiamond.Append("<td class=\"flr\">CHECK CERTIFICATE</td>");
                                //}
                                //else if (item["Fluorescence"].ToString().ToUpper() == "SLIGHT")
                                //{
                                //    sbDiamond.Append("<td class=\"flr\">FAINT</td>");
                                //}
                                //else if (item["Fluorescence"].ToString().ToUpper() == "VERY SLIGHT")
                                //{
                                //    sbDiamond.Append("<td class=\"flr\">VERY FAINT</td>");
                                //}
                                //else
                                //{
                                //    sbDiamond.Append("<td class=\"flr\">" + item["Fluorescence"].ToString().ToUpper() + "</td>");
                                //}
                                //sbDiamond.Append("<td class=\"flr\">" + item["Fluorescence"].ToString().ToUpper() + "</td>");
                                if (Request.Browser.IsMobileDevice)
                                {
                                    sbDiamond.Append("<td class=\"measurements\">" + item["Measurements"].ToString().Replace("*", "x") + "</td>");


                                    if (item["Shape"].ToString().ToUpper().Contains("ROUND"))
                                    {
                                        sbDiamond.Append("<td class=\"ratio\">" + item["Ratio"].ToString() + "</td>");
                                    }
                                    else
                                    {
                                        if (!String.IsNullOrEmpty(item["Ratio"].ToString()))
                                        {
                                            sbDiamond.Append("<td class=\"ratio\">" + item["Ratio"].ToString() + "</td>");
                                        }
                                        else
                                        {
                                            sbDiamond.Append("<td class=\"ratio\">" + CommonData.GetRatio(item["Measurements"].ToString().Replace("*", "x").Replace("-", "x")) + "</td>");
                                        }

                                    }


                                    //sbDiamond.Append("<td class=\"ratio\">" + item["Ratio"].ToString() + "</td>");
                                }

                                sbDiamond.Append("<td class=\"price\">");
                                if (UserSessionData.Currency.ToLower() == "aud")
                                    sbDiamond.Append("$");
                                else if (UserSessionData.Currency.ToLower() == "euro")
                                    sbDiamond.Append("€");
                                else if (UserSessionData.Currency.ToLower() == "usd")
                                    sbDiamond.Append("USD $");
                                else
                                    sbDiamond.Append("$");

                                sbDiamond.Append(item["Amount"].ToString() + "</td>");

                                sbDiamond.Append("<td class=\"lot\">" + item["LotNumber"].ToString().ToUpper() + "</td>");
                                sbDiamond.Append("<td class=\"view\">");

                                string strUrl = "1";

                                if (item["TableType"].ToString().ToUpper() == "RAPNET")
                                {
                                    strUrl = "1";
                                    string strSupplier = item["Supplier"].ToString().ToUpper();
                                    if (strSupplier == "N.E.R. JEWELRY INC.")
                                    {
                                        strUrl = "12";
                                    }
                                    else if (strSupplier == "OFER MIZRAHI DIAMONDS, INC.")
                                    {
                                        strUrl = "11";
                                    }
                                }
                                else if (item["TableType"].ToString().ToUpper() == "JBBROS")
                                { strUrl = "2"; }
                                else if (item["TableType"].ToString().ToUpper() == "VENUS")
                                { strUrl = "3"; }
                                else if (item["TableType"].ToString().ToUpper() == "CANTURI")
                                { strUrl = "4"; }
                                //else if (item["TableType"].ToString().ToUpper() == "PANACHE")
                                //{ strUrl = "5"; }
                                else if (item["TableType"].ToString().ToUpper() == "HARIKRISHNA")
                                { strUrl = "6"; }
                                else if (item["TableType"].ToString().ToUpper() == "FINESTAR")
                                { strUrl = "7"; }
                                else if (item["TableType"].ToString().ToUpper() == "CDINESH")
                                { strUrl = "8"; }
                                else if (item["TableType"].ToString().ToUpper() == "KAPUGEMS")
                                { strUrl = "9"; }
                                else if (item["TableType"].ToString().ToUpper() == "SRK")  // Add this for Client SRK
                                { strUrl = "10"; }
                                else if (item["TableType"].ToString().ToUpper() == "KIRANGEMS")
                                { strUrl = "13"; }

                                //NewlyAdded Suppliers //
                                else if (item["TableType"].ToString().ToUpper() == "DHARM")
                                { strUrl = "14"; }
                                else if (item["TableType"].ToString().ToUpper() == "YDVASH")
                                { strUrl = "5"; }
                                //NewlyAdded Suppliers //

                                sbDiamond.Append("<a href=\"" + Url.Content("~/Diamond/") + strUrl + "/" + item["DiamondID"].ToString() + "\" class=\"list-view-more\">More</a>");

                                sbDiamond.Append("<input type=\"submit\" name=\"\" value=\"More\" />");
                                sbDiamond.Append("<div id=\"listing-tab-box\">");
                                sbDiamond.Append("<div class=\"list-tbox\">");
                                sbDiamond.Append("<span>&nbsp;</span>");
                                sbDiamond.Append("<div class=\"cl\"></div>");
                                sbDiamond.Append("<ul>");
                                //sbDiamond.Append("<li><strong>LOT# :" + item["LotNumber"].ToString() + "</strong></li>");
                                sbDiamond.Append("<li><strong>MEASUREMENTS:  " + item["Measurements"].ToString().Replace("*", "x") + "</strong></li>");
                                sbDiamond.Append("<li><strong>CERT#:  " + item["DiamondCertificate"].ToString() + "</strong></li>");


                                if (item["Shape"].ToString().ToUpper().Contains("ROUND"))
                                {
                                    sbDiamond.Append("<li><strong>RATIO:  " + item["Ratio"].ToString() + "</strong></li>");

                                }
                                else
                                {
                                    if (!String.IsNullOrEmpty(item["Ratio"].ToString()))
                                    {
                                        sbDiamond.Append("<li><strong>RATIO:  " + item["Ratio"].ToString() + "</strong></li>");

                                    }
                                    else
                                    {
                                        sbDiamond.Append("<li><strong>RATIO:  " + CommonData.GetRatio(item["Measurements"].ToString().Replace("*", "x").Replace("-", "x")) + "</strong></li>");

                                    }

                                }
                                //sbDiamond.Append("<li><strong>RATIO:  " + item["Ratio"].ToString() + "</strong></li>");
                                sbDiamond.Append("</ul>");
                                sbDiamond.Append("<div class=\"cl\"></div>");
                                sbDiamond.Append("</div>");
                                sbDiamond.Append("</div>");
                                sbDiamond.Append("</td>");
                                //sbDiamond.Append("<td class=\"supplier-code\" id=\"selected-supplier-code-" + item["TableType"].ToString().ToUpper() + "-" + item["LotNumber"].ToString().ToUpper() + "\">" + strUrl + "</td>");
                                sbDiamond.Append("<td class=\"supplier-code  " + (strUrl == "4" ? "on-hand-diamond" : "") + "\" id=\"selected-supplier-code-" + item["TableType"].ToString().ToUpper() + "-" + item["DiamondID"].ToString().ToUpper() + "\">" + (strUrl == "4" ? "IN SALON" : strUrl) + "</td>");
                                
                                bool ShowHideVideo = DiamondHelper.ShowHideDiamondVideo(strUrl, AllowedSupplierIds);
                                
                                if (!String.IsNullOrEmpty(item["VideoURL"].ToString()) && ShowHideVideo)
                                {
                                    sbDiamond.Append("<td><img src=\"Images/video-icon.png\" align=\"top\" style='height:18px;' title=\"Video available\" /></td>");
                                }
                                else
                                {
                                    sbDiamond.Append("<td><span>&nbsp;</span></td>");
                                }
                                sbDiamond.Append("</tr>");
                            }



                            sbDiamond.Append("</tbody>");

                            sbDiamond.Append("</table>");

                            sbDiamond.Append("<div class=\"cl\"></div>");
                            sbDiamond.Append("</div>");

                            //View selected footer action buttons
                            sbDiamond.Append("<div class=\"cl\"></div>");

                            //if (!CommonData.IsDeviceMobile())
                            //{
                            //print selection button block
                            sbDiamond.Append("<p class=\"print-btn\">");
                            sbDiamond.Append("<a href=\"javascript:void(0)\" id=\"linkClearSelection\" class=\"clear-selection\">Clear Selection</a>");

                            sbDiamond.Append("<a href=\"javascript:void(0)\" id=\"linkPrintSelection\"><img src=\"" + Url.Content("~/Content/FrontEnd/images/print-btn.png") + "\" /></a>");
                            sbDiamond.Append("</p>");
                            //}
                            //request order button block
                            sbDiamond.Append("<p class=\"request-ord\">");
                            sbDiamond.Append("<a href=\"javascript:void(0)\" id=\"linkRequestOrder\">Request an order</a>");
                            sbDiamond.Append("</p>");

                            //sbDiamond.Append("<div class=\"cl\"></div>");

                            //request Availability button block
                            sbDiamond.Append("<p class=\"request-available\">");
                            sbDiamond.Append("<a href=\"javascript:void(0)\" id=\"linkRequestAvailability\">REQUEST AVAILABILITY <br />* ENQUIRY ONLY *</a>");
                            sbDiamond.Append("</p>");

                            //save selection button block
                            sbDiamond.Append("<p class=\"save-selection\">");
                            sbDiamond.Append("<a href=\"javascript:void(0)\" id=\"linkSaveSelection\">EMAIL SELECTION</a>");
                            sbDiamond.Append("</p>");

                            //clear selection button block
                            //sbDiamond.Append("<p class=\"clear-selection\">");
                            //sbDiamond.Append("<a href=\"javascript:void(0)\" id=\"linkClearSelection\">Clear Selection</a>");
                            //sbDiamond.Append("</p>");
                            sbDiamond.Append("<div class=\"cl\"></div>");

                            strData = sbDiamond.ToString();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                strMsg = "notok";
                strData = ex.Message;
            }
            return Json(new { msg = strMsg, data = strData, totalRecords = totalRecords });
        }

        private bool FnGetSelectionAlreadySelectedDiamonds(DataRow item, bool IsAlreadySelected)
        {
            if (Session["SelectedDiamondSelection"] != null)
            {
                string strSelectedDiamond = Session["SelectedDiamondSelection"].ToString();
                if (strSelectedDiamond.Contains("~"))
                {
                    string[] arrayDiamond = strSelectedDiamond.Split('~');
                    foreach (string itemDiamond in arrayDiamond)
                    {
                        if (!String.IsNullOrEmpty(itemDiamond))
                        {
                            // Add this for Client SRK
                            if (itemDiamond.ToLower().Replace("rapnet", "").Replace("srk","").Replace("canturi", "").Replace("jbbros", "").Replace("venus", "").Replace("panache", "").Replace("harikrishna", "").Replace("finestar", "").Replace("cdinesh", "").Replace("kapugems", "").Replace("sunrise", "").Replace("kirangems", "").Replace("dharm", "").Replace("ydvash", "") == item["DiamondID"].ToString().ToLower())
                            {
                                IsAlreadySelected = true;
                            }
                        }
                    }
                }
            }
            return IsAlreadySelected;
        }


        public JsonResult SaveDiamondSelection(string dimondNumber, string type, string isChecked)
        {
            string strMsg = "ok";
            string strData = "";
            int totalRecords = 0;
            try
            {
                string strSelectedDiamond = "";
                if (Session["SelectedDiamondSelection"] == null)
                {
                    Session["SelectedDiamondSelection"] = dimondNumber + type + "~";
                    strSelectedDiamond = Session["SelectedDiamondSelection"].ToString();
                }
                else
                {
                    if (isChecked.ToLower() == "true")
                    {
                        Session["SelectedDiamondSelection"] = Session["SelectedDiamondSelection"] + dimondNumber + type + "~";
                    }
                    else
                    {
                        Session["SelectedDiamondSelection"] = Session["SelectedDiamondSelection"].ToString().Replace(dimondNumber + type + "~", "");
                    }

                    strSelectedDiamond = Session["SelectedDiamondSelection"].ToString();
                }


            }
            catch (Exception ex)
            {
                strMsg = "notok";
                strData = ex.Message;
            }
            return Json(new { msg = strMsg, data = strData, totalRecords = totalRecords });
        }

        public JsonResult ClearDiamondSelection()
        {
            string strMsg = "ok";
            string strData = "";
            try
            {
                Session["SelectedDiamond"] = null;
            }
            catch (Exception ex)
            {
                strMsg = "notok";
                strData = ex.Message;
            }
            return Json(new { msg = strMsg, data = strData });
        }

        #region save selection
        [HttpPost]
        public ActionResult _SaveSelecton(SaveSelectonModels model)
        {
            StringBuilder sbSelectedDiamondQuery = new StringBuilder();
            string selectedDiamonds = model.SelectedDiamonds;

            if (!String.IsNullOrEmpty(selectedDiamonds))
            {
                string[] arryDiamonds = selectedDiamonds.Split(',');
                foreach (string item in arryDiamonds)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        string[] tmpArr = item.Split('#');
                        sbSelectedDiamondQuery.Append("INSERT INTO tblSaveSelectedDiamonds (SaveSelectionId,DiamondId,Type,Status,CreatedBy,CreatedOn,CreatedFromIp)");
                        sbSelectedDiamondQuery.Append("VALUES(");
                        sbSelectedDiamondQuery.Append("{SAVESELECTIONID},");
                        sbSelectedDiamondQuery.Append(tmpArr[0] + ",");
                        if (tmpArr[1].ToLower() == "rapnet")
                        {
                            sbSelectedDiamondQuery.Append("'R',");
                        }
                        else if (tmpArr[1].ToLower() == "canturi")
                        {
                            sbSelectedDiamondQuery.Append("'C',");
                        }
                        else if (tmpArr[1].ToLower() == "venus")
                        {
                            sbSelectedDiamondQuery.Append("'V',");
                        }
                        else if (tmpArr[1].ToLower() == "panache")
                        {
                            sbSelectedDiamondQuery.Append("'P',");
                        }
                        else if (tmpArr[1].ToLower() == "harikrishna")
                        {
                            sbSelectedDiamondQuery.Append("'H',");
                        }
                        else if (tmpArr[1].ToLower() == "finestar")
                        {
                            sbSelectedDiamondQuery.Append("'F',");
                        }
                        else if (tmpArr[1].ToLower() == "cdinesh")
                        {
                            sbSelectedDiamondQuery.Append("'CD',");
                        }
                        else if (tmpArr[1].ToLower() == "kapugems")
                        {
                            sbSelectedDiamondQuery.Append("'KG',");
                        }
                        else if (tmpArr[1].ToLower() == "kirangems")
                        {
                            sbSelectedDiamondQuery.Append("'KD',");
                        }
                        else if (tmpArr[1].ToLower() == "sunrise")
                        {
                            sbSelectedDiamondQuery.Append("'SD',");
                        }
                        // Add this for Client SRK
                        else if (tmpArr[1].ToLower() == "srk")
                        {
                            sbSelectedDiamondQuery.Append("'SRK',");
                        }

                        else if (tmpArr[1].ToLower() == "dharm")
                        {
                            sbSelectedDiamondQuery.Append("'DH',");
                        }
                        else if (tmpArr[1].ToLower() == "ydvash")
                        {
                            sbSelectedDiamondQuery.Append("'YD',");
                        }
                        else
                        {
                            sbSelectedDiamondQuery.Append("'J',");
                        }
                        sbSelectedDiamondQuery.Append("1,");
                        sbSelectedDiamondQuery.Append(UserSessionData.UserId + ",");
                        sbSelectedDiamondQuery.Append("GETUTCDATE(),");
                        sbSelectedDiamondQuery.Append("'" + Request.UserHostAddress + "'");

                        sbSelectedDiamondQuery.Append(");");
                    }

                }
            }
            model.SelectedDiamondsQuery = sbSelectedDiamondQuery.ToString();
            model.Flag = 1;
            model.ConsultantId = UserSessionData.UserId.ToString();
            model.Currency = UserSessionData.Currency;

            SaveSelectonHelper objSaveSelectonHelper = new SaveSelectonHelper();
            int saveSelectionId = objSaveSelectonHelper.AddUpdSaveSelecton(model);
            if (saveSelectionId != -1)
            {
                FnSendSaveSelectionMailToAdmin(model, objSaveSelectonHelper, saveSelectionId);
                FnSendSaveSelectionMailToConsultant(model, objSaveSelectonHelper, saveSelectionId);
            }
            ModelState.Clear();
            return Content("Thanks", "text/html");
        }

        private void FnSendSaveSelectionMailToAdmin(SaveSelectonModels model, SaveSelectonHelper objSaveSelectonHelper, int saveSelectionId)
        {
            model.Flag = 4;
            model.SaveSelectionId = saveSelectionId.ToString();
            DataSet dsSelectedDiamonds = objSaveSelectonHelper.GetSaveSelecton(model);
            string[] strShapes = { "CUSHION", "PRINCESS", "RADIANT" };
            if (dsSelectedDiamonds != null)
            {
                StringBuilder sbSelectedDiamonds = new StringBuilder();

                sbSelectedDiamonds.Append("<table width=\"750\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\" style=\"font-size:11px; border:0px; overflow:auto; font-family: arial; padding:0px; width:100%; margin:0px; text-align:center; border-bottom:1px solid #797979\">");
                sbSelectedDiamonds.Append("<thead style='border:0px;'>");
                sbSelectedDiamonds.Append("<tr>");
                sbSelectedDiamonds.Append("<th style='width:100px; padding:3px 2px'>SHAPE</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:40px;'>CARAT</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:40px;'>COLOR</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:50px;'>CLARITY</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px;  width:50px;'>CUT</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:54px'>POLISH</th>");
                sbSelectedDiamonds.Append("<th style ='padding:3px 2px; width:50px'>SYMMETRY</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:50px'>FLURO</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:70px'>PRICE</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px;width:90px'>MEASUREMENTS</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:100px; word-break: break-all;'>CERT #</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:90px'>LOT #</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:50px;'></th>");
                sbSelectedDiamonds.Append(" <th style='padding:3px 2px; width:60px'>DESIGN $</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:60px'>TOTAL $</th>");
                //sbSelectedDiamonds.Append("<th style='width:50px;padding:3px 2px;'></th>");
                //sbSelectedDiamonds.Append("<th style='width:50px;padding:3px 2px;'></th>");
                //sbSelectedDiamonds.Append("<th style='width:50px;padding:3px 2px;'></th>");
                sbSelectedDiamonds.Append("</tr>");
                sbSelectedDiamonds.Append("</thead>");
                sbSelectedDiamonds.Append("<tbody>");

                string carat = "";
                string shape = "";
                string color = "";
                string clarity = "";

                foreach (DataRow item in dsSelectedDiamonds.Tables[0].Rows)
                {
                    sbSelectedDiamonds.Append("<tr>");

                    string strDiamondImg = ConfigurationManager.AppSettings["WebsiteRootPath"] + "Content/FrontEnd/images/Diamonds/small/no-image.png";
                    if (System.IO.File.Exists(Server.MapPath("~/Content/FrontEnd/images/Diamonds/small/") + item["Shape"].ToString().ToLower().Trim() + ".png"))
                    {
                        strDiamondImg = ConfigurationManager.AppSettings["WebsiteRootPath"] + "Content/FrontEnd/images/Diamonds/small/" + item["Shape"].ToString().ToLower().Trim() + ".png";
                        shape = item["Shape"].ToString().ToUpper().Trim();
                    }


                    sbSelectedDiamonds.Append("<td style='width:100px; padding:3px 2px;'> <table border='0' style=\"font-size:11px; border:0px; overflow:auto; font-family: arial; padding:0px; width:100%; margin:0px; text-align:center; \"><tr>   <td style='width:20%; padding:3px 2px;'><img src=\"" + strDiamondImg + "\" align=\"top\" style='vertical-align:middle; display:inline-block' title=\"" + item["Shape"].ToString() + "\"> </td>");

                    if (shape.ToUpper().Contains("CUSHION") || shape.ToUpper().Contains("PRINCESS") || shape.ToUpper().Contains("RADIANT"))
                    {
                        string ratio = "0";
                        if (!String.IsNullOrEmpty(item["Ratio"].ToString()))
                        {
                            ratio = item["Ratio"].ToString();
                        }
                        else
                        {
                            ratio = CommonData.GetRatio(item["Measurements"].ToString().Replace("*", "x").Replace("-", "x"));
                        }
                        if (Convert.ToDecimal(ratio) >= 1 && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.059))
                        {
                            sbSelectedDiamonds.Append("<td style='width:80%; padding:3px 2px; text-align:left'><span style='vertical-align:middle; display:inline-block'>" + item["Shape"].ToString().ToUpper() + " SQUARE</span> </td>");
                        }
                        else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.060) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.088))
                        {
                            sbSelectedDiamonds.Append("<td style='width:80%; padding:3px 2px; text-align:left'><span style='vertical-align:middle; display:inline-block'> " + item["Shape"].ToString().ToUpper() + " SQUARISH</span> </td>");
                        }
                        else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.089) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.900))
                        {
                            sbSelectedDiamonds.Append("<td style='width:80%; padding:3px 2px; text-align:left'><span style='vertical-align:middle; display:inline-block'> " + item["Shape"].ToString().ToUpper() + " ELONGATED</span> </td>");
                        }
                        else
                        {
                            sbSelectedDiamonds.Append("<td style='width:80%; padding:3px 2px; text-align:left'><span style='vertical-align:middle; display:inline-block'> " + item["Shape"].ToString().ToUpper() + "</span> </td>");

                        }

                    }
                    else
                    {
                        sbSelectedDiamonds.Append("<td style='width:80%; padding:3px 2px; text-align:left'><span style='vertical-align:middle; display:inline-block'>" + item["Shape"].ToString().ToUpper() + "</span> </td>");
                    }

                    //sbSelectedDiamonds.Append("<small> " + item["Shape"].ToString().ToUpper() + "</small>");
                    sbSelectedDiamonds.Append(" </tr></table>  </td>");
                    decimal tmpCarat = 0;
                    try
                    {
                        tmpCarat = Convert.ToDecimal(item["Carat"].ToString());
                        carat = item["Carat"].ToString();
                    }
                    catch { }

                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + CommonData.FormatDecimal(tmpCarat) + "</td>");
                    //sbSelectedDiamonds.Append("<td>" + item["Carat"].ToString() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["Color"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                    color = item["Color"].ToString().ToUpper().Replace("+", "").Replace("-", "");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["Clarity"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                    clarity = item["Clarity"].ToString().ToUpper().Replace("+", "").Replace("-", "");
                    //ALL diamonds except for round, please do not display anything in the CUT field – make it a blank entry.
                    if (item["Shape"].ToString().ToLower().Trim() == "round")
                    {
                        sbSelectedDiamonds.Append("<td  style='padding:3px 2px;'>" + item["Cut"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                    }
                    else
                    {
                        sbSelectedDiamonds.Append("<td  style='padding:3px 2px;'>&nbsp;</td>");
                    }

                    //sbSelectedDiamonds.Append("<td>" + item["Cut"].ToString() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["Polish"].ToString().ToUpper() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["Symmetry"].ToString().ToUpper() + "</td>");
                    if (String.IsNullOrEmpty(item["Fluorescence"].ToString()))
                    {
                        sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>CHECK CERTIFICATE</td>");
                    }
                    else if (item["Fluorescence"].ToString().ToUpper() == "SLIGHT")
                    {
                        sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>FAINT</td>");
                    }
                    else if (item["Fluorescence"].ToString().ToUpper() == "VERY SLIGHT")
                    {
                        sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>VERY FAINT</td>");
                    }
                    else
                    {
                        sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["Fluorescence"].ToString().ToUpper() + "</td>");
                    }

                    //sbSelectedDiamonds.Append("<td>" + item["Fluorescence"].ToString().ToUpper() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>");
                    if (UserSessionData.Currency.ToLower() == "aud")
                        sbSelectedDiamonds.Append("$");
                    else if (UserSessionData.Currency.ToLower() == "euro")
                        sbSelectedDiamonds.Append("€");
                    else if (UserSessionData.Currency.ToLower() == "usd")
                        sbSelectedDiamonds.Append("$");
                    else
                        sbSelectedDiamonds.Append("$");


                    sbSelectedDiamonds.Append(item["Amount"].ToString() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px; width:100px; word-break: break-all;'>" + item["Measurements"].ToString() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["DiamondCertificate"].ToString() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["LotNumber"].ToString().ToUpper() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["Type"].ToString().ToUpper() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>&nbsp;</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>&nbsp;</td>");
                    //sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>&nbsp;</td>");
                    //sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>&nbsp;</td>");
                    //sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>&nbsp;</td>");
                    sbSelectedDiamonds.Append("</tr>");
                }
                sbSelectedDiamonds.Append("<tr><td style='width:100px; padding:3px 2px;height:35px;'>&nbsp;</td><td  style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td></tr>");
                sbSelectedDiamonds.Append("<tr><td style='width:100px; padding:3px 2px;height:35px;'>&nbsp;</td><td  style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td></tr>");
                sbSelectedDiamonds.Append("<tr><td style='width:100px; padding:3px 2px;height:35px;'>&nbsp;</td><td  style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td></tr>");

                sbSelectedDiamonds.Append("</tbody>");
                sbSelectedDiamonds.Append("</table>");


                //SaveSelectionMailToAdmin.html

                string strMessage = "";

                System.Net.Mail.MailMessage objEmailMessage = new System.Net.Mail.MailMessage();

                System.IO.FileStream objFsContent = new System.IO.FileStream(Server.MapPath("~/Content/EmailTemplates/SaveSelectionMailToAdmin.html"), System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.StreamReader objStreamContent = new System.IO.StreamReader(objFsContent);
                strMessage = objStreamContent.ReadToEnd();
                objFsContent.Close();
                objStreamContent.Close();
                objFsContent.Dispose();
                objStreamContent.Dispose();
                strMessage = strMessage.Replace("{0}", "Admin");
                strMessage = strMessage.Replace("{CONSULTANT_RNAME}", UserSessionData.UserName);
                strMessage = strMessage.Replace("{CLIENT_FULL_NAME}", model.ClientName);
                strMessage = strMessage.Replace("{CLIENT_PHONE}", model.Phone);

                DateTime dtDte = DateTime.ParseExact(model.Date, "dd/MM/yyyy", null);// Convert.ToDateTime(model.Date);

                strMessage = strMessage.Replace("{DATE}", dtDte.ToString("dd/MM/yyyy"));//model.Date);
                strMessage = strMessage.Replace("{SELECTED_DIAMONDS}", sbSelectedDiamonds.ToString());

                string diamondInfo = UserSessionData.UserName + " / " + CommonData.FormatDecimal(Convert.ToDecimal(carat)) + " " + shape + " " + color + " " + clarity + " / " + model.ClientName;


                //objEmailMessage.Subject = ConfigurationManager.AppSettings["SaveSelectionAdminSubject"];
                objEmailMessage.Subject = ConfigurationManager.AppSettings["SaveSelectionConsultantSubject"].Replace("{DIAMOND_INFO}", diamondInfo);
                objEmailMessage.To.Add(ConfigurationManager.AppSettings["SaveSelectionAdminToEmail"]);

                objEmailMessage.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["SaveSelectionAdminFrom"].ToString(CultureInfo.InvariantCulture), ConfigurationManager.AppSettings["SaveSelectionAdminFromName"]);
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
                }
            }
        }

        private void FnSendSaveSelectionMailToConsultant(SaveSelectonModels model, SaveSelectonHelper objSaveSelectonHelper, int saveSelectionId)
        {
            model.Flag = 4;
            model.SaveSelectionId = saveSelectionId.ToString();
            DataSet dsSelectedDiamonds = objSaveSelectonHelper.GetSaveSelecton(model);
            string[] strShapes = { "CUSHION", "PRINCESS", "RADIANT" };
            if (dsSelectedDiamonds != null)
            {
                StringBuilder sbSelectedDiamonds = new StringBuilder();

                sbSelectedDiamonds.Append("<table width=\"750\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\" style=\"font-size:11px; border:0px; overflow:auto; font-family: arial; padding:0px; width:100%; margin:0px; text-align:center; border-bottom:1px solid #797979\">");
                sbSelectedDiamonds.Append("<thead style='border:0px;'>");
                sbSelectedDiamonds.Append("<tr>");
                sbSelectedDiamonds.Append("<th style='width:100px; padding:3px 2px'>SHAPE</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:40px;'>CARAT</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:40px;'>COLOR</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:50px;'>CLARITY</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px;  width:50px;'>CUT</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:54px'>POLISH</th>");
                sbSelectedDiamonds.Append("<th style ='padding:3px 2px; width:50px'>SYMMETRY</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:50px'>FLURO</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:70px'>PRICE</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px;width:90px'>MEASUREMENTS</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:100px; word-break: break-all;'>CERT #</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:90px'>LOT #</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:50px;'></th>");
                sbSelectedDiamonds.Append(" <th style='padding:3px 2px; width:60px'>DESIGN $</th>");
                sbSelectedDiamonds.Append("<th style='padding:3px 2px; width:60px'>TOTAL $</th>");
                //sbSelectedDiamonds.Append("<th style='width:50px;padding:3px 2px;'></th>");
                //sbSelectedDiamonds.Append("<th style='width:50px;padding:3px 2px;'></th>"); 
                //sbSelectedDiamonds.Append("<th style='width:50px;padding:3px 2px;'></th>");
                sbSelectedDiamonds.Append("</tr>");
                sbSelectedDiamonds.Append("</thead>");
                sbSelectedDiamonds.Append("<tbody>");

                string carat = "";
                string shape = "";
                string color = "";
                string clarity = "";

                foreach (DataRow item in dsSelectedDiamonds.Tables[0].Rows)
                {
                    sbSelectedDiamonds.Append("<tr>");

                    string strDiamondImg = ConfigurationManager.AppSettings["WebsiteRootPath"] + "Content/FrontEnd/images/Diamonds/small/no-image.png";
                    if (System.IO.File.Exists(Server.MapPath("~/Content/FrontEnd/images/Diamonds/small/") + item["Shape"].ToString().ToLower().Trim() + ".png"))
                    {
                        strDiamondImg = ConfigurationManager.AppSettings["WebsiteRootPath"] + "Content/FrontEnd/images/Diamonds/small/" + item["Shape"].ToString().ToLower().Trim() + ".png";
                        shape = item["Shape"].ToString().ToUpper().Trim();
                    }
                    sbSelectedDiamonds.Append("<td style='width:100px; padding:3px 2px;'> <table border='0' style=\"font-size:11px; border:0px; overflow:auto; font-family: arial; padding:0px; width:100%; margin:0px; text-align:center\"><tr>   <td style='width:20%; padding:3px 2px;'><img src=\"" + strDiamondImg + "\" align=\"top\" style='vertical-align:middle; display:inline-block' title=\"" + item["Shape"].ToString() + "\"> </td>");

                    if (shape.ToUpper().Contains("CUSHION") || shape.ToUpper().Contains("PRINCESS") || shape.ToUpper().Contains("RADIANT"))
                    {
                        string ratio = "0";
                        if (!String.IsNullOrEmpty(item["Ratio"].ToString()))
                        {
                            ratio = item["Ratio"].ToString();
                        }
                        else
                        {
                            ratio = CommonData.GetRatio(item["Measurements"].ToString().Replace("*", "x").Replace("-", "x"));
                        }
                        if (Convert.ToDecimal(ratio) >= 1 && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.059))
                        {
                            sbSelectedDiamonds.Append("<td style='width:80%; padding:3px 2px; text-align:left'><span style='vertical-align:middle; display:inline-block; text-align:left'>" + item["Shape"].ToString().ToUpper() + " SQUARE</span> </td>");
                        }
                        else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.060) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.088))
                        {
                            sbSelectedDiamonds.Append("<td style='width:80%; padding:3px 2px; text-align:left'><span style='vertical-align:middle; display:inline-block; text-align:left'> " + item["Shape"].ToString().ToUpper() + " SQUARISH</span> </td>");
                        }
                        else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.089) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.900))
                        {
                            sbSelectedDiamonds.Append("<td style='width:80%; padding:3px 2px; text-align:left'><span style='vertical-align:middle; display:inline-block; text-align:left'> " + item["Shape"].ToString().ToUpper() + " ELONGATED</span> </td>");
                        }
                        else
                        {
                            sbSelectedDiamonds.Append("<td style='width:80%; padding:3px 2px; text-align:left'><span style='vertical-align:middle; display:inline-block; text-align:left'> " + item["Shape"].ToString().ToUpper() + "</span> </td>");

                        }

                    }
                    else
                    {
                        sbSelectedDiamonds.Append("<td style='width:80%; padding:3px 2px; text-align:left'><span style='vertical-align:middle; display:inline-block; text-align:left'>" + item["Shape"].ToString().ToUpper() + "</span> </td>");
                    }

                    //sbSelectedDiamonds.Append("<small> " + item["Shape"].ToString().ToUpper() + "</small>");
                    sbSelectedDiamonds.Append(" </tr></table>  </td>");
                    decimal tmpCarat = 0;
                    try
                    {
                        tmpCarat = Convert.ToDecimal(item["Carat"].ToString());
                        carat = item["Carat"].ToString();
                    }
                    catch { }

                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + CommonData.FormatDecimal(tmpCarat) + "</td>");
                    //sbSelectedDiamonds.Append("<td>" + item["Carat"].ToString() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["Color"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                    color = item["Color"].ToString().ToUpper().Replace("+", "").Replace("-", "");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["Clarity"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                    clarity = item["Clarity"].ToString().ToUpper().Replace("+", "").Replace("-", "");
                    //ALL diamonds except for round, please do not display anything in the CUT field – make it a blank entry.
                    if (item["Shape"].ToString().ToLower().Trim() == "round")
                    {
                        sbSelectedDiamonds.Append("<td  style='padding:3px 2px;'>" + item["Cut"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                    }
                    else
                    {
                        sbSelectedDiamonds.Append("<td  style='padding:3px 2px;'>&nbsp;</td>");
                    }

                    //sbSelectedDiamonds.Append("<td>" + item["Cut"].ToString() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["Polish"].ToString().ToUpper() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["Symmetry"].ToString().ToUpper() + "</td>");
                    if (String.IsNullOrEmpty(item["Fluorescence"].ToString()))
                    {
                        sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>CHECK CERTIFICATE</td>");
                    }
                    else if (item["Fluorescence"].ToString().ToUpper() == "SLIGHT")
                    {
                        sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>FAINT</td>");
                    }
                    else if (item["Fluorescence"].ToString().ToUpper() == "VERY SLIGHT")
                    {
                        sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>VERY FAINT</td>");
                    }
                    else
                    {
                        sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["Fluorescence"].ToString().ToUpper() + "</td>");
                    }

                    //sbSelectedDiamonds.Append("<td>" + item["Fluorescence"].ToString().ToUpper() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>");
                    if (UserSessionData.Currency.ToLower() == "aud")
                        sbSelectedDiamonds.Append("$");
                    else if (UserSessionData.Currency.ToLower() == "euro")
                        sbSelectedDiamonds.Append("€");
                    else if (UserSessionData.Currency.ToLower() == "usd")
                        sbSelectedDiamonds.Append("$");
                    else
                        sbSelectedDiamonds.Append("$");


                    sbSelectedDiamonds.Append(item["Amount"].ToString() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px; width:100px; word-break: break-all;'>" + item["Measurements"].ToString() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["DiamondCertificate"].ToString() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["LotNumber"].ToString().ToUpper() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>" + item["Type"].ToString().ToUpper() + "</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>&nbsp;</td>");
                    sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>&nbsp;</td>");
                    //sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>&nbsp;</td>");
                    //sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>&nbsp;</td>");
                    //sbSelectedDiamonds.Append("<td style='padding:3px 2px;'>&nbsp;</td>");
                    sbSelectedDiamonds.Append("</tr>");
                }

                sbSelectedDiamonds.Append("<tr><td style='width:100px; padding:3px 2px;height:35px;'>&nbsp;</td><td  style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td></tr>");
                sbSelectedDiamonds.Append("<tr><td style='width:100px; padding:3px 2px;height:35px;'>&nbsp;</td><td  style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td></tr>");
                sbSelectedDiamonds.Append("<tr><td style='width:100px; padding:3px 2px;height:35px;'>&nbsp;</td><td  style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td><td style='padding:3px 2px;'></td></tr>");


                sbSelectedDiamonds.Append("</tbody>");
                sbSelectedDiamonds.Append("</table>");


                //SaveSelectionMailToAdmin.html

                string strMessage = "";

                System.Net.Mail.MailMessage objEmailMessage = new System.Net.Mail.MailMessage();

                System.IO.FileStream objFsContent = new System.IO.FileStream(Server.MapPath("~/Content/EmailTemplates/SaveSelectionMailToAdmin.html"), System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.StreamReader objStreamContent = new System.IO.StreamReader(objFsContent);
                strMessage = objStreamContent.ReadToEnd();
                objFsContent.Close();
                objStreamContent.Close();
                objFsContent.Dispose();
                objStreamContent.Dispose();
                strMessage = strMessage.Replace("{0}", "Consultant");
                strMessage = strMessage.Replace("{CONSULTANT_RNAME}", UserSessionData.UserName);
                strMessage = strMessage.Replace("{CLIENT_FULL_NAME}", model.ClientName);
                strMessage = strMessage.Replace("{CLIENT_PHONE}", model.Phone);
                DateTime dtDte = DateTime.ParseExact(model.Date, "dd/MM/yyyy", null);// Convert.ToDateTime(model.Date);

                strMessage = strMessage.Replace("{DATE}", dtDte.ToString("dd/MM/yyyy"));
                //strMessage = strMessage.Replace("{DATE}", model.Date);
                strMessage = strMessage.Replace("{SELECTED_DIAMONDS}", sbSelectedDiamonds.ToString());

                string diamondInfo = UserSessionData.UserName + " / " + CommonData.FormatDecimal(Convert.ToDecimal(carat)) + " " + shape + " " + color + " " + clarity + " / " + model.ClientName;

                objEmailMessage.Subject = ConfigurationManager.AppSettings["SaveSelectionConsultantSubject"].Replace("{DIAMOND_INFO}", diamondInfo);
                objEmailMessage.To.Add(model.ConsultantEmail.Replace(";", ",").Replace("|", ","));

                objEmailMessage.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["SaveSelectionConsultantFrom"].ToString(CultureInfo.InvariantCulture), ConfigurationManager.AppSettings["SaveSelectionConsultantFromName"]);
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
                }
            }
        }
        #endregion

        #region print selected diamonds


        public JsonResult GetDiamondsForPrint()
        {
            string strMsg = "ok";
            string strData = "";
            try
            {
                string strSelectedDiamond = "";

                if (Session["SelectedDiamond"] != null)
                {
                    strSelectedDiamond = Session["SelectedDiamond"].ToString();
                }

                string strRapnetDiamondIds = "";
                string strCanturiDiamondIds = "";
                string strJbbrosDiamondIds = "";
                string strVenusDiamondIds = "";
                string strPanacheDiamondIds = "";
                string strHariKrishnaDiamondIds = "";
                string strFineStarDiamondIds = "";
                string strCDINESHDiamondIds = "";
                string strKapuGemsDiamondIds = "";
                string strSunriseDiamondIds = "";
                string strKiranGemsDiamondIds = "";
                string strDharmDiamondIds = "";
                string strYDVashDiamondIds = "";

                // Add this for Client SRK
                string strSrkDiamondIds = "";

                if (strSelectedDiamond.Contains("~"))
                {
                    string[] arrayDiamond = strSelectedDiamond.Split('~');
                    foreach (string item in arrayDiamond)
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            if (item.ToLower().Contains("rapnet"))
                            {
                                strRapnetDiamondIds += item.ToLower().Replace("rapnet", "") + ",";
                            }
                            else if (item.ToLower().Contains("jbbros"))
                            {
                                strJbbrosDiamondIds += item.ToLower().Replace("jbbros", "") + ",";
                            }
                            else if (item.ToLower().Contains("venus"))
                            {
                                strVenusDiamondIds += item.ToLower().Replace("venus", "") + ",";
                            }
                            //else if (item.ToLower().Contains("panache"))
                            //{
                            //    strPanacheDiamondIds += item.ToLower().Replace("panache", "") + ",";
                            //}
                            else if (item.ToLower().Contains("harikrishna"))
                            {
                                strHariKrishnaDiamondIds += item.ToLower().Replace("harikrishna", "") + ",";
                            }
                            else if (item.ToLower().Contains("finestar"))
                            {
                                strFineStarDiamondIds += item.ToLower().Replace("finestar", "") + ",";
                            }
                            else if (item.ToLower().Contains("cdinesh"))
                            {
                                strCDINESHDiamondIds += item.ToLower().Replace("cdinesh", "") + ",";
                            }
                            else if (item.ToLower().Contains("kapugems"))
                            {
                                strKapuGemsDiamondIds += item.ToLower().Replace("kapugems", "") + ",";
                            }
                            else if (item.ToLower().Contains("kirangems"))
                            {
                                strKiranGemsDiamondIds += item.ToLower().Replace("kirangems", "") + ",";
                            }
                            else if (item.ToLower().Contains("sunrise"))
                            {
                                strSunriseDiamondIds += item.ToLower().Replace("sunrise", "") + ",";
                            }
                            // Add this for Client SRK
                            else if (item.ToLower().Contains("srk"))
                            {
                                strSrkDiamondIds += item.ToLower().Replace("srk", "") + ",";
                            }
                            else if (item.ToLower().Contains("dharm"))
                            {
                                strDharmDiamondIds += item.ToLower().Replace("dharm", "") + ",";
                            }
                            else if (item.ToLower().Contains("ydvash"))
                            {
                                strYDVashDiamondIds += item.ToLower().Replace("ydvash", "") + ",";
                            }
                            else
                            {
                                strCanturiDiamondIds += item.ToLower().Replace("canturi", "") + ",";
                            }
                        }
                    }
                }


                string sqlQuery = String.Empty;
                if (!String.IsNullOrEmpty(strCanturiDiamondIds))
                {
                    if (strCanturiDiamondIds.EndsWith(","))
                    {
                        strCanturiDiamondIds = strCanturiDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = "SELECT " + DiamondHelper.GetCanturiCoulmns() + ",'Canturi' AS TableType,dbo.Udf_GetCanturiPrice(Price,'" + UserSessionData.Currency.ToUpper() + "','CANTURI',DiamondID) AS Amount FROM tblCanturiDiamond WHERE DiamondID IN (" + strCanturiDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(strRapnetDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strRapnetDiamondIds.EndsWith(","))
                    {
                        strRapnetDiamondIds = strRapnetDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'Rapnet' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblRapnetDiamond " +
                                        "WHERE DiamondID IN (" + strRapnetDiamondIds + ")";
                }



                if (!String.IsNullOrEmpty(strPanacheDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strPanacheDiamondIds.EndsWith(","))
                    {
                        strPanacheDiamondIds = strPanacheDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'Panache' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblPanacheDiamond " +
                                        "WHERE DiamondID IN (" + strPanacheDiamondIds + ")";
                }


                if (!String.IsNullOrEmpty(strHariKrishnaDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strHariKrishnaDiamondIds.EndsWith(","))
                    {
                        strHariKrishnaDiamondIds = strHariKrishnaDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'HariKrishna' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblHariKrishnaDiamond " +
                                        "WHERE DiamondID IN (" + strHariKrishnaDiamondIds + ")";
                }


                if (!String.IsNullOrEmpty(strFineStarDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strFineStarDiamondIds.EndsWith(","))
                    {
                        strFineStarDiamondIds = strFineStarDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'FineStar' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblFineStarDiamond " +
                                        "WHERE DiamondID IN (" + strFineStarDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(strCDINESHDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strCDINESHDiamondIds.EndsWith(","))
                    {
                        strCDINESHDiamondIds = strCDINESHDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'CDINESH' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblCDINESHDiamond " +
                                        "WHERE DiamondID IN (" + strCDINESHDiamondIds + ")";
                }


                if (!String.IsNullOrEmpty(strKapuGemsDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strKapuGemsDiamondIds.EndsWith(","))
                    {
                        strKapuGemsDiamondIds = strKapuGemsDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'KAPUGEMS' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKapuGemsDiamond " +
                                        "WHERE DiamondID IN (" + strKapuGemsDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(strKiranGemsDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strKiranGemsDiamondIds.EndsWith(","))
                    {
                        strKiranGemsDiamondIds = strKiranGemsDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'KIRANGEMS' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKiranGemsDiamond " +
                                        "WHERE DiamondID IN (" + strKiranGemsDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(strSunriseDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strSunriseDiamondIds.EndsWith(","))
                    {
                        strSunriseDiamondIds = strSunriseDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'SUNRISE' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblSunriseDiamond " +
                                        "WHERE DiamondID IN (" + strSunriseDiamondIds + ")";
                }


                // Add this for Client SRK
                if (!String.IsNullOrEmpty(strSrkDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strSrkDiamondIds.EndsWith(","))
                    {
                        strSrkDiamondIds = strSrkDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'SRK' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblSrkDiamond " +
                                        "WHERE DiamondID IN (" + strSrkDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(strJbbrosDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strJbbrosDiamondIds.EndsWith(","))
                    {
                        strJbbrosDiamondIds = strJbbrosDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'JBBros' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblJBBrosDiamond " +
                                        "WHERE DiamondID IN (" + strJbbrosDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(strVenusDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strVenusDiamondIds.EndsWith(","))
                    {
                        strVenusDiamondIds = strVenusDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'Venus' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblVenusJewelsDiamond " +
                                        "WHERE DiamondID IN (" + strVenusDiamondIds + ")";
                }
                if (!String.IsNullOrEmpty(strDharmDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strDharmDiamondIds.EndsWith(","))
                    {
                        strDharmDiamondIds = strDharmDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'Dharm' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblDharmDiamond " +
                                        "WHERE DiamondID IN (" + strDharmDiamondIds + ")";
                }
                if (!String.IsNullOrEmpty(strYDVashDiamondIds))
                {
                    if (!String.IsNullOrEmpty(sqlQuery))
                    {
                        sqlQuery = sqlQuery + "union all ";
                    }
                    if (strYDVashDiamondIds.EndsWith(","))
                    {
                        strYDVashDiamondIds = strYDVashDiamondIds.TrimEnd(',');
                    }
                    sqlQuery = sqlQuery + "SELECT *,'YDVash' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblYDVashDiamond " +
                                        "WHERE DiamondID IN (" + strYDVashDiamondIds + ")";
                }

                if (!String.IsNullOrEmpty(sqlQuery))
                {
                    DiamondHelper objDiamondHelper = new DiamondHelper();
                    DataSet ds = objDiamondHelper.GetDiamondBySelection(sqlQuery);

                    StringBuilder sbPrint = new StringBuilder();

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            sbPrint.Append("<!DOCTYPE html>");
                            sbPrint.Append("<html>");
                            sbPrint.Append("<head>");
                            sbPrint.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                            sbPrint.Append("<title>Canturi</title>");

                            sbPrint.Append("<style type='text/css' media='print'>");
                            sbPrint.Append("@page { ");
                            sbPrint.Append("size: landscape;margin: 2cm;}");
                            //sbPrint.Append("body { writing-mode: tb-rl;}");
                            sbPrint.Append("</style>");

                            sbPrint.Append("</head>");
                            sbPrint.Append("<body>");
                            sbPrint.Append("<table width=\"900\" cellpadding=\"0\" align=\"center\" cellspacing=\"0\" border=\"2\" style=\"border:0!important; background:#dceffe; font-family: arial\" > ");
                            sbPrint.Append("<tr style=\"background:#1c1616; color:#fff; font-weight:bold; text-transform:uppercase; font-size:13px\">");
                            sbPrint.Append("<th style=\"padding:10px 0; margin:0px;\">shape</th>");
                            sbPrint.Append("<th style=\"padding:10px 0; margin:0px;\">carat</th>");
                            sbPrint.Append("<th style=\"padding:10px 0; margin:0px;\">color</th>");
                            sbPrint.Append("<th style=\"padding:10px 0; margin:0px;\">clarity</th>");
                            sbPrint.Append("<th style=\"padding:10px 0; margin:0px;\">cut</th>");
                            sbPrint.Append("<th style=\"padding:10px 0; margin:0px;\">polish</th>");
                            sbPrint.Append("<th style=\"padding:10px 0; margin:0px;\">symmetry</th>");
                            //sbPrint.Append("<th style=\"padding:10px 0; margin:0px;\">FLUORESCENCE</th>");
                            sbPrint.Append("<th style=\"padding:10px 0; margin:0px;\">price</th>");
                            sbPrint.Append("<th style=\"padding:10px 0; margin:0px;\">MEASUREMENTS</th>");
                            sbPrint.Append("<th style=\"padding:10px 0; margin:0px;\">CERT #</th>");
                            sbPrint.Append("<th style=\"padding:10px 0; margin:0px;\">lot #</th>");
                            sbPrint.Append("<th style=\"padding:10px 5px 10px 0; margin:0px;\"></th>");
                            sbPrint.Append("</tr>");
                            int count = 0;
                            foreach (DataRow item in ds.Tables[0].Rows)
                            {
                                bool isSelected = false;


                                if (FnGetSelectionAlreadySelectedDiamonds(item, isSelected))
                                {
                                    string strUrl = "1";

                                    if (item["TableType"].ToString().ToUpper() == "RAPNET")
                                    {
                                        strUrl = "1";
                                        string strSupplier = item["Supplier"].ToString().ToUpper();

                                        if (strSupplier.ToUpper() == "N.E.R. JEWELRY INC.")
                                        {
                                            strUrl = "12";
                                        }
                                        else if (strSupplier.ToUpper() == "OFER MIZRAHI DIAMONDS, INC.")
                                        {
                                            strUrl = "11";
                                        }
                                    }
                                    else if (item["TableType"].ToString().ToUpper() == "JBBROS")
                                    { strUrl = "2"; }
                                    else if (item["TableType"].ToString().ToUpper() == "VENUS")
                                    { strUrl = "3"; }
                                    else if (item["TableType"].ToString().ToUpper() == "CANTURI")
                                    {
                                        strUrl = "4";
                                    }
                                    //else if (item["TableType"].ToString().ToUpper() == "PANACHE")
                                    //{
                                    //    strUrl = "5";
                                    //}
                                    else if (item["TableType"].ToString().ToUpper() == "HARIKRISHNA")
                                    {
                                        strUrl = "6";
                                    }
                                    else if (item["TableType"].ToString().ToUpper() == "FINESTAR")
                                    {
                                        strUrl = "7";
                                    }
                                    else if (item["TableType"].ToString().ToUpper() == "CDINESH")
                                    {
                                        strUrl = "8";
                                    }
                                    else if (item["TableType"].ToString().ToUpper() == "KAPUGEMS")
                                    {
                                        strUrl = "9";
                                    }
                                    // Add this for Client SRK
                                    else if (item["TableType"].ToString().ToUpper() == "SRK")
                                    {
                                        strUrl = "10";
                                    }
                                    else if (item["TableType"].ToString().ToUpper() == "KIRANGEMS")
                                    {
                                        strUrl = "13";
                                    }
                                    else if (item["TableType"].ToString().ToUpper() == "DHARM")
                                    {
                                        strUrl = "14";
                                    }
                                    



                                    if (count == 0)
                                    {
                                        sbPrint.Append("<tr style=\"background:#ededed; color:#000; text-transform:uppercase; font-size:12px\">");
                                    }
                                    else
                                    {
                                        sbPrint.Append("<tr style=\"background:#f6f8fa; color:#000; text-transform:uppercase; font-size:12px\">");
                                        count = 0;
                                    }
                                    string strDiamondImg = ConfigurationManager.AppSettings["WebsiteRootPath"] + "Content/FrontEnd/images/Diamonds/small/no-image.png";
                                    string shape = item["Shape"].ToString().ToLower().Trim();
                                    if (System.IO.File.Exists(Server.MapPath("~/Content/FrontEnd/images/Diamonds/small/") + item["Shape"].ToString().ToLower().Trim() + ".png"))
                                    {
                                        strDiamondImg = ConfigurationManager.AppSettings["WebsiteRootPath"] + "Content/FrontEnd/images/Diamonds/small/" + item["Shape"].ToString().ToLower().Trim() + ".png";
                                    }


                                    sbPrint.Append("<td align=\"center\" valign=\"middle\" style=\"padding:10px 0; margin:0px;\">");

                                    if (shape.ToUpper().Contains("CUSHION") || shape.ToUpper().Contains("PRINCESS") || shape.ToUpper().Contains("RADIANT"))
                                    {
                                        string ratio = "0";
                                        if (!String.IsNullOrEmpty(item["Ratio"].ToString()))
                                        {
                                            ratio = item["Ratio"].ToString();
                                        }
                                        else
                                        {
                                            ratio = CommonData.GetRatio(item["Measurements"].ToString().Replace("*", "x").Replace("-", "x"));
                                        }
                                        if (Convert.ToDecimal(ratio) >= 1 && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.059))
                                        {
                                            sbPrint.Append(item["Shape"].ToString().ToUpper() + " SQUARE");
                                        }
                                        else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.060) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.088))
                                        {
                                            sbPrint.Append(item["Shape"].ToString().ToUpper() + " SQUARISH");
                                        }
                                        else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.089) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.900))
                                        {
                                            sbPrint.Append(item["Shape"].ToString().ToUpper() + " ELONGATED");
                                        }
                                        else
                                        {
                                            sbPrint.Append(item["Shape"].ToString().ToUpper());

                                        }

                                    }


                                    //if (item["Shape"].ToString().ToUpper().Contains("CUSHION"))
                                    //{
                                    //    sbPrint.Append("CUSHION");
                                    //}
                                    else
                                    {
                                        sbPrint.Append(item["Shape"].ToString().ToUpper());
                                    }
                                    // sbPrint.Append(item["Shape"].ToString().ToUpper());

                                    //sbPrint.Append("<img src=\"" + strDiamondImg + "\" alt=\"\" title=\"\" style=\"display:inline-block; vertical-align:middle; padding-right:8px;\"> " + item["Shape"].ToString());

                                    sbPrint.Append("</td>");
                                    sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">" + item["Carat"].ToString().ToUpper() + "</td>");
                                    sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">" + item["Color"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                                    sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">" + item["Clarity"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                                    //ALL diamonds except for round, please do not display anything in the CUT field – make it a blank entry.
                                    if (item["Shape"].ToString().ToLower().Trim() == "round")
                                    {
                                        sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">" + item["Cut"].ToString().ToUpper().Replace("+", "").Replace("-", "") + "</td>");
                                    }
                                    else
                                    {
                                        sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">&nbsp;</td>");
                                    }


                                    sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">" + item["Polish"].ToString().ToUpper() + "</td>");
                                    sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">" + item["Symmetry"].ToString().ToUpper() + "</td>");
                                    //if (String.IsNullOrEmpty(item["Fluorescence"].ToString()))
                                    //{
                                    //    sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">CHECK CERTIFICATE</td>");

                                    //}
                                    //else if (item["Fluorescence"].ToString().ToUpper() == "SLIGHT")
                                    //{
                                    //    sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">FAINT</td>");


                                    //}
                                    //else if (item["Fluorescence"].ToString().ToUpper() == "VERY SLIGHT")
                                    //{
                                    //    sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">VERY FAINT</td>");

                                    //}
                                    //else
                                    //{
                                    //    sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">" + item["Fluorescence"].ToString().ToUpper() + "</td>");

                                    //}

                                    //sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">" + item["Fluorescence"].ToString().ToUpper() + "</td>");
                                    sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">");
                                    if (UserSessionData.Currency.ToLower() == "aud")
                                        sbPrint.Append("$");
                                    else if (UserSessionData.Currency.ToLower() == "euro")
                                        sbPrint.Append("€");
                                    else if (UserSessionData.Currency.ToLower() == "usd")
                                        sbPrint.Append("USD $");
                                    else
                                        sbPrint.Append("$");

                                    sbPrint.Append(item["Amount"].ToString() + "</td>");
                                    sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">" + item["MEASUREMENTS"].ToString() + "</td>");
                                    sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">" + item["DiamondCertificate"].ToString() + "</td>");
                                    sbPrint.Append("<td align=\"center\" style=\"padding:10px 0; margin:0px;\">" + item["LotNumber"].ToString().ToUpper() + "</td>");
                                    sbPrint.Append("<td align=\"center\" style=\"padding:10px 10px; margin:0px;\" class=\"" + (strUrl == "4" ? "on-hand-diamond" : "") + "\">" + (strUrl == "4" ? "IN SALON" : strUrl) + "</td>");
                                    sbPrint.Append("</tr>");
                                }
                            }



                            sbPrint.Append("</table>");

                            sbPrint.Append("</body>");
                            sbPrint.Append("</html>");
                            strData = sbPrint.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strMsg = "notok";
                strData = ex.Message;
            }
            return Json(new { msg = strMsg, data = strData });
        }
        #endregion
    }
}

