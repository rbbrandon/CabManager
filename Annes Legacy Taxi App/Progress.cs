//using System;
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
    public partial class ProgressGUI : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;

        public ProgressGUI(string title, string caption, string statusText, bool alwaysOnTop)
        {
            InitializeComponent();
            this.TopMost = alwaysOnTop;
            this.Text = title;
            captionLabel.Text = caption;
            statusLabel.Text = statusText;
        }

        public ProgressGUI(string title, string caption, string statusText) : this(title, caption, statusText, false) { }

        public int Value
        {
            get { return progressBar.Value; }
            
            set
            {
                if (value < progressBar.Minimum)
                    progressBar.Value = progressBar.Minimum;
                else if (value > progressBar.Maximum)
                    progressBar.Value = progressBar.Maximum;
                else
                    progressBar.Value = value;

                this.Invalidate();
            }
        }

        public string CaptionText
        {
            get { return captionLabel.Text; }
            set { captionLabel.Text = value; }
        }

        public string StatusText
        {
            get { return statusLabel.Text; }
            set { statusLabel.Text = value; }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams mdiCp = base.CreateParams;
                mdiCp.ClassStyle = mdiCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return mdiCp;
            }
        }
    }
}
