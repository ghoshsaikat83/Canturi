using Canturi.Models.BusinessEntity.Admin;
using Canturi.Models.BusinessEntity.FrontEnd;
using Canturi.Models.BusinessHelper.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Canturi.Models.BusinessHelper.CommonHelper
{
    public static class CommonData
    {
        public static List<SelectListItem> GetMonthNumber()
        {
            return new List<SelectListItem>
                       {
                           new SelectListItem {Text = "January (1)", Value = "1"},
                           new SelectListItem {Text = "February (2)", Value = "2"},
                           new SelectListItem {Text = "Month (3)", Value = "3"},
                           new SelectListItem {Text = "April (4)", Value = "4"},
                           new SelectListItem {Text = "May (5)", Value = "5"},
                           new SelectListItem {Text = "June (6)", Value = "6"},
                           new SelectListItem {Text = "July (7)", Value = "7"},
                           new SelectListItem {Text = "August (8)", Value = "8"},
                           new SelectListItem {Text = "September (9)", Value = "9"},
                           new SelectListItem {Text = "October (10)", Value = "10"},
                           new SelectListItem {Text = "November (11)", Value = "11"},
                           new SelectListItem {Text = "December (12)", Value = "12"}
                       };
        }

        public static List<SelectListItem> GetYearNumber()
        {
            int startYear = 2016;
            int endYear = DateTime.Now.Year;
            List<SelectListItem> itemList = new List<SelectListItem>();
            for (int i = startYear; i <= endYear; i++)
            {
                itemList.Add(new SelectListItem { Text = i.ToString().Substring(2, 2), Value = i.ToString() });
            }
            return itemList;
        }


        public static List<SelectListItem> GetConsultants()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();

            ConsultantModels model = new ConsultantModels { Flag = 2, Status = 1 };
            ConsultantHelper objHelper = new ConsultantHelper();


            List<ConsultantModels> ActiveConsultantList = objHelper.GetConsultants(model);

            for (int i = 0; i < ActiveConsultantList.Count; i++)
            {
                itemList.Add(new SelectListItem { Text = ActiveConsultantList[i].ConsultantName, Value = ActiveConsultantList[i].ConsultantId.ToString() });
            }

            return itemList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">message for print</param>
        /// <param name="status">0 for error, 1 for success</param>
        /// <returns></returns>
        public static String GetMessage(string message, int status)
        {
            //string strMsgClass = "MsgGreen";
            //if (status == 0)
            //    strMsgClass = "MsgRed";

            string strMsgClass = "alert alert-success alert-dismissable";
            string strIcon = "fa fa-check";
            if (status == 0)
            {
                strMsgClass = "alert alert-danger alert-dismissable";
                strIcon = "fa fa-ban";
            }


            return "<div class=\"alert-mesg\">" +
                     "<div class=\"" + strMsgClass + "\">" +
                         "<i class=\"" + strIcon + "\"></i>" +
                         "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">" +
                             "&times;" +
                         "</button>" +
                         message +
                      "</div>" +
                     "</div>";

            //return "<span class=\"" + strMsgClass + "\" >" + message + "</span>";
        }


        /// <summary>
        /// function to read a file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static String readingFile(string path)
        {
            StreamReader fp = File.OpenText(path);
            string result = fp.ReadToEnd();
            fp.Close();
            return result;
        }

        public static bool IsFileJPG_GIF_PNG(HttpPostedFileBase file)
        {
            var extension = Path.GetExtension(file.FileName ?? string.Empty);
            var validExtensions = new[] { ".jpg", ".jpeg", ".gif", ".png" };
            var isValid = validExtensions.Contains(extension.ToLower(), StringComparer.OrdinalIgnoreCase);
            return isValid;
        }

        public static bool IsFilePDF(HttpPostedFileBase file)
        {
            var extension = Path.GetExtension(file.FileName ?? string.Empty);
            var validExtensions = new[] { ".pdf" };
            var isValid = validExtensions.Contains(extension.ToLower(), StringComparer.OrdinalIgnoreCase);
            return isValid;
        }

        public static string FixDescription(string Desc, int Length)
        {
            string strNewDesc = "";
            strNewDesc = (new System.Text.RegularExpressions.Regex("<[^>]*>")).Replace(Desc, "");
            if (strNewDesc.Length > Length)
            {
                strNewDesc = strNewDesc.Substring(0, Length) + "...";
            }
            return strNewDesc;
        }

        public static List<SelectListItem> SalonList()
        {
            return new List<SelectListItem>{
                new SelectListItem { Text = "Salon 1", Value = "Salon 1" },
                new SelectListItem { Text = "Salon 2", Value = "Salon 2" }
            };
        }

        public static List<SelectListItem> ShapeList()
        {
            List<SelectListItem> listShape = new List<SelectListItem>();
            listShape.Add(new SelectListItem { Text = "Round", Value = "Round" });
            listShape.Add(new SelectListItem { Text = "Oval", Value = "Oval" });
            listShape.Add(new SelectListItem { Text = "Pear", Value = "Pear" });
            listShape.Add(new SelectListItem { Text = "Heart", Value = "Heart" });
            listShape.Add(new SelectListItem { Text = "Princess", Value = "Princess" });
            listShape.Add(new SelectListItem { Text = "Emerald", Value = "Emerald" });
            listShape.Add(new SelectListItem { Text = "Asscher", Value = "Asscher" });
            listShape.Add(new SelectListItem { Text = "Cushion", Value = "Cushion" });
            listShape.Add(new SelectListItem { Text = "Marquise", Value = "Marquise" });
            listShape.Add(new SelectListItem { Text = "Radiant", Value = "Radiant" });
            return listShape;
        }

        public static List<SelectListItem> CutTypeList()
        {
            return new List<SelectListItem>{
                new SelectListItem { Text = "Excellent", Value = "Excellent" },
                new SelectListItem { Text = "Very good", Value = "Very good" },
                new SelectListItem { Text = "Good", Value = "Good" },
                //new SelectListItem { Text = "Signature Ideal", Value = "Signature Ideal" }
            };
        }

        public static List<SelectListItem> ClarityTypeList()
        {
            List<SelectListItem> listClarityType = new List<SelectListItem>();
            listClarityType.Add(new SelectListItem { Text = "SI2", Value = "SI2" });
            listClarityType.Add(new SelectListItem { Text = "SI1", Value = "SI1" });
            listClarityType.Add(new SelectListItem { Text = "VS2", Value = "VS2" });
            listClarityType.Add(new SelectListItem { Text = "VS1", Value = "VS1" });
            listClarityType.Add(new SelectListItem { Text = "VVS2", Value = "VVS2" });
            listClarityType.Add(new SelectListItem { Text = "VVS1", Value = "VVS1" });
            listClarityType.Add(new SelectListItem { Text = "IF", Value = "IF" });
            listClarityType.Add(new SelectListItem { Text = "FL", Value = "FL" });

            return listClarityType;
        }

        public static List<SelectListItem> PolishTypeList()
        {
            List<SelectListItem> listPolishType = new List<SelectListItem>();
            listPolishType.Add(new SelectListItem { Text = "Excellent", Value = "Excellent" });
            listPolishType.Add(new SelectListItem { Text = "Very good", Value = "Very good" });
            listPolishType.Add(new SelectListItem { Text = "Good", Value = "Good" });
            return listPolishType;
        }

        public static List<SelectListItem> SymmetryTypeList()
        {
            List<SelectListItem> listSymmetryType = new List<SelectListItem>();
            listSymmetryType.Add(new SelectListItem { Text = "Excellent", Value = "Excellent" });
            listSymmetryType.Add(new SelectListItem { Text = "Very good", Value = "Very good" });
            listSymmetryType.Add(new SelectListItem { Text = "Good", Value = "Good" });
            return listSymmetryType;
        }

        public static List<SelectListItem> FlourescenceTypeList()
        {
            List<SelectListItem> listFlourescenceType = new List<SelectListItem>();
            listFlourescenceType.Add(new SelectListItem { Text = "None", Value = "None" });
            listFlourescenceType.Add(new SelectListItem { Text = "Faint", Value = "Faint" });
            listFlourescenceType.Add(new SelectListItem { Text = "Medium", Value = "Medium" });
            listFlourescenceType.Add(new SelectListItem { Text = "Strong", Value = "Strong" });
            //listFlourescenceType.Add(new SelectListItem { Text = "Very Strong", Value = "Very Strong" });
            return listFlourescenceType;
        }


        public static List<SelectListItem> LabTypeList()
        {
            List<SelectListItem> listLabType = new List<SelectListItem>();
            listLabType.Add(new SelectListItem { Text = "GIA", Value = "GIA" });
            listLabType.Add(new SelectListItem { Text = "GSL", Value = "GSL" });
            listLabType.Add(new SelectListItem { Text = "-", Value = "-" });
            //listFlourescenceType.Add(new SelectListItem { Text = "Very Strong", Value = "Very Strong" });
            return listLabType;
        }

        //public static List<SelectListItem> CaratTypeList()
        //{
        //    List<SelectListItem> listFlourescenceType = new List<SelectListItem>();
        //    listFlourescenceType.Add(new SelectListItem { Text = "None", Value = "None" });
        //    listFlourescenceType.Add(new SelectListItem { Text = "Faint", Value = "Faint" });
        //    listFlourescenceType.Add(new SelectListItem { Text = "Medium", Value = "Medium" });
        //    listFlourescenceType.Add(new SelectListItem { Text = "Strong", Value = "Strong" });
        //    return listFlourescenceType;
        //}

        public static List<SelectListItem> ColorTypeList()
        {
            List<SelectListItem> listColorType = new List<SelectListItem>();
            listColorType.Add(new SelectListItem { Text = "J", Value = "J" });
            listColorType.Add(new SelectListItem { Text = "I", Value = "I" });
            listColorType.Add(new SelectListItem { Text = "H", Value = "H" });
            listColorType.Add(new SelectListItem { Text = "G", Value = "G" });
            listColorType.Add(new SelectListItem { Text = "F", Value = "F" });
            listColorType.Add(new SelectListItem { Text = "E", Value = "E" });
            listColorType.Add(new SelectListItem { Text = "D", Value = "D" });
            return listColorType;
        }


        public static string GetCutRange(string cutMin, string cutMax)
        {
            string strCut = string.Empty;
            int cMin = Convert.ToInt32(cutMin) - 1;
            int cMax = Convert.ToInt32(cutMax) - 2;

            if (cMin == 0 && cMax == 2)
            {
                foreach (var item in CommonData.CutTypeList())
                {
                    strCut = strCut + "'" + item.Value + "',";
                }

            }
            else if (cMin == cMax)
            {
                strCut = "'" + CommonData.CutTypeList()[Convert.ToInt32(cMin)].Value + "',";
            }
            else
            {
                for (int i = Convert.ToInt32(cMin); i <= Convert.ToInt32(cMax); i++)
                {
                    strCut = strCut + "'" + CommonData.CutTypeList()[i].Value + "',";
                }
            }

            if (strCut.Contains("Excellent"))
            {
                strCut = strCut + "'Excellent +',";
            }
            if (strCut.EndsWith(","))
            {
                strCut = strCut.Substring(0, strCut.Length - 1);
            }
            return strCut;
        }

        public static string GetFlourescenceRange(string flourscenceMin, string flourscenceMax)
        {
            string strFlourscence = string.Empty;
            int fMin = Convert.ToInt32(flourscenceMin) - 1;
            int fMax = Convert.ToInt32(flourscenceMax) - 2;
            if (fMin == 0 && fMax == 2)
            {
                for (int i = Convert.ToInt32(fMin); i <= Convert.ToInt32(fMax); i++)
                {
                    strFlourscence = strFlourscence + "'" + CommonData.FlourescenceTypeList()[i].Value + "',";
                }
                //foreach (var item in CommonData.FlourescenceTypeList())
                //{
                //    strFlourscence = strFlourscence + "'" + item.Value + "',";
                //}

            }
            else if (fMin == fMax)
            {
                strFlourscence = "'" + CommonData.FlourescenceTypeList()[Convert.ToInt32(fMin)].Value + "',";
            }
            else
            {
                for (int i = Convert.ToInt32(fMin); i <= Convert.ToInt32(fMax); i++)
                {
                    strFlourscence = strFlourscence + "'" + CommonData.FlourescenceTypeList()[i].Value + "',";
                }
            }
            if (strFlourscence.EndsWith(","))
            {
                strFlourscence = strFlourscence.Substring(0, strFlourscence.Length - 1);
            }
            if (strFlourscence.ToLower().Contains("'strong'"))
            {
                strFlourscence = strFlourscence + ",'very strong'";
            }
            if (strFlourscence.ToLower().Contains("'faint'"))
            {
                strFlourscence = strFlourscence + ",'slight','very slight'";
            }
            strFlourscence = strFlourscence + ",''";
            return strFlourscence;
        }

        public static string GetClartiyRange(string clarityMin, string clarityMax)
        {
            string strClartiy = string.Empty;
            int cMin = 0, cMax = 0;
            cMin = Convert.ToInt32(clarityMin);// -1;
            cMax = Convert.ToInt32(clarityMax);// -1;

            if (cMin <= 4)
            {
                cMin = cMin - 1;
                cMax = cMax - 1;
            }
            else
            {
                cMin = cMin - 2;
                cMax = cMax - 2;
            }

            if (cMin < 0)
                cMin = 0;
            if (cMax < 0)
                cMax = 0;
            //else
            //{
            //    cMin = cMin - 1;
            //    cMax = cMax - 1;
            //}

            //if (cMin == 0 && cMax == 2)
            //{
            //    foreach (var item in CommonData.ClarityTypeList())
            //    {
            //        strClartiy = strClartiy + "'" + item.Value + "',";
            //    }

            //}
            if (cMin == cMax)
            {
                strClartiy = "'" + CommonData.ClarityTypeList()[cMin].Value + "',";
            }
            else
            {
                for (int i = cMin; i < cMax; i++)
                {
                    if (i <= 7)
                    {
                        strClartiy = strClartiy + "'" + CommonData.ClarityTypeList()[i].Value + "',";

                    }
                }
            }
            if (strClartiy.Contains("SI1"))
            {
                strClartiy = strClartiy + "'SI1-','SI1+',";
            }
            if (strClartiy.Contains("VS2"))
            {
                strClartiy = strClartiy + "'VS2-','VS2+',";
            }
            if (strClartiy.EndsWith(","))
            {
                strClartiy = strClartiy.Substring(0, strClartiy.Length - 1);
            }
            return strClartiy;
        }

        public static string GetPolishRange(string polishMin, string polishMax)
        {
            string strPolish = string.Empty;
            int pMin = Convert.ToInt32(polishMin) - 1;
            int pMax = Convert.ToInt32(polishMax) - 2;
            if (pMin == 0 && pMax == 2)
            {
                foreach (var item in CommonData.PolishTypeList())
                {
                    strPolish = strPolish + "'" + item.Value + "',";
                }

            }
            else if (Convert.ToInt32(pMin) == Convert.ToInt32(pMax))
            {
                strPolish = "'" + CommonData.PolishTypeList()[Convert.ToInt32(pMin)].Value + "',";
            }
            else
            {
                for (int i = Convert.ToInt32(pMin); i <= Convert.ToInt32(pMax); i++)
                {
                    strPolish = strPolish + "'" + CommonData.PolishTypeList()[i].Value + "',";
                }
            }
            if (strPolish.EndsWith(","))
            {
                strPolish = strPolish.Substring(0, strPolish.Length - 1);
            }
            return strPolish;
        }

        public static string GetColorRange(string colorMin, string colorMax)
        {
            string strColor = string.Empty;
            int cMin = Convert.ToInt32(colorMin) - 1;
            int cMax = Convert.ToInt32(colorMax) - 2;
            //if (cMin == 0 && cMax == 2)
            //{
            //    foreach (var item in CommonData.ColorTypeList())
            //    {
            //        strColor = strColor + "'" + item.Value + "',";
            //    }

            //}
            //else 
            if (Convert.ToInt32(cMin) == Convert.ToInt32(cMax))
            {
                strColor = "'" + CommonData.ColorTypeList()[Convert.ToInt32(cMin)].Value + "',";
            }
            else
            {
                for (int i = Convert.ToInt32(cMin); i <= Convert.ToInt32(cMax); i++)
                {
                    strColor = strColor + "'" + CommonData.ColorTypeList()[i].Value + "',";
                }
            }
            if (strColor.EndsWith(","))
            {
                strColor = strColor.Substring(0, strColor.Length - 1);
            }
            return strColor;
        }

        public static string GetSymmetryRange(string symmetryMin, string symmetryMax)
        {
            string strSymmetry = string.Empty;
            int sMin = Convert.ToInt32(symmetryMin) - 1;
            int sMax = Convert.ToInt32(symmetryMax) - 2;
            if (sMin == 0 && sMax == 2)
            {
                foreach (var item in CommonData.SymmetryTypeList())
                {
                    strSymmetry = strSymmetry + "'" + item.Value + "',";
                }

            }
            else if (sMin == sMax)
            {
                strSymmetry = "'" + CommonData.SymmetryTypeList()[Convert.ToInt32(sMin)].Value + "',";
            }
            else
            {
                for (int i = Convert.ToInt32(sMin); i <= Convert.ToInt32(sMax); i++)
                {
                    strSymmetry = strSymmetry + "'" + CommonData.SymmetryTypeList()[i].Value + "',";
                }
            }
            if (strSymmetry.EndsWith(","))
            {
                strSymmetry = strSymmetry.Substring(0, strSymmetry.Length - 1);
            }
            return strSymmetry;
        }

        public static string GetShapeRange(string diamondShape)
        {
            string strDiamond = string.Empty;
            if (String.IsNullOrEmpty(diamondShape))
            {
                foreach (var item in CommonData.ShapeList())
                {
                    strDiamond = strDiamond + "'" + item.Value + "',";
                }
            }
            else
            {
                string tmpDiamond = "";
                foreach (var item in diamondShape.Split(','))
                {
                    if (item.ToUpper().Contains("IN SALON"))
                    {
                    }
                    else if (item.ToUpper().Contains("MULTI SELECT")) { }
                    else
                    {
                        tmpDiamond = tmpDiamond + item + ",";
                    }
                }
                diamondShape = tmpDiamond;
                if (diamondShape.EndsWith(","))
                {
                    diamondShape = diamondShape.TrimEnd(',');
                }
                if (!diamondShape.StartsWith("'"))
                {
                    strDiamond = "'" + diamondShape + "',";
                }
                else
                {
                    strDiamond = diamondShape + ",";
                }

            }


            if (diamondShape == "Cushion")
            {
                strDiamond = strDiamond + "'CUSHION BRILLIANT', 'CUSHION MODIFIED', 'CUSHION MODIFIED BRILLIANT', 'CU', 'CUBR', 'CUMBR', 'CMB'";
            }

            if (diamondShape == "Asscher")
            {
                strDiamond = strDiamond + "'square emerald', 'sq. emerald', 'sqemerald', 'sqem', 'sqe'";
            }
            if (diamondShape == "Radiant")
            {
                strDiamond = strDiamond + "'Square radiant', 'SQR', 'sq'";
            }

            if (strDiamond.EndsWith(","))
            {
                strDiamond = strDiamond.Substring(0, strDiamond.Length - 1);
            }


            return strDiamond;
        }

        public static string GetCurrancyDropDown()
        {
            StringBuilder sbCurrancy = new StringBuilder();


            sbCurrancy.Append("<select name=\"ddlCurrancy\" id=\"ddlCurrancy\">");
            if (UserSessionData.Currency.ToLower() == "aud")
                sbCurrancy.Append("<option selected=\"selected\">AUD</option>");
            else
                sbCurrancy.Append("<option >AUD</option>");

            if (UserSessionData.Currency.ToLower() == "euro")
                sbCurrancy.Append("<option selected=\"selected\">EURO</option>");
            else
                sbCurrancy.Append("<option>EURO</option>");

            if (UserSessionData.Currency.ToLower() == "usd")
                sbCurrancy.Append("<option selected=\"selected\">USD</option>");
            else
                sbCurrancy.Append("<option>USD</option>");
            sbCurrancy.Append("</select>");

            return sbCurrancy.ToString();
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;

        }

        public static string GetShapeForVenus(string shape)
        {

            //CMB
            //EM
            //HT
            //M
            //OV
            //PR
            //PS
            //RA
            //RBC
            //SQE
            //SQR

            switch (shape.ToUpper())
            {
                case "RA":
                    shape = "Radiant";
                    break;
                case "RBC":
                    shape = "Round";
                    break;
                case "HT":
                    shape = "Heart";
                    break;
                case "PS":
                    shape = "Pear";
                    break;
                case "EM":
                    shape = "Emerald";
                    break;
                case "PR":
                    shape = "Princess";
                    break;
                case "OV":
                    shape = "Oval";
                    break;
                case "CMB":
                    shape = "Cushion";
                    break;
                case "SQE":
                    shape = "Square Emerald";
                    break;
                case "SQR":
                    shape = "Square Radiant";
                    break;
                case "M":
                    shape = "Marquise";
                    break;
                default:
                    shape = shape;
                    break;
            }
            return shape;
        }

        public static string GetCutForVenus(string cut)
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

        public static string GetCutForPanache(string cut)
        {
            cut = cut.Replace("+", "");
            switch (cut.ToUpper())
            {
                case "EX":
                    cut = "Excellent";
                    break;
                case "3EX":
                    cut = "Excellent";
                    break;
                case "FR":
                    cut = "Excellent";
                    break;
                case "VG":
                    cut = "Very Good";
                    break;
                case "G":
                    cut = "Good";
                    break;
                case "GD":
                    cut = "Good";
                    break;
                default:
                    cut = cut;
                    break;
            }
            return cut;
        }

        public static string GetCutForHariKrishna(string cut)
        {
            cut = cut.Replace("+", "");



            switch (cut.ToUpper())
            {
                case "EX":
                    cut = "EXCELLENT";
                    break;
                case "EX1":
                    cut = "EXCELLENT";
                    break;
                case "EX2":
                    cut = "EXCELLENT";
                    break;
                case "EX3":
                    cut = "EXCELLENT";
                    break;
                case "EX4":
                    cut = "EXCELLENT";
                    break;
                case "3EX":
                    cut = "EXCELLENT";
                    break;
                case "VG":
                    cut = "VERY GOOD";
                    break;
                case "VG1":
                    cut = "VERY GOOD";
                    break;

                case "VG2":
                    cut = "VERY GOOD";
                    break;
                case "VG3":
                    cut = "VERY GOOD";
                    break;

                case "G":
                    cut = "GOOD";
                    break;
                case "GD":
                    cut = "GOOD";
                    break;
                case "GD1":
                    cut = "GOOD";
                    break;
                case "GD2":
                    cut = "GOOD";
                    break;
                case "FG1":
                    cut = "GOOD";
                    break;
                default:
                    cut = cut;
                    break;
            }
            return cut;
        }


        public static string GetShapeForFineStar(string shape)
        {


            switch (shape.ToUpper())
            {

                case "ROUND":
                    shape = "Round";
                    break;
                case "PS":
                    shape = "PEAR";
                    break;
                case "PC":
                    shape = "PRINCESS";
                    break;
                case "MQ":
                    shape = "MARQUISE";
                    break;
                case "EM":
                    shape = "EMERALD";
                    break;
                case "SE":
                    shape = "ASSCHER";
                    break;
                case "RN":
                    shape = "RADIANT";
                    break;
                case "HT":
                    shape = "HEART";
                    break;
                case "OV":
                    shape = "OVAL";
                    break;
                case "CS":
                    shape = "CUSHION";
                    break;
                case "CM":
                    shape = "CUSHION";
                    break;
                default:
                    shape = shape;
                    break;
            }
            return shape;
        }

        public static string GetCutForFineStar(string cut)
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

        public static string GetPolishForFineStar(string polish)
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
                case "GD":
                    polish = "Good";
                    break;
                default:
                    polish = polish;
                    break;
            }
            return polish;
        }

        public static string GetSymmetryForFineStar(string symmetry)
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
                case "GD":
                    symmetry = "Good";
                    break;
                default:
                    symmetry = symmetry;
                    break;
            }
            return symmetry;
        }

        public static string GetFluorescenceForFineStar(string fluorescence)
        {
            switch (fluorescence.ToUpper())
            {
                case "FNT":
                    fluorescence = "Faint";
                    break;
                case "MED":
                    fluorescence = "Medium";
                    break;
                case "NON":
                    fluorescence = "None";
                    break;
                case "STG":
                    fluorescence = "Strong";
                    break;
                case "VST":
                    fluorescence = "Very Strong";
                    break;
                default:
                    fluorescence = fluorescence;
                    break;
            }
            return fluorescence;
        }

        public static string GetPolishForVenus(string polish)
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

        public static string GetPolishForHariKrishna(string polish)
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
                case "GD":
                    polish = "Good";
                    break;
                default:
                    polish = polish;
                    break;
            }
            return polish;
        }

        public static string GetFluorescenceForVenus(string fluorescence)
        {
            switch (fluorescence.ToUpper())
            {
                case "FL0":
                    fluorescence = "None";
                    break;
                case "FL1":
                    fluorescence = "Faint";
                    break;
                case "FL2":
                    fluorescence = "Medium";
                    break;
                case "FL3":
                    fluorescence = "Strong";
                    break;
                case "FL4":
                    fluorescence = "Very Strong";
                    break;
                default:
                    fluorescence = fluorescence;
                    break;
            }
            return fluorescence;
        }

        public static string GetFluorescenceForHariKrishna(string fluorescence)
        {
            switch (fluorescence.ToUpper())
            {
                case "N":
                    fluorescence = "None";
                    break;
                case "F":
                    fluorescence = "Faint";
                    break;
                case "M":
                    fluorescence = "Medium";
                    break;
                case "ST":
                    fluorescence = "Strong";
                    break;
                case "VS":
                    fluorescence = "Very Strong";
                    break;
                default:
                    fluorescence = fluorescence;
                    break;
            }
            return fluorescence;
        }

        public static string GetSymmetryForVenus(string symmetry)
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

        public static string GetSymmetryForHariKrishna(string symmetry)
        {
            switch (symmetry.ToUpper())
            {
                case "EX":
                    symmetry = "Excellent";
                    break;
                case "FR":
                    symmetry = "Excellent";
                    break;
                case "VG":
                    symmetry = "Very Good";
                    break;
                case "G":
                    symmetry = "Good";
                    break;
                case "GD":
                    symmetry = "Good";
                    break;
                default:
                    symmetry = symmetry;
                    break;
            }
            return symmetry;
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


        public static string GetShapeForYDVash(string shape)
        {


            switch (shape.ToUpper())
            {
                case "BR":
                    shape = "Round ";
                    break;
                case "PR":
                    shape = "Princess";
                    break;
                case "CU":
                    shape = "Cushion";
                    break;
                case "CB":
                    shape = "Cushion";
                    break;
                case "EM":
                    shape = "Emerald";
                    break;
                case "RAD":
                    shape = "Radiant";
                    break;
                case "PS":
                    shape = "Pear";
                    break;
                case "OV":
                    shape = "Oval";
                    break;
                case "HS":
                    shape = "Heart";
                    break;
                case "MQ":
                    shape = "Marquise";
                    break;
                case "AC":
                    shape = "Asscher";
                    break;
                default:
                    shape = shape;
                    break;
            }
            return shape;
        }

        public static string GetPolishForYDVash(string polish)
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

        public static string GetSymmetryForYDVash(string symmetry)
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

        public static string GetCutForYDVash(string cut)
        {
            cut = cut.Replace("+", "");
            switch (cut.ToUpper())
            {
                case "EX":
                    cut = "Excellent";
                    break;
                case "3EX":
                    cut = "Excellent";
                    break;
                case "FR":
                    cut = "Excellent";
                    break;
                case "VG":
                    cut = "Very Good";
                    break;
                case "G":
                    cut = "Good";
                    break;
                case "GD":
                    cut = "Good";
                    break;
                default:
                    cut = cut;
                    break;
            }
            return cut;
        }

        public static string GetFluorescenceForYDVash(string fluorescence)
        {
            switch (fluorescence.ToUpper())
            {
                case "NON":
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
                default:
                    fluorescence = fluorescence;
                    break;
            }
            return fluorescence;
        }

        public static string FormatDecimal(decimal? val)
        {
            string strDecimal = Math.Round(Convert.ToDecimal(val), 2).ToString("G29");
            if (strDecimal.Contains("."))
            {
                if (strDecimal.Split('.')[1].Length == 1)
                {
                    strDecimal = strDecimal + "0";
                }
            }
            else
            {
                strDecimal = strDecimal + ".00";
            }
            return strDecimal;
        }

        public static List<SelectListItem> ListStores()
        {
            List<SelectListItem> listStore = new List<SelectListItem>();
            try
            {
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", 1);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", 1);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = { prmStatus, prmFlag, prmErr };
                DataSet dsConsultant = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetStores", allParams);

                if (dsConsultant != null)
                {
                    if (dsConsultant.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow item in dsConsultant.Tables[0].Rows)
                        {
                            listStore.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["StoreId"].ToString() });
                        }
                    }
                }
            }
            catch
            {

            }
            return listStore;
        }

        public static bool IsDeviceMobile()
        {
            try
            {
                if (HttpContext.Current.Request.Browser.IsMobileDevice)
                {
                    //detect request browser and redirect to mobile website
                    string userAgent = HttpContext.Current.Request.UserAgent.ToLower();
                    bool isMobile = false;
                    if (userAgent.Contains("iphone")) { isMobile = true; }
                    if (userAgent.Contains("ipad;")) { isMobile = true; }
                    if (userAgent.Contains("ipod;")) { isMobile = true; }
                    if (userAgent.Contains("android")) { isMobile = true; }
                    if (userAgent.Contains("blackberry")) { isMobile = true; }
                    if (userAgent.Contains("mini")) { isMobile = true; }

                    if (isMobile)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }

        public static string GetRatio(string measurement)
        {
            string ratio = "";
            try
            {
                if (!String.IsNullOrEmpty(measurement))
                {
                    if (measurement.Contains("-"))
                    {
                        measurement = measurement.Replace("-", "x");
                    }

                    string[] arrayMeasurement = measurement.ToLower().Split('x');
                    ratio = (GetLargestValue(arrayMeasurement) / GetSecondLargestValue(arrayMeasurement)).ToString();

                    ratio = Math.Round(Convert.ToDecimal(ratio), 2).ToString("G29");

                }
            }
            catch { }
            return ratio;
        }

        public static decimal GetSecondLargestValue(string[] array)
        {
            try
            {
                decimal largest = decimal.MinValue;
                decimal second = decimal.MinValue;
                foreach (string i in array)
                {
                    if (Convert.ToDecimal(i) > largest)
                    {
                        second = largest;
                        largest = Convert.ToDecimal(i);
                    }
                    else if (Convert.ToDecimal(i) > second)
                        second = Convert.ToDecimal(i);
                }

                return second;
            }
            catch
            {
                return 0;
            }

        }

        public static decimal GetLargestValue(string[] array)
        {
            try
            {
                decimal? maxVal = null;
                int index = -1;
                for (int i = 0; i < array.Length; i++)
                {
                    decimal thisNum = Convert.ToDecimal(array[i]);
                    if (!maxVal.HasValue || thisNum > maxVal.Value)
                    {
                        maxVal = thisNum;
                        index = i;
                    }
                }

                return Convert.ToDecimal(maxVal);
            }
            catch
            {

                return 0;
            }

        }

        public static string GetDiamondType(string type)
        {
            if (type.ToLower() == "1" || type.ToLower() == "11" || type.ToLower() == "12")//rapnet
            {
                type = "R";
            }

            else if (type.ToLower() == "2")//jbbros
            {
                type = "J";
            }
            else if (type.ToLower() == "3")//venus
            {
                type = "V";
            }
            else if (type.ToLower() == "4")//canturi
            {
                type = "C";
            }
            else if (type.ToLower() == "5")//YDVASH 
            {
                type = "YD";
            }
            else if (type.ToLower() == "6")//canturi
            {
                type = "H";
            }
            else if (type.ToLower() == "7")//FineStar
            {
                type = "F";
            }
            else if (type.ToLower() == "8")//CDINESH
            {
                type = "CD";
            }
            //Replaced by Prashant on 20 Nov 2020
            //else if (type.ToLower() == "9")//KAPUGEMS
            //{
            //    type = "KG";
            //}
            else if (type.ToLower() == "9")//GlowStar
            {
                type = "GS";
            }
            else if (type.ToLower() == "10")//SUNRISE
            {
                type = "SD";
            }
            else if (type.ToLower() == "13")//KIRANGEMS
            {
                type = "KD";
            }
            else if (type.ToLower() == "14")//Dharam
            {
                type = "DH";
            }
            else
            {
                type = "";
            }
            return type;
        }


        public static string GetCushionShape(string shape, string ratio)
        {

            string[] strShapes = { "CUSHION", "PRINCESS", "RADIANT" };
            string strCushionShape = "CUSHION";

            if (strShapes.Contains(shape.ToUpper()))
            {
                //string ratio = "0";
                //if (!String.IsNullOrEmpty(item["Ratio"].ToString()))
                //{
                //    ratio = item["Ratio"].ToString();
                //}
                //else
                //{
                //    ratio = CommonData.GetRatio(item["Measurements"].ToString().Replace("*", "x").Replace("-", "x"));
                //}
                if (Convert.ToDecimal(ratio) >= 1 && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.059))
                {
                    strCushionShape = "SQUARE";
                }
                else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.060) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.088))
                {
                    strCushionShape = "SQUARISH";
                }
                else if (Convert.ToDecimal(ratio) >= Convert.ToDecimal(1.089) && Convert.ToDecimal(ratio) <= Convert.ToDecimal(1.900))
                {
                    strCushionShape = "ELONGATED";
                }


            }
            return strCushionShape;
        }

    }
}
