using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.IO;

namespace Robs_Taxi_Manager
{
    static public class Constants
    {
        public const string FORMAT_CURRENCY = "$#,##0.00";
        public const string VERSION_VARIABLE = "%VERSION%";
        public const string WEB_DOMAIN = @"http://www.cabmanager.com.au/";
        public const string UPDATE_TXT = WEB_DOMAIN + @"dl/version.txt";
        public const string UPDATE_INSTALLER = WEB_DOMAIN + @"dl/TaxiManagerSetup-" + VERSION_VARIABLE + ".exe";
        public const string COPYRIGHT_NOTICE = "© 2013-2022, Robert Brandon";
        public const string REGISTRY_SUBKEY = @"Software\Earth Dragon Software\Taxi App";
        public static string TEMP_SETUP_EXE = Path.GetTempPath() + "TaxiManagerSetup.exe";
        public static string APP_PATH = AppDomain.CurrentDomain.BaseDirectory;
        public static string FILE_PATH = Path.Combine(APP_PATH, "Files");
        public static string TEMPLATE_PATH = Path.Combine(FILE_PATH, "Templates");
        public static string INVOICE_PATH = Path.Combine(FILE_PATH, "Invoices");
        public static string DB_FILE = Path.Combine(FILE_PATH, "data.db3");
        public static string DRIVER_FILE = Path.Combine(FILE_PATH, "DriverList.txt");
        public static string DATA_TEMP_FILE = Path.Combine(FILE_PATH, "InvoiceData.csv");
        public static string DATA_SAVED_FILE = Path.Combine(FILE_PATH, "SavedData.ini");
        public static string RUNNING_SHEET_TEMPLATE_FILE = Path.Combine(TEMPLATE_PATH, "Taxi Running Sheets Template.xlsx");
        public static string EXPENSE_TEMPLATE_FILE = Path.Combine(TEMPLATE_PATH, "Expenses Template.xlsx");
        public static string INVOICE_TEMPLATE_FILE = Path.Combine(TEMPLATE_PATH, "Invoice Template.docx");
        public static string EXPENSES_INVOICE_TEMPLATE_FILE = Path.Combine(TEMPLATE_PATH, "Expenses Invoice Template.docx");
        
        // 0=Invoice Number; 1=Driver's Name; 2=Year; 3=Month.
        public const string INVOICE_FILENAME_FORMAT = "{0:0000} - {1} ({2:0000}-{3:00}).pdf";
        // update the below if updating the top. 0=Driver's Name; 1=Year; 2=Month
        public const string INVOICE_FILENAME_SEARCH_PATTERN = "* - {0} ({1:0000}-{2:00}).pdf";

        // 0=Invoice Number; 1=StartYear; 2=EndYear; 3=Quarter.
        public const string EXPENSES_INVOICE_FILENAME_FORMAT = "{0:0000} - Expenses ({1:0000}-{2:0000}) Q{3}.pdf";
        // update the below if updating the top. 0=StartYear; 1=EndYear; 2=Quarter
        public const string EXPENSES_INVOICE_FILENAME_SEARCH_PATTERN = "* - Expenses ({0:0000}-{1:0000}) Q{2}.pdf";

        public const string CSV_HEADER = "InvoiceNumber,DriverName,OwnerName,ABN,Street,City,State,Post,Date,InvoicePeriod,DriverTotal,TotalGST,Date1,Day1,Shift1,KMS1,Trips1,Fares1,DriversShare1,Date2,Day2,Shift2,KMS2,Trips2,Fares2,DriversShare2,Date3,Day3,Shift3,KMS3,Trips3,Fares3,DriversShare3,Date4,Day4,Shift4,KMS4,Trips4,Fares4,DriversShare4,Date5,Day5,Shift5,KMS5,Trips5,Fares5,DriversShare5,Date6,Day6,Shift6,KMS6,Trips6,Fares6,DriversShare6,Date7,Day7,Shift7,KMS7,Trips7,Fares7,DriversShare7,Date8,Day8,Shift8,KMS8,Trips8,Fares8,DriversShare8,Date9,Day9,Shift9,KMS9,Trips9,Fares9,DriversShare9,Date10,Day10,Shift10,KMS10,Trips10,Fares10,DriversShare10,Date11,Day11,Shift11,KMS11,Trips11,Fares11,DriversShare11,Date12,Day12,Shift12,KMS12,Trips12,Fares12,DriversShare12,Date13,Day13,Shift13,KMS13,Trips13,Fares13,DriversShare13,Date14,Day14,Shift14,KMS14,Trips14,Fares14,DriversShare14,Date15,Day15,Shift15,KMS15,Trips15,Fares15,DriversShare15,Date16,Day16,Shift16,KMS16,Trips16,Fares16,DriversShare16,Date17,Day17,Shift17,KMS17,Trips17,Fares17,DriversShare17,Date18,Day18,Shift18,KMS18,Trips18,Fares18,DriversShare18,Date19,Day19,Shift19,KMS19,Trips19,Fares19,DriversShare19,Date20,Day20,Shift20,KMS20,Trips20,Fares20,DriversShare20,Date21,Day21,Shift21,KMS21,Trips21,Fares21,DriversShare21,Date22,Day22,Shift22,KMS22,Trips22,Fares22,DriversShare22,Date23,Day23,Shift23,KMS23,Trips23,Fares23,DriversShare23,Date24,Day24,Shift24,KMS24,Trips24,Fares24,DriversShare24,Date25,Day25,Shift25,KMS25,Trips25,Fares25,DriversShare25,Date26,Day26,Shift26,KMS26,Trips26,Fares26,DriversShare26,Date27,Day27,Shift27,KMS27,Trips27,Fares27,DriversShare27,Date28,Day28,Shift28,KMS28,Trips28,Fares28,DriversShare28,Date29,Day29,Shift29,KMS29,Trips29,Fares29,DriversShare29,Date30,Day30,Shift30,KMS30,Trips30,Fares30,DriversShare30,Date31,Day31,Shift31,KMS31,Trips31,Fares31,DriversShare31,TripLevyTotal\r\n";
        public const string CSV_HEADER_EXPENSES = "InvoiceNumber,DriverName,OwnerName,ABN,Street,City,State,Post,Date,InvoicePeriod,ExpenseTotal,TotalGST,Category1,Cost1,GST1,Category2,Cost2,GST2,Category3,Cost3,GST3,Category4,Cost4,GST4,Category5,Cost5,GST5,Category6,Cost6,GST6,Category7,Cost7,GST7,Category8,Cost8,GST8,Category9,Cost9,GST9,Category10,Cost10,GST10,Category11,Cost11,GST11,Category12,Cost12,GST12,Category13,Cost13,GST13,Category14,Cost14,GST14,Category15,Cost15,GST15,Category16,Cost16,GST16,Category17,Cost17,GST17,Category18,Cost18,GST18,Category19,Cost19,GST19,Category20,Cost20,GST20,Category21,Cost21,GST21,Category22,Cost22,GST22,Category23,Cost23,GST23,Category24,Cost24,GST24,Category25,Cost25,GST25,Category26,Cost26,GST26,Category27,Cost27,GST27,Category28,Cost28,GST28,Category29,Cost29,GST29,Category30,Cost30,GST30,Category31,Cost31,GST31\r\n";

        // 0=day; 1=month; 2=year.
        public const string DATE_FORMAT = "{0:00}/{1:00}/{2:0000}";

        // 0=fromYear; 1=toYear; 2=quarter.
        public const string EXPENSES_FILENAME_FORMAT = "Expenses_{0:0000}-{1:0000}_Q{2}.xlsx";

        // 0=Driver's name; 1=fromYear; 2=toYear.
        public const string RUNNING_SHEET_FILENAME_FORMAT = "{0} {1:0000}-{2:0000}.xlsx";
    }
}
