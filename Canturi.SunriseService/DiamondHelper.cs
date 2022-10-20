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

namespace Canturi.SunriseService
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
        public void SunriseDiamond()
        {
            try
            {


                //WebRequest request = WebRequest.Create("http://diamonds.kirangems.com/GemKOnline/jsnsearch/jsndetail/query?username=stefano&password=stefano&view=canturi");
                //request.Method = "POST";
                //WebResponse response = request.GetResponse();

                //string responseText = "";
                //var encoding = ASCIIEncoding.ASCII;
                //using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                //{
                //    responseText = reader.ReadToEnd();
                //}

                //string fileName = Guid.NewGuid().ToString() + ".json";
                //System.IO.File.WriteAllText(ConfigurationSettings.AppSettings["JsonFolderPath"].ToString() + fileName, responseText);




                string jsonData = "";
                LogError("WebClient start");
                using (WebClient wc = new WebClient())
                {
                    jsonData = wc.DownloadString(ConfigurationSettings.AppSettings["SunriseApiUrl"].ToString());
                }
                LogError("WebClient end");
                if (!String.IsNullOrEmpty(jsonData))
                {
                    //JsonFolderPath
                    string fileName = Guid.NewGuid().ToString() + ".json";
                    System.IO.File.WriteAllText(ConfigurationSettings.AppSettings["JsonFolderPath"].ToString() + fileName, jsonData);
                    LogError("Json file save: " + ConfigurationSettings.AppSettings["JsonFolderPath"].ToString() + fileName);


                    // jsonData = File.ReadAllText(@"D:\Projects\Canturi\Canturi.SunriseService\JsonData\2a27a99d-c974-493d-ab58-8b22c090ff0b.json");
                    List<RootObject> rootDate = JsonConvert.DeserializeObject<List<RootObject>>(jsonData);

                    int count = 1;
                    foreach (var item in rootDate)
                    {

                        if (IsValidDiamond(item.Luster, item.Location, item.Clarity, item.Status))
                        {
                            AddUpdSunriseJewelsDiamond(item, count);
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


        public int AddUpdSunriseJewelsDiamond(RootObject objDiamondModels, int rowNumber)
        {
            int result = -1;

            try
            {
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", 0);
                SqlParameter prmSkuDiamondID = SqlHelper.CreateParameter("@SkuDiamondID", "");
                SqlParameter prmDiamondImage = SqlHelper.CreateParameter("@DiamondImage", "");
                SqlParameter prmLotNumber = SqlHelper.CreateParameter("@LotNumber", objDiamondModels.RefNo);
                SqlParameter prmShape = SqlHelper.CreateParameter("@Shape", GetShape(objDiamondModels.Shape));
                SqlParameter prmCarat = SqlHelper.CreateParameter("@Carat", objDiamondModels.Carat);
                SqlParameter prmColor = SqlHelper.CreateParameter("@Color", objDiamondModels.Color);
                SqlParameter prmClarity = SqlHelper.CreateParameter("@Clarity", objDiamondModels.Clarity);
                SqlParameter prmCut = SqlHelper.CreateParameter("@Cut", GetCut(objDiamondModels.Cut));
                SqlParameter prmPolish = SqlHelper.CreateParameter("@Polish", GetPolish(objDiamondModels.Polish));
                SqlParameter prmSymmetry = SqlHelper.CreateParameter("@Symmetry", GetSymmetry(objDiamondModels.Symmetry));
                SqlParameter prmFluorescence = SqlHelper.CreateParameter("@Fluorescence", GetFluorescence(objDiamondModels.Flourescence));


                SqlParameter prmDepth = SqlHelper.CreateParameter("@Depth", objDiamondModels.Depth);
                SqlParameter prmTable = SqlHelper.CreateParameter("@Table", objDiamondModels.Table);



                SqlParameter prmMeasurements = SqlHelper.CreateParameter("@Measurements", FnMeasurements(objDiamondModels.Length, objDiamondModels.Width, objDiamondModels.Depth));
                SqlParameter prmRatio = SqlHelper.CreateParameter("@Ratio", "");

                SqlParameter prmLAB = SqlHelper.CreateParameter("@LAB", objDiamondModels.Lab);



                SqlParameter prmPrice = SqlHelper.CreateParameter("@Price", (Convert.ToDecimal(objDiamondModels.NetAmt)));// * Convert.ToDecimal(objDiamondModels.Carat)

                SqlParameter prmSupplier = SqlHelper.CreateParameter("@Supplier", "Sunrise");


                SqlParameter prmEyeClean = SqlHelper.CreateParameter("@EyeClean", FnEyeClean(objDiamondModels.Clarity, objDiamondModels.Location));
                SqlParameter prmPerCaratPrice = SqlHelper.CreateParameter("@PerCaratPrice", objDiamondModels.NetAmt);

                SqlParameter prmDiamondCertificate = SqlHelper.CreateParameter("@DiamondCertificate", objDiamondModels.DiamondCertificate);
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
                                                prmDiamondCertificate,prmRowNumber,
                                                prmCreatedBy, prmCreatedFromIp,  
                                                prmFlag, prmErr 
                                           };
                SqlHelper.ExecuteNonQuery(_constring, CommandType.StoredProcedure, "Usp_AddUpdSunriseDiamond", allParams);

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

        public string FnEyeClean(string clarity, string location)
        {
            string[] hongKongEyeClean = { "S12", "SI1", "VS2", "VS1", "VVS2", "VVS1", "IF", "FL" };
            string[] indiaEyeClean = { "VS1", "VVS2", "VVS1", "IF", "FL" };
            if (location.ToUpper() == "HONG KONG")
            {
                if (hongKongEyeClean.Contains(clarity.ToUpper()))
                {
                    return "yes";
                }
            }
            if (location.ToUpper() == "INDIA")
            {
                if (indiaEyeClean.Contains(clarity.ToUpper()))
                {
                    return "yes";
                }
            }
            return "no";
        }

        public bool IsValidDiamond(string luster, string location, string clarity, string status)
        {
            if (location.ToUpper() == "INDIA")
            {
                string[] invalidClarity = { "SI2", "SI1", "VS2" };
                if (invalidClarity.Contains(clarity.Trim().ToUpper()))
                {
                    return false;
                }
            }
            if (!String.IsNullOrEmpty(status))
            {
                if (status.ToUpper() == "B")
                {
                    return false;
                }
            }

            if (String.IsNullOrEmpty(luster))
            {
                return true;
            }
            else
            {
                if (luster.ToUpper() == "M0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }


        public string GetShape(string shape)
        {
            switch (shape.ToUpper())
            {
                case "ROUND":
                    shape = "Round";
                    break;
                case "PRINCESS":
                    shape = "Princess";
                    break;
                case "CUSHION":
                    shape = "Cushion";
                    break;
                case "CUSHION MODIFIED":
                    shape = "Cushion";
                    break;
                case "EMERALD":
                    shape = "Emerald";
                    break;
                case "SQ. EMERALD":
                    shape = "Asscher";
                    break;
                case "RADIANT":
                    shape = "Radiant";
                    break;
                case "SQ. RADIANT":
                    shape = "Radiant";
                    break;
                case "PEAR":
                    shape = "Pear";
                    break;
                case "OVAL":
                    shape = "Oval";
                    break;
                case "HEART":
                    shape = "Heart";
                    break;
                case "MARQUISE":
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


    public class DiamondData
    {
        [JsonProperty(PropertyName = "Location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "Lab")]
        public string Lab { get; set; }

        [JsonProperty(PropertyName = "View Image")]
        public string ViewImage { get; set; }

        [JsonProperty(PropertyName = "View HD")]
        public string ViewHD { get; set; }

        [JsonProperty(PropertyName = "Ref. No")]
        public string RefNo { get; set; }

        [JsonProperty(PropertyName = "Shape")]
        public string Shape { get; set; }

        [JsonProperty(PropertyName = "Pointer")]
        public string Pointer { get; set; }

        [JsonProperty(PropertyName = "Certi No.")]
        public string CertiNo { get; set; }

        [JsonProperty(PropertyName = "Color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "Clarity")]
        public string Clarity { get; set; }

        [JsonProperty(PropertyName = "Cts")]
        public string Cts { get; set; }

        [JsonProperty(PropertyName = "Cut")]
        public string Cut { get; set; }

        [JsonProperty(PropertyName = "Polish")]
        public string Polish { get; set; }

        [JsonProperty(PropertyName = "Symm")]
        public string Symm { get; set; }

        [JsonProperty(PropertyName = "Fls")]
        public string Fls { get; set; }

        [JsonProperty(PropertyName = "Rap Price($)")]
        public string RapPrice { get; set; }

        [JsonProperty(PropertyName = "Rap Amt($)")]
        public string RapAmt { get; set; }

        [JsonProperty(PropertyName = "Disc(%)")]
        public string DiscPerc { get; set; }

        [JsonProperty(PropertyName = "Net Amt($)")]
        public string NetAmt { get; set; }

        [JsonProperty(PropertyName = "Length")]
        public string Length { get; set; }

        [JsonProperty(PropertyName = "Width")]
        public string Width { get; set; }

        [JsonProperty(PropertyName = "Depth")]
        public string Depth { get; set; }

        [JsonProperty(PropertyName = "Depth(%)")]
        public string DepthPerc { get; set; }

        [JsonProperty(PropertyName = "Table(%)")]
        public string Table { get; set; }

        [JsonProperty(PropertyName = "Key To Symbol")]
        public string KeyToSymbol { get; set; }

        [JsonProperty(PropertyName = "Cr Ang")]
        public string CrAng { get; set; }

        [JsonProperty(PropertyName = "Cr Ht")]
        public string CrHt { get; set; }

        [JsonProperty(PropertyName = "Pav Ang")]
        public string PavAng { get; set; }

        [JsonProperty(PropertyName = "Pav Ht")]
        public string PavHt { get; set; }

        [JsonProperty(PropertyName = "Girdle Type")]
        public string GirdleType { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "Luster")]
        public string Luster { get; set; }

        [JsonProperty(PropertyName = "Shade")]
        public string Shade { get; set; }

        [JsonProperty(PropertyName = "Lab Link")]
        public string LabLink { get; set; }
    }

    public class RootObject
    {
        [JsonProperty(PropertyName = "Location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "Lab")]
        public string Lab { get; set; }

        [JsonProperty(PropertyName = "View Image")]
        public string ViewImage { get; set; }

        [JsonProperty(PropertyName = "View HD")]
        public string ViewHD { get; set; }

        [JsonProperty(PropertyName = "Ref. No")]
        public string RefNo { get; set; }

        [JsonProperty(PropertyName = "Shape")]
        public string Shape { get; set; }

        [JsonProperty(PropertyName = "Pointer")]
        public string Pointer { get; set; }

        [JsonProperty(PropertyName = "Certi No.")]
        public string DiamondCertificate { get; set; }

        [JsonProperty(PropertyName = "Color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "Clarity")]
        public string Clarity { get; set; }

        [JsonProperty(PropertyName = "Cts")]
        public string Carat { get; set; }

        [JsonProperty(PropertyName = "Cut")]
        public string Cut { get; set; }

        [JsonProperty(PropertyName = "Polish")]
        public string Polish { get; set; }

        [JsonProperty(PropertyName = "Symm")]
        public string Symmetry { get; set; }

        [JsonProperty(PropertyName = "Fls")]
        public string Flourescence { get; set; }

        [JsonProperty(PropertyName = "Rap Price($)")]
        public string RapPrice { get; set; }

        [JsonProperty(PropertyName = "Rap Amt($)")]
        public string RapAmt { get; set; }

        [JsonProperty(PropertyName = "Disc(%)")]
        public string DiscPerc { get; set; }

        [JsonProperty(PropertyName = "Net Amt($)")]
        public string NetAmt { get; set; }

        [JsonProperty(PropertyName = "Length")]
        public string Length { get; set; }

        [JsonProperty(PropertyName = "Width")]
        public string Width { get; set; }

        [JsonProperty(PropertyName = "Depth")]
        public string Depth { get; set; }

        [JsonProperty(PropertyName = "Depth(%)")]
        public string DepthPerc { get; set; }

        [JsonProperty(PropertyName = "Table(%)")]
        public string Table { get; set; }

        [JsonProperty(PropertyName = "Key To Symbol")]
        public string KeyToSymbol { get; set; }

        [JsonProperty(PropertyName = "Cr Ang")]
        public string CrAng { get; set; }

        [JsonProperty(PropertyName = "Cr Ht")]
        public string CrHt { get; set; }

        [JsonProperty(PropertyName = "Pav Ang")]
        public string PavAng { get; set; }

        [JsonProperty(PropertyName = "Pav Ht")]
        public string PavHt { get; set; }

        [JsonProperty(PropertyName = "Girdle Type")]
        public string GirdleType { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "Luster")]
        public string Luster { get; set; }

        [JsonProperty(PropertyName = "Shade")]
        public string Shade { get; set; }

        [JsonProperty(PropertyName = "Lab Link")]
        public string LabLink { get; set; }
    }
}
