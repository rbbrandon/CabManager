using System;
//using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.IO;

namespace Robs_Taxi_Manager
{
    public partial class UpdateGUI : Form
    {
        private Version _CurrentVersion;
        private Version _NewVersion;
        private bool _URIExists = false;

        public UpdateGUI()
        {
            InitializeComponent();
        }

        #region StackOverflow
        // Source: http://stackoverflow.com/a/9459441
        private void startDownload()
        {
            //Thread thread = new Thread(() =>
            //{
            string setupExeUri = Constants.UPDATE_INSTALLER.Replace(Constants.VERSION_VARIABLE, _NewVersion.ToString(4));

            try
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri(setupExeUri), Constants.TEMP_SETUP_EXE);
            }
            catch
            {
                label1.Text = "Error";
                label2.Text = "An error occcurred while checking for updates.\nPlease check if you have an internet connection.";
                button1.Enabled = true;
            }
            //});
            //thread.Start();
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                _URIExists = true;
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                label2.Text = "Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive + " (" +
                    int.Parse(Math.Truncate(percentage).ToString()) + "%)";
                progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (_URIExists)
                {
                    progressBar1.Value = 100;
                    label1.Text = "Finished.";
                    label2.Text = "File download complete. Press \"OK\" to install it.";
                }
                else
                {
                    label1.Text = "Failed.";
                    label2.Text = "File failed to download. Please notify support:\n" +
                        "Current Version: " + _CurrentVersion.ToString(4) +
                        "\nNew Version: " + _NewVersion.ToString(4);
                }

                button1.Enabled = true;
            });
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100 && File.Exists(Constants.TEMP_SETUP_EXE))
            {
                try
                {
                    System.Diagnostics.Process.Start(Constants.TEMP_SETUP_EXE);
                    Application.Exit();
                }
                catch
                {
                    MessageBox.Show("Update cancelled by user.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            this.Close();
        }

        //private void UpdateGUI_Load(object sender, EventArgs e)
        //{
        //    using (WebClient client = new WebClient())
        //    {
        //        _CurrentVersion = new Version(Application.ProductVersion);
        //        _NewVersion = new Version(client.DownloadString(Constants.UPDATE_TXT));

        //        if (_CurrentVersion.CompareTo(_NewVersion) < 0)
        //        {
        //            // An update is available!
        //            label1.Text = "Downloading update file.";
        //            startDownload();
        //        }
        //        else
        //        {
        //            label1.Text = "Finished.";
        //            label2.Text = "No updates available. Your product is up to date.";
        //            button1.Enabled = true;
        //        }
        //    }
        //}

        private void UpdateGUI_Load(object sender, EventArgs e)
        {
            try
            { 
                using (WebClient client = new WebClient())
                {
                    client.DownloadStringCompleted += (dlSender, dlE) =>
                    {
                        // do something with the results
                        try
                        {
                            _CurrentVersion = new Version(Application.ProductVersion);
                            _NewVersion = new Version(dlE.Result);

                            if (_CurrentVersion.CompareTo(_NewVersion) < 0)
                            {
                                // An update is available!
                                label1.Text = "Downloading update file.";
                                startDownload();
                            }
                            else
                            {
                                label1.Text = "Finished.";
                                label2.Text = "No updates available. Your product is up to date.";
                                button1.Enabled = true;
                            }
                        }
                        catch
                        {
                            label1.Text = "Error";
                            label2.Text = "An error occcurred while checking for updates.\nPlease check if you have an internet connection.";
                            button1.Enabled = true;
                        }
                    };
                    client.DownloadStringAsync(new Uri(Constants.UPDATE_TXT));
                }
            }
            catch
            {
                label1.Text = "Error";
                label2.Text = "An error occcurred while checking for updates.\nPlease check if you have an internet connection.";
                button1.Enabled = true;
            }
        }
    }
}
