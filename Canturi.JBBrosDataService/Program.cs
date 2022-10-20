using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.JBBrosDataService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new JBBrosService() 
            };
            ServiceBase.Run(ServicesToRun);
            

            //Diamond diamond = new Diamond();
            //diamond.JBBrosDiamond();

            //DataTable dtAllFiles = new DataTable();

            //foreach (var file in Directory.GetFiles("E:\\JbBros_data\\", "*.xlsx"))
            //{
            //    dtAllFiles.Merge(ConvertExcelToDataTable(file));
            //}

            //var result = from r in dtAllFiles.AsEnumerable()
            //             where r.Field<string>("Details") != "Srno" 
            //             select r;
            //DataTable dtResult = result.CopyToDataTable();  





            //Diamond objDiamond = new Diamond();
            //objDiamond.WriteDataTableToExcel(dtResult, "Person Details", "E:\\JbBros\\Final" + DateTime.Now.Ticks.ToString() + ".xlsx", "Details");
        }


        public static DataTable ConvertExcelToDataTable(string FileName)
        {
            DataTable dtResult = null;
            int totalSheet = 0; //No of sheets on excel file  
            using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];
                objConn.Close();
                return dtResult; //Returning Dattable  
            }
        }
    }
}
