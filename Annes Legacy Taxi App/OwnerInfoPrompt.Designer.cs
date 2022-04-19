namespace Robs_Taxi_Manager
{
    partial class OwnerInfoPrompt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OwnerInfoPrompt));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bNameTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.postTextBox = new System.Windows.Forms.TextBox();
            this.stateComboBox = new System.Windows.Forms.ComboBox();
            this.cityTextBox = new System.Windows.Forms.TextBox();
            this.stTextBox = new System.Windows.Forms.TextBox();
            this.abnTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.bNameTextBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.postTextBox);
            this.groupBox1.Controls.Add(this.stateComboBox);
            this.groupBox1.Controls.Add(this.cityTextBox);
            this.groupBox1.Controls.Add(this.stTextBox);
            this.groupBox1.Controls.Add(this.abnTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.okButton);
            this.groupBox1.Controls.Add(this.nameTextBox);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox1.Location = new System.Drawing.Point(12, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 239);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Owner Information:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(3, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Business Name:";
            // 
            // bNameTextBox
            // 
            this.bNameTextBox.Location = new System.Drawing.Point(96, 51);
            this.bNameTextBox.Name = "bNameTextBox";
            this.bNameTextBox.Size = new System.Drawing.Size(243, 22);
            this.bNameTextBox.TabIndex = 1;
            this.toolTip1.SetToolTip(this.bNameTextBox, "The business name you want to appear on the driver invoices.");
            this.bNameTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(189, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Postcode:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(54, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "State:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(61, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "City:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(6, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Street Address:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(10, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Business ABN:";
            // 
            // postTextBox
            // 
            this.postTextBox.Location = new System.Drawing.Point(252, 163);
            this.postTextBox.Name = "postTextBox";
            this.postTextBox.ShortcutsEnabled = false;
            this.postTextBox.Size = new System.Drawing.Size(87, 22);
            this.postTextBox.TabIndex = 6;
            this.toolTip1.SetToolTip(this.postTextBox, "Your business\'s postcode.\r\n(NOTE: Will appear on invoices.)");
            this.postTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.postTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.postTextBox_KeyPress);
            // 
            // stateComboBox
            // 
            this.stateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stateComboBox.FormattingEnabled = true;
            this.stateComboBox.Items.AddRange(new object[] {
            "ACT",
            "NSW",
            "NT",
            "QLD",
            "SA",
            "TAS",
            "VIC",
            "WA"});
            this.stateComboBox.Location = new System.Drawing.Point(96, 163);
            this.stateComboBox.Name = "stateComboBox";
            this.stateComboBox.Size = new System.Drawing.Size(87, 21);
            this.stateComboBox.TabIndex = 5;
            this.toolTip1.SetToolTip(this.stateComboBox, "The state that your business is in.\r\n(NOTE: Will appear on invoices.)");
            // 
            // cityTextBox
            // 
            this.cityTextBox.Location = new System.Drawing.Point(96, 135);
            this.cityTextBox.Name = "cityTextBox";
            this.cityTextBox.ShortcutsEnabled = false;
            this.cityTextBox.Size = new System.Drawing.Size(243, 22);
            this.cityTextBox.TabIndex = 4;
            this.toolTip1.SetToolTip(this.cityTextBox, "The city that the business is located in.\r\n(NOTE: Will appear on invoices.)");
            this.cityTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // stTextBox
            // 
            this.stTextBox.Location = new System.Drawing.Point(96, 107);
            this.stTextBox.Name = "stTextBox";
            this.stTextBox.ShortcutsEnabled = false;
            this.stTextBox.Size = new System.Drawing.Size(243, 22);
            this.stTextBox.TabIndex = 3;
            this.toolTip1.SetToolTip(this.stTextBox, "Street number and name that the business is registered to.\r\n(NOTE: Will appear on" +
        " invoices.)");
            this.stTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // abnTextBox
            // 
            this.abnTextBox.Location = new System.Drawing.Point(96, 79);
            this.abnTextBox.Name = "abnTextBox";
            this.abnTextBox.ShortcutsEnabled = false;
            this.abnTextBox.Size = new System.Drawing.Size(243, 22);
            this.abnTextBox.TabIndex = 2;
            this.toolTip1.SetToolTip(this.abnTextBox, "The ABN of your taxi business.");
            this.abnTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.abnTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.abnTextBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(29, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Full Name:";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Enabled = false;
            this.okButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.okButton.Location = new System.Drawing.Point(139, 204);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(96, 22);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ShortcutsEnabled = false;
            this.nameTextBox.Size = new System.Drawing.Size(243, 22);
            this.nameTextBox.TabIndex = 0;
            this.toolTip1.SetToolTip(this.nameTextBox, "The name of the owner as a driver.");
            this.nameTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkGreen;
            this.label3.Location = new System.Drawing.Point(22, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(333, 50);
            this.label3.TabIndex = 2;
            this.label3.Text = "Please enter in the information for the\r\nowner of the taxi(s) below";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // OwnerInfoPrompt
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 317);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OwnerInfoPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CabManager";
            this.Load += new System.EventHandler(this.OwnerInfoPrompt_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox postTextBox;
        private System.Windows.Forms.ComboBox stateComboBox;
        private System.Windows.Forms.TextBox cityTextBox;
        private System.Windows.Forms.TextBox stTextBox;
        private System.Windows.Forms.TextBox abnTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox bNameTextBox;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}