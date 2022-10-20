using Newtonsoft.Json;
using RestSharp;
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

namespace Canturi.DharmService
{
    public class Dharm
    {
        int DiamondCount;
        #region Private Variable
        string _constring = string.Empty;

        #endregion

        public Dharm()
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


        public void CDharmDiamond()
        {
            try
            {
                var client = new RestClient("http://www.dharamhk.com/dharamwebapi/api/StockDispApi/getDiamondData");
                var request = new RestRequest(Method.POST);
                string uniqID = ConfigurationManager.AppSettings["WebAPIUserId"].ToString();
                string company = ConfigurationManager.AppSettings["WebAPICompany"].ToString();
                string actCode = ConfigurationManager.AppSettings["WebAPIActCode"].ToString();

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\n \n uniqID : '" + uniqID + "',\n company :'" + company + "',\n actCode: '" + actCode + "',\n selectAll: '',\n StartIndex: '1',\n count: '2000000'\n,\n columns: '',\n finder: '',\n sort: ''\n }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                var model = JsonConvert.DeserializeObject<DharmResponseModel>(response.Content);

                /*
                // uncomment to get all data from API to csv
                StringBuilder sb = new StringBuilder();

                IEnumerable<string> columnNames = model.DataList.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in model.DataList.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                    sb.AppendLine(string.Join(",", fields));
                }

                File.WriteAllText("e:/Dharam-diamond.csv", sb.ToString());
                */

                /*
                StringBuilder sb1 = new StringBuilder();
                IEnumerable<string> columnNames1 = model.DataList.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                sb1.AppendLine(string.Join(",", columnNames1));
                foreach (DataRow dr in model.DataList.Rows)
                {
                    foreach (DataColumn dc in model.DataList.Columns)
                        sb1.Append(FormatCSV(dr[dc.ColumnName].ToString()) + ",");
                    sb1.Remove(sb1.Length - 1, 1);
                    sb1.AppendLine();
                }

                File.WriteAllText("e:/Dharam-diamond1.csv", sb.ToString());

                */



                if (model.DataList != null && model.DataList.Rows.Count > 0)
                {
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
                    MainDiamondTable.Columns.Add("EyeClean");
                    MainDiamondTable.Columns.Add("Measurements");

                    DataRow[] filterRows = model.DataList.Select();

                    foreach (DataRow dataRow in filterRows)
                    {
                        if (dataRow["MILKY"].ToString().ToUpper() == "NO")
                        {
                            DataRow row = MainDiamondTable.NewRow();
                            row["VendorStockNumber"] = dataRow["Ref"].ToString();
                            row["ShapeTitle"] = dataRow["Shape"].ToString();
                            row["Weight"] = dataRow["Size"];
                            row["ColorTitle"] = dataRow["Color"].ToString().Replace(" +", "").Replace("-", "");
                            row["ClarityTitle"] = dataRow["Clarity"].ToString().Replace(" +", "").Replace("-", "");
                            row["CutLongTitle"] = getCUT().Where(x => x.Key == dataRow["Cut"].ToString().Replace(" +", "").Replace("-", "")).FirstOrDefault().Value;
                            row["PolishTitle"] = getPolish().Where(x => x.Key == dataRow["Polish"].ToString()).FirstOrDefault().Value;
                            row["SymmetryTitle"] = getSymmetry().Where(x => x.Key == dataRow["Sym"].ToString()).FirstOrDefault().Value;
                            row["FluorescenceIntensityTitle"] = dataRow["Flour"].ToString();
                            row["DepthPercent"] = dataRow["Depth"];
                            row["TablePercent"] = dataRow["Table"];
                            row["MeasLength"] = dataRow["M1"];
                            row["MeasWidth"] = dataRow["M2"];
                            row["MeasDepth"] = dataRow["M3"];
                            row["LabTitle"] = dataRow["Cert"].ToString();
                            row["Ratio"] = "";
                            row["CertificateNumber"] = dataRow["ReportNo"].ToString();
                            if (!dataRow["ReportNo"].Equals(DBNull.Value))
                            {
                                if (!String.IsNullOrEmpty(dataRow["ReportNo"].ToString()))
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

                            row["RapNetPrice"] = dataRow["Rate"];
                            row["FinalPrice"] = dataRow["Price/Carat"];
                            row["TotalSalesPriceInCurrency"] = dataRow["Price/Carat"];
                            row["FluorescenceColorTitle"] = dataRow["Flour"].ToString();
                            row["GirdleSizeMin"] = 0;
                            row["GirdleSizeMax"] = 0;
                            row["GirdleConditionTitle"] = dataRow["GirdleCondition"];
                            row["CuletSizeTitle"] = dataRow["Cut"];
                            row["CuletConditionTitle"] = "";
                            if (dataRow["EyeClean"].Equals(DBNull.Value))
                            {
                                row["EyeClean"] = "no";
                            }
                            else
                            {
                                if (dataRow["EyeClean"].ToString().ToUpper() == "100 % EYE CLEAN")
                                {
                                    row["EyeClean"] = "yes";
                                }
                                else
                                {
                                    row["EyeClean"] = dataRow["EyeClean"];
                                }
                            }
                            if (!string.IsNullOrEmpty(dataRow["M1"].ToString()))
                                row["Measurements"] = dataRow["M1"] + "x" + dataRow["M2"] + "x" + dataRow["M3"];
                            else
                                row["Measurements"] = null;

                            MainDiamondTable.Rows.Add(row);
                        }
                    }

                    int PageNumber = 1;

                    SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                    SqlParameter prmDiamond = SqlHelper.CreateParameter("@DiamondDharmTable", MainDiamondTable);
                    SqlParameter prmPage = SqlHelper.CreateParameter("@Page", PageNumber);
                    SqlParameter[] AllParams = { prmErr, prmDiamond, prmPage };
                    SqlHelper.ExecuteNonQuery(_constring, CommandType.StoredProcedure, "Usp_AddUpdDharmDiamond", AllParams);
                    if (prmErr.Value != null)
                    {
                        if (prmErr.Value.ToString() == "0")
                        {
                            LogError("Successfully Inserting Diamond Procedure - " + DateTime.Now.ToString());
                        }
                        else
                        {
                            LogError("Error on Inserting Diamond Procedure - " + DateTime.Now.ToString());
                        }
                    }
                    else
                    {
                        LogError("Error on Inserting Diamond Procedure - " + DateTime.Now.ToString());
                    }
                }
                else
                {
                    LogError("No record receives from Supplier Web API - " + DateTime.Now.ToString() + "because - " + model.MessageType);
                }
            }
            catch (Exception ex)
            {

                LogError("Error on Inserting Diamond Procedure - " + DateTime.Now.ToString() + "\n" + ex.Message);
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
            StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
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
