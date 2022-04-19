using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Robs_Taxi_Manager
{
    public partial class EditDriverGUI : Form
    {
        private string _IniSection;
        private string _OriginalName;

        public EditDriverGUI(string iniSection)
        {
            _IniSection = iniSection;
            InitializeComponent();
        }

        private void EditDriverGUI_Load(object sender, EventArgs e)
        {
            IniFile iniFile = new IniFile();
            if (File.Exists(Constants.DATA_SAVED_FILE))
            {
                iniFile.Load(Constants.DATA_SAVED_FILE);

                _OriginalName = iniFile.GetKeyValue(_IniSection, "Name");

                nameTextBox.Text = _OriginalName;
                this.Text += _OriginalName;

                if (nameTextBox.Text.Trim() != string.Empty)
                    saveButton.Enabled = true;
                else
                    saveButton.Enabled = false;
            }
            else
                this.Close();
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (nameTextBox.Text.Trim() != string.Empty)
                saveButton.Enabled = true;
            else
                saveButton.Enabled = false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string newName = nameTextBox.Text.Trim();
            IniFile iniFile = new IniFile();

            if (File.Exists(Constants.DATA_SAVED_FILE))
                iniFile.Load(Constants.DATA_SAVED_FILE);

            iniFile.SetKeyValue(_IniSection, "Name", newName);
            iniFile.Save(Constants.DATA_SAVED_FILE);

            if (_OriginalName != newName)
            {
                // Driver's name has been changed.
                // Get a list of the driver's current running sheets.
                var files = Directory.GetFiles(Constants.FILE_PATH, _OriginalName + " ????-????.xlsx");

                // Rename each of them to match the new name
                foreach (string file in files)
                {
                    string newFile = Constants.FILE_PATH + file.Replace(Constants.FILE_PATH, "").Replace(_OriginalName, newName);

                    while (true)
                    {
                        try
                        {
                            File.Move(file, newFile);
                            break;
                        }
                        catch (System.IO.IOException ioe)
                        {
                            MessageBox.Show("There was an error while trying to rename " + _OriginalName + "'s running sheets.\n" +
                                "The running sheets may still be open. Please ensure that all running sheets are closed, and then click \"OK\" to try again.\n\n" +
                                "System error message:\n\"" + ioe.Message + "\"", "Error Renaming Running Sheets", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        // Source: http://stackoverflow.com/a/937558
        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}
