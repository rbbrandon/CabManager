using System;
using System.IO;
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
using System.Diagnostics;
using System.Security.Principal;
using System.Drawing;

namespace Robs_Taxi_Manager
{
    public partial class MainGUI : Form
    {
        private Mutex _Mutex;
        private int _StartMonth;
        private int _FromYear;
        private int _ToYear;
        private int _Quarter;
        private int _MonthSheetIndex;
        private string _OwnerName;
        //private SQLiteDatabase _SQLiteDatabase;

        public MainGUI(Mutex mutex)
        {
            _Mutex = mutex;
            InitializeComponent();
            this.Text += Constants.COPYRIGHT_NOTICE;

            Image logo = Utils.ResizeImage(Utils.GetImageFromByteArray(Properties.Resources.CabManagerLogoPNG), 496, 92);
            pictureBox1.Size = logo.Size;
            pictureBox1.Image = logo;
            pictureBox1.Left = this.ClientSize.Width / 2 - pictureBox1.Width / 2;
        }

        private void MainGUI_Load(object sender, EventArgs e)
        {
            GC.Collect();

            bool needAdminAccess = false;

            try
            {
                FileStream fs = File.Create(Constants.FILE_PATH + "\\test.tmp");
                fs.Close();
                File.Delete(Constants.FILE_PATH + "\\test.tmp");
            }
            catch
            {
                needAdminAccess = true;
            }

            // Run program as admin if required to access files (e.g. Program Files).
            if (needAdminAccess && !IsAdministrator())
            {
                // Restart program and run as admin
                var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                startInfo.Verb = "runas";
                
                try
                {
                    _Mutex.ReleaseMutex();
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch
                {
                    MessageBox.Show("This program needs admin access to write to " + Constants.FILE_PATH + ".\nThis program will now close.", "Error: Insufficient Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Application.Exit();
            }

            // Check Requirements:
            if (!Requirements.CheckOffice("14.0", "Excel") && !Requirements.CheckOffice("15.0", "Excel") && !Requirements.CheckOffice("16.0", "Excel"))
            {
                MessageBox.Show("The 32-bit version of Microsoft Excel 2010+ is required for this program to run.\nPlease install it and then try running this program again.", "Error: Microsoft Excel Not Installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            if (!Requirements.CheckOffice("14.0", "Word") && !Requirements.CheckOffice("15.0", "Word") && !Requirements.CheckOffice("16.0", "Word"))
            {
                MessageBox.Show("The 32-bit version of Microsoft Word 2010+ is required for this program to generate invoices.\nTo enable invoice generation, please install it and then try running this program again.", "Warning: Microsoft Word Not Installed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                generateButton.Enabled = false;
            }

            if (!Requirements.CheckPDF())
            {
                // For some reason, the page opens when application restarts. This check stops it if it's restarting.
                if (!needAdminAccess || IsAdministrator())
                {
                    if (MessageBox.Show("Invoices are generated as PDF files, however you do not have a PDF reader installed.\n\nWould you like to open your browser to download Adobe Reader now?",
                        "Note: PDF Reader Not Installed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        System.Diagnostics.Process.Start("https://get.adobe.com/reader");
                }
            }

            // Populate year combobox.
            for (int i = 2012; i <= DateTime.Now.Year; i++)
            {
                yearCombo.Items.Add(i);
            }
            yearCombo.SelectedItem = DateTime.Now.Year;

            // Set month combobox to current month.
            monthCombo.SelectedIndex = DateTime.Now.Month - 1;

            // Create Directories if not exist.
            if (!Directory.Exists(Constants.FILE_PATH))
                Directory.CreateDirectory(Constants.FILE_PATH);
            if (!Directory.Exists(Constants.INVOICE_PATH))
                Directory.CreateDirectory(Constants.INVOICE_PATH);
            if (!Directory.Exists(Constants.TEMPLATE_PATH))
                Directory.CreateDirectory(Constants.TEMPLATE_PATH);

            // Create/Load the program's database file.
            //LoadDatabase();

            // Get product registration status.
            RegEdit regEdit = new RegEdit(RegEdit.Base.CurrentUser, Constants.REGISTRY_SUBKEY);
            IniFile iniFile = new IniFile();
            if (File.Exists(Constants.DATA_SAVED_FILE))
                iniFile.Load(Constants.DATA_SAVED_FILE);

            _OwnerName = iniFile.GetKeyValue("Owner", "Name");

            if (_OwnerName == string.Empty)
            {
                _OwnerName = regEdit.Read("Owner");

                if (_OwnerName == null)
                {
                    // Owner name not in registry or ini file- prompt
                    using (OwnerInfoPrompt ownerInfoPrompt = new OwnerInfoPrompt())
                        if (ownerInfoPrompt.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                            this.Close();

                    iniFile.Load(Constants.DATA_SAVED_FILE);
                    _OwnerName = iniFile.GetKeyValue("Owner", "Name");
                }
                else
                    iniFile.SetKeyValue("Owner", "Name", _OwnerName);
            }

            // Set registration text.
            registeredLabel.Text = "Registered Taxi Owner: " + iniFile.GetKeyValue("Owner", "BusinessName").Replace("&", "&&");
            registeredLabel.Left = this.Width - registeredLabel.Width - 15;

            // If legacy driver list exists, import into ini:
            if (File.Exists(Constants.DRIVER_FILE))
            {
                // Get count of drivers
                int driverCount;

                for (driverCount = 0; ; driverCount++)
                    if (iniFile.GetKeyValue("Driver" + driverCount, "Name") == string.Empty)
                        break;

                // Read file to ini
                var lines = File.ReadLines(Constants.DRIVER_FILE);
                foreach (string line in lines)
                {
                    if (line == "All" || line == "Expenses" || line == _OwnerName)
                        continue;

                    bool found = false;
                    for (int i = 0; i <= driverCount; i++)
                    {
                        string iniDriver = iniFile.GetKeyValue("Driver" + i, "Name");

                        if (iniDriver.ToLower() == line.ToLower())
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        iniFile.SetKeyValue("Driver" + driverCount, "Name", line);
                        driverCount += 1;
                    }
                };
                
                File.Delete(Constants.DRIVER_FILE);
            }


            // Set last invoice number to 0 if not found.
            if (iniFile.GetKeyValue("Data", "LastInvoiceNumber") == string.Empty)
                iniFile.SetKeyValue("Data", "LastInvoiceNumber", "0");

            // Save ini file.
            iniFile.Save(Constants.DATA_SAVED_FILE);

            // Read driverlist into driver combobox and set selected to "All".
            LoadDriverList();
            driverCombo.SelectedIndex = 0;

            // Changelog info
            string version = regEdit.Read("Version");
            if (version != Application.ProductVersion)
            {
                using (ChangelogGUI ChangelogGUI = new ChangelogGUI())
                {
                    ChangelogGUI.ShowDialog(this);
                    regEdit.Write("Version", Application.ProductVersion);
                }
            }

            // Check for "Commercial Passenger Vehicle Service Levy" update.
            string levyApplied = regEdit.Read("LevyApplied");
            if (levyApplied != "1")
            {
                if (MessageBox.Show("From the 1st of July, 2018, the \"Commercial Passenger Vehicle Service Levy\" is included in all fares collected, which affects owner/driver shares.\r\n" +
                                    "Would you like to update all existing driver sheets that this change affects to incorporate this levy?\r\n\r\n" +
                                    "NOTE: If you don't apply this change, ALL calculations will be incorrect, and you'll be over-paying your drivers!", "IMPORTANT!",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    ExcelHelper.InsertTripLevy(this);
                    regEdit.Write("LevyApplied", "1");
                    MessageBox.Show("All sheets have been updated to include the levy. You will need to re-generate any invoices that previously didn't have the levy.", "Update complete");
                }
            }

            // Check for updates
            UpdateCheck();
        }

        private void LoadDatabase()
        {
            //_SQLiteDatabase = new SQLiteDatabase(Constants.DB_FILE);
            //_SQLiteDatabase.Initialise();
        }

        private static bool IsAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                isAdmin = false;
            }
            catch (Exception)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        private void UpdateCheck()
        {
            Version currentVersion = new Version(Application.ProductVersion);

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadStringCompleted += (dlSender, dlE) =>
                    {
                        // Do something with the results
                        Version newVersion = new Version(dlE.Result);

                        if (currentVersion < newVersion)
                        {
                            // An update is available!
                            updateLabel.ForeColor = System.Drawing.Color.Red;
                            updateLabel.Text = "v" + currentVersion.ToString() +": An update (v" + newVersion.ToString() + ") is available.";
                            if (MessageBox.Show("An update is available. Would you like to download and apply it now?", "Update Available",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                checkForUpdatesToolStripMenuItem.PerformClick();
                        }
                        else
                        {
                            updateLabel.ForeColor = System.Drawing.Color.DarkGreen;
                            updateLabel.Text = "v" + currentVersion.ToString() + ": Up to date.";
                        }
                    };
                    client.DownloadStringAsync(new Uri(Constants.UPDATE_TXT));
                }
            }
            catch
            {
                updateLabel.ForeColor = System.Drawing.Color.Red;
                updateLabel.Text = "v" + currentVersion.ToString() + ": Cannot contact server.";
            }
        }

        private void LoadDriverList()
        {
            driverCombo.Items.Clear();

            driverCombo.Items.Add("All");
            driverCombo.Items.Add("Expenses");

            IniFile iniFile = new IniFile();
            iniFile.Load(Constants.DATA_SAVED_FILE);

            driverCombo.Items.Add(iniFile.GetKeyValue("Owner", "Name"));

            for (int i = 0; ; i++)
                if (iniFile.GetKeyValue("Driver" + i, "Name") == string.Empty)
                    break;
                else
                    driverCombo.Items.Add(iniFile.GetKeyValue("Driver" + i, "Name"));
        }

        private void addDriverButton_Click(object sender, EventArgs e)
        {
            // Prompt for new driver GUI.
            AddDriverGUI addDriverGUI = new AddDriverGUI();
            if (addDriverGUI.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                // Reload the driver list, and set the selected driver to the last one (i.e. the one just added).
                LoadDriverList();
                driverCombo.SelectedIndex = driverCombo.Items.Count - 1;
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            if (driverCombo.SelectedIndex > 1)
            {
                // A Driver is selected.
                string runningSheet = CopyDriverTemplateIfNeeded();

                // Open sheet.
                if (!String.IsNullOrEmpty(runningSheet))
                    ExcelHelper.EditRunningSheet(runningSheet, _MonthSheetIndex);
            }
            else
            {
                // "Expenses" is selected.
                string expensesSheet = CopyExpensesTemplateIfNeeded();

                // Open sheet.
                if (!String.IsNullOrEmpty(expensesSheet))
                    ExcelHelper.EditExpenses(expensesSheet);
            }
            this.Enabled = true;
            this.Refresh();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            if (driverCombo.SelectedIndex == 1)
            {
                // "Expenses" is selected.
                this.Enabled = false;
                GenerateExpensesInvoice();
                this.Enabled = true;
                this.Refresh();
            }
            else
            {
                // A Driver (or "All") is selected.
                this.Enabled = false;
                GenerateDriverInvoice();
                this.Enabled = true;
                this.Refresh();
            }
        }

        private void driverCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (driverCombo.SelectedIndex == 0)
                editButton.Enabled = false;
            else
                editButton.Enabled = true;

            if (driverCombo.SelectedIndex > 1)
            {
                editLink.Enabled = true;
                editLink.Visible = true;
            }
            else
            {
                editLink.Enabled = false;
                editLink.Visible = false;
            }
        }

        private string CopyDriverTemplateIfNeeded()
        {
            string runningSheet = Path.Combine(Constants.FILE_PATH, driverCombo.SelectedItem.ToString() + " " + _FromYear + "-" + _ToYear + ".xlsx");

            if (!File.Exists(runningSheet))
            {
                // No running sheet exists for the selected driver for the selected financial year.
                if (MessageBox.Show("There isn't a running sheet for \"" + driverCombo.SelectedItem.ToString() + "\" for the " + _FromYear + "/" + _ToYear + " financial year.\n" +
                    "Would you like to create one?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    // Copy the running sheet template for the selected driver.
                    File.Copy(Constants.RUNNING_SHEET_TEMPLATE_FILE, runningSheet);

                    // <<Set years in excel>>
                    ExcelHelper.SetRunningSheetYear(runningSheet, _FromYear);
                }
            }

            if (File.Exists(runningSheet))
            {
                return runningSheet;
            }

            return null;
        }

        private string CopyExpensesTemplateIfNeeded()
        {
            string expensesSheet = Path.Combine(Constants.FILE_PATH, String.Format(Constants.EXPENSES_FILENAME_FORMAT, _FromYear, _ToYear, _Quarter));

            if (!File.Exists(expensesSheet))
            {
                // No running sheet exists for the selected driver for the selected financial year.
                if (MessageBox.Show("There isn't an expenses sheet for Q" + _Quarter + " of the " + _FromYear + "/" + _ToYear + " financial year.\n" +
                    "Would you like to create one?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    // Copy the running sheet template for the selected driver.
                    File.Copy(Constants.EXPENSE_TEMPLATE_FILE, expensesSheet);

                    // <<Set years in excel>>
                    ExcelHelper.SetupExpenses(expensesSheet, _FromYear, _StartMonth);
                }
            }

            if (File.Exists(expensesSheet))
            {
                return expensesSheet;
            }

            return null;
        }

        private void monthCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set running sheet month index.
            _MonthSheetIndex = monthCombo.SelectedIndex < 6 ? monthCombo.SelectedIndex + 7 : monthCombo.SelectedIndex - 5;

            // Set Quarter
            if (monthCombo.SelectedIndex < 3)
            {
                _Quarter = 3;
                _StartMonth = 1;
            }
            else if (monthCombo.SelectedIndex < 6)
            {
                _Quarter = 4;
                _StartMonth = 4;
            }
            else if (monthCombo.SelectedIndex < 9)
            {
                _Quarter = 1;
                _StartMonth = 7;
            }
            else
            {
                _Quarter = 2;
                _StartMonth = 10;
            }

            yearCombo_SelectedIndexChanged(sender, e);
        }

        private void yearCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set Financial Year
            if (monthCombo.SelectedIndex < 6)
            {
                _FromYear = (int)yearCombo.SelectedItem - 1;
                _ToYear = (int)yearCombo.SelectedItem;
            }
            else
            {
                _FromYear = (int)yearCombo.SelectedItem;
                _ToYear = (int)yearCombo.SelectedItem + 1;
            }
        }

        private void GenerateExpensesInvoice()
        {
            // Open progress bar.
            ProgressGUI progress = new ProgressGUI(this.Text, "Generating Expense Invoice", "Getting invoice number...", true);
            progress.Show(this);

            // Get last invoice number.
            int invoiceNumber = GetInvoiceNumber();

            progress.Value = 50;
            progress.StatusText = "Processing...";

            if (ExcelHelper.CreateExpensesInvoice(invoiceNumber + 1, _Quarter, (int)yearCombo.SelectedItem, _OwnerName))
            {
                // Increment invoice number if invoice generation was successful.
                invoiceNumber += 1;
            }

            progress.Value = 100;
            progress.StatusText = "Saving invoice number...";

            // Save new last invoice number value.
            SaveInvoiceNumber(invoiceNumber);

            progress.Close();
        }

        private void GenerateDriverInvoice()
        {
            // Open progress bar.
            ProgressGUI progress = new ProgressGUI(this.Text, "Generating Driver Invoices", "Getting invoice number...", true);
            progress.Show(this);

            // Get last invoice number.
            int invoiceNumber = GetInvoiceNumber();

            // Generate Invoice(s):
            if (driverCombo.SelectedIndex != 0)
            {
                progress.Value = 30;

                if (ExcelHelper.CreateDriverInvoice(invoiceNumber + 1, _OwnerName, (string)driverCombo.SelectedItem, (int)monthCombo.SelectedIndex + 1, (int)yearCombo.SelectedItem))
                {
                    // Increment invoice number if invoice generation was successful.
                    invoiceNumber += 1;
                }
            }
            else if (driverCombo.Items.Count > 2)
            {
                for (int driverID = 2; driverID < driverCombo.Items.Count; driverID++)
                {
                    progress.Value = (int)(((driverID - 1) / (float)(driverCombo.Items.Count - 2)) * 100);
                    progress.StatusText = "Processing driver: " + (string)driverCombo.Items[driverID];

                    if (ExcelHelper.CreateDriverInvoice(invoiceNumber + 1, _OwnerName, (string)driverCombo.Items[driverID], (int)monthCombo.SelectedIndex + 1, (int)yearCombo.SelectedItem))
                    {
                        // Increment invoice number if invoice generation was successful.
                        invoiceNumber += 1;
                    }
                }
            }

            progress.Value = 100;
            progress.StatusText = "Saving invoice number...";

            // Save new last invoice number value.
            SaveInvoiceNumber(invoiceNumber);

            progress.Close();
        }

        private int GetInvoiceNumber()
        {
            // Get last invoice number.
            int invoiceNumber;
            IniFile _ini = new IniFile();
            if (File.Exists(Constants.DATA_SAVED_FILE))
            {
                _ini.Load(Constants.DATA_SAVED_FILE);
                invoiceNumber = int.Parse(_ini.GetKeyValue("Data", "LastInvoiceNumber"));
            }
            else
            {
                invoiceNumber = 0;
            }
            _ini = null;

            return invoiceNumber;
        }

        private void SaveInvoiceNumber(int invoiceNumber)
        {
            IniFile _ini = new IniFile();

            if (File.Exists(Constants.DATA_SAVED_FILE))
                _ini.Load(Constants.DATA_SAVED_FILE);
            else
                _ini.AddSection("Data");

            _ini.SetKeyValue("Data", "LastInvoiceNumber", invoiceNumber.ToString());
            _ini.Save(Constants.DATA_SAVED_FILE);
            _ini = null;
        }

        private void aboutToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (AboutGUI aboutGUI = new AboutGUI())
                aboutGUI.ShowDialog(this);
        }

        private void closeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void changelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ChangelogGUI ChangelogGUI = new ChangelogGUI())
                ChangelogGUI.ShowDialog(this);
        }

        private void viewOwnerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int comboSelectedIndex = driverCombo.SelectedIndex;
            using (OwnerInfoPrompt ownerInfoPrompt = new OwnerInfoPrompt())
                ownerInfoPrompt.ShowDialog(this);

            // Load owner info
            IniFile iniFile = new IniFile();
            if (File.Exists(Constants.DATA_SAVED_FILE))
                iniFile.Load(Constants.DATA_SAVED_FILE);

            _OwnerName = iniFile.GetKeyValue("Owner", "Name");

            // Set registration text.
            registeredLabel.Text = "Registered Taxi Owner: " + iniFile.GetKeyValue("Owner", "BusinessName").Replace("&", "&&");
            registeredLabel.Left = this.Width - registeredLabel.Width - 15;

            // Read driverlist into driver combobox and set selected to the previously selected index.
            LoadDriverList();
            driverCombo.SelectedIndex = comboSelectedIndex;
        }

        private void editLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (driverCombo.SelectedIndex == 2)
            {
                // Selected driver is the owner- load owner info
                viewOwnerDetailsToolStripMenuItem.PerformClick();
            }
            else
            {
                int comboSelectedIndex = driverCombo.SelectedIndex;

                using (EditDriverGUI ownerInfoPrompt = new EditDriverGUI("Driver" + (driverCombo.SelectedIndex - 3)))
                    ownerInfoPrompt.ShowDialog(this);

                // Read driverlist into driver combobox and set selected to the previously selected index.
                LoadDriverList();
                driverCombo.SelectedIndex = comboSelectedIndex;
            }
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (UpdateGUI updateGUI = new UpdateGUI())
                updateGUI.ShowDialog(this);

            UpdateCheck();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.WEB_DOMAIN);
        }

        private void levyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("From the 1st of July, 2018, the \"Commercial Passenger Vehicle Service Levy\" is included in all fares collected, which affects owner/driver shares.\r\n" +
                                    "Would you like to update all existing driver sheets that this change affects to incorporate this levy?\r\n\r\n" +
                                    "NOTE: If you don't apply this change, ALL calculations will be incorrect, and you'll be over-paying your drivers!", "IMPORTANT!",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                ExcelHelper.InsertTripLevy(this);
                RegEdit regEdit = new RegEdit(RegEdit.Base.CurrentUser, Constants.REGISTRY_SUBKEY);
                regEdit.Write("LevyApplied", "1");
                MessageBox.Show("All sheets have been updated to include the levy. You will need to re-generate any invoices that previously didn't have the levy.", "Update complete");
            }
        }
    }
}
