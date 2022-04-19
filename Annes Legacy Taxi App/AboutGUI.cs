using System;
using System.Drawing;
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
    public partial class AboutGUI : Form
    {
        // ImageConverter object used to convert byte arrays containing JPEG or PNG file images into 
        //  Bitmap objects. This is static and only gets instantiated once.
        private static readonly ImageConverter _imageConverter = new ImageConverter();

        public AboutGUI()
        {
            InitializeComponent();
            copyrightLabel.Text += Constants.COPYRIGHT_NOTICE;

            Image logo = Utils.ResizeImage(Utils.GetImageFromByteArray(Properties.Resources.CabManagerLogoPNG), 254, 50);
            pictureBox1.Size = logo.Size;
            pictureBox1.Image = logo;
            pictureBox1.Left = (int)((this.ClientSize.Width / 2) - (pictureBox1.Width / 2));
        }
        
        private void AboutGUI_Load(object sender, EventArgs e)
        {
            IniFile iniFile = new IniFile();
            iniFile.Load(Constants.DATA_SAVED_FILE);

            aboutRTFBox.Rtf =   @"{\rtf1\ansi \b Version:\b0\tab\tab\i " + Application.ProductVersion + @" (beta)\i0\par" +
                                @"\b Registered to:\b0\tab\i " + iniFile.GetKeyValue("Owner", "BusinessName") + @"\i0\par";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:rbbr@wideband.net.au");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Constants.WEB_DOMAIN);
        }
    }
}
