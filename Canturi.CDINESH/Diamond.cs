using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Canturi.JBBrosDataService;


namespace Canturi.CDINESH
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

        public void CdinishDiamond()
        {
            try
            {
                cdinesh.com.StockDwnlSoapClient objStockDwnlSoapClient = new cdinesh.com.StockDwnlSoapClient();
                DataSet dsStock = new DataSet();
                dsStock = objStockDwnlSoapClient.GetFullStock(ConfigurationSettings.AppSettings["APIUserId"].ToString());

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
                MainDiamondTable.Columns.Add("VideoURL");
                MainDiamondTable.Columns.Add("CertURL");

                string strFilterCriteria = "";
                strFilterCriteria = strFilterCriteria + "Certificate='GIA'";
                strFilterCriteria = strFilterCriteria + " AND SHAPE IN ('RBC','EMR','PRI','MQ','SQE','RAD','CMB','PS','HS','OV')";
                strFilterCriteria = strFilterCriteria + " AND COLOUR IN ('J','I','H','G','F','E','D','J+','I+','H+','G+','F+','E+','D+','J-','I-','H-','G-','F-','E-','D-')";
                strFilterCriteria = strFilterCriteria + " AND CLARITY IN ('SI2','SI1','VS2','VS1','VVS2','VVS1','IF','FL')";
                strFilterCriteria = strFilterCriteria + " AND Cut IN ('EX','G','VG','EX+','G+','VG+','EX-','G-','VG-')";
                strFilterCriteria = strFilterCriteria + " AND POLISH IN ('EX','G','VG','EX+','G+','VG+','EX-','G-','VG-')";
                strFilterCriteria = strFilterCriteria + " AND SYMMETRY IN ('EX','G','VG','EX+','G+','VG+','EX-','G-','VG-')";
                strFilterCriteria = strFilterCriteria + " AND Flourence IN ('N','F','M','S','N+','F+','M+','S+','N-','F-','M-','S-','VS','SL','VSL')";
                strFilterCriteria = strFilterCriteria + " AND ISNULL(PacketNo,'') NOT IN ('')";
                strFilterCriteria = strFilterCriteria + " AND ISNULL(Milky,'') IN ('','NON','non','Non')";

                //strFilterCriteria = strFilterCriteria + "";                


                DataRow[] filterRows = dsStock.Tables[0].Select(strFilterCriteria);

                foreach (DataRow dataRow in filterRows)
                {
                    DataRow row = MainDiamondTable.NewRow();
                    row["DiamondID"] = dataRow["packetID"].ToString();
                    row["VendorStockNumber"] = dataRow["PacketNo"].ToString();
                    row["ShapeTitle"] = getShape().Where(x => x.Key == dataRow["Shape"].ToString()).FirstOrDefault().Value;
                    row["Weight"] = dataRow["Wt"];
                    row["ColorTitle"] = dataRow["Colour"].ToString().Replace("+", "").Replace("-", "");
                    row["ClarityTitle"] = dataRow["Clarity"].ToString().Replace("+", "").Replace("-", "");
                    row["CutLongTitle"] = getCUT().Where(x => x.Key == dataRow["Cut"].ToString().Replace("+", "").Replace("-", "")).FirstOrDefault().Value;
                    row["PolishTitle"] = getPolish().Where(x => x.Key == dataRow["Polish"].ToString()).FirstOrDefault().Value;
                    row["SymmetryTitle"] = getSymmetry().Where(x => x.Key == dataRow["Symmetry"].ToString()).FirstOrDefault().Value;
                    row["FluorescenceIntensityTitle"] = getFloroscence().Where(x => x.Key == dataRow["Flourence"].ToString()).FirstOrDefault().Value;
                    row["DepthPercent"] = dataRow["DepthPer"];
                    row["TablePercent"] = dataRow["TablePer"];
                    row["MeasLength"] = dataRow["Length"];
                    row["MeasWidth"] = dataRow["Width"];
                    row["MeasDepth"] = dataRow["Depth"];
                    row["LabTitle"] = dataRow["Certificate"].ToString();
                    row["Ratio"] = dataRow["Ratio"];
                    row["CertificateNumber"] = dataRow["CertificateNo"].ToString();
                    
                    if (!dataRow["ImageCerti"].Equals(DBNull.Value))
                    {
                        if (!String.IsNullOrEmpty(dataRow["ImageCerti"].ToString()))
                        {
                            row["HasCertFile"] = "True";
                            row["CertURL"] = dataRow["ImageCerti"].ToString();
                        }
                        else
                        {
                            row["HasCertFile"] = "False";
                            row["CertURL"] = "";
                        }
                    }
                    else
                    {
                        row["HasCertFile"] = "False";
                        row["CertURL"] = "";
                    }

                    row["RapNetPrice"] = dataRow["price"];
                    row["FinalPrice"] = dataRow["TotalPrice"];
                    row["TotalSalesPriceInCurrency"] = dataRow["TotalPrice"];
                    row["FluorescenceColorTitle"] = dataRow["Flourence"].ToString();
                    row["GirdleSizeMin"] = 0;
                    row["GirdleSizeMax"] = 0;
                    row["GirdleConditionTitle"] = dataRow["Cut"];
                    row["CuletSizeTitle"] = dataRow["Cut"];
                    row["CuletConditionTitle"] = "";
                    if (dataRow["EyeClean"].Equals(DBNull.Value))
                    {
                        row["EyeClean"] = "no";
                    }
                    else
                    {
                        if (dataRow["EyeClean"].ToString().ToUpper() == "100")
                        {
                            row["EyeClean"] = "yes";
                        }
                        else
                        {
                            row["EyeClean"] = "no";
                        }
                    }
                    row["Measurements"] = dataRow["Measurements"];
                    row["VideoURL"] = dataRow["VideoCD"].ToString();
                     
                    MainDiamondTable.Rows.Add(row);
                }

                int PageNumber = 1;

                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                SqlParameter prmDiamond = SqlHelper.CreateParameter("@DiamondCdinishTable", MainDiamondTable);
                SqlParameter prmPage = SqlHelper.CreateParameter("@Page", PageNumber);
                SqlParameter[] AllParams = { prmErr, prmDiamond, prmPage };
                SqlHelper.ExecuteNonQuery(_constring, CommandType.StoredProcedure, "Usp_AddUpdCdinishDiamond", AllParams);
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
            catch (Exception ex)
            {

                LogError("Error on Inserting Diamond Procedure - " + DateTime.Now.ToString() + "\n" + ex.Message);
            }
        }

        public static void LogError(string error)
        {
            StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
            str.WriteLine(error);
            str.Close();
            str.Dispose();
        }



        public bool IsValidCut(string val)
        {
            //val = val.ToUpper();
            //if (val.Contains("IDEAL") || val.Contains("FAIR") || val.Contains("FAIR +") || val.Contains("POOR"))
            //{
            return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public List<KeyValuePair<string, string>> getShape()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("RBC", "ROUND"));
            list.Add(new KeyValuePair<string, string>("EMR", "EMERALD"));
            list.Add(new KeyValuePair<string, string>("PRI", "PRINCESS"));
            list.Add(new KeyValuePair<string, string>("MQ", "MARQUISE"));
            list.Add(new KeyValuePair<string, string>("SQE", "ASSCHER"));
            list.Add(new KeyValuePair<string, string>("RAD", "RADIANT"));
            list.Add(new KeyValuePair<string, string>("CMB", "CUSHION"));
            list.Add(new KeyValuePair<string, string>("PS", "PEAR"));
            list.Add(new KeyValuePair<string, string>("HS", "HEART"));
            list.Add(new KeyValuePair<string, string>("OV", "OVAL"));

            return list;
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
