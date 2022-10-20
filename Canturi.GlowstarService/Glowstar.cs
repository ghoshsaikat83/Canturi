using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.GlowstarService
{
    public class Glowstar
    {
        //int DiamondCount;
        #region Private Variable
        string _constring = string.Empty;

        #endregion

        public Glowstar()
        {
            try
            {
                _constring = ConfigurationManager.AppSettings["CanturiConnectionStr"].ToString();
            }
            catch
            {
                throw new ApplicationException("Sorry, connection to database could not made");
            }
        }
        public static string FormatCSV(string input)
        {
            try
            {
                if (input == null)
                    return string.Empty;

                bool containsQuote = false;
                bool containsComma = false;
                int len = input.Length;
                for (int i = 0; i < len && (containsComma == false || containsQuote == false); i++)
                {
                    char ch = input[i];
                    if (ch == '"')
                        containsQuote = true;
                    else if (ch == ',')
                        containsComma = true;
                }

                if (containsQuote && containsComma)
                    input = input.Replace("\"", "\"\"");

                if (containsComma)
                    return "\"" + input + "\"";
                else
                    return input;
            }
            catch
            {
                throw;
            }
        }


        public void CGlowstarDiamond()
        {
            try
            {
                /*
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.DefaultConnectionLimit = 9999;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072 | (SecurityProtocolType)48;
                */
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                string jsonData = "";
                LogError("WebClient start");
                WebRequest request = WebRequest.Create(ConfigurationManager.AppSettings["APIurl"].ToString());
                request.Method = "GET"; 
                
                WebResponse response = request.GetResponse();
                var encoding = ASCIIEncoding.ASCII;
                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                {
                    jsonData = reader.ReadToEnd();
                }
                LogError("WebClient end");
                dynamic rawData = JsonConvert.DeserializeObject(jsonData);
                dynamic PKTDTL = ((Newtonsoft.Json.Linq.JContainer)rawData["PKTDTL"]);
                //var model = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JContainer>(rawData);
                if (PKTDTL.Count > 0)
                {
                //var model = JsonConvert.DeserializeObject<GlowstarResponseModel>(PKTDTL);

                //if (model.DataList != null && model.DataList.Rows.Count > 0)
                //{
                    DataTable MainDiamondTable = new DataTable();
                    MainDiamondTable.Columns.Add("DiamondID");
                    MainDiamondTable.Columns.Add("DiamondImage");
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
                    MainDiamondTable.Columns.Add("EyeClean");
                    MainDiamondTable.Columns.Add("Measurements");

                    //DataRow[] filterRows = model.DataList.Select();

                    foreach (var dataRow in PKTDTL)
                    {

                        DataRow row = MainDiamondTable.NewRow();
                        row["DiamondImage"] = dataRow["DIAMONDIMG_URL"].ToString(); 
                        row["VendorStockNumber"] = dataRow["Loat_NO"].ToString();  
                        row["ShapeTitle"] = dataRow["Shape"].ToString();
                        row["Weight"] = dataRow["Weight"];
                        row["ColorTitle"] = dataRow["Color"].ToString().Replace(" +", "").Replace("-", "");
                        row["ClarityTitle"] = dataRow["Clarity"].ToString().Replace(" +", "").Replace("-", "");
                        row["CutLongTitle"] = getCUT().Where(x => x.Key == dataRow["Cut"].ToString().Replace(" +", "").Replace("-", "")).FirstOrDefault().Value;
                        row["PolishTitle"] = getPolish().Where(x => x.Key == dataRow["Polish"].ToString()).FirstOrDefault().Value;
                        row["SymmetryTitle"] = getSymmetry().Where(x => x.Key == dataRow["Symmetry"].ToString()).FirstOrDefault().Value;
                        row["FluorescenceIntensityTitle"] = dataRow["Fluorescence"].ToString();
                        row["DepthPercent"] = dataRow["Depth"];
                        row["TablePercent"] = dataRow["Table"];
                        row["MeasLength"] =  dataRow["Length"];
                        row["MeasWidth"] = dataRow["Width"];
                        row["MeasDepth"] = dataRow["Depth"];
                        row["LabTitle"] = dataRow["Lab"].ToString();
                        row["CertificateNumber"] = dataRow["CertiNo"].ToString();
                        if (!dataRow["CertiNo"].Equals(DBNull.Value))
                        {
                            if (!String.IsNullOrEmpty(dataRow["CertiNo"].ToString()))
                            {
                                row["HasCertFile"] = "True";
                            }
                            else
                            {
                                row["HasCertFile"] = "False";
                            }
                        }
                        else
                        {
                            row["HasCertFile"] = "False";
                        }

                        row["RapNetPrice"] = dataRow["Rap"];
                        row["FinalPrice"] = dataRow["Price/Carat"];  // Not Found  ? NetDollar
                        row["TotalSalesPriceInCurrency"] = dataRow["NetDollar"];
                        row["FluorescenceColorTitle"] = dataRow["Fluorescence"].ToString();
                        row["GirdleSizeMin"] = 0;
                        row["GirdleSizeMax"] = 0;
                        row["GirdleConditionTitle"] = dataRow["Girdle Condition"];
                        row["CuletSizeTitle"] = dataRow["Cut"]; // Not Found
                        row["CuletConditionTitle"] = dataRow["Culet"];
                        if (dataRow["EyeC"].Equals(DBNull.Value))
                        {
                            row["EyeClean"] = "no";
                        }
                        else
                        {
                            if (dataRow["EyeC"].ToString().Contains("100"))
                            {
                                row["EyeClean"] = "yes";
                            }
                            else
                            {
                                row["EyeClean"] = dataRow["EyeC"];
                            }
                        }
                        if (!string.IsNullOrEmpty(dataRow["Length"].ToString()))
                        {
                            row["Measurements"] = dataRow["Length"] + "x" + dataRow["Width"] + "x" + dataRow["Depth"];
                            /* Instructions from Document
                             - In Measurements (LxWxD)– take the largest number from the first 2 digits (in orange) and divide by the lowest number. 
                            The last digit (green) should never be used.
                            For example: 8.5 x 10.4 x 3.2
                            ---10.4 ÷ 8.5 = 1.223 RATIO (always calculate to 3 decimal places)
                            */
                            decimal l = Convert.ToDecimal(dataRow["Length"]);
                            decimal w = Convert.ToDecimal(dataRow["Width"]);
                            decimal[] m = { l, w };
                            row["Ratio"] = Math.Truncate((m.Max() / m.Min())* 1000)/1000;  // truncating to 3 decimal places
                        }
                        else
                        {
                            row["Measurements"] = null;
                            row["Ratio"] = null;
                        }
                        MainDiamondTable.Rows.Add(row);

                    }

                    int PageNumber = 1;

                    SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                    SqlParameter prmDiamond = SqlHelper.CreateParameter("@DiamondGlowstarTable", MainDiamondTable);
                    SqlParameter prmPage = SqlHelper.CreateParameter("@Page", PageNumber);
                    SqlParameter[] AllParams = { prmErr, prmDiamond, prmPage };
                    SqlHelper.ExecuteNonQuery(_constring, CommandType.StoredProcedure, "Usp_AddUpdGlowstarDiamond", AllParams);
                    if (prmErr.Value != null)
                    {
                        if (prmErr.Value.ToString() == "0")
                        {
                            LogError("Successfully Inserting Diamond Procedure - " + DateTime.Now.ToString());
                        }
                        else
                        {
                            LogError("Error on Inserting Diamond Procedure - " + DateTime.Now.ToString() + " Error Parameter: " + prmErr.Value);
                        }
                    }
                    else
                    {
                        LogError("Error on Inserting Diamond Procedure - " + DateTime.Now.ToString() + " Error Parameter: null");
                    }
                }
                else
                {
                    LogError("No record receives from Supplier Web API - " + DateTime.Now.ToString() + "because - " + "No Reason");   //model.MessageType
                }
            }
            catch (Exception ex)
            {

                LogError("Error on Inserting Diamond Procedure - " + DateTime.Now.ToString() + " Exception: \n" + ex.Message);
            }
        }

        public bool IsValidDiamond(string val)
        {
            string[] validDiamonds = { "Round", "Princess", "Emerald", "Asscher", "Marquise", "Oval", "Radiant", "Pear", "Heart", "Cushion" };
            if (validDiamonds.Contains(val))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void LogError(string error)
        {
            StreamWriter str = new StreamWriter(@"" + ConfigurationManager.AppSettings["ErrorFilePath"].ToString() + "", true);
            str.WriteLine(error);
            str.Close();
            str.Dispose();
        }

        public List<KeyValuePair<string, string>> getCUT()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("EX", "EXCELLENT"));
            list.Add(new KeyValuePair<string, string>("VG", "VERY GOOD"));
            list.Add(new KeyValuePair<string, string>("G", "GOOD"));

            return list;
        }

        public List<KeyValuePair<string, string>> getPolish()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("EX", "EXCELLENT"));
            list.Add(new KeyValuePair<string, string>("VG", "VERY GOOD"));
            list.Add(new KeyValuePair<string, string>("G", "GOOD"));
            return list;
        }
        public List<KeyValuePair<string, string>> getSymmetry()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("EX", "EXCELLENT"));
            list.Add(new KeyValuePair<string, string>("VG", "VERY GOOD"));
            list.Add(new KeyValuePair<string, string>("G", "GOOD"));
            return list;
        }

        public List<KeyValuePair<string, string>> getFloroscence()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("N", "NONE"));
            list.Add(new KeyValuePair<string, string>("F", "FAINT"));
            list.Add(new KeyValuePair<string, string>("SL", "FAINT"));
            list.Add(new KeyValuePair<string, string>("VSL", "VERY FAINT"));
            list.Add(new KeyValuePair<string, string>("M", "MEDIUM"));
            list.Add(new KeyValuePair<string, string>("S", "STRONG"));
            list.Add(new KeyValuePair<string, string>("VS", "VERY STRONG"));
            return list;
        }
    }
}
