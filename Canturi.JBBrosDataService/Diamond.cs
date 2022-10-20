using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Canturi.JBBrosDataService
{
    public class Diamond
    {
        int DiamondCount;
        #region Private Variable
        string _constring = string.Empty;

        #endregion
        public string Color { get; set; }
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

        public List<JBBrosDiamondModel> ConsumeExternalAPI(int page)
        {

            using (WebClient client = new WebClient())
            {
                List<JBBrosDiamondModel> listJBBrosDiamond = new List<JBBrosDiamondModel>();
                StringBuilder sb = new StringBuilder();
                sb.Append("https://websvr.jbbros.com/jbapi.aspx?UserId=" + ConfigurationSettings.AppSettings["APIUserId"].ToString());
                sb.Append("&APIKey=" + ConfigurationSettings.AppSettings["APIKey"].ToString());
                sb.Append("&Action=S&Shape=" + ConfigurationSettings.AppSettings["Shape"].ToString());
                sb.Append("&CaratFrom=" + ConfigurationSettings.AppSettings["CaratFrom"].ToString());
                sb.Append("&CaratTo=" + ConfigurationSettings.AppSettings["CaratTo"].ToString());
                sb.Append("&Color=" + Color);
                sb.Append("&Purity=" + ConfigurationSettings.AppSettings["Purity"].ToString());
                sb.Append("&Lab=" + ConfigurationSettings.AppSettings["Lab"].ToString() + "&PG=" + page);

                client.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                "(compatible; MSIE 6.0; Windows NT 5.1; " +
                ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                //string reqStr = "http://websvr.jbbros.com/jbapi.aspx?UserId=louise&APIKey=BCD52CFC-560D-4543-AED7-C1E990B29078&Action=S&Shape=RD&CaratFrom=1.01&CaratTo=1.09&Color=H&Purity=SI1&Lab=GIA&PG=1";

                string data = client.DownloadString(sb.ToString());
                data = data.Replace("}][{", "},{");
                //string data = client.DownloadString(reqStr);
                return listJBBrosDiamond = JsonConvert.DeserializeObject<List<JBBrosDiamondModel>>(data);
            }
        }

        public bool IsNotValid(string cut, string luster)
        {
            string[] invalidLuster = { "M1", "M2", "M3" };
            if (invalidLuster.Contains(luster.ToUpper()))
            {
                return true;
            }

            cut = cut.ToUpper();
            if (cut.Contains("IDEAL") || cut.Contains("FAIR") || cut.Contains("FAIR +") || cut.Contains("POOR"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbool)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            if (IsFontbool == true)
            {
                range.Font.Bold = IsFontbool;
            }
        }

        public bool WriteDataTableToExcel(System.Data.DataTable dataTable, string worksheetName, string saveAsLocation, string ReporType)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;

            try
            {
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // for making Excel visible
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = worksheetName;


                excelSheet.Cells[1, 1] = ReporType;
                excelSheet.Cells[1, 2] = "Date : " + DateTime.Now.ToShortDateString();

                // loop through each row and add values to our sheet
                int rowcount = 2;

                foreach (DataRow datarow in dataTable.Rows)
                {
                    rowcount += 1;
                    for (int i = 1; i <= dataTable.Columns.Count; i++)
                    {
                        // on the first iteration we add the column headers
                        if (rowcount == 3)
                        {
                            excelSheet.Cells[2, i] = dataTable.Columns[i - 1].ColumnName;
                            excelSheet.Cells.Font.Color = System.Drawing.Color.Black;

                        }

                        excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();

                        //for alternate rows
                        if (rowcount > 3)
                        {
                            if (i == dataTable.Columns.Count)
                            {
                                if (rowcount % 2 == 0)
                                {
                                    excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                                    FormattingExcelCells(excelCellrange, "#CCCCFF", System.Drawing.Color.Black, false);
                                }

                            }
                        }

                    }

                }

                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                excelCellrange.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;


                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[2, dataTable.Columns.Count]];
                FormattingExcelCells(excelCellrange, "#000099", System.Drawing.Color.White, true);


                //now save the workbook and exit Excel


                excelworkBook.SaveAs(saveAsLocation); ;
                excelworkBook.Close();
                excel.Quit();
                return true;
            }
            catch (Exception ex)
            {
                
                return false;
            }
            finally
            {
                excelSheet = null;
                excelCellrange = null;
                excelworkBook = null;
            }

        }
        

        private DataTable ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
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


            //WriteDataTableToExcel(table, "Person Details", "E:\\JbBros\\" + DateTime.Now.Ticks.ToString() + ".xlsx", "Details");

            
            return table;
        }


        public void JBBrosDiamond()
        {

            string[] arrColor = ConfigurationSettings.AppSettings["Color"].ToString().Split(',');
            Color = arrColor[0];
            int colorCount = 0;
            int i = 1;
            while (i > 0)
            {
                try
                {
                    List<JBBrosDiamondModel> jbBrosDiamondList = ConsumeExternalAPI(i);
                
                    //ConvertToDatatable(jbBrosDiamondList);


                    if (jbBrosDiamondList != null && jbBrosDiamondList.Count() > 0)
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
                        MainDiamondTable.Columns.Add("VideoURL");

                        foreach (JBBrosDiamondModel model in jbBrosDiamondList)
                        {
                            if (model.RefNo == "917406827")
                            {
                                string stop = "";
                            }

                            //string[] arr = { "917109221", "917007949", "317208337", "717306762", "617709733" };
                            //if (arr.Contains(model.RefNo.ToString()))
                            //{
                            //    IEnumerable<JBBrosDiamondModel> data = model as IEnumerable<JBBrosDiamondModel>;
                            //    dtFiletr = ConvertToDatatable(data.ToList());


                            //    string Availability = model.Availability;
                            //}
                            //else
                            //{
                            //    if (model.Availability.ToUpper() != "J")
                            //    {
                            //        string notok = "";
                            //    }
                            //}

                            if (model.Availability.ToUpper() != "J")
                            {
                                if (!String.IsNullOrEmpty(model.Cut))
                                {
                                    if (!IsNotValid(model.Cut,model.Luster))
                                    {
                                        DataRow row = MainDiamondTable.NewRow();
                                        row["DiamondID"] = model.Srno;
                                        row["VendorStockNumber"] = model.RefNo.ToString();
                                        row["ShapeTitle"] = getShape().Where(x => x.Key == model.Shape).FirstOrDefault().Value;
                                        row["Weight"] = model.Carat;
                                        row["ColorTitle"] = model.Color.Replace("+", "").Replace("-", "");
                                        row["ClarityTitle"] = model.Purity.Replace("+", "").Replace("-", "");
                                        row["CutLongTitle"] = getCUT().Where(x => x.Key == model.Cut.Replace("+", "").Replace("-", "")).FirstOrDefault().Value;
                                        row["PolishTitle"] = getPolish().Where(x => x.Key == model.Polish).FirstOrDefault().Value;
                                        row["SymmetryTitle"] = getSymmetry().Where(x => x.Key == model.Symmetry).FirstOrDefault().Value;
                                        row["FluorescenceIntensityTitle"] = getFloroscence().Where(x => x.Key == model.FL).FirstOrDefault().Value;
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
                                        row["CuletConditionTitle"] = "";
                                        row["EyeClean"] = model.EyeClean;
                                        row["VideoURL"] = model.Stone360Link;
                                        MainDiamondTable.Rows.Add(row);
                                    }
                                }
                            }

                        }
                        int PageNumber = 1;
                        if (colorCount == 0)
                        {
                            PageNumber = i;
                        }
                        else
                        {
                            PageNumber = 2;
                        }
                        SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                        SqlParameter prmDiamond = SqlHelper.CreateParameter("@DiamondJbBrosTable", MainDiamondTable);
                        SqlParameter prmPage = SqlHelper.CreateParameter("@Page", PageNumber);
                        SqlParameter[] AllParams = { prmErr, prmDiamond, prmPage };
                        SqlHelper.ExecuteNonQuery(_constring, CommandType.StoredProcedure, "Usp_AddUpdJBBrosDiamond", AllParams);
                        if (prmErr.Value != null)
                        {
                            if (prmErr.Value.ToString() == "0")
                            {
                                //StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                                //str.WriteLine("Successfully Inserting Diamond Procedure - " + DateTime.Now.ToString());
                                //str.Close();
                                //str.Dispose();
                                i++;
                            }
                            else
                            {
                                StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                                str.WriteLine("Error on Inserting Diamond Procedure - " + DateTime.Now.ToString());
                                str.Close();
                                str.Dispose();
                            }
                        }
                        else
                        {
                            StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                            str.WriteLine("Error on Inserting Diamond Procedure - " + DateTime.Now.ToString());
                            str.Close();
                            str.Dispose();
                        }
                    }
                    else
                    {
                        if (colorCount == arrColor.Length)
                        {
                            StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                            str.WriteLine("Successfully Inserting Diamond Procedure - " + DateTime.Now.ToString());
                            str.Close();
                            str.Dispose();
                            break;
                        }
                        else
                        {
                            colorCount++;
                            if (colorCount < arrColor.Length)  // Added this check on 04 July 2021 because it was throwing error on arrColor[20] coz its range is 0-19
                            {
                                Color = arrColor[colorCount];
                                i = 1;
                            }                            
                        }
                    }

                }
                catch (Exception ex)
                {
                    StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                    str.WriteLine("Error on Function Inserting Diamond - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.Source.ToString());
                    str.Close();
                    str.Dispose();
                }
            }
        }

        public static void LogError(string error)
        {
            StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
            str.WriteLine(error);
            str.Close();
            str.Dispose();
        }



        public List<KeyValuePair<string, string>> getShape()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("CUMBR", "CUSHION MODIFIED BRILLIANT"));
            list.Add(new KeyValuePair<string, string>("EM", "EMERALD"));
            list.Add(new KeyValuePair<string, string>("FS", "FANCY SHAPE"));
            list.Add(new KeyValuePair<string, string>("HRT", "HEART"));
            list.Add(new KeyValuePair<string, string>("MQ", "MARQUISE"));
            list.Add(new KeyValuePair<string, string>("OV", "OVAL"));
            list.Add(new KeyValuePair<string, string>("PS", "PEAR"));
            list.Add(new KeyValuePair<string, string>("PR", "PRINCESS"));
            list.Add(new KeyValuePair<string, string>("RT", "RADIANT"));
            list.Add(new KeyValuePair<string, string>("RD", "ROUND"));
            list.Add(new KeyValuePair<string, string>("SQEM", "SQUARE EMERALD"));
            list.Add(new KeyValuePair<string, string>("SQRT", "SQUARE RADIANT"));
            list.Add(new KeyValuePair<string, string>("TR", "TRILLIANT"));
            list.Add(new KeyValuePair<string, string>("SQBR", "SQUARE BRILLIANT"));
            list.Add(new KeyValuePair<string, string>("RECBR", "RECTANGLE BRILLIANT"));
            list.Add(new KeyValuePair<string, string>("TA", "TRIANGLE"));
            list.Add(new KeyValuePair<string, string>("ST", "STEP-TRIANGLE CUT"));
            list.Add(new KeyValuePair<string, string>("CUBR", "CUSHION BRILLIANT"));
            list.Add(new KeyValuePair<string, string>("CU", "CUSHION"));
            return list;
        }


        public List<KeyValuePair<string, string>> getCUT()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("ID", "IDEAL"));
            list.Add(new KeyValuePair<string, string>("EX+", "EXCELLENT"));
            list.Add(new KeyValuePair<string, string>("EX", "EXCELLENT"));
            list.Add(new KeyValuePair<string, string>("VG+", "VERY GOOD"));
            list.Add(new KeyValuePair<string, string>("VG", "VERY GOOD"));
            list.Add(new KeyValuePair<string, string>("GD", "GOOD"));
            list.Add(new KeyValuePair<string, string>("GD+", "GOOD"));
            list.Add(new KeyValuePair<string, string>("FR+", "FAIR"));
            list.Add(new KeyValuePair<string, string>("FR", "FAIR"));
            list.Add(new KeyValuePair<string, string>("PR", "POOR"));
            return list;
        }

        public List<KeyValuePair<string, string>> getPolish()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("ID", "IDEAL"));
            list.Add(new KeyValuePair<string, string>("EX", "EXCELLENT"));
            list.Add(new KeyValuePair<string, string>("VG", "VERY GOOD"));
            list.Add(new KeyValuePair<string, string>("GD", "GOOD"));
            list.Add(new KeyValuePair<string, string>("FR", "FAIR"));
            list.Add(new KeyValuePair<string, string>("PR", "POOR"));
            return list;
        }
        public List<KeyValuePair<string, string>> getSymmetry()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("ID", "IDEAL"));
            list.Add(new KeyValuePair<string, string>("EX", "EXCELLENT"));
            list.Add(new KeyValuePair<string, string>("VG", "VERY GOOD"));
            list.Add(new KeyValuePair<string, string>("GD", "GOOD"));
            list.Add(new KeyValuePair<string, string>("FR", "FAIR"));
            list.Add(new KeyValuePair<string, string>("PR", "POOR"));
            return list;
        }

        public List<KeyValuePair<string, string>> getFloroscence()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("N", "NONE"));
            list.Add(new KeyValuePair<string, string>("F", "FAINT"));
            list.Add(new KeyValuePair<string, string>("M", "MEDIUM"));
            list.Add(new KeyValuePair<string, string>("S", "STRONG"));
            list.Add(new KeyValuePair<string, string>("VS", "VERY STRONG"));
            return list;
        }
    }

    public class JBBrosDiamondModel
    {
        public int Srno { get; set; }
        public string RefNo { get; set; }
        public string Shape { get; set; }
        public string Availability { get; set; }
        public double Carat { get; set; }
        public double Price { get; set; }
        public double RapOff { get; set; }
        public double RapaportPrice { get; set; }
        public string Lab { get; set; }
        public string CertNo { get; set; }
        public string Color { get; set; }
        public string CS { get; set; }
        public string Purity { get; set; }
        public string Cut { get; set; }
        public string Intensity { get; set; }
        public string Overtone { get; set; }
        public string Symmetry { get; set; }
        public string Polish { get; set; }
        public string FL { get; set; }
        public string Luster { get; set; }
        public string TI { get; set; }
        public string BIS { get; set; }
        public string BIC { get; set; }
        public string OPPV { get; set; }
        public string OPCR { get; set; }
        public string OPTA { get; set; }
        public string EyeClean { get; set; }
        public string HA { get; set; }
        public string Culate { get; set; }
        public string EFCR { get; set; }
        public string EFPV { get; set; }
        public string TOI { get; set; }
        public string GirdleCondition { get; set; }
        public string FluoresentColor { get; set; }
        public string BowTie { get; set; }
        public double TotalDepthPer { get; set; }
        public double TabelPer { get; set; }
        public double Girdle { get; set; }
        public double CrownAngle { get; set; }
        public double CrownHeight { get; set; }
        public double PavalionHeight { get; set; }
        public double PavalionAngle { get; set; }
        public double TotalDept_mm { get; set; }
        public double TotalDepth_Per { get; set; }
        public double DimMinAndWidth { get; set; }
        public double DimMaxAndWidth { get; set; }
        public double Ratio { get; set; }
        public string KeytoSymbols { get; set; }
        public string ReportComments { get; set; }
        public string CertiLink { get; set; }
        public string StoneImageLink { get; set; }
        public string Stone360Link { get; set; }

    }
}
