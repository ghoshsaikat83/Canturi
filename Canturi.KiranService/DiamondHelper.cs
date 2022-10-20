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

namespace Canturi.KiranService
{
    public class DiamondHelper
    {
        int DiamondCount;
        #region Private Variable
        string _constring = string.Empty;
        #endregion
        public DiamondHelper()
        {
            try
            {

                _constring = ConfigurationSettings.AppSettings["CanturiConnectionStr"].ToString();
            }
            catch
            {
                LogError("Sorry, connection to database could not made");

            }
        }
        public void KiranDiamond()
        {
            try
            {
                string jsonData = "";
                LogError("WebClient start");
                string Username = ConfigurationSettings.AppSettings["ApiUserName"].ToString();
                string Password = ConfigurationSettings.AppSettings["ApiPassword"].ToString();
                string View = ConfigurationSettings.AppSettings["ApiView"].ToString();
                WebRequest request = WebRequest.Create(ConfigurationSettings.AppSettings["KiranApiUrl"].ToString() + "?username=" + Username + "&password=" + Password + "&view=" + View);
                request.Method = "POST";
                WebResponse response = request.GetResponse();

                var encoding = ASCIIEncoding.ASCII;
                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                {
                    jsonData = reader.ReadToEnd();
                }
                LogError("WebClient end");

                //jsonData = File.ReadAllText(@"D:\Sanjeev Projects\Canturi\Canturi.KiranService\JsonData\50cf07a9-9ae1-4741-b036-cfbe7d130fbd.json");
                if (!String.IsNullOrEmpty(jsonData))
                {
                    //JsonFolderPath
                    string fileName = Guid.NewGuid().ToString() + ".json";
                    System.IO.File.WriteAllText(ConfigurationSettings.AppSettings["JsonFolderPath"].ToString() + fileName, jsonData);
                    LogError("Json file save: " + ConfigurationSettings.AppSettings["JsonFolderPath"].ToString() + fileName);

                    RootObject rootData = JsonConvert.DeserializeObject<RootObject>(jsonData);

                    int count = 1;
                    foreach (var item in rootData.StoneDetails)
                    {

                        if (IsValidDiamond(item.Luster, item.Status, item.Lab, item.Remark, item.ColTinge))
                        {
                            AddUpdKiranJewelsDiamond(item, count);
                            count++;
                        }
                    }
                    LogError("Successfully Inserting Diamond Procedure");
                }
                else
                {
                    LogError("Empty jsonData");
                }
            }
            catch (Exception ex)
            {

                LogError(ex.Message);
            }


        }


        public int AddUpdKiranJewelsDiamond(StoneDetails objDiamondModels, int rowNumber)
        {
            int result = -1;
            try
            {
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", 0);
                SqlParameter prmSkuDiamondID = SqlHelper.CreateParameter("@SkuDiamondID", "");
                SqlParameter prmDiamondImage = SqlHelper.CreateParameter("@DiamondImage", objDiamondModels.daylight);
                SqlParameter prmLotNumber = SqlHelper.CreateParameter("@LotNumber", objDiamondModels.StoneNo);
                SqlParameter prmShape = SqlHelper.CreateParameter("@Shape", GetShape(objDiamondModels.Shp));
                SqlParameter prmCarat = SqlHelper.CreateParameter("@Carat", objDiamondModels.Cts);
                SqlParameter prmColor = SqlHelper.CreateParameter("@Color", objDiamondModels.Col);
                SqlParameter prmClarity = SqlHelper.CreateParameter("@Clarity", objDiamondModels.Clr);
                SqlParameter prmCut = SqlHelper.CreateParameter("@Cut", GetCut(objDiamondModels.Cut));
                SqlParameter prmPolish = SqlHelper.CreateParameter("@Polish", GetPolish(objDiamondModels.Pol));
                SqlParameter prmSymmetry = SqlHelper.CreateParameter("@Symmetry", GetSymmetry(objDiamondModels.Sym));
                SqlParameter prmFluorescence = SqlHelper.CreateParameter("@Fluorescence", GetFluorescence(objDiamondModels.Flr));


                SqlParameter prmDepth = SqlHelper.CreateParameter("@Depth", objDiamondModels.TD);
                SqlParameter prmTable = SqlHelper.CreateParameter("@Table", objDiamondModels.Tbl);

                SqlParameter prmMeasurements = SqlHelper.CreateParameter("@Measurements", FnMeasurements(objDiamondModels.Max, objDiamondModels.Min, objDiamondModels.Hgt));
                SqlParameter prmRatio = SqlHelper.CreateParameter("@Ratio", "");
                SqlParameter prmLAB = SqlHelper.CreateParameter("@LAB", objDiamondModels.Lab);



                SqlParameter prmPrice = SqlHelper.CreateParameter("@Price", (Convert.ToDecimal(objDiamondModels.Price) * Convert.ToDecimal(objDiamondModels.Cts)));// * Convert.ToDecimal(objDiamondModels.Carat)


                SqlParameter prmSupplier = SqlHelper.CreateParameter("@Supplier", "KiranGems");


                SqlParameter prmEyeClean = SqlHelper.CreateParameter("@EyeClean", FnEyeClean(objDiamondModels.BIC, objDiamondModels.BIS, objDiamondModels.WIC));
                SqlParameter prmPerCaratPrice = SqlHelper.CreateParameter("@PerCaratPrice", (Convert.ToDecimal(objDiamondModels.Price)));// * Convert.ToDecimal(objDiamondModels.Carat)

                SqlParameter prmDiamondCertificate = SqlHelper.CreateParameter("@DiamondCertificate", objDiamondModels.RepNo);

                SqlParameter prmRowNumber = SqlHelper.CreateParameter("@RowNumber", rowNumber);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", 0);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", "");
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", 1);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    
                                                prmDiamondID, prmSkuDiamondID, prmDiamondImage, 
                                                prmLotNumber, prmShape,prmCarat, 
                                                prmColor, prmClarity,prmCut, 
                                                prmPolish, prmSymmetry,prmFluorescence, 
                                                prmDepth, prmTable, prmMeasurements,  
                                                prmRatio,prmLAB,  prmPrice,
                                                prmSupplier,prmEyeClean,prmPerCaratPrice,
                                                prmDiamondCertificate,prmRowNumber,prmCreatedBy, 
                                                prmCreatedFromIp,prmFlag, prmErr 
                                           };
                SqlHelper.ExecuteNonQuery(_constring, CommandType.StoredProcedure, "Usp_AddUpdKiranDiamond", allParams);

                if (prmErr.Value != null)
                {
                    result = (int)prmErr.Value;
                }
                if (result != 0)
                {
                    LogError("Error while Inserting Diamond Procedure");
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }

            return result;
        }

        public string FnMeasurements(string length, string width, string depth)
        {
            return length + " x " + width + " x " + depth;
        }

        public string FnEyeClean(string BIC, string BIS, string WIC)
        {
            if (string.IsNullOrEmpty(BIC) && string.IsNullOrEmpty(BIS) && string.IsNullOrEmpty(WIC))
            {
                return "yes";
            }
            else if (BIC == "B1" && BIS == "B1" && WIC == "W1")
            {
                return "yes";
            }
            else if (BIC == "B1" && string.IsNullOrEmpty(BIS) && string.IsNullOrEmpty(WIC))
            {
                return "yes";
            }
            else if (string.IsNullOrEmpty(BIC) && BIS == "B1" && string.IsNullOrEmpty(WIC))
            {
                return "yes";
            }
            else if (string.IsNullOrEmpty(BIC) && string.IsNullOrEmpty(BIS) && WIC == "W1")
            {
                return "yes";
            }
            else if (BIC == "B1" && BIS == "B1" && string.IsNullOrEmpty(WIC))
            {
                return "yes";
            }
            else if (string.IsNullOrEmpty(BIC) && BIS == "B1" && WIC == "W1")
            {
                return "yes";
            }
            else if (BIC == "B1" && string.IsNullOrEmpty(BIS) && WIC == "W1")
            {
                return "yes";
            }
            else
            {
                return "no";
            }


            //else if (BIC == "B2" && string.IsNullOrEmpty(BIS) && string.IsNullOrEmpty(WIC))
            //{
            //    return "no";
            //}
            //else if (BIC == "B3" && string.IsNullOrEmpty(BIS) && string.IsNullOrEmpty(WIC))
            //{
            //    return "no";
            //}
            //else if (BIC == "B4" && string.IsNullOrEmpty(BIS) && string.IsNullOrEmpty(WIC))
            //{
            //    return "no";
            //}

            //else if (string.IsNullOrEmpty(BIC) && BIS == "B2" && string.IsNullOrEmpty(WIC))
            //{
            //    return "no";
            //}
            //else if (string.IsNullOrEmpty(BIC) && BIS == "B3" && string.IsNullOrEmpty(WIC))
            //{
            //    return "no";
            //}
            //else if (string.IsNullOrEmpty(BIC) && BIS == "B4" && string.IsNullOrEmpty(WIC))
            //{
            //    return "no";
            //}

            //else if (string.IsNullOrEmpty(BIC) && string.IsNullOrEmpty(BIS) && WIC == "W2")
            //{
            //    return "no";
            //}
            //else if (string.IsNullOrEmpty(BIC) && string.IsNullOrEmpty(BIS) && WIC == "W3")
            //{
            //    return "no";
            //}
            //else if (BIC == "B1" && BIS == "B1" && WIC=="W2")
            //{
            //    return "no";
            //}
            //else if (BIC == "B1" && BIS == "B1" && WIC == "W3")
            //{
            //    return "no";
            //}




            //else if (BIC == "B1" && BIS == "B2" && WIC == "W3")
            //{
            //    return "no";
            //}
            //else if (BIC == "B1" && BIS == "B2" && WIC == "W1")
            //{
            //    return "no";
            //}
            //else if (BIC == "B1" && BIS == "B3" && WIC == "W1")
            //{
            //    return "no";
            //}
            //else if (BIC == "B1" && BIS == "B4" && WIC == "W1")
            //{
            //    return "no";
            //}
            //else if (BIC == "B1" && BIS == "B1" && WIC == "W3")
            //{
            //    return "no";
            //}


            //else if (BIC == "B4" && BIS == "B1" && string.IsNullOrEmpty(WIC))
            //{
            //    return "no";
            //}
            //else
            //{
            //    if (WIC == "W1")
            //    {
            //        return "yes";
            //    }
            //    return "no";
            //}
        }

        public bool IsValidDiamond(string luster, string status, string lab, string remark, string colTinge)
        {
            if (!String.IsNullOrEmpty(luster))
            {
                if (luster.ToUpper() == "M1")
                {

                }
                else if (luster.ToUpper() == "NONE")
                {

                }
                else
                {
                    return false;
                }
            }


            if (!string.IsNullOrEmpty(status))
            {
                if (status.ToUpper() != "AVAILABLE")
                {
                    return false;
                }

            }

            if (!string.IsNullOrEmpty(remark))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(colTinge))
            {
                if (colTinge.ToUpper() != "MIX TINGE 1")
                {
                    return false;
                }
            }


            if (!string.IsNullOrEmpty(lab) && lab.ToUpper() != "GIA")
            {
                return false;
            }
            return true;
        }


        public string GetShape(string shape)
        {
            switch (shape.ToUpper())
            {
                case "RB":
                    shape = "Round";
                    break;
                case "PR":
                    shape = "Princess";
                    break;
                case "CU":
                    shape = "Cushion";
                    break;
                case "ASH":
                    shape = "Asscher";
                    break;
                case "EM":
                    shape = "Emerald";
                    break;
                case "RD":
                    shape = "Radiant";
                    break;
                case "SQ. RADIANT":
                    shape = "Radiant";
                    break;
                case "PE":
                    shape = "Pear";
                    break;
                case "OL":
                    shape = "Oval";
                    break;
                case "HT":
                    shape = "Heart";
                    break;
                case "MQ":
                    shape = "Marquise";
                    break;
                default:
                    shape = shape;
                    break;
            }
            return shape;
        }

        public string GetCut(string cut)
        {
            switch (cut.ToUpper())
            {
                case "G":
                    cut = "Good";
                    break;
                case "VG":
                    cut = "Very Good";
                    break;
                case "EX":
                    cut = "Excellent";
                    break;
                default:
                    cut = cut;
                    break;
            }
            return cut;
        }

        public string GetPolish(string polish)
        {
            switch (polish.ToUpper())
            {
                case "G":
                    polish = "Good";
                    break;
                case "VG":
                    polish = "Very Good";
                    break;
                case "EX":
                    polish = "Excellent";
                    break;
                default:
                    polish = polish;
                    break;
            }
            return polish;
        }

        public string GetSymmetry(string symmetry)
        {
            switch (symmetry.ToUpper())
            {
                case "G":
                    symmetry = "Good";
                    break;
                case "VG":
                    symmetry = "Very Good";
                    break;
                case "EX":
                    symmetry = "Excellent";
                    break;
                default:
                    symmetry = symmetry;
                    break;
            }
            return symmetry;
        }

        public string GetFluorescence(string fluorescence)
        {
            switch (fluorescence.ToUpper())
            {
                case "NON":
                    fluorescence = "None";
                    break;
                case "FNT":
                    fluorescence = "Faint";
                    break;
                case "SLT":
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

        public static void LogError(string error)
        {
            StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
            str.WriteLine(error + " - " + DateTime.Now.ToString());
            str.Close();
            str.Dispose();
        }
    }


    public class StoneDetails
    {
        public string SrNo { get; set; }
        public string StoneNo { get; set; }
        public string Status { get; set; }
        public string Shp { get; set; }
        public string Cts { get; set; }
        public string Col { get; set; }
        public string Scol { get; set; }
        public string Clr { get; set; }
        public string Sclr { get; set; }
        public string Cut { get; set; }
        public string Pol { get; set; }
        public string Sym { get; set; }
        public string Flr { get; set; }
        public string Lab { get; set; }
        public string Rap { get; set; }
        public string Disc { get; set; }
        public string Price { get; set; }
        public string Amt { get; set; }
        public string Max { get; set; }
        public string Min { get; set; }
        public string Hgt { get; set; }
        public string Tbl { get; set; }
        public string TD { get; set; }
        public string Girdle { get; set; }
        public string CA { get; set; }
        public string CH { get; set; }
        public string PA { get; set; }
        public string PD { get; set; }
        public string Culet { get; set; }
        public string KTS { get; set; }
        public string Loc { get; set; }
        public string LW { get; set; }
        public string HAndA { get; set; }
        public string ColTinge { get; set; }
        public string Luster { get; set; }
        public string Remark { get; set; }
        public string BIC { get; set; }
        public string BIS { get; set; }
        public string WIC { get; set; }
        public string LI { get; set; }
        public string CertType { get; set; }
        public string RepNo { get; set; }
        public string ReportComment { get; set; }
        public string GirdlePer { get; set; }
        public string video { get; set; }
        public string certificate { get; set; }
        public string daylight { get; set; }
        public string heart { get; set; }
        public string arrow { get; set; }
        public string plotting { get; set; }
        public string dimension { get; set; }
    }

    public class StoneHeader
    {
        public string dataType { get; set; }
        public string Name { get; set; }
    }

    public class RootObject
    {
        public string PageSize { get; set; }
        public string TotalPage { get; set; }
        public string TotalRecords { get; set; }
        public List<StoneHeader> StoneHeader { get; set; }
        public List<StoneDetails> StoneDetails { get; set; }
    }
}
