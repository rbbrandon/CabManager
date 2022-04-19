using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Robs_Taxi_Manager
{
    public static class Requirements
    {
        public static bool CheckAll()
        {
            bool result = true;

            result &= CheckOffice("14.0", "Word");
            result &= CheckOffice("14.0", "Excel");
            result &= CheckOffice("15.0", "Word");
            result &= CheckOffice("15.0", "Excel");
            result &= CheckOffice("16.0", "Word");
            result &= CheckOffice("16.0", "Excel");

            return result;
        }

        // Indicates whether the specified version of an office product is installed.
        //
        // version -- Specify one of these strings for the required Office version:
        //    '12.0'       Office 2007
        //    '14.0'       Office 2010
        //    '15.0'       Office 2013
        //    '16.0'       Office 2016
        //    '16.0'       Office 2019
        //
        // product -- Specify one of these strings for the required Office product:
        //    'Word'      Microsoft Word
        //    'Excel'     Microsoft Excel
        public static bool CheckOffice(string version, string product)
        {
            bool result = true;
            string key = "SOFTWARE\\Microsoft\\Office\\" + version + "\\" + product + "\\InstallRoot";

            RegEdit regEdit = new RegEdit(RegEdit.Base.LocalMachine, key);

            if (regEdit.Read("Path") == null)
                result = false;

            if (!result) {
                key = "SOFTWARE\\Wow6432Node\\Microsoft\\Office\\" + version + "\\" + product + "\\InstallRoot";

                regEdit = new RegEdit(RegEdit.Base.LocalMachine, key);

                if (regEdit.Read("Path") == null)
                    result = false;
            }

            return result;
        }

        // If a PDF reader is installed, the CLSID key for the application/pdf reg key will exist.
        public static bool CheckPDF()
        {
            bool result = true;
            string key = "MIME\\Database\\Content Type\\application/pdf";

            RegEdit regEdit = new RegEdit(RegEdit.Base.ClassesRoot, key);

            if (regEdit.Read("CLSID") == null)
                result = false;

            return result;
        }
    }
}
