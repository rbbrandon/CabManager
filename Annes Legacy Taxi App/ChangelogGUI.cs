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
    public partial class ChangelogGUI : Form
    {
        public ChangelogGUI()
        {
            InitializeComponent();
        }

        private void ChangelogGUI_Load(object sender, EventArgs e)
        {
            try
            {
                richTextBox.LoadFile(Constants.APP_PATH + "\\Changelog.rtf");
            }
            catch
            {

            }
        }
    }
}
