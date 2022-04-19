using System;
using System.IO;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robs_Taxi_Manager
{
    public partial class AddDriverGUI : Form
    {
        public AddDriverGUI()
        {
            InitializeComponent();
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
                addButton.Enabled = false;
            else
                addButton.Enabled = true;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // Check to see if the entered name is unique or not.
            IniFile iniFile = new IniFile();
            if (File.Exists(Constants.DATA_SAVED_FILE))
            {
                bool found = false;
                int driverNum = 0;
                iniFile.Load(Constants.DATA_SAVED_FILE);

                for (driverNum = 0; found == false; driverNum++)
                {
                    string driverName = iniFile.GetKeyValue("Driver" + driverNum, "Name");

                    if (driverName == string.Empty)
                        break;

                    if (driverName.ToLower().Trim() == nameTextBox.Text.ToLower().Trim())
                        found = true;
                }

                // If name is already in the list, cancel the "Add" button press and notify user.
                if (found)
                {
                    MessageBox.Show("This driver is already in the drivers list. Please enter another, or cancel.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                }
                else
                {
                    // If name is unique, add it to the driver list.
                    iniFile.SetKeyValue("Driver" + driverNum, "Name", nameTextBox.Text.Trim());
                    iniFile.Save(Constants.DATA_SAVED_FILE);
                }
            }
        }
    }
}
