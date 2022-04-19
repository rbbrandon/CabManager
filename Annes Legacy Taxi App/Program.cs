using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Robs_Taxi_Manager
{
    static class Program
    {
        static Mutex _Mutex = new Mutex(true, "{30278492-7c19-42de-a27a-58f7711a8cdb}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if(_Mutex.WaitOne(3000)) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainGUI(_Mutex));

                _Mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("Can only run one instance of this program at a time.");
            }
        }
    }
}
