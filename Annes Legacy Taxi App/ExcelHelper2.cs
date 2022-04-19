using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Annes_Legacy_Taxi_App
{
    class ExcelHelper2
    {
        private DataTable ReadSheet(string fileName, string sheetName)
        {
            if (fileName.Length < 4)
                throw new ArgumentException("The specified file name is too short: " + fileName);
            if (!Path.GetExtension(fileName).Equals(".xlsx"))
                throw new ArgumentOutOfRangeException("The specified file is not an excel file: " + fileName);
            if (!File.Exists(fileName))
                throw new FileNotFoundException(fileName + " cannot be found.");

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + fileName + "';Extended Properties='Excel 12.0;HDR=Yes';";
            OleDbConnection oleDbConnection = new OleDbConnection(connectionString);
            oleDbConnection.Open();
            OleDbCommand oleDbCommand = new OleDbCommand("SELECT * FROM [Sheet1$]", oleDbConnection);
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
            oleDbDataAdapter.SelectCommand = oleDbCommand;
            DataSet dataset = new DataSet();
            oleDbDataAdapter.Fill(dataset);
            oleDbConnection.Close();
            DataTable dataTable = dataset.Tables[0];

            return dataTable;
        }

        public static void SetRunningSheetYear(string runningSheet, int startYear)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = false;

                try
                {
                    // Open an existing workbook
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(runningSheet, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

                    // Get all sheets in the workbook
                    Excel.Sheets excelSheets = excelWorkbook.Worksheets;

                    // Set sheet names to correct Month and Year.
                    for (int i = 1; i <= 6; i++)
                    {
                        Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets[i];
                        excelWorksheet.Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 6) + " " + startYear.ToString();
                    }
                    for (int i = 7; i <= 12; i++)
                    {
                        Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets[i];
                        excelWorksheet.Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i - 6) + " " + (startYear + 1).ToString();
                    }

                    // Close (saving changes)
                    excelWorkbook.Close(true);
                    excelWorkbook = null;
                    excelSheets = null;
                }
                catch (Exception e)
                {

                }
                finally
                {
                    excelApp.Application.Quit();
                    excelApp = null;
                    GC.Collect();
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
