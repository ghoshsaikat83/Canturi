using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using Canturi.Models.BusinessEntity.FrontEnd;

namespace Canturi.Models.BusinessHelper.CommonHelper
{
    public class JBBrosHelper
    {
        int DiamondCount;
        #region Private Variable
        string _constring = string.Empty;
        #endregion

       

        public void CanturiDaimond(List<JBBrosDiamondModel> jbBrosDiamondList)
        {
            try
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

                foreach (JBBrosDiamondModel model in jbBrosDiamondList)
                {                    
                    DataRow row = MainDiamondTable.NewRow();
                    row["DiamondID"] = model.Srno;
                    row["VendorStockNumber"] = model.RefNo.ToString();
                    row["ShapeTitle"] = model.getShape().Where(x=>x.Key==model.Shape).Single().Value;
                    row["Weight"] = model.Carat;
                    row["ColorTitle"] = model.Color;
                    row["ClarityTitle"] = model.Purity;
                    row["CutLongTitle"] = model.getCUT().Where(x=>x.Key==model.Cut).Single().Value;
                    row["PolishTitle"] = model.getPolish().Where(x=>x.Key==model.Polish).Single().Value;
                    row["SymmetryTitle"] = model.getPolish().Where(x => x.Key == model.Polish).Single().Value;
                    row["FluorescenceIntensityTitle"] = model.getFloroscence().Where(x=>x.Key==model.FL).Single().Value;
                    row["DepthPercent"] = model.TotalDepthPer;
                    row["TablePercent"] = model.TabelPer;
                    row["MeasLength"] = model.DimMaxAndWidth;
                    row["MeasWidth"] = model.DimMinAndWidth;
                    row["MeasDepth"] = model.TotalDept_mm;
                    row["LabTitle"] = model.Lab;
                    row["Ratio"] = model.Ratio;                   
                    row["CertificateNumber"] = model.CertNo;
                    if (model.CertNo != "NULL")
                        row["HasCertFile"] = "True";
                    row["RapNetPrice"] = model.RapaportPrice;
                    row["FinalPrice"] = model.Price;
                    row["TotalSalesPriceInCurrency"] = model.Price;
                    row["FluorescenceColorTitle"] = model.FluoresentColor;
                    row["GirdleSizeMin"] = 0;
                    row["GirdleSizeMax"] = 0;
                    row["GirdleConditionTitle"] = model.GirdleCondition;
                    row["CuletSizeTitle"] = model.Culate;
                    row["CuletConditionTitle"] ="";                    
                    MainDiamondTable.Rows.Add(row);
                }

                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                SqlParameter prmDiamond = SqlHelper.CreateParameter("@Diamond", MainDiamondTable);
                SqlParameter[] AllParams = { prmErr, prmDiamond };
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_AddUpdJBBrosDiamond", AllParams);
                //if (prmErr.Value != null)
                //{
                //    if (prmErr.Value.ToString() == "0")
                //    {
                //        StreamWriter str = new StreamWriter(@"" + ConfigurationManager.AppSettings["ErrorFilePath"].ToString() + "", true);
                //        str.WriteLine("Successfully Inserting Diamond Procedure - " + DateTime.Now.ToString());
                //        str.Close();
                //        str.Dispose();
                //    }
                //    else
                //    {
                //        StreamWriter str = new StreamWriter(@"" + ConfigurationManager.AppSettings["ErrorFilePath"].ToString() + "", true);
                //        str.WriteLine("Error on Inserting Diamond Procedure - " + DateTime.Now.ToString());
                //        str.Close();
                //        str.Dispose();
                //    }
                //}
                //else
                //{
                //    StreamWriter str = new StreamWriter(@"" + ConfigurationManager.AppSettings["ErrorFilePath"].ToString() + "", true);
                //    str.WriteLine("Error on Inserting Diamond Procedure - " + DateTime.Now.ToString());
                //    str.Close();
                //    str.Dispose();
                //}




            }
            catch (Exception ex)
            {
                //StreamWriter str = new StreamWriter(@"" + ConfigurationManager.AppSettings["ErrorFilePath"].ToString() + "", true);
                //str.WriteLine("Error on Function Inserting Diamond - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.Source.ToString());
                //str.Close();
                //str.Dispose();
            }
        }           
    }
}
