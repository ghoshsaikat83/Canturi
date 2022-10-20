using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
//using Excel = Microsoft.Office.Interop.Excel;


namespace Canturi.CDINESH
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            //CDINESH.cdinesh.com.StockDwnlSoapClient obj = new cdinesh.com.StockDwnlSoapClient();
            ////com.cdinesh.www.StockDwnl obj = new com.cdinesh.www.StockDwnl();
            //DataSet ds = new DataSet();
            //ds = obj.GetFullStock("canturi");

            //ExportDataSetToExcel(ds);



            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new CDINESH() 
            };
            ServiceBase.Run(ServicesToRun);

            //Diamond diamond = new Diamond();
            //diamond.CdinishDiamond();
        }

        //private static void ExportDataSetToExcel(DataSet ds)
        //{
        //    //Creae an Excel application instance
        //    Excel.Application excelApp = new Excel.Application();

        //    //Create an Excel workbook instance and open it from the predefined location
        //    Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(@"E:\Org.xlsx");

        //    foreach (DataTable table in ds.Tables)
        //    {
        //        //Add a new worksheet to workbook with the Datatable name
        //        Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
        //        excelWorkSheet.Name = table.TableName;

        //        for (int i = 1; i < table.Columns.Count + 1; i++)
        //        {
        //            excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
        //        }

        //        for (int j = 0; j < table.Rows.Count; j++)
        //        {
        //            for (int k = 0; k < table.Columns.Count; k++)
        //            {
        //                excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
        //            }
        //        }
        //    }

        //    excelWorkBook.Save();
        //    excelWorkBook.Close();
        //    excelApp.Quit();

        //}
    }
}
