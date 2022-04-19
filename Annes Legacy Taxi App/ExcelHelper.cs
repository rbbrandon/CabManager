using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.Windows.Forms;

namespace Robs_Taxi_Manager
{
    public static class ExcelHelper
    {
        public static void SetRunningSheetYear(string runningSheet, int startYear)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;

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
            excelApp.Application.Quit();
            excelApp = null;
            GC.Collect();
        }
        
        public static void EditRunningSheet(string runningSheet, int month)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;

            // Open an existing workbook
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(runningSheet, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

            // Get sheet in the workbook
            Excel.Worksheet excelWorksheet = excelWorkbook.Sheets[month];

            // Activate Worksheet
            ((Excel._Worksheet)excelWorksheet).Activate();

            // Activate cell after the last fares collected number.
            ((Excel.Range)excelWorksheet.Cells[GetLastFaresRow(excelWorksheet, "C") + 1, 3]).Activate();
        }

        public static void SetupExpenses(string expensesSheet, int startYear, int startMonth)
        {
            //excelWorksheet.Cells[row, column].Value = 
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;

            // Open an existing workbook
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(expensesSheet, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

            // Get all sheets in the workbook
            Excel.Worksheet excelWorksheet = excelWorkbook.Worksheets[1];

            excelWorksheet.Cells[2, 1].Value = String.Format("{0:00}/01/{1:0000}", startMonth, startYear);

            // Close (saving changes)
            excelWorkbook.Close(true);
            excelWorkbook = null;
            excelApp.Application.Quit();
            excelApp = null;
            GC.Collect();
        }

        public static void EditExpenses(string expensesSheet)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;

            // Open an existing workbook
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(expensesSheet, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

            // Get sheet in the workbook
            Excel.Worksheet excelWorksheet = excelWorkbook.Sheets[1];

            // Activate Worksheet
            ((Excel._Worksheet)excelWorksheet).Activate();

            // Activate cell after the last fares collected number.
            ((Excel.Range)excelWorksheet.Cells[GetLastUsedRow(excelWorksheet) + 1, 1]).Activate();
        }

        public static bool CreateDriverInvoice(int invoiceNumber, string ownerName, string driverName, int month, int year)
        {
            bool incrementInvoiceNumber = true;
            decimal driversShareTotal = 0.0M;
            string invoiceDate = String.Format(Constants.DATE_FORMAT, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            string csvData;
            List<DriverInfo> driverInfoList = new List<DriverInfo>();
            int startYear = month < 7 ? year - 1 : year;
            int sheetNumber = month < 7 ? month + 6 : month - 6;

            // Calculate invoice file name. 
            string invoiceFileName = String.Format(Constants.INVOICE_FILENAME_FORMAT,
                invoiceNumber, driverName, year, month);
            string invoiceFile = Path.Combine(Constants.INVOICE_PATH, invoiceFileName);

            // Check if an invoice for the specified month and driver already exists.
            string searchPattern = String.Format(Constants.INVOICE_FILENAME_SEARCH_PATTERN, driverName, year, month);
            string[] fileList = Directory.GetFiles(Constants.INVOICE_PATH, searchPattern, SearchOption.TopDirectoryOnly);
            if (fileList.GetLength(0) > 0)
            {
                // Invoice for this person already exists
                if (MessageBox.Show(String.Format("An invoice for \"{0}\" already exists for the month of {1:00}/{2:0000}.\nDo you want to re-generate it?\n\n(Note: This will recalculate the takings, and overwrite the existing file using the original invoice number)",
                    driverName, month, year), "Overwrite file?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Set invoiceFile to the existing file (to overwrite).
                    invoiceFile = fileList[0];

                    // Get the initial number from the file name (i.e. the invoice number).
                    invoiceNumber = Convert.ToInt32(new string(Path.GetFileName(invoiceFile).TakeWhile(Char.IsDigit).ToArray()));

                    // Don't increment the invoice number (as we are reusing an old one).
                    incrementInvoiceNumber = false;
                }
                else
                {
                    // Invoice exists, and user doesn't want to re-generate it
                    // Exit without incrementing invoice number.
                    return false;
                }
            }

            if (!File.Exists(Constants.DATA_SAVED_FILE))
            {
                MessageBox.Show("Cannot load owner information. Please re-run this program.", "Owner Information Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            IniFile iniFile = new IniFile();
            iniFile.Load(Constants.DATA_SAVED_FILE);

            csvData = Constants.CSV_HEADER + String.Format("{0:00000},{1},{10},{2},{3},{4},{5},{6},{7},{8:00}/{9:0000},<<DRIVERTOTAL>>,<<GST>>",
                invoiceNumber, driverName, iniFile.GetKeyValue("Owner", "ABN"), iniFile.GetKeyValue("Owner", "StreetAddress"),
                iniFile.GetKeyValue("Owner", "City"), iniFile.GetKeyValue("Owner", "State"), iniFile.GetKeyValue("Owner", "PostCode"),
                invoiceDate, month, year, iniFile.GetKeyValue("Owner", "BusinessName"));

            // Run Excel (hidden)
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;

            // If the driver is also the owner, they will need the owner's share in every workbook.
            // If so, add all drivers' workbooks to the list.
            if (driverName.ToLower() == ownerName.ToLower())
            {
                string ownerRunningSheet = Path.Combine(Constants.FILE_PATH, String.Format(Constants.RUNNING_SHEET_FILENAME_FORMAT, driverName, startYear, startYear + 1));
                if (File.Exists(ownerRunningSheet))
                {
                    //Excel.Workbook ownerWorkbook = excelApp.Workbooks.Open(ownerRunningSheet, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

                    //driverInfoList.Add(new DriverInfo(driverName, ownerRunningSheet, ownerWorkbook));

                    List<string> drivers = new List<string>();
                    drivers.Add(ownerName);

                    // Add all drivers.
                    for (int driverNum = 0; ; driverNum++)
                    {
                        string dname = iniFile.GetKeyValue("Driver" + driverNum, "Name");

                        if (dname == string.Empty)
                            break;

                        drivers.Add(dname);
                    }

                    foreach (string driver in drivers)
                    {
                        string runningSheet = Path.Combine(Constants.FILE_PATH, String.Format(Constants.RUNNING_SHEET_FILENAME_FORMAT, driver, startYear, startYear + 1));

                        if (File.Exists(runningSheet))
                        {
                            Excel.Workbook workbook = excelApp.Workbooks.Open(runningSheet, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

                            driverInfoList.Add(new DriverInfo(driver, runningSheet, workbook));
                        }
                    }
                }
            }
            else
            {
                string runningSheet = Path.Combine(Constants.FILE_PATH, String.Format(Constants.RUNNING_SHEET_FILENAME_FORMAT, driverName, startYear, startYear + 1));
                if (File.Exists(runningSheet))
                {
                    Excel.Workbook workbook = excelApp.Workbooks.Open(runningSheet, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

                    driverInfoList.Add(new DriverInfo(driverName, runningSheet, workbook));
                }
            }

            if (driverInfoList.Count > 0)
            {
                decimal tripLevies = 0.0M;

                for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
                {
                    int kms = 0;
                    int trips = 0;
                    decimal fares = 0.0M;
                    decimal driversShare = 0.0M;
                    string shift = "";
                    DateTime date = new DateTime(year, month, day);

                    foreach (DriverInfo driver in driverInfoList)
                    {
                        Excel._Worksheet worksheet = ((Excel._Worksheet)driver.Workbook.Worksheets[sheetNumber]);
                        //worksheet.Activate();
                        int row = day + 2;

                        // Add day's fare for this driver to this day's total fare.
                        dynamic fareCell = worksheet.get_Range("C" + row).Value;
                        decimal fare = 0.0M;

                        if (Decimal.TryParse(Convert.ToString(fareCell), out fare))
                        {
                            fares += fare;

                            dynamic tripLevyCell = worksheet.get_Range("R" + row).Value;
                            decimal tripLevy = 0.0M;
                            if (Decimal.TryParse(Convert.ToString(tripLevyCell), out tripLevy))
                                tripLevies += tripLevy;

                            dynamic kmCell = worksheet.get_Range("P" + row).Value;
                            int km = 0;
                            if (Int32.TryParse(Convert.ToString(kmCell), out km))
                                kms += km;

                            dynamic tripCell = worksheet.get_Range("Q" + row).Value;
                            int trip = 0;
                            if (Int32.TryParse(Convert.ToString(tripCell), out trip))
                                trips += trip;

                            dynamic shiftCell = worksheet.get_Range("O" + row).Value;

                            // If driver is the owner
                            if (driverName.ToLower() == ownerName.ToLower())
                            {
                                // If current workbook belongs to the owner.
                                if (driver.Name.ToLower() == ownerName.ToLower())
                                {
                                    // Driver gets full fare amount (100% - trip levy)
                                    driversShare += fare - tripLevy;

                                    // Driver's shift is set
                                    shift = Convert.ToString(shiftCell);
                                }
                                else
                                {
                                    // Workbook doesn't belong to the owner, so only get owner's share.
                                    dynamic ownersShareCell = worksheet.get_Range("G" + row).Value;
                                    decimal oShare = 0.0M;
                                    if (Decimal.TryParse(Convert.ToString(ownersShareCell), out oShare))
                                        driversShare += oShare;
                                }
                            }
                            else
                            {
                                // Driver is not the owner, get the driver's share.
                                dynamic driversShareCell = worksheet.get_Range("J" + row).Value;
                                decimal dShare = 0.0M;
                                if (Decimal.TryParse(Convert.ToString(driversShareCell), out dShare))
                                    driversShare += dShare;

                                // Get driver's shift
                                shift = Convert.ToString(shiftCell);
                            }
                        }
                    }

                    driversShareTotal += driversShare;
                    csvData += String.Format("," + Constants.DATE_FORMAT + ",{3},{4},{5},{6},${7:0.00},${8:0.00}",
                        date.Day, date.Month, date.Year, date.DayOfWeek.ToString().Substring(0, 3), shift, kms, trips, fares, driversShare);
                }

                // Add blank days into the CSV (if any)
                for (int day = DateTime.DaysInMonth(year, month) + 1; day <= 31; day++)
                {
                    csvData += ",,,,,,,";
                }

                // Add trip levy information
                csvData += "," + tripLevies.ToString("0.00");

                // Close all workbooks.
                foreach (DriverInfo driver in driverInfoList)
                {
                    driver.Workbook.Close(false);
                    driver.Workbook = null;
                }

                csvData = csvData.Replace("<<DRIVERTOTAL>>", String.Format("${0:0.00}", driversShareTotal));
                csvData = csvData.Replace("<<GST>>", String.Format("${0:0.00}", driversShareTotal / 11));

                if (driversShareTotal > 0)
                {
                    File.WriteAllText(Constants.DATA_TEMP_FILE, csvData);

                    // <<Merge CSV into Word now>>
                    bool mergeResult = WordHelper.MergeDriverInvoice(invoiceFile, Constants.INVOICE_TEMPLATE_FILE);

                    if (incrementInvoiceNumber)
                        incrementInvoiceNumber = mergeResult;
                }
                else
                {
                    // Driver didn't earn anything this month. Don't generate an invoice nor increment the invoice number.
                    incrementInvoiceNumber = false;
                }
            }
            else
            {
                // Don't increment invoice number as generation wasn't successful.
                incrementInvoiceNumber = false;
            }

            excelApp.Application.Quit();
            excelApp = null;
            GC.Collect();

            return incrementInvoiceNumber;
        }

        public static bool CreateExpensesInvoice(int invoiceNumber, int quarter, int year, string ownerName)
        {
            bool incrementInvoiceNumber = true;
            string invoiceDate = String.Format(Constants.DATE_FORMAT, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            string csvData;
            int startYear = quarter > 2 ? year - 1 : year;

            // Calculate invoice file name. 
            string invoiceFileName = String.Format(Constants.EXPENSES_INVOICE_FILENAME_FORMAT,
                invoiceNumber, startYear, startYear + 1, quarter);
            string invoiceFile = Path.Combine(Constants.INVOICE_PATH, invoiceFileName);

            // Check if an invoice for the specified month and driver already exists.
            string searchPattern = String.Format(Constants.EXPENSES_INVOICE_FILENAME_SEARCH_PATTERN, startYear, startYear + 1, quarter);
            string[] fileList = Directory.GetFiles(Constants.INVOICE_PATH, searchPattern, SearchOption.TopDirectoryOnly);
            if (fileList.GetLength(0) > 0)
            {
                // Invoice for this person already exists
                if (MessageBox.Show(String.Format("An expenses invoice for Q{0} of the {1:0000}-{2:0000} financial year already exists.\nDo you want to re-generate it?\n\n(Note: This will recalculate all values, and overwrite the existing file using the original invoice number)",
                    quarter, startYear, startYear + 1), "Overwrite file?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Set invoiceFile to the existing file (to overwrite).
                    invoiceFile = fileList[0];

                    // Get the initial number from the file name (i.e. the invoice number).
                    invoiceNumber = Convert.ToInt32(new string(Path.GetFileName(invoiceFile).TakeWhile(Char.IsDigit).ToArray()));

                    // Don't increment the invoice number (as we are reusing an old one).
                    incrementInvoiceNumber = false;
                }
                else
                {
                    // Invoice exists, and user doesn't want to re-generate it
                    // Exit without incrementing invoice number.
                    return false;
                }
            }

            int startMonth = 0;
            int endMonth = 0;

            switch (quarter)
            {
                case 1:
                    startMonth = 7;
                    endMonth = 9;
                    break;
                case 2:
                    startMonth = 10;
                    endMonth = 12;
                    break;
                case 3:
                    startMonth = 1;
                    endMonth = 3;
                    break;
                case 4:
                    startMonth = 4;
                    endMonth = 6;
                    break;
            }

            IniFile iniFile = new IniFile();
            if (!File.Exists(Constants.DATA_SAVED_FILE))
            {
                MessageBox.Show("Cannot load owner information. Please re-run this program.", "Owner Information Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            iniFile.Load(Constants.DATA_SAVED_FILE);

            csvData = Constants.CSV_HEADER_EXPENSES + String.Format("{0:00000},{1},{11},{2},{3},{4},{5},{6},{7},{8:00}/{10:0000} - {9:00}/{10:0000},<<EXPENSETOTAL>>,<<GST>>",
                invoiceNumber, ownerName, iniFile.GetKeyValue("Owner", "ABN"), iniFile.GetKeyValue("Owner", "StreetAddress"),
                iniFile.GetKeyValue("Owner", "City"), iniFile.GetKeyValue("Owner", "State"), iniFile.GetKeyValue("Owner", "PostCode"),
                invoiceDate, startMonth, endMonth, year, iniFile.GetKeyValue("Owner", "BusinessName"));

            List<Expense> expensesList = new List<Expense>();
            string expensesSheet = Path.Combine(Constants.FILE_PATH, String.Format(Constants.EXPENSES_FILENAME_FORMAT, startYear, startYear + 1, quarter));

            if (File.Exists(expensesSheet))
            {
                // Run Excel (hidden)
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = false;

                Excel.Workbook workbook = excelApp.Workbooks.Open(expensesSheet, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Excel.Worksheet worksheet = workbook.Sheets[1];
                int lastRow = GetLastUsedRow(worksheet);

                for (int row = 2; row <= lastRow; row++)
                {
                    decimal cost = worksheet.get_Range("B" + row).Value == null ? 0.0M : (decimal)worksheet.get_Range("B" + row).Value;
                    decimal gst = worksheet.get_Range("C" + row).Value == null ? 0.0M : (decimal)worksheet.get_Range("C" + row).Value;

                    if (cost > 0.0M)
                    {
                        string category = worksheet.get_Range("D" + row).Value == null ? "<Not Specified>" : (string)worksheet.get_Range("D" + row).Value;

                        bool found = false;
                        foreach (Expense expense in expensesList)
                        {
                            if (expense.Category.ToLower() == category.ToLower())
                            {
                                expense.Cost += cost;
                                expense.GST += gst;
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                            expensesList.Add(new Expense(category, cost, gst));
                    }
                }

                workbook.Close(false);
                workbook = null;

                decimal expenseTotal = 0.0M;
                decimal expenseGSTTotal = 0.0M;
                foreach (Expense expense in expensesList)
                {
                    csvData += String.Format(",{0}:,${1:0.00},${2:0.00}", expense.Category, expense.Cost, expense.GST);
                    expenseTotal += expense.Cost;
                    expenseGSTTotal += expense.GST;
                }

                // Add blank lines.
                for (int i = expensesList.Count; i < 31; i++)
                {
                    csvData += ",,,";
                }

                csvData = csvData.Replace("<<EXPENSETOTAL>>", String.Format("${0:0.00}", expenseTotal));
                csvData = csvData.Replace("<<GST>>", String.Format("${0:0.00}", expenseGSTTotal));

                File.WriteAllText(Constants.DATA_TEMP_FILE, csvData);

                // <<Merge CSV into Word now>>
                bool mergeResult = WordHelper.MergeDriverInvoice(invoiceFile, Constants.EXPENSES_INVOICE_TEMPLATE_FILE);

                if (incrementInvoiceNumber)
                    incrementInvoiceNumber = mergeResult;

                excelApp.Application.Quit();
                excelApp = null;
                GC.Collect();
            }
            else
            {
                incrementInvoiceNumber = false;
            }

            return incrementInvoiceNumber;
        }

        private static int GetLastFaresRow(Excel.Worksheet excelWorksheet, string column)
        {
            int lastRow = 2;

            // Get the row number of the last entered row
            for (int row = 33; row > 2; row--)
            {
                if (excelWorksheet.get_Range(column + row).Value != null)
                {
                    lastRow = row;
                    break;
                }
            }

            return lastRow;
        }

        private static int GetLastUsedRow(Excel.Worksheet excelWorksheet)
        {
            int lastRow = 2;

            // Get the last cell that has ever had anything entered into it.
            Excel.Range last = excelWorksheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);

            // Check each row in column "B" from the last detected cell up, to ensure this is the last row
            for (int row = last.Row; row > 2; row--)
            {
                if (excelWorksheet.get_Range("B" + row).Value != null)
                {
                    lastRow = row;
                    break;
                }
            }

            return lastRow;
        }

        public static bool InsertTripLevy(IWin32Window owner)
        {
            bool result = true;
            float driversShare = 0.45F;

            ProgressGUI progress = new ProgressGUI("Applying Levy Update", "Updating Sheets...", "Getting file list...", true);
            progress.Show(owner);
            
            string[] fileList = Directory.GetFiles(Constants.FILE_PATH, "*2018-2019.xlsx");
            if (fileList.Count() <= 0)
                return result;

            float progressValue = 0.0F;
            float sheetWorth = 100F / (fileList.Count() * 12);

            // Run Excel (hidden)
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;

            foreach (string file in fileList)
            {
                if (file.Substring(0, 2) == "~$")
                    continue;

                progress.StatusText = "Processing: " + Path.GetFileName(file);
                Excel.Workbook workbook = excelApp.Workbooks.Open(file, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

                for (int sheet = 1; sheet <= 12; sheet++)
                {
                    Excel.Worksheet worksheet = workbook.Sheets[sheet];
                    worksheet.Unprotect();

                    Excel.Range tripLevyHeader = worksheet.Range["R1"];
                    tripLevyHeader.Value = "Trip Levy";
                    tripLevyHeader.Font.Bold = true;

                    Excel.Range levyGSTHeader = worksheet.Range["S1"];
                    levyGSTHeader.Value = "Levy GST";
                    levyGSTHeader.Font.Bold = true;

                    int month = sheet < 7 ? sheet + 6 : sheet - 6;
                    int year = sheet < 7 ? 2018 : 2019;
                    int maxDaysInMonth = DateTime.DaysInMonth(year, month);
                    float cellWorth = sheetWorth / maxDaysInMonth;

                    for (int dayOfMonth = 1; dayOfMonth <= maxDaysInMonth; dayOfMonth++)
                    {
                        int row = dayOfMonth + 2;

                        Excel.Range tripLevyCell = worksheet.Range["R" + row.ToString()];
                        tripLevyCell.NumberFormat = Constants.FORMAT_CURRENCY;
                        tripLevyCell.Formula = "=IF(ISNUMBER(Q" + row.ToString() + "), Q" + row.ToString() + "*1.1, \"\")";

                        Excel.Range levyGSTCell = worksheet.Range["S" + row.ToString()];
                        levyGSTCell.NumberFormat = Constants.FORMAT_CURRENCY;
                        levyGSTCell.Formula = "=IF(ISNUMBER(R" + row.ToString() + "), R" + row.ToString() + "/11, \"\")";

                        Excel.Range ownerTakingsCell = worksheet.Range["G" + row.ToString()];
                        ownerTakingsCell.NumberFormat = Constants.FORMAT_CURRENCY;
                        ownerTakingsCell.Formula = "=(C" + row.ToString() + "-IF(ISNUMBER(R" + row.ToString() + "), R" + row.ToString() + ", 0))*" + driversShare.ToString("0.000");

                        Excel.Range driverTakingsCell = worksheet.Range["J" + row.ToString()];
                        driverTakingsCell.NumberFormat = Constants.FORMAT_CURRENCY;
                        driverTakingsCell.Formula = "=(C" + row.ToString() + "-IF(ISNUMBER(R" + row.ToString() + "), R" + row.ToString() + ", 0))*" + (1 - driversShare).ToString("0.000");

                        // Mark editible cells as "unlocked"
                        Excel.Range holidayCell = worksheet.Range["B" + row.ToString()];
                        holidayCell.BorderAround();
                        holidayCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightCyan);
                        holidayCell.Locked = false;

                        Excel.Range faresCell = worksheet.Range["C" + row.ToString()];
                        faresCell.BorderAround();
                        faresCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightCyan);
                        faresCell.Locked = false;

                        Excel.Range shiftCell = worksheet.Range["O" + row.ToString()];
                        shiftCell.BorderAround();
                        shiftCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightCyan);
                        shiftCell.Locked = false;

                        Excel.Range kmCell = worksheet.Range["P" + row.ToString()];
                        kmCell.BorderAround();
                        kmCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightCyan);
                        kmCell.Locked = false;

                        Excel.Range tripCell = worksheet.Range["Q" + row.ToString()];
                        tripCell.BorderAround();
                        tripCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightCyan);
                        tripCell.Locked = false;

                        progressValue += cellWorth;
                        progress.Value = (int)progressValue;
                    }

                    Excel.Range tripLevyTotalCell = worksheet.Range["R" + (maxDaysInMonth + 4).ToString()];
                    tripLevyTotalCell.NumberFormat = Constants.FORMAT_CURRENCY;
                    tripLevyTotalCell.Formula = "=SUM(R3:R" + (maxDaysInMonth + 2).ToString() + ")";

                    Excel.Range levyGSTTotalCell = worksheet.Range["S" + (maxDaysInMonth + 4).ToString()];
                    levyGSTTotalCell.NumberFormat = Constants.FORMAT_CURRENCY;
                    levyGSTTotalCell.Formula = "=SUM(S3:S" + (maxDaysInMonth + 2).ToString() + ")";

                    worksheet.Protect();
                }

                workbook.Close(true);
                workbook = null;
            }

            excelApp.Application.Quit();
            excelApp = null;
            GC.Collect();
            progress.Close();

            return result;
        }
    }
}
