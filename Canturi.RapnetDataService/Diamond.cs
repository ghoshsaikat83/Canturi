using Canturi.RapnetDataService.com.rapaport.technet;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace Canturi.RapnetDataService
{
    public class Diamond
    {
        int DiamondCount;
        #region Private Variable
        string _constring = string.Empty;
        #endregion

        public Diamond()
        {
            try
            {

                _constring = ConfigurationSettings.AppSettings["CanturiConnectionStr"].ToString();
            }
            catch
            {
                throw new ApplicationException("Sorry, connection to database could not made");
            }
        }

        public RootObject RapnetApi(int page)
        {

            using (WebClient client = new WebClient())
            {
                string Data = "{" +
                                "\"request\": {" +
                                    "\"header\": {" +
                                        "\"username\": \"" + ConfigurationSettings.AppSettings["RapnetUserName"] + "\"," +
                                        "\"password\": \"" + ConfigurationSettings.AppSettings["RapnetPassword"] + "\"" +

                                    "}," +
                                    "\"body\": {" +
                    // "\"search_type\": \"White\","+
                    //"\"shapes\": [\"Sq. Emerald\"]," +
                    //"\"size_from\": 0.2,"+
                    //"\"size_to\": 15.3,"+
                    //"\"color_from\": \"D\","+
                    //"\"color_to\": \"K\","+
                    //"\"clarity_from\": \"IF\","+
                    //"\"clarity_to\": \"VS2\","+
                    //"\"cut_from\": \"Excellent\","+
                    //"\"cut_to\": \"Fair\","+
                    //"\"polish_from\": \"Excellent\","+
                    //"\"polish_to\": \"Fair\","+
                    //"\"symmetry_from\": \"Excellent\","+
                    //"\"symmetry_to\": \"Fair\","+
                    //"\"price_total_from\": 100,"+
                    //"\"price_total_to\": 150000,"+
                    //"\"labs\": [\"GIA\", \"IGI\"],"+
                    //"\"table_percent_from\": \"26.0\","+
                    //"\"table_percent_to\": \"66.0\","+
                                        "\"page_number\": " + page + "," +
                                        "\"page_size\": 50" +//," +
                    //"\"sort_by\": \"price\"," +
                    //"\"sort_direction\": \"Asc\"" +
                                    "}" +
                                "}" +
                            "}";
                string URL = "https://technet.rapaport.com/HTTP/JSON/RetailFeed/GetDiamonds.aspx ";
                WebRequest webRequest = WebRequest.Create(URL);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                Stream reqStream = webRequest.GetRequestStream();
                string postData = Data;
                byte[] postArray = Encoding.ASCII.GetBytes(postData);
                reqStream.Write(postArray, 0, postArray.Length);
                reqStream.Close();
                StreamReader sr = new StreamReader(webRequest.GetResponse().GetResponseStream());
                string Result = sr.ReadToEnd();



                return JsonConvert.DeserializeObject<RootObject>(Result);
            }
        }

        public bool IsNotValidCut(string val)
        {
            val = val.ToUpper();
            if (val.Contains("NONE") || val.Contains("IDEAL") || val.Contains("FAIR") || val.Contains("POOR"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsValidData(DataRow row)
        {
            if (!row["Availability"].Equals(DBNull.Value))
            {
                if (row["Availability"].ToString().ToUpper() == "OUT ON MEMO")
                {
                    return false;
                }
            }

            if (!row["Clarity Enhanced"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["Clarity Enhanced"]))
                {
                    return false;
                }
            }
            if (!row["Color Enhanced"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["Color Enhanced"]))
                {
                    return false;
                }
            }
            if (!row["Clarity Enhanced"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["Clarity Enhanced"]))
                {
                    return false;
                }
            }
            if (!row["HPHT"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["HPHT"]))
                {
                    return false;
                }
            }
            if (!row["Irradiated"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["Irradiated"]))
                {
                    return false;
                }
            }
            if (!row["Laser Drilled"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["Laser Drilled"]))
                {
                    return false;
                }
            }
            if (!row["Other Treatment"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["Other Treatment"]))
                {
                    return false;
                }
            }

            if (!row["Milky"].Equals(DBNull.Value))
            {
                if (!String.IsNullOrEmpty(row["Milky"].ToString()))
                {
                    if (row["Milky"].ToString().ToUpper() != "NONE") 
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsValidKapuGemsData(DataRow row)
        {

            if (!row["Availability"].Equals(DBNull.Value))
            {
                if (row["Availability"].ToString().ToUpper() == "OUT ON MEMO")
                {
                    return false;
                }
            }

            if (!row["Clarity Enhanced"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["Clarity Enhanced"]))
                {
                    return false;
                }
            }
            if (!row["Color Enhanced"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["Color Enhanced"]))
                {
                    return false;
                }
            }
            if (!row["Clarity Enhanced"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["Clarity Enhanced"]))
                {
                    return false;
                }
            }
            if (!row["HPHT"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["HPHT"]))
                {
                    return false;
                }
            }
            if (!row["Irradiated"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["Irradiated"]))
                {
                    return false;
                }
            }
            if (!row["Laser Drilled"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["Laser Drilled"]))
                {
                    return false;
                }
            }
            if (!row["Other Treatment"].Equals(DBNull.Value))
            {
                if (Convert.ToBoolean(row["Other Treatment"]))
                {
                    return false;
                }
            }
            if (!row["Milky"].Equals(DBNull.Value))
            {
                if (!String.IsNullOrEmpty(row["Milky"].ToString()))
                {
                    if (row["Milky"].ToString().ToUpper() != "NONE")
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public void CanturiDaimond()
        {
            try
            {

                //You must request a ticket using HTTPS protocol
                string URLAuth = "https://technet.rapaport.com/HTTP/Authenticate.aspx";
                WebClient webClient = new WebClient();

                NameValueCollection formData = new NameValueCollection();
                formData["Username"] = ConfigurationSettings.AppSettings["RapnetUserName"].ToString();
                formData["Password"] = ConfigurationSettings.AppSettings["RapnetPassword"].ToString();
                byte[] responseBytes = webClient.UploadValues(URLAuth, "POST", formData);
                string ResultAuth = Encoding.UTF8.GetString(responseBytes);

                //After receiving an encrypted ticket, you can use it to authenticate your session.
                //Now you can choose to change the protocol to HTTP so it works faster.

                //Download File
                string URL = "http://technet.rapaport.com/HTTP/DLS/GetFile.aspx";
                WebRequest webRequest = WebRequest.Create(URL);

                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                Stream reqStream = webRequest.GetRequestStream();
                string postData = "ticket=" + ResultAuth;
                byte[] postArray = Encoding.ASCII.GetBytes(postData);
                reqStream.Write(postArray, 0, postArray.Length);
                reqStream.Close();

                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse(); ;

                Stream stream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string content = reader.ReadToEnd();

                byte[] fileBytes = Encoding.ASCII.GetBytes(content);
                string fileName = Guid.NewGuid() + ".csv";
                FileStream fs = new FileStream(ConfigurationSettings.AppSettings["DlsCsvPath"] + fileName, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(fileBytes);
                bw.Close();

                webResponse.Close();

                //string fileName = "8cae9f11-92ef-470f-a6b3-c9efbaf4abcc.csv";

                //Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(ConfigurationSettings.AppSettings["DlsCsvPath"] + fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //wb.SaveAs(ConfigurationSettings.AppSettings["DlsCsvPath"] + fileName.Replace(".csv", ".xlsx"), Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //wb.Close();
                //app.Quit();

                //*
                //string fileName = "0eee0de8-0c31-489f-a14b-45c6273ae42a.csv";

                //fileName = "rapnet-data.csv";
                DataTable dtRapnetData = GetDataTabletFromCSVFile(ConfigurationSettings.AppSettings["DlsCsvPath"] + fileName);
                DataTable MainDiamondTable = new DataTable();
                MainDiamondTable.Columns.Add("DiamondID");
                MainDiamondTable.Columns.Add("VendorStockNumber");
                MainDiamondTable.Columns.Add("ShapeTitle");
                MainDiamondTable.Columns.Add("Weight");
                MainDiamondTable.Columns.Add("ColorTitle");
                MainDiamondTable.Columns.Add("ClarityTitle");
                MainDiamondTable.Columns.Add("CutLongTitle");
                MainDiamondTable.Columns.Add("PolishTitle");
                MainDiamondTable.Columns.Add("SymmetryTitle");
                MainDiamondTable.Columns.Add("FluorescenceIntensityTitle");
                MainDiamondTable.Columns.Add("DepthPercent");
                MainDiamondTable.Columns.Add("TablePercent");

                MainDiamondTable.Columns.Add("MeasLength");
                MainDiamondTable.Columns.Add("MeasWidth");
                MainDiamondTable.Columns.Add("MeasDepth");
                MainDiamondTable.Columns.Add("Measurements");
                MainDiamondTable.Columns.Add("LabTitle");
                MainDiamondTable.Columns.Add("Ratio");
                MainDiamondTable.Columns.Add("CertificateNumber");
                MainDiamondTable.Columns.Add("DiamondCertificate");
                MainDiamondTable.Columns.Add("HasCertFile");
                MainDiamondTable.Columns.Add("PerCaratPrice");
                MainDiamondTable.Columns.Add("RapNetPrice");
                MainDiamondTable.Columns.Add("FinalPrice");
                MainDiamondTable.Columns.Add("TotalSalesPriceInCurrency");
                MainDiamondTable.Columns.Add("FluorescenceColorTitle");
                MainDiamondTable.Columns.Add("GirdleSizeMin");
                MainDiamondTable.Columns.Add("GirdleSizeMax");
                MainDiamondTable.Columns.Add("GirdleConditionTitle");
                MainDiamondTable.Columns.Add("CuletSizeTitle");
                MainDiamondTable.Columns.Add("CuletConditionTitle");
                MainDiamondTable.Columns.Add("SellerName");
                MainDiamondTable.Columns.Add("VideoURL");

                //DataRow[] rapnetDiamondsRows = dtRapnetData.Select("ISNULL(Milky,'') IN ('','NONE','none','None')");

                DataRow[] rapnetDiamonds = dtRapnetData.Select("[RapNet Account ID] <>'66444' AND [AVAILABILITY]  <> 'OUT ON MEMO'");
                DataRow[] kapuGemsDiamonds = dtRapnetData.Select("[RapNet Account ID]='66444' AND [AVAILABILITY]  <> 'OUT ON MEMO'");


                InsertRapnetDiamonds(MainDiamondTable, rapnetDiamonds);


                LogError("KapuGems Start - " + DateTime.Now.ToString());
                //insert kapu gems
                InsertKapuGemsDiamonds(kapuGemsDiamonds);
                //*/
            }
            catch (Exception ex)
            {

                LogError(ex.Message);
            }
        }

        private void InsertRapnetDiamonds(DataTable MainDiamondTable, DataRow[] rapnetDiamonds)
        {
            foreach (DataRow row in rapnetDiamonds)
            {
                try
                {
                    //PLEASE FILTER/BLOCK THE BELOW SUPPLIER FROM APPEARING ON OUR WEBSITE. (THIS IS RAPNET DUMMY DATA THAT NEEDS TO BE REMOVED AS THEY DON’T ACTUALLY EXIST);
                    // - SELLER NAME: SHASHVAT JEWELS PRIVATE LIMITED
                    //RAPNET CODE: 76719
                    //NAME CODE: SHASHVAT
                    if (row["RapNet Account ID"].ToString().Trim() != "76719")
                    {
                        //https://newmediaguru.basecamphq.com/projects/13179000-canturi-com-czz01_01/posts/99534986/comments#comment_358373824
                        //these 6 columns have the value of TRUE - do not display on our website.
                        if (IsValidData(row))
                        {
                            if (!IsNotValidCut(row["Cut"].ToString()))
                            {
                                //DataSet dsDiamonds = new DataSet();

                                //RetailFeed objRetailFeed = new RetailFeed();

                                //objRetailFeed = new RetailFeed();
                                //objRetailFeed.Login(ConfigurationSettings.AppSettings["RapnetUserName"], ConfigurationSettings.AppSettings["RapnetPassword"]);

                                //DataTable dtDiamonds = new DataTable();

                                //dtDiamonds = objRetailFeed.GetSingleDiamond(row["Diamond ID"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(row["Diamond ID"]));


                                DataRow tmpRow = MainDiamondTable.NewRow();
                                tmpRow["DiamondID"] = row["Diamond ID"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(row["Diamond ID"]);
                                tmpRow["VendorStockNumber"] = row["Diamond ID"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(row["Diamond ID"]);// row["Stock Number"];
                                tmpRow["ShapeTitle"] = row["Shape"];
                                tmpRow["Weight"] = row["Weight"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(row["Weight"].ToString().Replace("%", ""));// row["Weight"];
                                tmpRow["ColorTitle"] = row["Color"].ToString().Replace("+", "").Replace("-", "");
                                tmpRow["ClarityTitle"] = row["Clarity"].ToString().Replace("+", "").Replace("-", "");
                                tmpRow["CutLongTitle"] = row["Cut"].ToString().Replace("+", "").Replace("-", "");
                                tmpRow["PolishTitle"] = row["Polish"];
                                tmpRow["SymmetryTitle"] = row["Symmetry"];
                                tmpRow["FluorescenceIntensityTitle"] = row["Fluorescence Intensity"];
                                tmpRow["DepthPercent"] = row["Depth"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(row["Depth"].ToString().Replace("%", ""));// row["Depth"];
                                tmpRow["TablePercent"] = row["Table"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(row["Table"].ToString().Replace("%", "")); //row["Table"];
                                tmpRow["Measurements"] = row["Measurements"];
                                tmpRow["MeasLength"] = row["Meas Length"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(row["Meas Length"].ToString().Replace("%", "")); //row["Meas Length"];
                                tmpRow["MeasWidth"] = row["Meas Width"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(row["Meas Width"].ToString().Replace("%", "")); //row["Meas Width"];
                                tmpRow["MeasDepth"] = row["Meas Depth"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(row["Meas Depth"].ToString().Replace("%", "")); //row["Meas Depth"];
                                tmpRow["LabTitle"] = row["Lab"];
                                tmpRow["Ratio"] = row["Ratio"];
                                tmpRow["CertificateNumber"] = row["Certificate Number"];
                                tmpRow["DiamondCertificate"] = row["Certificate URL"];

                                tmpRow["HasCertFile"] = false;
                                if (!row["Certificate URL"].Equals(DBNull.Value))
                                {
                                    tmpRow["HasCertFile"] = true;
                                }

                                decimal pricePerCarat = 0;
                                if (row["Price Per Carat"].Equals(DBNull.Value))
                                {
                                    //strPricePercarat = row["Cash Price Per Carat"].ToString();
                                    pricePerCarat = row["Cash Price Per Carat"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(row["Cash Price Per Carat"]);
                                }
                                else
                                {
                                    //strPricePercarat = row["Price Per Carat"].ToString();
                                    pricePerCarat = row["Price Per Carat"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(row["Price Per Carat"]);
                                }


                                //decimal 



                                tmpRow["PerCaratPrice"] = pricePerCarat;// row["Price Per Carat"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(row["Price Per Carat"]); //row["Price Per Carat"];
                                tmpRow["RapNetPrice"] = pricePerCarat;// row["Price Per Carat"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(row["Price Per Carat"]);// row["Price Per Carat"];
                                tmpRow["FinalPrice"] = pricePerCarat;// row["Price Per Carat"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(row["Price Per Carat"]); //row["Price Per Carat"];
                                tmpRow["TotalSalesPriceInCurrency"] = pricePerCarat;// row["Price Per Carat"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(row["Price Per Carat"]);// row["Price Per Carat"];
                                tmpRow["FluorescenceColorTitle"] = row["Fluorescence Intensity"];
                                tmpRow["GirdleSizeMin"] = row["Girdle Min"];
                                tmpRow["GirdleSizeMax"] = row["Girdle Max"];
                                tmpRow["GirdleConditionTitle"] = row["Girdle"];
                                tmpRow["CuletSizeTitle"] = row["Culet"];
                                tmpRow["CuletConditionTitle"] = row["Culet Condition"];
                                tmpRow["SellerName"] = row["Seller Name"];
                                if (!row["Image URL"].Equals(DBNull.Value))
                                {
                                    tmpRow["VideoURL"] = string.IsNullOrEmpty(Convert.ToString(row["Image URL"])) ? "" : Convert.ToString(row["Image URL"]).Trim();  // Regex.Replace(Convert.ToString(row["Image URL"]).Trim(), @"\t|\n|\r", "")
                                }
                                else
                                {
                                    tmpRow["VideoURL"] = "";
                                }
                                MainDiamondTable.Rows.Add(tmpRow);
                            }
                        }
                    }
                    else
                    {

                    }
                }
                catch
                {

                }

            }

            SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
            SqlParameter prmDiamond = SqlHelper.CreateParameter("@Diamond", MainDiamondTable);
            SqlParameter prmPageNo = SqlHelper.CreateParameter("@PageNo", 1);
            SqlParameter[] AllParams = { prmErr, prmDiamond, prmPageNo };
            
            SqlHelper.ExecuteNonQuery(_constring, CommandType.StoredProcedure, "Usp_AddUpdRapnetDiamond", AllParams);
            if (prmErr.Value != null)
            {
                if (prmErr.Value.ToString() == "0")
                {
                    LogError("Successfully Inserting Daimond Procedure - " + DateTime.Now.ToString());
                }
                else
                {
                    LogError("Error on Inserting Daimond Procedure - " + DateTime.Now.ToString());
                }
            }
            else
            {
                LogError("Error on Inserting Daimond Procedure - " + DateTime.Now.ToString());
            }
        }

        private void InsertKapuGemsDiamonds(DataRow[] kapuGemsDiamonds)
        {
            int rowCount = 1;
            foreach (DataRow row in kapuGemsDiamonds)
            {
                if (!String.IsNullOrEmpty(row["Diamond ID"].ToString()))
                {
                    if (IsValidKapuGemsData(row))
                    {
                        if (!IsNotValidCut(row["Cut"].ToString()))
                        {
                            try
                            {
                                KapuGemsDiamondModels objDiamondModels = new KapuGemsDiamondModels
                                {
                                    Supplier = "Kapu Gems",
                                    LotNumber = row["Diamond ID"].Equals(DBNull.Value) ? "0" : row["Diamond ID"].ToString(),
                                    Shape = row["Shape"].Equals(DBNull.Value) ? "" : GetShapeForKapuGems(row["Shape"].ToString()),
                                    Carat = row["Weight"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(row["Weight"]),
                                    Color = row["Color"].ToString().Replace("+", "").Replace("-", ""),
                                    Clartiy = row["Clarity"].ToString().Replace("+", "").Replace("-", ""),
                                    Cut = row["Cut"].Equals(DBNull.Value) ? "" : GetCutForKapuGems(row["Cut"].ToString().Replace("+", "").Replace("-", "")),
                                    Polish = row["Polish"].Equals(DBNull.Value) ? "" : GetPolishForKapuGems(row["Polish"].ToString()),
                                    Symmetry = row["Symmetry"].Equals(DBNull.Value) ? "" : GetSymmetryForKapuGems(row["Symmetry"].ToString()),
                                    Flourescence = row["Fluorescence Intensity"].Equals(DBNull.Value) ? "" : GetFluorescenceForKapuGems(row["Fluorescence Intensity"].ToString()),
                                    Depth = row["Depth"].Equals(DBNull.Value) ? "" : row["Depth"].ToString().Replace("%", ""),
                                    Table = row["Table"].Equals(DBNull.Value) ? "" : row["Table"].ToString().Replace("%", ""),
                                    Measurements = row["Measurements"].Equals(DBNull.Value) ? "" : row["Measurements"].ToString(),

                                    Lab = row["Lab"].Equals(DBNull.Value) ? "" : row["Lab"].ToString(),
                                    PageNumber = rowCount,
                                    Price = row["Total Price"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(row["Total Price"]),
                                    Flag = 1
                                };
                                objDiamondModels.DiamondCertificate = row["Certificate Number"].ToString();

                                objDiamondModels.HasCertFile = false;
                                if (!row["Certificate URL"].Equals(DBNull.Value))
                                {
                                    objDiamondModels.HasCertFile = true;
                                }


                                double pricePerCarat = 0;
                                if (row["Price Per Carat"].Equals(DBNull.Value))
                                {
                                    pricePerCarat = row["Cash Price Per Carat"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(row["Cash Price Per Carat"]);
                                }
                                else
                                {
                                    pricePerCarat = row["Price Per Carat"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(row["Price Per Carat"]);
                                }

                                objDiamondModels.PerCaratPrice = pricePerCarat;
                                objDiamondModels.EyeClean = "Yes";
                                if (objDiamondModels.Clartiy == "SI2")
                                {
                                    objDiamondModels.EyeClean = "No";
                                }
                                AddUpdKapuGemsJewelsDiamond(objDiamondModels);

                                rowCount++;
                            }
                            catch
                            {

                            }
                        }
                    }


                }

            }
            LogError("Successfully Inserting Kapu Gems Daimond Procedure - " + DateTime.Now.ToString());
        }


        public int AddUpdKapuGemsJewelsDiamond(KapuGemsDiamondModels objDiamondModels)
        {
            int result = -1;

            try
            {
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", objDiamondModels.DiamondId);
                SqlParameter prmSkuDiamondID = SqlHelper.CreateParameter("@SkuDiamondID", objDiamondModels.SkuDiamondId);
                SqlParameter prmDiamondImage = SqlHelper.CreateParameter("@DiamondImage", objDiamondModels.DiamondImage);
                SqlParameter prmLotNumber = SqlHelper.CreateParameter("@LotNumber", objDiamondModels.LotNumber);
                SqlParameter prmShape = SqlHelper.CreateParameter("@Shape", objDiamondModels.Shape);
                SqlParameter prmCarat = SqlHelper.CreateParameter("@Carat", objDiamondModels.Carat);
                SqlParameter prmColor = SqlHelper.CreateParameter("@Color", objDiamondModels.Color);
                SqlParameter prmClarity = SqlHelper.CreateParameter("@Clarity", objDiamondModels.Clartiy);
                SqlParameter prmCut = SqlHelper.CreateParameter("@Cut", objDiamondModels.Cut);
                SqlParameter prmPolish = SqlHelper.CreateParameter("@Polish", objDiamondModels.Polish);
                SqlParameter prmSymmetry = SqlHelper.CreateParameter("@Symmetry", objDiamondModels.Symmetry);
                SqlParameter prmFluorescence = SqlHelper.CreateParameter("@Fluorescence", objDiamondModels.Flourescence);


                SqlParameter prmDepth = SqlHelper.CreateParameter("@Depth", objDiamondModels.Depth);
                SqlParameter prmTable = SqlHelper.CreateParameter("@Table", objDiamondModels.Table);



                SqlParameter prmMeasurements = SqlHelper.CreateParameter("@Measurements", objDiamondModels.Measurements);
                SqlParameter prmRatio = SqlHelper.CreateParameter("@Ratio", objDiamondModels.Ratio);

                SqlParameter prmLAB = SqlHelper.CreateParameter("@LAB", objDiamondModels.Lab);



                SqlParameter prmPrice = SqlHelper.CreateParameter("@Price", objDiamondModels.Price);
                SqlParameter prmHasCertFile = SqlHelper.CreateParameter("@HasCertFile", objDiamondModels.HasCertFile);

                SqlParameter prmSupplier = SqlHelper.CreateParameter("@Supplier", objDiamondModels.Supplier);

                //objDiamondModels.EyeClean = "NO";
                // if (objDiamondModels.IsClean)
                //{
                //    objDiamondModels.EyeClean = "YES";
                // }
                SqlParameter prmEyeClean = SqlHelper.CreateParameter("@EyeClean", objDiamondModels.EyeClean);
                SqlParameter prmPerCaratPrice = SqlHelper.CreateParameter("@PerCaratPrice", objDiamondModels.PerCaratPrice);

                SqlParameter prmDiamondCertificate = SqlHelper.CreateParameter("@DiamondCertificate", objDiamondModels.DiamondCertificate);
                SqlParameter prmRowNumber = SqlHelper.CreateParameter("@RowNumber", objDiamondModels.PageNumber);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objDiamondModels.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objDiamondModels.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objDiamondModels.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    
                                                prmDiamondID, prmSkuDiamondID, prmDiamondImage, 
                                                prmLotNumber, prmShape,prmCarat, 
                                                prmColor, prmClarity,prmCut, 
                                                prmPolish, prmSymmetry,prmFluorescence, 
                                                prmDepth, prmTable, prmMeasurements,  
                                                prmRatio,prmLAB,  prmPrice,prmHasCertFile,
                                                prmSupplier,prmEyeClean,prmPerCaratPrice,
                                                prmDiamondCertificate,prmRowNumber,
                                                prmCreatedBy, prmCreatedFromIp,  
                                                prmFlag, prmErr 
                                           };
                SqlHelper.ExecuteNonQuery(_constring, CommandType.StoredProcedure, "Usp_AddUpdKapuJemsDiamond", allParams);

                if (prmErr.Value != null)
                {
                    result = (int)prmErr.Value;
                }
            }
            catch
            {
                throw;
            }

            return result;
        }


        public void CanturiDaimond1()
        {
            try
            {
                Canturi.RapnetDataService.com.rapaport.technet1.Feed RF = new Canturi.RapnetDataService.com.rapaport.technet1.Feed();
                Canturi.RapnetDataService.com.rapaport.technet1.FeedParameters Params = new Canturi.RapnetDataService.com.rapaport.technet1.FeedParameters();

                //This must be done in HTTPS protocol
                RF.Login("Angelinda", "Canturi55");

                Params.PageSize = 50;//Max Page size is 50

                int DiamondCount = 0;




                DataSet dsDiamonds = new DataSet();
                int i = 1;
                i = 1;
                int rowsCount = 0;
                int totalLoop = 0;
                while (i > 0)
                {
                    DataTable dtDiamonds = new DataTable();
                    try
                    {
                        Params.PageNumber = i;
                        DataSet ds = RF.GetDiamonds(Params, ref DiamondCount);
                        dtDiamonds = ds.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                        str.WriteLine(ex.Message);
                        str.Close();
                        str.Dispose();
                    }
                    finally
                    {
                        if (i == 1)
                        {
                            double reminder = Math.IEEERemainder(((double)DiamondCount), (double)50);
                            totalLoop = Convert.ToInt32(DiamondCount / 50);
                            if (reminder != 0)
                            {
                                totalLoop = totalLoop + 1;
                            }
                        }
                        i++;

                        rowsCount = rowsCount + dtDiamonds.Rows.Count;
                        if (totalLoop <= i)
                        {
                            //if (DiamondCount <= rowsCount)
                            //{
                            i = 0;
                            //}
                        }
                        else
                        {
                            dsDiamonds.Merge(dtDiamonds);
                        }
                    }
                }
                WriteToExcelSpreadsheet("1.xlsx", dsDiamonds.Tables[0]);
                DataTable MainDiamondTable = new DataTable();
                MainDiamondTable.Columns.Add("DiamondID");
                MainDiamondTable.Columns.Add("VendorStockNumber");
                MainDiamondTable.Columns.Add("ShapeTitle");
                MainDiamondTable.Columns.Add("Weight");
                MainDiamondTable.Columns.Add("ColorTitle");
                MainDiamondTable.Columns.Add("ClarityTitle");
                MainDiamondTable.Columns.Add("CutLongTitle");
                MainDiamondTable.Columns.Add("PolishTitle");
                MainDiamondTable.Columns.Add("SymmetryTitle");
                MainDiamondTable.Columns.Add("FluorescenceIntensityTitle");
                MainDiamondTable.Columns.Add("DepthPercent");
                MainDiamondTable.Columns.Add("TablePercent");
                MainDiamondTable.Columns.Add("MeasLength");
                MainDiamondTable.Columns.Add("MeasWidth");
                MainDiamondTable.Columns.Add("MeasDepth");
                MainDiamondTable.Columns.Add("LabTitle");
                MainDiamondTable.Columns.Add("Ratio");
                MainDiamondTable.Columns.Add("CertificateNumber");
                MainDiamondTable.Columns.Add("HasCertFile");

                MainDiamondTable.Columns.Add("RapNetPrice");
                MainDiamondTable.Columns.Add("FinalPrice");
                MainDiamondTable.Columns.Add("TotalSalesPriceInCurrency");
                MainDiamondTable.Columns.Add("FluorescenceColorTitle");
                MainDiamondTable.Columns.Add("GirdleSizeMin");
                MainDiamondTable.Columns.Add("GirdleSizeMax");
                MainDiamondTable.Columns.Add("GirdleConditionTitle");
                MainDiamondTable.Columns.Add("CuletSizeTitle");
                MainDiamondTable.Columns.Add("CuletConditionTitle");

                foreach (DataRow row in dsDiamonds.Tables[0].Rows)
                {
                    if (row["VendorStockNumber"].ToString().ToUpper() == "ER-8096")
                    {

                    }
                    MainDiamondTable.ImportRow(row);
                }

                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                SqlParameter prmDiamond = SqlHelper.CreateParameter("@Diamond", MainDiamondTable);
                SqlParameter prmPageNo = SqlHelper.CreateParameter("@PageNo", 1);
                SqlParameter[] AllParams = { prmErr, prmDiamond, prmPageNo };
                SqlHelper.ExecuteNonQuery(_constring, CommandType.StoredProcedure, "Usp_AddUpdRapnetDiamond", AllParams);
                if (prmErr.Value != null)
                {
                    if (prmErr.Value.ToString() == "0")
                    {
                        StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                        str.WriteLine("Successfully Inserting Daimond Procedure - " + DateTime.Now.ToString());
                        str.Close();
                        str.Dispose();
                    }
                    else
                    {
                        StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                        str.WriteLine("Error on Inserting Daimond Procedure - " + DateTime.Now.ToString());
                        str.Close();
                        str.Dispose();
                    }
                }
                else
                {
                    StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                    str.WriteLine("Error on Inserting Daimond Procedure - " + DateTime.Now.ToString());
                    str.Close();
                    str.Dispose();
                }

            }
            catch (Exception ex)
            {

                StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                str.WriteLine(ex.Message);
                str.Close();
                str.Dispose();
            }
        }



        public void CanturiDaimond2()
        {
            try
            {
                StreamWriter str1 = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                str1.WriteLine("Service start on : " + DateTime.Now.ToString());
                str1.Close();
                str1.Dispose();
                //int i = 1;
                //while (i > 0)
                //{
                //    try
                //    {
                //        RootObject rootObject = RapnetApi(i);
                //        if (rootObject.response.body.diamonds != null && rootObject.response.body.diamonds.Count() > 0)
                //        {
                //            DataTable MainDiamondTable = new DataTable();
                //            MainDiamondTable.Columns.Add("DiamondID");
                //            MainDiamondTable.Columns.Add("VendorStockNumber");
                //            MainDiamondTable.Columns.Add("ShapeTitle");
                //            MainDiamondTable.Columns.Add("Weight");
                //            MainDiamondTable.Columns.Add("ColorTitle");
                //            MainDiamondTable.Columns.Add("ClarityTitle");
                //            MainDiamondTable.Columns.Add("CutLongTitle");
                //            MainDiamondTable.Columns.Add("PolishTitle");
                //            MainDiamondTable.Columns.Add("SymmetryTitle");
                //            MainDiamondTable.Columns.Add("FluorescenceIntensityTitle");
                //            MainDiamondTable.Columns.Add("DepthPercent");
                //            MainDiamondTable.Columns.Add("TablePercent");
                //            MainDiamondTable.Columns.Add("MeasLength");
                //            MainDiamondTable.Columns.Add("MeasWidth");
                //            MainDiamondTable.Columns.Add("MeasDepth");
                //            MainDiamondTable.Columns.Add("LabTitle");
                //            MainDiamondTable.Columns.Add("Ratio");
                //            MainDiamondTable.Columns.Add("CertificateNumber");
                //            MainDiamondTable.Columns.Add("HasCertFile");

                //            MainDiamondTable.Columns.Add("RapNetPrice");
                //            MainDiamondTable.Columns.Add("FinalPrice");
                //            MainDiamondTable.Columns.Add("TotalSalesPriceInCurrency");
                //            MainDiamondTable.Columns.Add("FluorescenceColorTitle");
                //            MainDiamondTable.Columns.Add("GirdleSizeMin");
                //            MainDiamondTable.Columns.Add("GirdleSizeMax");
                //            MainDiamondTable.Columns.Add("GirdleConditionTitle");
                //            MainDiamondTable.Columns.Add("CuletSizeTitle");
                //            MainDiamondTable.Columns.Add("CuletConditionTitle");

                //            foreach (RapnetDiamond item in rootObject.response.body.diamonds)
                //            {
                //                if (item.stock_num.ToUpper() == "ER-8096")
                //                {

                //                }
                //                //MainDiamondTable.ImportRow(row);
                //                DataRow row = MainDiamondTable.NewRow();

                //                row["DiamondID"] = item.diamond_id;
                //                row["VendorStockNumber"] = item.stock_num;
                //                row["ShapeTitle"] = item.shape;
                //                row["Weight"] = item.size;
                //                row["ColorTitle"] = item.color;
                //                row["ClarityTitle"] = item.clarity;
                //                row["CutLongTitle"] = item.cut;
                //                row["PolishTitle"] = item.polish;
                //                row["SymmetryTitle"] = item.symmetry;
                //                row["FluorescenceIntensityTitle"] = item.fluor_intensity;
                //                row["DepthPercent"] = item.depth_percent;
                //                row["TablePercent"] = item.table_percent;
                //                row["MeasLength"] = item.meas_length;
                //                row["MeasWidth"] = item.meas_width;
                //                row["MeasDepth"] = item.meas_depth;
                //                row["LabTitle"] = item.lab;
                //                row["Ratio"] = "";
                //                row["CertificateNumber"] = item.cert_num;
                //                row["HasCertFile"] = item.has_cert_file;

                //                row["RapNetPrice"] = item.total_sales_price;
                //                row["FinalPrice"] = item.total_sales_price_in_currency;
                //                row["TotalSalesPriceInCurrency"] = item.total_sales_price_in_currency;
                //                row["FluorescenceColorTitle"] = item.fluor_color;
                //                row["GirdleSizeMin"] = item.girdle_min;
                //                row["GirdleSizeMax"] = item.girdle_max;
                //                row["GirdleConditionTitle"] = item.girdle_condition;
                //                row["CuletSizeTitle"] = item.culet_size;
                //                row["CuletConditionTitle"] = item.culet_condition;

                //                MainDiamondTable.Rows.Add(row);
                //            }

                //            SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                //            SqlParameter prmDiamond = SqlHelper.CreateParameter("@Diamond", MainDiamondTable);
                //            SqlParameter prmPageNo = SqlHelper.CreateParameter("@PageNo", i);
                //            SqlParameter[] AllParams = { prmErr, prmDiamond, prmPageNo };
                //            SqlHelper.ExecuteNonQuery(_constring, CommandType.StoredProcedure, "Usp_AddUpdRapnetDiamond", AllParams);
                //            if (prmErr.Value != null)
                //            {
                //                if (prmErr.Value.ToString() == "0")
                //                {
                //                    i++;
                //                    StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                //                    str.WriteLine("Successfully Inserting Daimond Procedure - " + DateTime.Now.ToString());
                //                    str.Close();
                //                    str.Dispose();
                //                }
                //                else
                //                {
                //                    StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                //                    str.WriteLine("Error on Inserting Daimond Procedure - " + DateTime.Now.ToString());
                //                    str.Close();
                //                    str.Dispose();
                //                }
                //            }
                //            else
                //            {
                //                StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                //                str.WriteLine("Error on Inserting Daimond Procedure - " + DateTime.Now.ToString());
                //                str.Close();
                //                str.Dispose();
                //            }
                //        }
                //        else
                //        {
                //            break;
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //    }
                //}




                DataSet dsDiamonds = new DataSet();
                int i = 1;
                RetailFeed objRetailFeed = new RetailFeed();
                foreach (Shapes val in Enum.GetValues(typeof(Shapes)))
                {
                    objRetailFeed = new RetailFeed();
                    objRetailFeed.Login(ConfigurationSettings.AppSettings["RapnetUserName"], ConfigurationSettings.AppSettings["RapnetPassword"]);

                    i = 1;
                    int rowsCount = 0;
                    int totalLoop = 0;
                    while (i > 0)
                    {
                        DataTable dtDiamonds = new DataTable();
                        try
                        {

                            dtDiamonds = objRetailFeed.GetDiamonds(val, 0.25, 30.00, Colors.D, Colors.M, Clarities.IF, Clarities.I1, Cuts.EXCELLENT, Cuts.FAIR, 1, 99999999.00, i, 50, ref DiamondCount);

                            if (i == 1)
                            {
                                double reminder = Math.IEEERemainder(((double)DiamondCount), (double)50);
                                totalLoop = Convert.ToInt32(DiamondCount / 50);
                                if (reminder != 0)
                                {
                                    totalLoop = totalLoop + 1;
                                }
                            }
                            i++;

                            rowsCount = rowsCount + dtDiamonds.Rows.Count;
                            if (totalLoop <= i)
                            {
                                //if (DiamondCount <= rowsCount)
                                //{
                                i = 0;
                                //}
                            }
                            else
                            {
                                dsDiamonds.Merge(dtDiamonds);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                            str.WriteLine(ex.Message);
                            str.Close();
                            str.Dispose();

                            objRetailFeed = new RetailFeed();
                            objRetailFeed.Login(ConfigurationSettings.AppSettings["RapnetUserName"], ConfigurationSettings.AppSettings["RapnetPassword"]);

                        }
                    }
                }


                // WriteToExcelSpreadsheet("1.xlsx", dsDiamonds.Tables[0]);
                DataTable MainDiamondTable = new DataTable();
                MainDiamondTable.Columns.Add("DiamondID");
                MainDiamondTable.Columns.Add("VendorStockNumber");
                MainDiamondTable.Columns.Add("ShapeTitle");
                MainDiamondTable.Columns.Add("Weight");
                MainDiamondTable.Columns.Add("ColorTitle");
                MainDiamondTable.Columns.Add("ClarityTitle");
                MainDiamondTable.Columns.Add("CutLongTitle");
                MainDiamondTable.Columns.Add("PolishTitle");
                MainDiamondTable.Columns.Add("SymmetryTitle");
                MainDiamondTable.Columns.Add("FluorescenceIntensityTitle");
                MainDiamondTable.Columns.Add("DepthPercent");
                MainDiamondTable.Columns.Add("TablePercent");
                MainDiamondTable.Columns.Add("MeasLength");
                MainDiamondTable.Columns.Add("MeasWidth");
                MainDiamondTable.Columns.Add("MeasDepth");
                MainDiamondTable.Columns.Add("LabTitle");
                MainDiamondTable.Columns.Add("Ratio");
                MainDiamondTable.Columns.Add("CertificateNumber");
                MainDiamondTable.Columns.Add("HasCertFile");

                MainDiamondTable.Columns.Add("RapNetPrice");
                MainDiamondTable.Columns.Add("FinalPrice");
                MainDiamondTable.Columns.Add("TotalSalesPriceInCurrency");
                MainDiamondTable.Columns.Add("FluorescenceColorTitle");
                MainDiamondTable.Columns.Add("GirdleSizeMin");
                MainDiamondTable.Columns.Add("GirdleSizeMax");
                MainDiamondTable.Columns.Add("GirdleConditionTitle");
                MainDiamondTable.Columns.Add("CuletSizeTitle");
                MainDiamondTable.Columns.Add("CuletConditionTitle");

                foreach (DataRow row in dsDiamonds.Tables[0].Rows)
                {
                    if (row["VendorStockNumber"].ToString().ToUpper() == "ER-8096")
                    {

                    }
                    MainDiamondTable.ImportRow(row);
                }

                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                SqlParameter prmDiamond = SqlHelper.CreateParameter("@Diamond", MainDiamondTable);
                SqlParameter prmPageNo = SqlHelper.CreateParameter("@PageNo", 1);
                SqlParameter[] AllParams = { prmErr, prmDiamond, prmPageNo };
                SqlHelper.ExecuteNonQuery(_constring, CommandType.StoredProcedure, "Usp_AddUpdRapnetDiamond", AllParams);
                if (prmErr.Value != null)
                {
                    if (prmErr.Value.ToString() == "0")
                    {
                        StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                        str.WriteLine("Successfully Inserting Daimond Procedure - " + DateTime.Now.ToString());
                        str.Close();
                        str.Dispose();
                    }
                    else
                    {
                        StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                        str.WriteLine("Error on Inserting Daimond Procedure - " + DateTime.Now.ToString());
                        str.Close();
                        str.Dispose();
                    }
                }
                else
                {
                    StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                    str.WriteLine("Error on Inserting Daimond Procedure - " + DateTime.Now.ToString());
                    str.Close();
                    str.Dispose();
                }




            }
            catch (Exception ex)
            {
                StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                str.WriteLine("Error on Function Inserting Daimond - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.Source.ToString());
                str.Close();
                str.Dispose();
            }
            StreamWriter str2 = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
            str2.WriteLine("Service end on : " + DateTime.Now.ToString());
            str2.Close();
            str2.Dispose();
        }


        public void WriteToExcelSpreadsheet(string fileName, System.Data.DataTable dt)
        {
            string filepath = "d:/2.xlsx";
            //dt = SQLProductProvider.GetExcelImport();
            // dt.WriteXml(filepath, XmlWriteMode.IgnoreSchema);

            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            int iCol, iRow, iColVal;
            Object missing = System.Reflection.Missing.Value;
            // Open the document that was chosen by the dialog
            Microsoft.Office.Interop.Excel.Workbook aBook;
            try
            {
                //'re-initialize excel app
                ExlApp = new Microsoft.Office.Interop.Excel.Application(); if (ExlApp == null)
                {
                    //'throw an exception
                    throw (new Exception("Unable to Start Microsoft Excel"));
                }
                else
                {
                    //'supresses overwrite warnings
                    ExlApp.DisplayAlerts = false;
                    //aBook = New Excel.Workbook
                    //'check if file exists
                    if (File.Exists(filepath))
                    {
                        aBook = ExlApp.Workbooks._Open(filepath, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                    }
                    else
                    {
                        aBook = ExlApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                    }//End If
                    //With ExlApp
                    ExlApp.SheetsInNewWorkbook = 1;
                    //ExlApp.Worksheets[1].Select();
                    //For displaying the column name in the the excel file.
                    for (iCol = 0; iCol < dt.Columns.Count; iCol++)
                    {
                        //'clear column name before setting a new value
                        ExlApp.Cells[1, iCol + 1] = "";
                        ExlApp.Cells[1, iCol + 1] = dt.Columns[iCol].ColumnName.ToString();
                    }//next
                    //For displaying the column value row-by-row in the the excel file.
                    for (iRow = 0; iRow < dt.Rows.Count; iRow++)
                    {
                        try
                        {
                            for (iColVal = 0; iColVal < dt.Columns.Count; iColVal++)
                            {
                                if (dt.Rows[iRow].ItemArray[iColVal] is string)
                                {
                                    ExlApp.Cells[iRow + 2, iColVal + 1] = "'" + dt.Rows[iRow].ItemArray[iColVal].ToString();
                                }
                                else
                                {
                                    ExlApp.Cells[iRow + 2, iColVal + 1] = dt.Rows[iRow].ItemArray[iColVal].ToString();
                                }//End If
                            }//next
                        }
                        catch (Exception ex)
                        {
                            Console.Write("ERROR: " + ex.Message);
                        }//End Try
                    }//next
                    if (File.Exists(filepath))
                    {
                        ExlApp.ActiveWorkbook.Save(); //fileName)
                    }
                    else
                    {
                        ExlApp.ActiveWorkbook.SaveAs(filepath.Trim(), missing, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);
                    }//End If
                    ExlApp.ActiveWorkbook.Close(true, missing, missing);
                    //End With
                    //Console.Write("File exported sucessfully");
                }//End if

            }
            catch (System.Runtime.InteropServices.COMException ex)
            {

                Console.Write("ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {

                Console.Write("ERROR: " + ex.Message);
            }
            finally
            {
                ExlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ExlApp);
                aBook = null;
                ExlApp = null;
            }//End Try
        }//End Sub
        //public void AddDiamondVendorDetail()
        //{
        //    try
        //    {
        //        DataSet dsDiamonds = SqlHelper.ExecuteDataset(_constring, CommandType.StoredProcedure, "USP_Znode_GetSchedulerDiamond");
        //        if (dsDiamonds != null)
        //        {
        //            if (dsDiamonds.Tables[0] != null && dsDiamonds.Tables[0].Rows.Count > 0)
        //            {
        //                DataTable dtVendorDetail = new DataTable();
        //                dtVendorDetail.Columns.Add("DiamondID", typeof(Int32));
        //                dtVendorDetail.Columns.Add("Act_No", typeof(Int32));
        //                dtVendorDetail.Columns.Add("Company", typeof(string));
        //                dtVendorDetail.Columns.Add("Name", typeof(string));
        //                dtVendorDetail.Columns.Add("Email", typeof(string));
        //                dtVendorDetail.Columns.Add("Phone", typeof(string));
        //                dtVendorDetail.Columns.Add("COUNTRY", typeof(string));
        //                dtVendorDetail.Columns.Add("STATE", typeof(string));
        //                dtVendorDetail.Columns.Add("CITY", typeof(string));
        //                dtVendorDetail.Columns.Add("DiamondLocationCountry", typeof(string));
        //                dtVendorDetail.Columns.Add("DiamondLocationCity", typeof(string));

        //                RetailFeed objRetailFeed = new RetailFeed();
        //                objRetailFeed.Login("52146", "diamond12");
        //                for (int _Row = 0; _Row < dsDiamonds.Tables[0].Rows.Count; _Row++)
        //                {
        //                    DataTable dtDiamondVendor = new DataTable();
        //                    dtDiamondVendor = objRetailFeed.GetSingleDiamond(Convert.ToInt32(dsDiamonds.Tables[0].Rows[_Row]["DiamondId"]));
        //                    DataRow dr = dtVendorDetail.NewRow();
        //                    dr[0] = dtDiamondVendor.Rows[0]["DiamondID"].ToString();
        //                    dr[1] = dtDiamondVendor.Rows[0]["Act_No"].ToString();
        //                    dr[2] = dtDiamondVendor.Rows[0]["Company"].ToString();
        //                    dr[3] = dtDiamondVendor.Rows[0]["Name"].ToString();
        //                    dr[4] = dtDiamondVendor.Rows[0]["Email"].ToString();
        //                    dr[5] = dtDiamondVendor.Rows[0]["Phone"].ToString();
        //                    dr[6] = dtDiamondVendor.Rows[0]["COUNTRY"].ToString();
        //                    dr[7] = dtDiamondVendor.Rows[0]["STATE"].ToString();
        //                    dr[8] = dtDiamondVendor.Rows[0]["CITY"].ToString();
        //                    dr[9] = dtDiamondVendor.Rows[0]["DiamondLocationCountry"].ToString();
        //                    dr[10] = dtDiamondVendor.Rows[0]["DiamondLocationCity"].ToString();
        //                    dtVendorDetail.Rows.Add(dr);
        //                    //StreamWriter str = new StreamWriter(@"C:\ArthurJewelersDiamondScheduler\ErrorFile\err.txt", true);
        //                    //str.WriteLine("Successfully Adding Daimond vendor detail to datatable- " + DateTime.Now.ToString());
        //                    //str.Close();
        //                    //str.Dispose();
        //                }
        //                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
        //                SqlParameter prmDiamond = SqlHelper.CreateParameter("@VendorDetail", dtVendorDetail);
        //                SqlParameter[] AllParams = { prmErr, prmDiamond };
        //                SqlHelper.ExecuteNonQuery(_constring, CommandType.StoredProcedure, "USP_ZNode_InsertDiamondVendor", AllParams);

        //                if (prmErr.Value != null)
        //                {
        //                    if (prmErr.Value.ToString() == "0")
        //                    {
        //                        StreamWriter str = new StreamWriter(@"C:\ArthurJewelersDiamondScheduler\ErrorFile\err.txt", true);
        //                        str.WriteLine("Successfully Inserting Daimond vendor detail Procedure - " + DateTime.Now.ToString());
        //                        str.Close();
        //                        str.Dispose();
        //                    }
        //                    else
        //                    {
        //                        StreamWriter str = new StreamWriter(@"C:\ArthurJewelersDiamondScheduler\ErrorFile\err.txt", true);
        //                        str.WriteLine("Error on Inserting Daimond vendor detail Procedure - " + DateTime.Now.ToString());
        //                        str.Close();
        //                        str.Dispose();
        //                    }
        //                }
        //                else
        //                {
        //                    StreamWriter str = new StreamWriter(@"C:\ArthurJewelersDiamondScheduler\ErrorFile\err.txt", true);
        //                    str.WriteLine("Error on Inserting Daimond vendor detail Procedure - " + DateTime.Now.ToString());
        //                    str.Close();
        //                    str.Dispose();
        //                }
        //            }
        //        }
        //    }
        //catch (Exception ex)
        //{
        //    StreamWriter str = new StreamWriter(@"C:\ArthurJewelersDiamondScheduler\ErrorFile\err.txt", true);
        //    str.WriteLine("Error on Function Inserting Daimond Vendor detail - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.Source.ToString());
        //    str.Close();
        //    str.Dispose();
        //}
        //}


        public static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    //read column names
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            return csvData;
        }


        public static void LogError(string error)
        {
            StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
            str.WriteLine(error);
            str.Close();
            str.Dispose();
        }


        public static string GetShapeForKapuGems(string shape)
        {


            switch (shape.ToUpper())
            {
                case "Round":
                    shape = "Round ";
                    break;
                case "Princess":
                    shape = "Princess";
                    break;
                case "Cushion":
                    shape = "Cushion";
                    break;
                case "Cushion Modified":
                    shape = "Cushion";
                    break;
                case "Emerald":
                    shape = "Emerald";
                    break;
                case "Sq. Emerald":
                    shape = "Asscher";
                    break;
                case "Radiant":
                    shape = "Radiant";
                    break;
                case "Sq. Radiant":
                    shape = "Radiant";
                    break;
                case "Pear":
                    shape = "Pear";
                    break;
                case "Oval":
                    shape = "Oval";
                    break;
                case "Heart":
                    shape = "Heart";
                    break;
                case "Marquise":
                    shape = "Marquise";
                    break;
                default:
                    shape = shape;
                    break;
            }
            return shape;
        }

        public static string GetCutForKapuGems(string cut)
        {
            cut = cut.Replace("+", "");
            switch (cut.ToUpper())
            {
                case "EX":
                    cut = "EXCELLENT";
                    break;
                case "3EX":
                    cut = "EXCELLENT";
                    break;
                case "VG":
                    cut = "VERY GOOD";
                    break;
                case "G":
                    cut = "GOOD";
                    break;
                case "GD":
                    cut = "GOOD";
                    break;
                case "FR":
                    cut = "FAIR";
                    break;
                default:
                    cut = cut;
                    break;
            }
            return cut;
        }

        public static string GetPolishForKapuGems(string polish)
        {
            switch (polish.ToUpper())
            {
                case "EX":
                    polish = "Excellent";
                    break;
                case "VG":
                    polish = "Very Good";
                    break;
                case "G":
                    polish = "Good";
                    break;
                default:
                    polish = polish;
                    break;
            }
            return polish;
        }


        public static string GetSymmetryForKapuGems(string symmetry)
        {
            switch (symmetry.ToUpper())
            {
                case "EX":
                    symmetry = "Excellent";
                    break;
                case "VG":
                    symmetry = "Very Good";
                    break;
                case "G":
                    symmetry = "Good";
                    break;
                default:
                    symmetry = symmetry;
                    break;
            }
            return symmetry;
        }

        public static string GetFluorescenceForKapuGems(string fluorescence)
        {
            switch (fluorescence.ToUpper())
            {
                case "NONE":
                    fluorescence = "None";
                    break;
                case "FNT":
                    fluorescence = "Faint";
                    break;
                case "MED":
                    fluorescence = "Medium";
                    break;
                case "STG":
                    fluorescence = "Strong";
                    break;
                case "VSTG":
                    fluorescence = "Very Strong";
                    break;
                default:
                    fluorescence = fluorescence;
                    break;
            }
            return fluorescence;
        }
    }

    public class Header
    {
        public int error_code { get; set; }
        public string error_message { get; set; }
    }

    public class SearchResults
    {
        public int diamonds_returned { get; set; }
        public int total_diamonds_found { get; set; }
        public string sorted_by { get; set; }
        public string sort_direction { get; set; }
    }

    public class RapnetDiamond
    {
        public int diamond_id { get; set; }
        public string shape { get; set; }
        public double size { get; set; }
        public string color { get; set; }
        public string fancy_color_dominant_color { get; set; }
        public string fancy_color_secondary_color { get; set; }
        public string fancy_color_overtone { get; set; }
        public string fancy_color_intensity { get; set; }
        public string clarity { get; set; }
        public string cut { get; set; }
        public string symmetry { get; set; }
        public string polish { get; set; }
        public double? depth_percent { get; set; }
        public double? table_percent { get; set; }
        public double? meas_length { get; set; }
        public double? meas_width { get; set; }
        public double? meas_depth { get; set; }
        public string girdle_min { get; set; }
        public string girdle_max { get; set; }
        public string girdle_condition { get; set; }
        public string culet_size { get; set; }
        public string culet_condition { get; set; }
        public string fluor_color { get; set; }
        public string fluor_intensity { get; set; }
        public bool has_cert_file { get; set; }
        public string lab { get; set; }
        public double total_sales_price { get; set; }
        public string currency_code { get; set; }
        public string currency_symbol { get; set; }
        public double total_sales_price_in_currency { get; set; }
        public string cert_num { get; set; }
        public string stock_num { get; set; }
        public bool has_sarineloupe { get; set; }
    }

    public class Body
    {
        public SearchResults search_results { get; set; }
        public List<RapnetDiamond> diamonds { get; set; }
    }

    public class Response
    {
        public Header header { get; set; }
        public Body body { get; set; }
    }

    public class RootObject
    {
        public Response response { get; set; }
    }

    public class KapuGemsDiamondModels
    {
        public int Flag { get; set; }
        public string DiamondId { get; set; }

        public string SkuDiamondId { get; set; }


        public string DiamondImage { get; set; }


        public string LotNumber { get; set; }


        public string Shape { get; set; }


        public double Carat { get; set; }


        public string Color { get; set; }


        public string Clartiy { get; set; }


        public string Cut { get; set; }


        public string Polish { get; set; }


        public string Symmetry { get; set; }


        public string Flourescence { get; set; }


        public string Depth { get; set; }


        public string Table { get; set; }


        public string Measurements { get; set; }


        public string Ratio { get; set; }


        public string Lab { get; set; }


        public double Price { get; set; }


        public string DiamondCertificate { get; set; }

        public bool HasCertFile { get; set; }

        public string Supplier { get; set; }

        public string EyeClean { get; set; }

        public bool IsClean { get; set; }
        public double PerCaratPrice { get; set; }

        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedFromIp { get; set; }
        public string Message { get; set; }
        public int IsShowMessage { get; set; }
        public int IsShowPrivillage { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }

    //public class RapnetDiamond
    //{
    //    public long diamond_id { get; set; }
    //    public string shape { get; set; }
    //    public float size { get; set; }
    //    public string color { get; set; }
    //    public string fancy_color_dominant_color { get; set; }
    //    public string fancy_color_secondary_color { get; set; }
    //    public string fancy_color_overtone { get; set; }
    //    public string fancy_color_intensity { get; set; }
    //    public string clarity { get; set; }
    //    public string cut { get; set; }
    //    public string symmetry { get; set; }
    //    public string polish { get; set; }
    //    public float depth_percent { get; set; }
    //    public float table_percent { get; set; }
    //    public float meas_length { get; set; }
    //    public float meas_width { get; set; }
    //    public float meas_depth { get; set; }
    //    public string girdle_min { get; set; }
    //    public string girdle_max { get; set; }
    //    public string girdle_condition { get; set; }
    //    public string culet_size { get; set; }
    //    public string culet_condition { get; set; }
    //    public string fluor_color { get; set; }
    //    public string fluor_intensity { get; set; }
    //    public bool has_cert_file { get; set; }
    //    public string lab { get; set; }
    //    public float total_sales_price { get; set; }
    //    public string currency_code { get; set; }
    //    public string currency_symbol { get; set; }
    //    public float total_sales_price_in_currency { get; set; }
    //    public string cert_num { get; set; }
    //    public string stock_num { get; set; }
    //    public bool has_sarineloupe { get; set; }
    //}
}
