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
using System.Text.RegularExpressions;

namespace Robs_Taxi_Manager
{
    public partial class OwnerInfoPrompt : Form
    {
        private string _OriginalName;

        public OwnerInfoPrompt()
        {
            InitializeComponent();
        }

        private void OwnerInfoPrompt_Load(object sender, EventArgs e)
        {
            stateComboBox.SelectedIndex = 6;

            // Load Owner information (if there's a data file)
            if (File.Exists(Constants.DATA_SAVED_FILE))
            {
                IniFile iniFile = new IniFile();
                iniFile.Load(Constants.DATA_SAVED_FILE);

                _OriginalName = iniFile.GetKeyValue("Owner", "Name");
                nameTextBox.Text = _OriginalName;
                bNameTextBox.Text = iniFile.GetKeyValue("Owner", "BusinessName");
                abnTextBox.Text = iniFile.GetKeyValue("Owner", "ABN");
                stTextBox.Text = iniFile.GetKeyValue("Owner", "StreetAddress");
                postTextBox.Text = iniFile.GetKeyValue("Owner", "PostCode");
                cityTextBox.Text = iniFile.GetKeyValue("Owner", "City");
                stateComboBox.Text = iniFile.GetKeyValue("Owner", "State");

                EnableOKButtonIfValid();
            }
        }

        private void EnableOKButtonIfValid()
        {
            if (nameTextBox.Text.Trim() == string.Empty ||
                stTextBox.Text.Trim() == string.Empty ||
                cityTextBox.Text.Trim() == string.Empty ||
                abnTextBox.Text.Trim() == string.Empty ||
                postTextBox.Text.Trim() == string.Empty ||
                bNameTextBox.Text.Trim() == string.Empty)
                okButton.Enabled = false;
            else
                okButton.Enabled = true;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            IniFile iniFile = new IniFile();

            if (File.Exists(Constants.DATA_SAVED_FILE))
                iniFile.Load(Constants.DATA_SAVED_FILE);

            string abn = abnTextBox.Text.Replace(" ", "").Trim();

            for (int i = abn.Length - 3; i >= 0; i -= 3)
            {
                abn = abn.Insert(i, " ");
            }

            string newName = nameTextBox.Text.Trim();

            iniFile.SetKeyValue("Owner", "Name", newName);
            iniFile.SetKeyValue("Owner", "BusinessName", bNameTextBox.Text.Trim());
            iniFile.SetKeyValue("Owner", "ABN", abn);
            iniFile.SetKeyValue("Owner", "StreetAddress", stTextBox.Text.Trim());
            iniFile.SetKeyValue("Owner", "State", stateComboBox.SelectedItem.ToString());
            iniFile.SetKeyValue("Owner", "PostCode", postTextBox.Text.Trim());
            iniFile.SetKeyValue("Owner", "City", cityTextBox.Text.Trim());

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

            this.Close();
        }

        private void abnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ' '))
            {
                e.Handled = true;
            }
        }

        private void postTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            EnableOKButtonIfValid();
        }
    }
}
