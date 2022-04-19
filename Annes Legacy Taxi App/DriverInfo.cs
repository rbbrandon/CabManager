//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Robs_Taxi_Manager
{
    public class DriverInfo
    {
        public DriverInfo(string driverName, string runningSheet, Excel.Workbook workbook)
        {
            Name = driverName;
            Workbook = workbook;
            RunningSheet = runningSheet;
        }

        public string Name { get; set; }
        public string RunningSheet { get; set; }
        public Excel.Workbook Workbook { get; set; }
    }
}
