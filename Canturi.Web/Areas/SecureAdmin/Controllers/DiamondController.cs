using Canturi.Models.BusinessEntity.Admin;
using Canturi.Models.BusinessHelper.Admin;
using Canturi.Models.BusinessHelper.CommonHelper;
using Canturi.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Web.Areas.SecureAdmin.Controllers
{
    [AdminSessionExpire]
    public class DiamondController : Controller
    {
        //
        // GET: /SecureAdmin/Diamond/

        List<DiamondModels> ActiveDiamondList = new List<DiamondModels>();
        public ActionResult Index()
        {
            DiamondModels model = new DiamondModels();
            model.Flag = 1;
            //get grid parameters from URL/POST (if any)
            var activeGridParameters = ASPRazorWebGridSample.UI.GridParameters.GetGridParameters();
            int pageSize = 20;   //displayed rows per page

            DiamondHelper objHelper = new DiamondHelper();



            ActiveDiamondList = objHelper.GetDiamonds(model);
            var ActiveDiamond = GetDataUsingLINQ(activeGridParameters.Sort,       //order by column
                                        activeGridParameters.SortDirection,   //order by direction
                                        activeGridParameters.Page ?? 1,       //returned page
                                        pageSize, ActiveDiamondList);               //displayed rows per page




            //set record count for use in view

            ViewBag.ActiveGridRecordCount = ActiveDiamondList.Count;

            return View(Tuple.Create(ActiveDiamond, model));
        }

        //get data from datasource using LINQ (sample data access layer)
        private IEnumerable<DiamondModels> GetDataUsingLINQ(string sort, string sortDir, int page, int numRows, List<DiamondModels> DiamondList)
        {

            //List<RoleModels> list = RolesList.AsQueryable().Skip((page - 1) * numRows).Take(numRows).ToList();
            List<DiamondModels> list = DiamondList.AsQueryable().ToList();


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
            DiamondModels model = new DiamondModels();

            MarkUpHelper markup = new MarkUpHelper();
            model.OtherMarkupList = markup.GetMarkUpList(new MarkUpModels { Flag = 1 });

            model.IsCurrencyLock = true;
            model.IsMarkupLock = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(DiamondModels model)
        {
            try
            {
                model.IsCurrencyLock = true;
                model.IsMarkupLock = true;
                if (!model.IsCurrencyLock)
                {
                    //ModelState.Remove("AudValue");
                    //model.AudValue = 0;
                }
                if (!model.IsMarkupLock)
                {
                    //ModelState.Remove("PriceFrom");
                    //ModelState.Remove("PriceTo");
                    //ModelState.Remove("MarkUpPercentage");
                    //ModelState.Remove("MarkUpAmount");
                    //ModelState.Remove("MarkUpTax");
                }
                else
                {
                    try
                    {
                        Convert.ToDecimal(model.Markup.PriceFrom);
                        Convert.ToDecimal(model.Markup.PriceTo);
                        Convert.ToDecimal(model.Markup.MarkUpPercentage);
                        Convert.ToDecimal(model.Markup.MarkUpAmount);
                        Convert.ToDecimal(model.Markup.MarkUpTax);
                        if (Convert.ToInt64(model.Markup.PriceFrom) >= Convert.ToInt64(model.Markup.PriceTo))
                        {
                            model.Message = "Markup price To should be greater then Price From...!";
                            model.MessageClass = "MsgRed";
                            TempData["CommonMessage"] = CommonData.GetMessage(model.Message, 0);
                            model.IsShowMessage = 1;
                            return View(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        model.Message = "Please input correct values in Markup's";
                        model.MessageClass = "MsgRed";
                        TempData["CommonMessage"] = CommonData.GetMessage(model.Message, 0);
                        model.IsShowMessage = 1;
                        return View(model);
                    }
                }
                if (ModelState.IsValid)
                {
                    DiamondHelper objDiamondHelper = new DiamondHelper();

                    #region [-- CODE FOR UPLOAD DIAMOND IMAGE FILE --]
                    if (Request.Files["fuDiamondImage"] != null)
                    {
                        HttpPostedFileBase fuDiamondImage = Request.Files["fuDiamondImage"];

                        if (!String.IsNullOrEmpty(fuDiamondImage.FileName))
                        {
                            string extension = Path.GetExtension(fuDiamondImage.FileName).ToLower();
                            if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                            {
                                var rnd = new Random();
                                int randomNumber = rnd.Next(123456789);
                                string dirPath = Server.MapPath("~/Content/Uploads/DiamondImage/");
                                if (Directory.Exists(dirPath))
                                {
                                    string strTempFileName = randomNumber + Path.GetExtension(fuDiamondImage.FileName);
                                    string strFileOrignalName = fuDiamondImage.FileName;
                                    fuDiamondImage.SaveAs(dirPath + strTempFileName);
                                    model.DiamondImage = strTempFileName;
                                }
                            }
                            else
                            {
                                //StringResource.GetStringResourceFile("admin.Diamond")

                                model.Message = "Diamond image format not correct. please upload only .jpg, .jpeg, .png format file.";
                                model.MessageClass = "MsgRed";
                                TempData["CommonMessage"] = CommonData.GetMessage(model.Message, 0);
                                model.IsShowMessage = 1;
                                return View(model);
                            }
                        }
                    }
                    #endregion

                    #region [-- CODE FOR UPLOAD DIAMOND CERTIFICATE FILE --]
                    if (Request.Files["fuDiamondCertificate"] != null)
                    {
                        HttpPostedFileBase fuDiamondCertificate = Request.Files["fuDiamondCertificate"];

                        if (!String.IsNullOrEmpty(fuDiamondCertificate.FileName))
                        {
                            string extension = Path.GetExtension(fuDiamondCertificate.FileName).ToLower();
                            if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".doc" || extension == ".docx" || extension == ".pdf")
                            {
                                var rnd = new Random();
                                int randomNumber = rnd.Next(123456789);
                                string dirPath = Server.MapPath("~/Content/Uploads/DiamondCertificate/");
                                if (Directory.Exists(dirPath))
                                {
                                    string strTempFileName = fuDiamondCertificate.FileName;// randomNumber + Path.GetExtension(fuDiamondCertificate.FileName);
                                    string strFileOrignalName = fuDiamondCertificate.FileName;
                                    fuDiamondCertificate.SaveAs(dirPath + strTempFileName);
                                    model.DiamondCertificate = strTempFileName;
                                }
                            }
                            else
                            {
                                model.Message = "Diamond image format not correct. please upload only .jpg, .jpeg, .png, .doc, .docx, .pdf format file.";
                                model.MessageClass = "MsgRed";
                                TempData["CommonMessage"] = CommonData.GetMessage(model.Message, 0);
                                model.IsShowMessage = 1;
                                return View(model);
                            }
                        }
                    }
                    #endregion

                    #region [-- CODE FOR UPLOAD DIAMOND VIDEO FILE --]
                    if (Request.Files["fuDiamondVideo"] != null)
                    {
                        HttpPostedFileBase fuDiamondVideo = Request.Files["fuDiamondVideo"];

                        if (!String.IsNullOrEmpty(fuDiamondVideo.FileName))
                        {
                            string extension = Path.GetExtension(fuDiamondVideo.FileName).ToLower();
                            if (extension == ".mp4" || extension == ".mov" || extension == ".ogg" || extension == ".webm")
                            {
                                var rnd = new Random();
                                int randomNumber = rnd.Next(123456789);
                                string dirPath = Server.MapPath("~/Content/Uploads/DiamondVideo/");
                                if (Directory.Exists(dirPath))
                                {
                                    string strTempFileName = Path.GetFileNameWithoutExtension(fuDiamondVideo.FileName)+ randomNumber.ToString() + extension;
                                    string strFileOrignalName = fuDiamondVideo.FileName;
                                    fuDiamondVideo.SaveAs(dirPath + strTempFileName);
                                    model.DiamondVideo = strTempFileName;
                                }
                            }
                            else
                            {
                                model.Message = "Diamond video format not correct. Please upload only .mp4, .mov, .ogg, .webm format file only.";
                                model.MessageClass = "MsgRed";
                                TempData["CommonMessage"] = CommonData.GetMessage(model.Message, 0);
                                model.IsShowMessage = 1;
                                return View(model);
                            }
                        }
                    }
                    #endregion

                    //
                    model.PerCaratPrice = model.Price;
                    model.Price = Convert.ToDouble(model.Price) * Convert.ToDouble(model.Carat);

                    model.Flag = 1;

                    int result = objDiamondHelper.AddUpdCanturiDiamond(model);
                    if (result > 0)
                    {
                        if (model.IsMarkupLock)
                        {
                            model.Markup.DiamondID = result;
                            model.Markup.IsInHand = true;
                            model.Markup.Flag = 4;
                            MarkUpHelper objMarkUpHelper = new MarkUpHelper();
                            objMarkUpHelper.AddUpdMarkUp(model.Markup);
                        }
                        TempData["CommonMessage"] = CommonData.GetMessage("Diamond added successfully.", 1);
                        return Redirect(Url.Content("~/SecureAdmin/Diamond/"));
                    }
                    else if (result == -2)
                    {
                        TempData["CommonMessage"] = CommonData.GetMessage("Lot# already exists", 0);
                    }
                    else
                    {
                        TempData["CommonMessage"] = CommonData.GetMessage("Error, Please try again", 0);
                    }

                }
            }
            catch (Exception ex)
            {
                new AppError().LogMe(ex);
                TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
            }
            return View(model);
        }

        

        public ActionResult Edit(string id)
        {
            DiamondModels model = new DiamondModels();
            try
            {

                MarkUpHelper markup = new MarkUpHelper();

                model.Flag = 2;
                model.DiamondId = id;
                DiamondHelper objDiamondHelper = new DiamondHelper();
                model = objDiamondHelper.GetDiamonds(model)[0];


                model.AudFinalPrice = markup.GetCanturiDiamondFinalPrice(model.Price.ToString(),model.DiamondId.ToString());

                model.Price = Math.Truncate((model.Price / model.Carat) * 100) / 100;

                model.Markup = markup.GetMarkUpById(new MarkUpModels { Flag = 3, DiamondID = Convert.ToInt32(model.DiamondId) });

                model.OtherMarkupList = markup.GetMarkUpList(new MarkUpModels { Flag = 1 });
                model.IsMarkupLock = true;
                model.IsCurrencyLock = true;
            }
            catch (Exception)
            {
                return Redirect(Url.Content("~/secureadmin/diamond/"));
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string id, DiamondModels model)
        {
            try
            {
                model.IsCurrencyLock = true;
                model.IsMarkupLock = true;
                if (!model.IsCurrencyLock)
                {
                    //ModelState.Remove("AudValue");
                    //model.AudValue = 0;
                }
                if (!model.IsMarkupLock)
                {
                    //ModelState.Remove("PriceFrom");
                    //ModelState.Remove("PriceTo");
                    //ModelState.Remove("MarkUpPercentage");
                    //ModelState.Remove("MarkUpAmount");
                    //ModelState.Remove("MarkUpTax");
                }
                else
                {
                    try
                    {
                        Convert.ToDecimal(model.Markup.PriceFrom);
                        Convert.ToDecimal(model.Markup.PriceTo);
                        Convert.ToDecimal(model.Markup.MarkUpPercentage);
                        Convert.ToDecimal(model.Markup.MarkUpAmount);
                        Convert.ToDecimal(model.Markup.MarkUpTax);
                        if (Convert.ToInt64(model.Markup.PriceFrom) >= Convert.ToInt64(model.Markup.PriceTo))
                        {
                            model.Message = "Markup price To should be greater then Price From...!";
                            model.MessageClass = "MsgRed";
                            TempData["CommonMessage"] = CommonData.GetMessage(model.Message, 0);
                            model.IsShowMessage = 1;
                            return View(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        model.Message = "Please input correct values in Markup's";
                        model.MessageClass = "MsgRed";
                        TempData["CommonMessage"] = CommonData.GetMessage(model.Message, 0);
                        model.IsShowMessage = 1;
                        return View(model);
                    }
                }
                if (ModelState.IsValid)
                {
                    #region [-- CODE FOR UPLOAD DIAMOND IMAGE FILE --]
                    if (Request.Files["fuDiamondImage"] != null)
                    {
                        HttpPostedFileBase fuDiamondImage = Request.Files["fuDiamondImage"];

                        if (!String.IsNullOrEmpty(fuDiamondImage.FileName))
                        {
                            string extension = Path.GetExtension(fuDiamondImage.FileName).ToLower();
                            if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                            {
                                var rnd = new Random();
                                int randomNumber = rnd.Next(123456789);
                                string dirPath = Server.MapPath("~/Content/Uploads/DiamondImage/");
                                if (Directory.Exists(dirPath))
                                {
                                    string strTempFileName = randomNumber + Path.GetExtension(fuDiamondImage.FileName);
                                    string strFileOrignalName = fuDiamondImage.FileName;
                                    fuDiamondImage.SaveAs(dirPath + strTempFileName);
                                    model.DiamondImage = strTempFileName;
                                }
                            }
                            else
                            {
                                //StringResource.GetStringResourceFile("admin.Diamond")

                                model.Message = "Diamond image format not correct. please upload only .jpg, .jpeg, .png format file.";
                                model.MessageClass = "MsgRed";
                                TempData["CommonMessage"] = CommonData.GetMessage(model.Message, 0);
                                model.IsShowMessage = 1;
                                return View(model);
                            }
                        }
                    }
                    #endregion

                    #region [-- CODE FOR UPLOAD DIAMOND CERTIFICATE FILE --]
                    if (Request.Files["fuDiamondCertificate"] != null)
                    {
                        HttpPostedFileBase fuDiamondCertificate = Request.Files["fuDiamondCertificate"];

                        if (!String.IsNullOrEmpty(fuDiamondCertificate.FileName))
                        {
                            string extension = Path.GetExtension(fuDiamondCertificate.FileName).ToLower();
                            if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".doc" || extension == ".docx" || extension == ".pdf")
                            {
                                var rnd = new Random();
                                int randomNumber = rnd.Next(123456789);
                                string dirPath = Server.MapPath("~/Content/Uploads/DiamondCertificate/");
                                if (Directory.Exists(dirPath))
                                {
                                    string strTempFileName = fuDiamondCertificate.FileName;// randomNumber + Path.GetExtension(fuDiamondCertificate.FileName);
                                    string strFileOrignalName = fuDiamondCertificate.FileName;
                                    fuDiamondCertificate.SaveAs(dirPath + strTempFileName);
                                    model.DiamondCertificate = strTempFileName;
                                }
                            }
                            else
                            {
                                model.Message = "Diamond image format not correct. please upload only .jpg, .jpeg, .png, .doc, .docx, .pdf format file.";
                                model.MessageClass = "MsgRed";
                                TempData["CommonMessage"] = CommonData.GetMessage(model.Message, 0);
                                model.IsShowMessage = 1;
                                return View(model);
                            }
                        }
                    }
                    #endregion

                    #region [-- CODE FOR UPLOAD DIAMOND VIDEO FILE --]
                    if (Request.Files["fuDiamondVideo"] != null)
                    {
                        HttpPostedFileBase fuDiamondVideo = Request.Files["fuDiamondVideo"];

                        if (!String.IsNullOrEmpty(fuDiamondVideo.FileName))
                        {
                            string extension = Path.GetExtension(fuDiamondVideo.FileName).ToLower();
                            if (extension == ".mp4" || extension == ".mov" || extension == ".ogg" || extension == ".webm")
                            {
                                var rnd = new Random();
                                int randomNumber = rnd.Next(123456789);
                                string dirPath = Server.MapPath("~/Content/Uploads/DiamondVideo/");
                                if (Directory.Exists(dirPath))
                                {
                                    string strTempFileName = Path.GetFileNameWithoutExtension(fuDiamondVideo.FileName) + randomNumber.ToString() + extension;
                                    string strFileOrignalName = fuDiamondVideo.FileName;
                                    fuDiamondVideo.SaveAs(dirPath + strTempFileName);
                                    model.DiamondVideo = strTempFileName;
                                }
                            }
                            else
                            {
                                model.Message = "Diamond video format not correct. Please upload only .mp4, .mov, .ogg, .webm format file only.";
                                model.MessageClass = "MsgRed";
                                TempData["CommonMessage"] = CommonData.GetMessage(model.Message, 0);
                                model.IsShowMessage = 1;
                                return View(model);
                            }
                        }
                    }
                    #endregion
                    //
                    model.PerCaratPrice = model.Price;
                    model.Price = Convert.ToDouble(model.Price) * Convert.ToDouble(model.Carat);

                    model.Flag = 2;
                    model.DiamondId = id;
                    DiamondHelper objDiamondHelper = new DiamondHelper();
                    int result = objDiamondHelper.AddUpdCanturiDiamond(model);
                    if (result == 0)
                    {
                        if (model.IsMarkupLock)
                        {
                            model.Markup.DiamondID = Convert.ToInt32(model.DiamondId);
                            model.Markup.IsInHand = true;
                            model.Markup.Flag = 5;
                            model.Markup.Status = 1;
                            MarkUpHelper objMarkUpHelper = new MarkUpHelper();
                            objMarkUpHelper.AddUpdMarkUp(model.Markup);
                        }
                        else
                        {
                            model.Markup.DiamondID = Convert.ToInt32(model.DiamondId);
                            model.Markup.IsInHand = true;
                            model.Markup.Flag = 5;
                            model.Markup.Status = 0;
                            MarkUpHelper objMarkUpHelper = new MarkUpHelper();
                            objMarkUpHelper.AddUpdMarkUp(model.Markup);
                        }
                        TempData["CommonMessage"] = CommonData.GetMessage("Diamond updated successfully.", 1);
                        return Redirect(Url.Content("~/SecureAdmin/Diamond/"));
                    }
                    else if (result == -2)
                    {
                        TempData["CommonMessage"] = CommonData.GetMessage("Lot# already exists", 0);
                    }
                    else
                    {
                        TempData["CommonMessage"] = CommonData.GetMessage("Error, Please try again", 0);
                    }
                    model.Price = model.Price / model.Carat;
                }
                else
                {
                    TempData["CommonMessage"] = CommonData.GetMessage("Error, Please provide required validation", 0);
                }
            }
            catch (Exception ex)
            {
                TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
            }

            return View(model);
        }

        public ActionResult CsvUpload()
        {
            DiamondModels model = new DiamondModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult CsvUpload(DiamondModels model)
        {
            try
            {
                string csvType = Request.Form["rbtnCSV"];
                int rowCount = 0;

                #region [-- CODE FOR UPLOAD DIAMOND CSV FILE --]
                if (Request.Files["fuCsvFile"] != null)
                {
                    HttpPostedFileBase fuCsvFile = Request.Files["fuCsvFile"];

                    if (!String.IsNullOrEmpty(fuCsvFile.FileName))
                    {
                        string extension = Path.GetExtension(fuCsvFile.FileName).ToLower();
                        if (extension == ".csv")
                        {
                            var rnd = new Random();
                            int randomNumber = rnd.Next(123456789);
                            string dirPath = Server.MapPath("~/Content/Uploads/");
                            if (Directory.Exists(dirPath))
                            {
                                string strTempFileName = randomNumber + Path.GetExtension(fuCsvFile.FileName);
                                string strFileOrignalName = fuCsvFile.FileName;
                                fuCsvFile.SaveAs(dirPath + strTempFileName);
                                model.DiamondImage = strTempFileName;

                                //Creating object of datatable  
                                DataTable tblcsv = new DataTable();
                                //creating columns  

                                if (csvType.ToLower() == "canturi")
                                {
                                    tblcsv.Columns.Add("SUPPLIER");
                                    tblcsv.Columns.Add("LOT #");
                                    tblcsv.Columns.Add("Shape");
                                    tblcsv.Columns.Add("Carat");
                                    tblcsv.Columns.Add("COLOUR");
                                    tblcsv.Columns.Add("Clarity");
                                    tblcsv.Columns.Add("Cut");
                                    tblcsv.Columns.Add("Polish");
                                    tblcsv.Columns.Add("SYMMETRY");
                                    tblcsv.Columns.Add("FLUORESCENCE");
                                    tblcsv.Columns.Add("Depth");
                                    tblcsv.Columns.Add("Table");
                                    tblcsv.Columns.Add("MEASUREMENTS");
                                    tblcsv.Columns.Add("Ratio");
                                    tblcsv.Columns.Add("LAB");
                                    //tblcsv.Columns.Add("Price");
                                    tblcsv.Columns.Add("CERT #");
                                    tblcsv.Columns.Add("EYE CLEAN");
                                    tblcsv.Columns.Add("$/Ct");
                                    tblcsv.Columns.Add("TOTAL PRICE");
                                    tblcsv.Columns.Add("View");
                                }
                                else
                                {
                                    tblcsv.Columns.Add("SUPPLIER");
                                    tblcsv.Columns.Add("LOT #");
                                    tblcsv.Columns.Add("SHAPE");
                                    tblcsv.Columns.Add("CARAT");
                                    tblcsv.Columns.Add("COLOR");
                                    tblcsv.Columns.Add("CLARITY");
                                    tblcsv.Columns.Add("CUT");
                                    tblcsv.Columns.Add("POLISH");
                                    tblcsv.Columns.Add("SYMMETRY");
                                    tblcsv.Columns.Add("FLUORESCENCE");
                                    tblcsv.Columns.Add("DEPTH");
                                    tblcsv.Columns.Add("TABLE");
                                    tblcsv.Columns.Add("MESUREMENTS");
                                    //tblcsv.Columns.Add("Ratio");
                                    tblcsv.Columns.Add("LAB");
                                    //tblcsv.Columns.Add("Price");
                                    tblcsv.Columns.Add("CERT #");
                                    tblcsv.Columns.Add("CERT LINK");
                                    tblcsv.Columns.Add("EYE CLEAN");
                                    tblcsv.Columns.Add("$/CT");
                                    tblcsv.Columns.Add("TOTAL PRICE");
                                    //tblcsv.Columns.Add("View");
                                }

                                //getting full file path of Uploaded file  
                                string CSVFilePath = dirPath + strTempFileName;// Path.GetFullPath(FileUpload1.PostedFile.FileName);
                                if (csvType.ToLower() == "canturi" || csvType.ToLower() == "venus")
                                {
                                    if (csvType.ToLower() == "venus")
                                    {
                                        tblcsv.Columns.Add("Luster");
                                        tblcsv.Columns.Add("VIDEO LINK");
                                    }
                                    else
                                    {
                                        ReadCsvToDataTable(tblcsv, CSVFilePath);
                                    }
                                }
                                DiamondHelper objDiamondHelper = new DiamondHelper();

                                if (csvType.ToLower() == "canturi")
                                {
                                    rowCount = ExtractCanturiData(tblcsv, objDiamondHelper, rowCount);
                                }
                                if (csvType.ToLower() == "panache")
                                {
                                    DataTable dt = ConvertCSVtoDataTable(CSVFilePath);
                                    rowCount = ExtractPanacheData(dt, objDiamondHelper, rowCount);
                                }

                                if (csvType.ToLower() == "ydvash")
                                {
                                    tblcsv.Columns.Add("Stone Image");
                                    DataTable dt = ConvertCSVtoDataTable(CSVFilePath);
                                    rowCount = ExtractYDVashData(dt, objDiamondHelper, rowCount);
                                }

                                if (csvType.ToLower() == "harikrishna")
                                {
                                    tblcsv.Columns.Add("VideoLink");
                                    DataTable dt = ConvertCSVtoDataTable(CSVFilePath);
                                    rowCount = ExtractHariKrishnaData(dt, objDiamondHelper, rowCount);

                                }
                                if (csvType.ToLower() == "venus")
                                {
                                    DataTable dt = ConvertCSVtoDataTable(CSVFilePath);

                                    rowCount = ExtractVenusData(dt, objDiamondHelper, rowCount);
                                }
                                if (csvType.ToLower() == "finestar")
                                {
                                    tblcsv.Columns.Add("Ratio");
                                    tblcsv.Columns.Add("VideoLink");
                                    DataTable dt = ConvertCSVtoDataTable(CSVFilePath);
                                    rowCount = ExtractFineStarData(dt, objDiamondHelper, rowCount);
                                }
                                if (csvType.ToLower() == "kapugems")
                                {
                                    DataTable dt = ConvertCSVtoDataTable(CSVFilePath);
                                    rowCount = ExtractKapuGemsData(dt, objDiamondHelper, rowCount);
                                }
                            }
                            if (rowCount != 0)
                            {
                                TempData["CommonMessage"] = CommonData.GetMessage("Csv data exported successfully", 1);
                            }
                            else
                            {
                                if (TempData["CommonMessage"] == null)
                                {
                                    TempData["CommonMessage"] = CommonData.GetMessage("Error, Please try again...!", 0);
                                }
                            }
                        }
                        else
                        {
                            model.Message = "Csv file format not correct. please upload only .csv format file.";
                            model.MessageClass = "MsgRed";
                            TempData["CommonMessage"] = CommonData.GetMessage(model.Message, 0);
                            model.IsShowMessage = 1;
                            return View(model);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                new AppError().LogMe(ex);
                TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
            }

            return View(model);
        }


        private static void ReadCsvToDataTable(DataTable tblcsv, string CSVFilePath)
        {
            //Reading All text  
            string ReadCSV = System.IO.File.ReadAllText(CSVFilePath);
            //spliting row after new line  
            foreach (string csvRow in ReadCSV.Split('\n'))
            {
                if (!string.IsNullOrEmpty(csvRow))
                {
                    //Adding each row into datatable  
                    tblcsv.Rows.Add();
                    int count = 0;
                    foreach (string FileRec in csvRow.Split(','))
                    {
                        try
                        {
                            tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;
                        }
                        catch
                        {

                        }
                        count++;
                    }
                }


            }
        }

        private int ExtractCanturiData(DataTable tblcsv, DiamondHelper objDiamondHelper, int rowCount)
        {
            foreach (DataRow item in tblcsv.Rows)
            {
                if (rowCount != 0)
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(item["LOT #"].ToString()))
                        {
                            DiamondModels objDiamondModels = new DiamondModels
                            {
                                Supplier = item["SUPPLIER"].ToString(),
                                LotNumber = item["LOT #"].ToString(),
                                Shape = item["Shape"].ToString(),
                                Carat = Convert.ToDouble(item["Carat"]),
                                Color = item["COLOUR"].ToString().Replace("+", "").Replace("-", ""),
                                Clartiy = item["Clarity"].ToString().Replace("+", "").Replace("-", ""),
                                Cut = item["Cut"].ToString().Replace("+", "").Replace("-", ""),
                                Polish = item["Polish"].ToString(),
                                Symmetry = item["Symmetry"].ToString(),
                                Flourescence = item["Fluorescence"].ToString(),
                                Depth = item["Depth"].ToString(),
                                Table = item["Table"].ToString(),
                                Measurements = item["Measurements"].ToString(),
                                Ratio = item["Ratio"].ToString(),
                                Lab = item["LAB"].ToString(),
                                Price = Convert.ToDouble(item["TOTAL PRICE"]),
                                PerCaratPrice = Convert.ToDouble(item["$/Ct"]),
                                EyeClean = item["EYE CLEAN"].ToString(),
                                DiamondCertificate = item["CERT #"].ToString(),
                                Flag = 1
                            };
                            objDiamondHelper.AddUpdCanturiDiamond(objDiamondModels);
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
                        return 0;
                    }

                }
                rowCount++;
            }
            return rowCount;
        }

        private static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            try
            {
                using (StreamReader sr = new StreamReader(strFilePath))
                {
                    string[] headers = sr.ReadLine().Split(',');
                    foreach (string header in headers)
                    {
                        dt.Columns.Add(header);
                    }

                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                            DataRow dr = dt.NewRow();


                            for (int i = 0; i < headers.Length; i++)
                            {
                                dr[i] = rows[i];
                            }
                            dt.Rows.Add(dr);
                        }
                        catch
                        {

                        }

                    }

                }
            }
            catch
            {

            }
            return dt;
        }

        private static DataTable GetDataTableFromCsv(string path, bool isFirstRowHeader)
        {
            string header = isFirstRowHeader ? "Yes" : "No";

            string pathOnly = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);

            string sql = @"SELECT * FROM [" + fileName + "]";

            using (OleDbConnection connection = new OleDbConnection(
                        @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                        ";Extended Properties=\"Text;HDR=" + header + "\""))
            using (OleDbCommand command = new OleDbCommand(sql, connection))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                dataTable.Locale = CultureInfo.CurrentCulture;
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        private int ExtractPanacheData(DataTable tblcsv, DiamondHelper objDiamondHelper, int rowCount)
        {
            rowCount = 1;
            foreach (DataRow item in tblcsv.Rows)
            {
                if (!String.IsNullOrEmpty(item["STOCK"].ToString()))
                {
                    try
                    {
                        DiamondModels objDiamondModels = new DiamondModels
                        {
                            Supplier = "Panache",//item["SUPPLIER"].Equals(DBNull.Value) ? "" : item["SUPPLIER"].ToString(),//  item["SUPPLIER"].ToString(),
                            LotNumber = item["STOCK"].Equals(DBNull.Value) ? "" : item["STOCK"].ToString(),//item["LOT #"].ToString(),
                            Shape = "Round",//-	File supplied to us without a SHAPE column – ALL diamonds on Panache are ROUND shape
                            Carat = item["SIZE"].Equals(DBNull.Value) ? 0 : String.IsNullOrEmpty(item["SIZE"].ToString()) ? 0 : Convert.ToDouble(item["SIZE"]),//Convert.ToDouble(item["Carat"]),
                            Color = item["COL"].Equals(DBNull.Value) ? "" : item["COL"].ToString().Replace("+", "").Replace("-", ""),// item["COLOR"].ToString(),
                            Clartiy = item["CLR"].Equals(DBNull.Value) ? "" : item["CLR"].ToString().Replace("+", "").Replace("-", ""),// item["Clarity"].ToString(),
                            Cut = item["CUT"].Equals(DBNull.Value) ? "" : CommonData.GetCutForVenus(item["CUT"].ToString().Replace("+", "").Replace("-", "")),// CommonData.GetCutForVenus(item["Cut"].ToString()),
                            Polish = item["POL"].Equals(DBNull.Value) ? "" : CommonData.GetPolishForVenus(item["POL"].ToString()),//CommonData.GetPolishForVenus(item["Polish"].ToString()),
                            Symmetry = item["SYM"].Equals(DBNull.Value) ? "" : CommonData.GetSymmetryForVenus(item["SYM"].ToString()),//CommonData.GetSymmetryForVenus(item["Symmetry"].ToString()),
                            Flourescence = item["FL"].Equals(DBNull.Value) ? "" : CommonData.GetFluorescenceForHariKrishna(item["FL"].ToString()),//CommonData.GetFluorescenceForVenus(item["Fluorescence"].ToString()),
                            Depth = item["TD"].Equals(DBNull.Value) ? "" : item["TD"].ToString(),//item["Depth"].ToString(),
                            Table = item["TB"].Equals(DBNull.Value) ? "" : item["TB"].ToString(),//item["Table"].ToString(),
                            Measurements = item["M.M"].Equals(DBNull.Value) ? "" : item["M.M"].ToString(),// item["MESUREMENTS"].ToString(),                                                    
                            //Ratio = item["Ratio"].ToString(),
                            Lab = item["LAB"].Equals(DBNull.Value) ? "" : item["LAB"].ToString(),// item["LAB"].ToString(),
                            Price = item["T-$"].Equals(DBNull.Value) ? 0 : String.IsNullOrEmpty(item["T-$"].ToString()) ? 0 : Convert.ToDouble(item["T-$"]),// Convert.ToDouble(item["TOTAL PRICE"]),
                            PerCaratPrice = item["$/CTS"].Equals(DBNull.Value) ? 0 : String.IsNullOrEmpty(item["$/CTS"].ToString()) ? 0 : Convert.ToDouble(item["$/CTS"]),// Convert.ToDouble(item["$/Ct"]),


                            EyeClean = item["EYE CLEAN"].Equals(DBNull.Value) ? "" : item["EYE CLEAN"].ToString(),// item["EYE CLEAN"].ToString(),
                            DiamondCertificate = item["CERT"].Equals(DBNull.Value) ? "" : item["CERT"].ToString(),//item["CERT #"].ToString(),
                            PageNumber = rowCount,
                            Flag = 1
                        };

                        if (item["STOCK"].ToString() == "9098107")
                        {

                        }

                        if (item["EYE CLEAN"].Equals(DBNull.Value))
                        {
                            objDiamondModels.EyeClean = "yes";
                        }
                        else if (String.IsNullOrEmpty(item["EYE CLEAN"].ToString()))
                        {
                            objDiamondModels.EyeClean = "yes";
                        }
                        else
                        {
                            if (item["EYE CLEAN"].ToString().ToUpper().TrimEnd().TrimStart().Trim() == "-")
                            {
                                objDiamondModels.EyeClean = "yes";
                            }
                            else if (item["EYE CLEAN"].ToString().ToUpper().TrimEnd().TrimStart().Trim() == "YES")
                            {
                                objDiamondModels.EyeClean = "yes";
                            }
                            else if (item["EYE CLEAN"].ToString().ToUpper().TrimEnd().TrimStart().Trim() == "0")
                            {
                                objDiamondModels.EyeClean = "yes";
                            }
                            else if (item["EYE CLEAN"].ToString().ToUpper().TrimEnd().TrimStart().Trim() == "95%")
                            {
                                objDiamondModels.EyeClean = "no";
                            }
                            else
                            {
                                objDiamondModels.EyeClean = "no";
                            }

                        }

                        objDiamondHelper.AddUpdPanacheJewelsDiamond(objDiamondModels);
                        rowCount++;
                    }
                    catch (Exception ex)
                    {
                        TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
                        return 0;
                    }

                }

            }
            return rowCount;
        }

        private int ExtractHariKrishnaData(DataTable tblcsv, DiamondHelper objDiamondHelper, int rowCount)
        {
            rowCount = 1;
            //Status

            // DataRow[] filterRows = tblcsv.Select("[Status] IN ('MKAV','MKIS') AND [MILKY] IN ('M0','NV')"); //'AVAILABLE','WEB HOLD'

            DataRow[] filterRows = tblcsv.Select("[MILKY] IN ('M0','NV') AND [Color Shade] IN ('WH','NV')"); //'AVAILABLE','WEB HOLD'



            foreach (DataRow item in filterRows)
            {
                if (!String.IsNullOrEmpty(item["Stock NO"].ToString()))
                {
                    try
                    {
                        if (item["LAB"].ToString().ToUpper() == "GIA")
                        {
                            if (item["Stock NO"].ToString().ToUpper() == "222115-371")
                            {
                                string s = "ok";
                            }
                            DiamondModels objDiamondModels = new DiamondModels
                            {
                                Supplier = "Hari Krishna",//item["SUPPLIER"].Equals(DBNull.Value) ? "" : item["SUPPLIER"].ToString(),//  item["SUPPLIER"].ToString(),
                                LotNumber = item["Stock NO"].Equals(DBNull.Value) ? "" : item["Stock NO"].ToString(),//item["LOT #"].ToString(),
                                Shape = item["SHAPE"].Equals(DBNull.Value) ? "" : item["SHAPE"].ToString(),
                                Carat = item["Carat"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(item["Carat"]),//Convert.ToDouble(item["Carat"]),
                                Color = item["Color"].Equals(DBNull.Value) ? "" : item["Color"].ToString().Replace("+", "").Replace("-", ""),// item["COLOR"].ToString(),
                                Clartiy = item["Clarity"].Equals(DBNull.Value) ? "" : item["Clarity"].ToString().Replace("+", "").Replace("-", ""),// item["Clarity"].ToString(),
                                Cut = item["CUT"].Equals(DBNull.Value) ? "" : CommonData.GetCutForHariKrishna(item["CUT"].ToString().Replace("+", "").Replace("-", "")),// CommonData.GetCutForVenus(item["Cut"].ToString()),
                                Polish = item["Polish"].Equals(DBNull.Value) ? "" : CommonData.GetPolishForHariKrishna(item["Polish"].ToString()),//CommonData.GetPolishForVenus(item["Polish"].ToString()),
                                Symmetry = item["Symmetry"].Equals(DBNull.Value) ? "" : CommonData.GetSymmetryForVenus(item["Symmetry"].ToString()),//CommonData.GetSymmetryForVenus(item["Symmetry"].ToString()),
                                Flourescence = item["Fluorescent"].Equals(DBNull.Value) ? "" : CommonData.GetFluorescenceForHariKrishna(item["Fluorescent"].ToString()),//CommonData.GetFluorescenceForVenus(item["Fluorescence"].ToString()),
                                Depth = item["TD%"].Equals(DBNull.Value) ? "" : item["TD%"].ToString(),//item["Depth"].ToString(),
                                Table = item["Tab%"].Equals(DBNull.Value) ? "" : item["Tab%"].ToString(),//item["Table"].ToString(),


                                Measurements = item["Measurement"].Equals(DBNull.Value) ? "" : item["Measurement"].ToString().Replace("-", "x"),// item["MESUREMENTS"].ToString(),    




                                //Ratio = item["Ratio"].Equals(DBNull.Value) ? "" : item["Ratio"].ToString(),
                                Lab = item["LAB"].Equals(DBNull.Value) ? "" : item["LAB"].ToString(),// item["LAB"].ToString(),
                                Price = item["Amount"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(item["Amount"]),// Convert.ToDouble(item["TOTAL PRICE"]),
                                PerCaratPrice = item["PR/CT"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(item["PR/CT"]),// Convert.ToDouble(item["$/Ct"]),


                                EyeClean = item["EC"].Equals(DBNull.Value) ? "" : item["EC"].ToString(),// item["EYE CLEAN"].ToString(),
                                DiamondCertificate = item["CERT_NO"].Equals(DBNull.Value) ? "" : item["CERT_NO"].ToString(),//item["CERT #"].ToString(),
                                DiamondVideo = item["VideoLink"].Equals(DBNull.Value) ? "" : item["VideoLink"].ToString(),
                                PageNumber = rowCount,
                                Flag = 1
                            };
                            try
                            {
                                string[] mesaruments = (item["Measurement"].Equals(DBNull.Value) ? "" : item["Measurement"].ToString().Replace("-", "x")).ToLower().Split('x');

                                if (mesaruments.Count() != 0)
                                {
                                    decimal firstValue = Convert.ToDecimal(mesaruments[0]);
                                    decimal lastValue = Convert.ToDecimal(mesaruments[1]);

                                    decimal largestValue = firstValue > lastValue ? firstValue : lastValue;

                                    decimal lowestValue = lastValue < firstValue ? lastValue : firstValue;
                                    objDiamondModels.Ratio = String.Format("{0:0.000}", (largestValue / lowestValue));


                                }

                            }
                            catch { }






                            if (item["EC"].Equals(DBNull.Value))
                            {
                                objDiamondModels.EyeClean = "no";
                            }
                            else if (String.IsNullOrEmpty(item["EC"].ToString()))
                            {
                                objDiamondModels.EyeClean = "no";
                            }
                            else
                            {
                                if (item["EC"].ToString().ToUpper() == "E1")
                                {
                                    objDiamondModels.EyeClean = "yes";
                                }
                                else
                                {
                                    objDiamondModels.EyeClean = "no";
                                }
                            }

                            objDiamondHelper.AddUpdHariKrishnaJewelsDiamond(objDiamondModels);
                            rowCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
                        return 0;
                    }

                }

            }
            return rowCount;
        }

        private int ExtractVenusData(DataTable tblcsv, DiamondHelper objDiamondHelper, int rowCount)
        {
            DataRow[] filterRows = tblcsv.Select("Luster IN ('EX','VG','GD')");

            foreach (DataRow item in filterRows)
            {
                //if (rowCount != 0)
                //{

                if (!String.IsNullOrEmpty(item["LOT #"].ToString()))
                {
                    try
                    {
                        DiamondModels objDiamondModels = new DiamondModels
                        {
                            Supplier = item["SUPPLIER"].Equals(DBNull.Value) ? "" : item["SUPPLIER"].ToString(),//  item["SUPPLIER"].ToString(),
                            LotNumber = item["LOT #"].Equals(DBNull.Value) ? "" : item["LOT #"].ToString(),//item["LOT #"].ToString(),
                            Shape = item["Shape"].Equals(DBNull.Value) ? "" : CommonData.GetShapeForVenus(item["Shape"].ToString()),// CommonData.GetShapeForVenus( item["Shape"].ToString()),
                            Carat = item["Carat"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(item["Carat"]),//Convert.ToDouble(item["Carat"]),
                            Color = item["COLOR"].Equals(DBNull.Value) ? "" : item["COLOR"].ToString().Replace("+", "").Replace("-", ""),// item["COLOR"].ToString(),
                            Clartiy = item["Clarity"].Equals(DBNull.Value) ? "" : item["Clarity"].ToString().Replace("+", "").Replace("-", ""),// item["Clarity"].ToString(),
                            Cut = item["Cut"].Equals(DBNull.Value) ? "" : CommonData.GetCutForVenus(item["Cut"].ToString().Replace("+", "").Replace("-", "")),// CommonData.GetCutForVenus(item["Cut"].ToString()),
                            Polish = item["Polish"].Equals(DBNull.Value) ? "" : CommonData.GetPolishForVenus(item["Polish"].ToString()),//CommonData.GetPolishForVenus(item["Polish"].ToString()),
                            Symmetry = item["Symmetry"].Equals(DBNull.Value) ? "" : CommonData.GetSymmetryForVenus(item["Symmetry"].ToString()),//CommonData.GetSymmetryForVenus(item["Symmetry"].ToString()),
                            Flourescence = item["Fluorescence"].Equals(DBNull.Value) ? "" : CommonData.GetFluorescenceForVenus(item["Fluorescence"].ToString()),//CommonData.GetFluorescenceForVenus(item["Fluorescence"].ToString()),
                            Depth = item["Depth"].Equals(DBNull.Value) ? "" : item["Depth"].ToString(),//item["Depth"].ToString(),
                            Table = item["Table"].Equals(DBNull.Value) ? "" : item["Table"].ToString(),//item["Table"].ToString(),
                            Measurements = item["MESUREMENTS"].Equals(DBNull.Value) ? "" : item["MESUREMENTS"].ToString(),// item["MESUREMENTS"].ToString(),                                                    
                            //Ratio = item["Ratio"].ToString(),
                            Lab = item["LAB"].Equals(DBNull.Value) ? "" : item["LAB"].ToString(),// item["LAB"].ToString(),
                            Price = item["TOTAL PRICE"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(item["TOTAL PRICE"]),// Convert.ToDouble(item["TOTAL PRICE"]),
                            PerCaratPrice = item["$/Ct"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(item["$/Ct"]),// Convert.ToDouble(item["$/Ct"]),




                            EyeClean = item["EYE CLEAN"].Equals(DBNull.Value) ? "" : item["EYE CLEAN"].ToString(),// item["EYE CLEAN"].ToString(),
                            DiamondCertificate = item["CERT #"].Equals(DBNull.Value) ? "" : item["CERT #"].ToString(),//item["CERT #"].ToString(),
                            PageNumber = rowCount,
                            DiamondVideo = item["VIDEO LINK"].Equals(DBNull.Value) ? "" : item["VIDEO LINK"].ToString(),
                            CertURL = item["CERT LINK"].Equals(DBNull.Value) ? "" : item["CERT LINK"].ToString(),//item["CERT #"].ToString(),
                            Flag = 1
                        };

                        if (item["EYE CLEAN"].Equals(DBNull.Value))
                        {
                            objDiamondModels.EyeClean = "yes";
                        }
                        else if (String.IsNullOrEmpty(item["EYE CLEAN"].ToString()))
                        {
                            objDiamondModels.EyeClean = "yes";
                        }
                        else
                        {
                            if (item["EYE CLEAN"].ToString().ToUpper() == "E0")
                            {
                                objDiamondModels.EyeClean = "yes";
                            }
                            else if (item["EYE CLEAN"].ToString().ToUpper() == "E1")
                            {
                                objDiamondModels.EyeClean = "no";
                            }
                            else if (item["EYE CLEAN"].ToString().ToUpper() == "E2")
                            {
                                objDiamondModels.EyeClean = "no";
                            }
                            else
                            {
                                objDiamondModels.EyeClean = "no";
                            }

                        }

                        objDiamondHelper.AddUpdVenusJewelsDiamond(objDiamondModels);
                    }
                    catch (Exception ex)
                    {
                        TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
                        return 0;
                    }

                }
                //}
                rowCount++;
            }
            return rowCount;
        }

        private int ExtractFineStarData(DataTable tblcsv, DiamondHelper objDiamondHelper, int rowCount)
        {
            rowCount = 1;

            string strFilterCriteria = "";
            strFilterCriteria = strFilterCriteria + "LAB='GIA' ";
            strFilterCriteria = strFilterCriteria + " AND SHAPE IN ('ROUND','PS','PC','MQ','EM','SE','RN','HT','OV','CS','CM')";
            strFilterCriteria = strFilterCriteria + " AND COL IN ('J','I','H','G','F','E','D','J+','I+','H+','G+','F+','E+','D+','J-','I-','H-','G-','F-','E-','D-')";
            strFilterCriteria = strFilterCriteria + " AND CLARITY IN ('SI2','SI1','VS2','VS1','VVS2','VVS1','IF','FL')";
            strFilterCriteria = strFilterCriteria + " AND Cut IN ('EX','G','GD','VG','EX+','G+','GD+','VG+','EX-','G-','GD-','VG-')";
            strFilterCriteria = strFilterCriteria + " AND POL IN ('EX','G','GD','VG','EX+','G+','GD+','VG+','EX-','G-','GD-','VG-')";
            strFilterCriteria = strFilterCriteria + " AND Sym IN ('EX','G','GD','VG','EX+','G+','GD+','VG+','EX-','G-','GD-','VG-')";
            strFilterCriteria = strFilterCriteria + " AND Fls IN ('FNT','MED','NON','STG','FNT+','MED+','NON+','STG+','FNT-','MED-','NON-','STG-','VST')";


            DataRow[] drFineStar = tblcsv.Select(strFilterCriteria);

            foreach (DataRow item in drFineStar)
            {

                if (!String.IsNullOrEmpty(item["PACKET NO"].ToString()))
                {
                    try
                    {
                        //string[] strPacketNumber = { "541870141", "441880651", "441880721", "849755261", "420766721", "575727742", "528741271", "528740580", "849755880", "848742951" };

                        //if (strPacketNumber.Contains(item["PACKET NO"].ToString()))
                        //{

                        //}
                        DiamondModels objDiamondModels = new DiamondModels
                        {

                            Supplier = "Fine Star",
                            LotNumber = item["PACKET NO"].Equals(DBNull.Value) ? "" : item["PACKET NO"].ToString(),
                            Shape = item["SHAPE"].Equals(DBNull.Value) ? "" : CommonData.GetShapeForFineStar(item["Shape"].ToString()),
                            Carat = item["CARAT"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(item["CARAT"]),
                            Color = item["COL"].Equals(DBNull.Value) ? "" : item["COL"].ToString().Replace("+", "").Replace("-", ""),
                            Clartiy = item["CLARITY"].Equals(DBNull.Value) ? "" : item["CLARITY"].ToString().Replace("+", "").Replace("-", ""),
                            Cut = item["CUT"].Equals(DBNull.Value) ? "" : CommonData.GetCutForFineStar(item["Cut"].ToString().Replace("+", "").Replace("-", "")),
                            Polish = item["POL"].Equals(DBNull.Value) ? "" : CommonData.GetPolishForFineStar(item["POL"].ToString()),
                            Symmetry = item["SYM"].Equals(DBNull.Value) ? "" : CommonData.GetSymmetryForFineStar(item["SYM"].ToString()),
                            Flourescence = item["FLS"].Equals(DBNull.Value) ? "" : CommonData.GetFluorescenceForFineStar(item["FLS"].ToString()),
                            Depth = item["DEPTH"].Equals(DBNull.Value) ? "" : item["DEPTH"].ToString(),
                            Table = item["Table %"].Equals(DBNull.Value) ? "" : item["Table %"].ToString(),
                            Measurements = item["Length"].ToString() + "x" + item["Width"].ToString() + "x" + item["Depth"].ToString(),// L * W * D
                            Ratio = item["RATIO"].ToString(),
                            Lab = item["LAB"].Equals(DBNull.Value) ? "" : item["LAB"].ToString(),
                            Price = item["Price/ct"].Equals(DBNull.Value) ? 0 : (Convert.ToDouble(item["Price/ct"].ToString().TrimStart('"').TrimEnd('"')) * (item["CARAT"].Equals(DBNull.Value) ? 1 : Convert.ToDouble(item["CARAT"]))),
                            PerCaratPrice = item["Price/ct"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(item["Price/ct"].ToString().TrimStart('"').TrimEnd('"')),



                            EyeClean = item["EC"].Equals(DBNull.Value) ? "" : item["EC"].ToString(),
                            DiamondCertificate = item["REPORT NO"].Equals(DBNull.Value) ? "" : item["REPORT NO"].ToString(),//item["CERT #"].ToString(),
                            DiamondVideo = "", //item["VideoLink"].Equals(DBNull.Value) ? "" : item["VideoLink"].ToString(),
                            PageNumber = rowCount,
                            Flag = 1
                        };

                        if (item["EC"].Equals(DBNull.Value))
                        {
                            objDiamondModels.EyeClean = "yes";
                        }
                        else if (String.IsNullOrEmpty(item["EC"].ToString()))
                        {
                            objDiamondModels.EyeClean = "yes";
                        }
                        else
                        {
                            if (item["EC"].ToString().ToUpper() == "Y")
                            {
                                objDiamondModels.EyeClean = "yes";
                            }
                            else if (item["EC"].ToString().ToUpper() == "N")
                            {
                                objDiamondModels.EyeClean = "no";
                            }
                            else
                            {
                                objDiamondModels.EyeClean = "no";
                            }

                        }

                        objDiamondHelper.AddUpdFineStarDiamond(objDiamondModels);
                    }
                    catch (Exception ex)
                    {
                        TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);

                        return 0;
                    }

                }
                rowCount++;
            }
            return rowCount;
        }

        private int ExtractKapuGemsData(DataTable tblcsv, DiamondHelper objDiamondHelper, int rowCount)
        {
            DataRow[] filterRows = tblcsv.Select("[MILKY] = '-' AND [VIEW CERT] = 'GIA'");

            foreach (DataRow item in filterRows)
            {
                if (!String.IsNullOrEmpty(item["Packet Id"].ToString()))
                {
                    try
                    {
                        DiamondModels objDiamondModels = new DiamondModels
                        {
                            Supplier = "Kapu Gems",//  item["SUPPLIER"].ToString(),
                            LotNumber = item["Packet Id"].Equals(DBNull.Value) ? "" : item["Packet Id"].ToString(),//item["LOT #"].ToString(),
                            Shape = item["Shape"].Equals(DBNull.Value) ? "" : CommonData.GetShapeForKapuGems(item["Shape"].ToString()),// CommonData.GetShapeForVenus( item["Shape"].ToString()),
                            Carat = item["Weight"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(item["Weight"]),//Convert.ToDouble(item["Carat"]),
                            Color = item["COL"].Equals(DBNull.Value) ? "" : item["COL"].ToString().Replace("+", "").Replace("-", ""),// item["COLOR"].ToString(),
                            Clartiy = item["CLRT"].Equals(DBNull.Value) ? "" : item["CLRT"].ToString().Replace("+", "").Replace("-", ""),// item["Clarity"].ToString(),
                            Cut = item["Cut"].Equals(DBNull.Value) ? "" : CommonData.GetCutForKapuGems(item["Cut"].ToString().Replace("+", "").Replace("-", "")),// CommonData.GetCutForVenus(item["Cut"].ToString()),
                            Polish = item["POL"].Equals(DBNull.Value) ? "" : CommonData.GetPolishForKapuGems(item["POL"].ToString()),//CommonData.GetPolishForVenus(item["Polish"].ToString()),
                            Symmetry = item["SYM"].Equals(DBNull.Value) ? "" : CommonData.GetSymmetryForKapuGems(item["SYM"].ToString()),//CommonData.GetSymmetryForVenus(item["Symmetry"].ToString()),
                            Flourescence = item["FLOR"].Equals(DBNull.Value) ? "" : CommonData.GetFluorescenceForKapuGems(item["FLOR"].ToString()),//CommonData.GetFluorescenceForVenus(item["Fluorescence"].ToString()),
                            Depth = item["DEP%"].Equals(DBNull.Value) ? "" : item["DEP%"].ToString(),//item["Depth"].ToString(),
                            Table = item["TAB%"].Equals(DBNull.Value) ? "" : item["TAB%"].ToString(),//item["Table"].ToString(),
                            Measurements = item["MEASUREMENT"].Equals(DBNull.Value) ? "" : item["MEASUREMENT"].ToString(),// item["MESUREMENTS"].ToString(),                                                    
                            //Ratio = item["Ratio"].ToString(),
                            Lab = item["VIEW CERT"].Equals(DBNull.Value) ? "" : item["VIEW CERT"].ToString(),// item["LAB"].ToString(),

                            Price = item["Amount"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(item["Amount"]),// Convert.ToDouble(item["TOTAL PRICE"]),
                            PerCaratPrice = item["PR/CT"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(item["PR/CT"]),// Convert.ToDouble(item["$/Ct"]),




                            EyeClean = item["INCL. VISIBLITY"].Equals(DBNull.Value) ? "" : item["INCL. VISIBLITY"].ToString(),// item["EYE CLEAN"].ToString(),
                            DiamondCertificate = item["REPORT NO"].Equals(DBNull.Value) ? "" : item["REPORT NO"].ToString(),//item["CERT #"].ToString(),
                            PageNumber = rowCount,
                            Flag = 1
                        };

                        if (String.IsNullOrEmpty(objDiamondModels.EyeClean))
                        {
                            objDiamondModels.EyeClean = "yes";
                        }
                        else
                        {
                            if (objDiamondModels.EyeClean.ToUpper() == "-")
                            {
                                objDiamondModels.EyeClean = "yes";
                            }
                            else if (objDiamondModels.EyeClean.ToUpper() == "EX11")
                            {
                                objDiamondModels.EyeClean = "yes";
                            }
                            else if (objDiamondModels.EyeClean.ToUpper() == "EC2")
                            {
                                objDiamondModels.EyeClean = "no";
                            }
                            else
                            {
                                objDiamondModels.EyeClean = "no";
                            }

                        }

                        objDiamondHelper.AddUpdKapuGemsJewelsDiamond(objDiamondModels);
                    }
                    catch (Exception ex)
                    {
                        TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
                        return 0;
                    }

                }
                rowCount++;
            }
            return rowCount;
        }

        private int ExtractYDVashData(DataTable tblcsv, DiamondHelper objDiamondHelper, int rowCount)
        {
            rowCount = 1;
            foreach (DataRow item in tblcsv.Rows)
            {
                if (!String.IsNullOrEmpty(item["LotID"].ToString()))
                {
                    try
                    {
                        DiamondModels objDiamondModels = new DiamondModels
                        {
                            Supplier = "YDVash",
                            LotNumber = item["LotID"].Equals(DBNull.Value) ? "" : item["LotID"].ToString(),
                            Shape = item["Sh"].Equals(DBNull.Value) ? "" : CommonData.GetShapeForYDVash(item["Sh"].ToString()),
                            Carat = item["Weight"].Equals(DBNull.Value) ? 0 : String.IsNullOrEmpty(item["Weight"].ToString()) ? 0 : Convert.ToDouble(item["Weight"]),
                            Color = item["Col"].Equals(DBNull.Value) ? "" : item["Col"].ToString().Replace(" +", "").Replace("-", ""),
                            Clartiy = item["Cla"].Equals(DBNull.Value) ? "" : item["Cla"].ToString().Replace(" +", "").Replace("-", ""),
                            Cut = item["Cut"].Equals(DBNull.Value) ? "" : CommonData.GetCutForYDVash(item["Cut"].ToString().Replace(" +", "").Replace("-", "")),
                            Polish = item["Pol"].Equals(DBNull.Value) ? "" : CommonData.GetPolishForYDVash(item["Pol"].ToString()),
                            Symmetry = item["Sym"].Equals(DBNull.Value) ? "" : CommonData.GetSymmetryForYDVash(item["Sym"].ToString()),
                            Flourescence = item["Flr"].Equals(DBNull.Value) ? "" : CommonData.GetFluorescenceForYDVash(item["Flr"].ToString()),
                            Depth = item["Dep."].Equals(DBNull.Value) ? "" : item["Dep."].ToString(),
                            Table = item["Tab."].Equals(DBNull.Value) ? "" : item["Tab."].ToString(),
                            Measurements = item["M1"].Equals(DBNull.Value) ? "" : item["M2"].Equals(DBNull.Value) ? "" : item["M3"].Equals(DBNull.Value) ? "" : (item["M1"].ToString() + "x" + item["M2"].ToString() + "x" + item["M3"].ToString()),
                            Ratio = "",
                            Lab = item["Lab"].Equals(DBNull.Value) ? "" : item["Lab"].ToString(),
                            Price = item["Total Amount us $"].Equals(DBNull.Value) ? 0 : String.IsNullOrEmpty(item["Total Amount us $"].ToString()) ? 0 : Convert.ToDouble(item["Total Amount us $"]),// Convert.ToDouble(item["TOTAL PRICE"]),
                            PerCaratPrice = item["Price p/ct"].Equals(DBNull.Value) ? 0 : String.IsNullOrEmpty(item["Price p/ct"].ToString()) ? 0 : Convert.ToDouble(item["Price p/ct"]),// Convert.ToDouble(item["$/Ct"]),


                            EyeClean = item["EYE CLEAN"].Equals(DBNull.Value) ? "" : item["EYE CLEAN"].ToString(),
                            DiamondCertificate = item["Cert number"].Equals(DBNull.Value) ? "" : item["Cert number"].ToString(),
                            PageNumber = rowCount,
                            DiamondVideo = item["Stone Image"].Equals(DBNull.Value) ? "" : item["Stone Image"].ToString(),
                            Flag = 1
                        };

                        if (String.IsNullOrEmpty(item["EYE CLEAN"].ToString()))
                        {
                            if (objDiamondModels.Clartiy == "VS2" || objDiamondModels.Clartiy == "VS1" || objDiamondModels.Clartiy == "VVS2" || objDiamondModels.Clartiy == "VVS1" || objDiamondModels.Clartiy == "IF" || objDiamondModels.Clartiy == "FL" || objDiamondModels.Clartiy == "I1")
                            {
                                objDiamondModels.EyeClean = "yes";
                            }
                            else if (objDiamondModels.Clartiy == "SI1" || objDiamondModels.Clartiy == "SI2")
                            {
                                objDiamondModels.EyeClean = "no";
                            }
                        }
                        else
                        {
                            if (item["EYE CLEAN"].ToString().ToUpper().TrimEnd().TrimStart().Trim() == "E1")
                            {
                                objDiamondModels.EyeClean = "no";
                            }
                            else if (item["EYE CLEAN"].ToString().ToUpper().TrimEnd().TrimStart().Trim() == "E0")
                            {
                                objDiamondModels.EyeClean = "yes";
                            }
                        }

                        objDiamondHelper.AddUpdYDVashDiamond(objDiamondModels);
                        rowCount++;
                    }
                    catch (Exception ex)
                    {
                        TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
                        return 0;
                    }

                }

            }
            return rowCount;
        }

        public ActionResult Delete(string id)
        {
            try
            {
                DiamondModels model = new DiamondModels();
                model.DiamondId = id;
                model.Flag = 3;
                model.Status = 3;
                DiamondHelper objDiamondHelper = new DiamondHelper();
                objDiamondHelper.AddUpdCanturiDiamond(model);
                //TempData["MessageClass"] = "MsgGreen";
                TempData["CommonMessage"] = CommonData.GetMessage(StringResource.GetStringResourceFile("admin.Diamond.Delete"), 1);// "Record Updated successfully!";
            }
            catch (Exception ex)
            {
                new AppError().LogMe(ex);
                TempData["CommonMessage"] = CommonData.GetMessage(ex.Message, 0);
            }
            return Redirect(Url.Content("~/SecureAdmin/Diamond/"));
        }


    }
}
