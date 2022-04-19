namespace Robs_Taxi_Manager
{
    partial class MainGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGUI));
            this.selectMonthLabel = new System.Windows.Forms.Label();
            this.monthCombo = new System.Windows.Forms.ComboBox();
            this.yearCombo = new System.Windows.Forms.ComboBox();
            this.selectDriverLabel = new System.Windows.Forms.Label();
            this.driverCombo = new System.Windows.Forms.ComboBox();
            this.addDriverButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.generateButton = new System.Windows.Forms.Button();
            this.optionsGroup = new System.Windows.Forms.GroupBox();
            this.editLink = new System.Windows.Forms.LinkLabel();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.registerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewOwnerDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.changelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.registeredLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.updateLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.levyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsGroup.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // selectMonthLabel
            // 
            this.selectMonthLabel.AutoSize = true;
            this.selectMonthLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectMonthLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.selectMonthLabel.Location = new System.Drawing.Point(48, 25);
            this.selectMonthLabel.Name = "selectMonthLabel";
            this.selectMonthLabel.Size = new System.Drawing.Size(133, 13);
            this.selectMonthLabel.TabIndex = 1;
            this.selectMonthLabel.Text = "Please Select the Month:";
            // 
            // monthCombo
            // 
            this.monthCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthCombo.FormattingEnabled = true;
            this.monthCombo.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.monthCombo.Location = new System.Drawing.Point(188, 22);
            this.monthCombo.Name = "monthCombo";
            this.monthCombo.Size = new System.Drawing.Size(165, 21);
            this.monthCombo.TabIndex = 2;
            this.monthCombo.SelectedIndexChanged += new System.EventHandler(this.monthCombo_SelectedIndexChanged);
            // 
            // yearCombo
            // 
            this.yearCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.yearCombo.FormattingEnabled = true;
            this.yearCombo.Location = new System.Drawing.Point(359, 22);
            this.yearCombo.Name = "yearCombo";
            this.yearCombo.Size = new System.Drawing.Size(77, 21);
            this.yearCombo.TabIndex = 3;
            this.yearCombo.SelectedIndexChanged += new System.EventHandler(this.yearCombo_SelectedIndexChanged);
            // 
            // selectDriverLabel
            // 
            this.selectDriverLabel.AutoSize = true;
            this.selectDriverLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectDriverLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.selectDriverLabel.Location = new System.Drawing.Point(64, 52);
            this.selectDriverLabel.Name = "selectDriverLabel";
            this.selectDriverLabel.Size = new System.Drawing.Size(117, 13);
            this.selectDriverLabel.TabIndex = 4;
            this.selectDriverLabel.Text = "Please Select a Driver:";
            // 
            // driverCombo
            // 
            this.driverCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.driverCombo.FormattingEnabled = true;
            this.driverCombo.Items.AddRange(new object[] {
            "All",
            "Expenses"});
            this.driverCombo.Location = new System.Drawing.Point(188, 49);
            this.driverCombo.Name = "driverCombo";
            this.driverCombo.Size = new System.Drawing.Size(248, 21);
            this.driverCombo.TabIndex = 5;
            this.driverCombo.SelectedIndexChanged += new System.EventHandler(this.driverCombo_SelectedIndexChanged);
            // 
            // addDriverButton
            // 
            this.addDriverButton.Location = new System.Drawing.Point(13, 220);
            this.addDriverButton.Name = "addDriverButton";
            this.addDriverButton.Size = new System.Drawing.Size(140, 23);
            this.addDriverButton.TabIndex = 6;
            this.addDriverButton.Text = "Add Driver";
            this.addDriverButton.UseVisualStyleBackColor = true;
            this.addDriverButton.Click += new System.EventHandler(this.addDriverButton_Click);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(186, 220);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(140, 23);
            this.editButton.TabIndex = 7;
            this.editButton.Text = "Edit Spreadsheet";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(359, 220);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(140, 23);
            this.generateButton.TabIndex = 8;
            this.generateButton.Text = "Generate Invoice";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // optionsGroup
            // 
            this.optionsGroup.Controls.Add(this.editLink);
            this.optionsGroup.Controls.Add(this.selectMonthLabel);
            this.optionsGroup.Controls.Add(this.monthCombo);
            this.optionsGroup.Controls.Add(this.yearCombo);
            this.optionsGroup.Controls.Add(this.selectDriverLabel);
            this.optionsGroup.Controls.Add(this.driverCombo);
            this.optionsGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionsGroup.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.optionsGroup.Location = new System.Drawing.Point(13, 126);
            this.optionsGroup.Name = "optionsGroup";
            this.optionsGroup.Size = new System.Drawing.Size(486, 80);
            this.optionsGroup.TabIndex = 9;
            this.optionsGroup.TabStop = false;
            this.optionsGroup.Text = "Please manage your taxi by using the below options:";
            // 
            // editLink
            // 
            this.editLink.AutoSize = true;
            this.editLink.Enabled = false;
            this.editLink.Font = new System.Drawing.Font("Wingdings", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.editLink.Location = new System.Drawing.Point(439, 52);
            this.editLink.Name = "editLink";
            this.editLink.Size = new System.Drawing.Size(23, 16);
            this.editLink.TabIndex = 6;
            this.editLink.TabStop = true;
            this.editLink.Text = "!";
            this.toolTip1.SetToolTip(this.editLink, "Edit current driver\'s details.");
            this.editLink.Visible = false;
            this.editLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.editLink_LinkClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // registerToolStripMenuItem
            // 
            this.registerToolStripMenuItem.Checked = true;
            this.registerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.registerToolStripMenuItem.Enabled = false;
            this.registerToolStripMenuItem.Name = "registerToolStripMenuItem";
            this.registerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.registerToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.registerToolStripMenuItem.Text = "&Register";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            // 
            // helpToolStripMenuItem2
            // 
            this.helpToolStripMenuItem2.Name = "helpToolStripMenuItem2";
            this.helpToolStripMenuItem2.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpToolStripMenuItem2.Size = new System.Drawing.Size(157, 22);
            this.helpToolStripMenuItem2.Text = "&Manual";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // closeToolStripMenuItem1
            // 
            this.closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
            this.closeToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.closeToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem1.Text = "&Close";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "&Help";
            // 
            // registerToolStripMenuItem1
            // 
            this.registerToolStripMenuItem1.Name = "registerToolStripMenuItem1";
            this.registerToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.registerToolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
            this.registerToolStripMenuItem1.Text = "&Register";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
            this.aboutToolStripMenuItem1.Text = "&About";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem2,
            this.helpToolStripMenuItem3});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(510, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem2
            // 
            this.fileToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.viewOwnerDetailsToolStripMenuItem,
            this.levyToolStripMenuItem,
            this.toolStripMenuItem1,
            this.closeToolStripMenuItem2});
            this.fileToolStripMenuItem2.Name = "fileToolStripMenuItem2";
            this.fileToolStripMenuItem2.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem2.Text = "&File";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for &Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // viewOwnerDetailsToolStripMenuItem
            // 
            this.viewOwnerDetailsToolStripMenuItem.Name = "viewOwnerDetailsToolStripMenuItem";
            this.viewOwnerDetailsToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.viewOwnerDetailsToolStripMenuItem.Text = "&Edit Owner Details";
            this.viewOwnerDetailsToolStripMenuItem.Click += new System.EventHandler(this.viewOwnerDetailsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(221, 6);
            // 
            // closeToolStripMenuItem2
            // 
            this.closeToolStripMenuItem2.Name = "closeToolStripMenuItem2";
            this.closeToolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.closeToolStripMenuItem2.Size = new System.Drawing.Size(224, 22);
            this.closeToolStripMenuItem2.Text = "&Close";
            this.closeToolStripMenuItem2.Click += new System.EventHandler(this.closeToolStripMenuItem2_Click);
            // 
            // helpToolStripMenuItem3
            // 
            this.helpToolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changelogToolStripMenuItem,
            this.aboutToolStripMenuItem2});
            this.helpToolStripMenuItem3.Name = "helpToolStripMenuItem3";
            this.helpToolStripMenuItem3.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem3.Text = "&Help";
            // 
            // changelogToolStripMenuItem
            // 
            this.changelogToolStripMenuItem.Name = "changelogToolStripMenuItem";
            this.changelogToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.changelogToolStripMenuItem.Text = "Change&log";
            this.changelogToolStripMenuItem.Click += new System.EventHandler(this.changelogToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem2
            // 
            this.aboutToolStripMenuItem2.Name = "aboutToolStripMenuItem2";
            this.aboutToolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.aboutToolStripMenuItem2.Size = new System.Drawing.Size(149, 22);
            this.aboutToolStripMenuItem2.Text = "&About";
            this.aboutToolStripMenuItem2.Click += new System.EventHandler(this.aboutToolStripMenuItem2_Click);
            // 
            // registeredLabel
            // 
            this.registeredLabel.AutoSize = true;
            this.registeredLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.registeredLabel.Location = new System.Drawing.Point(290, 246);
            this.registeredLabel.Name = "registeredLabel";
            this.registeredLabel.Size = new System.Drawing.Size(220, 13);
            this.registeredLabel.TabIndex = 11;
            this.registeredLabel.Text = "Registered Taxi Owner: <Not Registered>";
            this.registeredLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // updateLabel
            // 
            this.updateLabel.AutoSize = true;
            this.updateLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.updateLabel.Location = new System.Drawing.Point(10, 246);
            this.updateLabel.Name = "updateLabel";
            this.updateLabel.Size = new System.Drawing.Size(133, 13);
            this.updateLabel.TabIndex = 12;
            this.updateLabel.Text = "(Checking for updates...)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(13, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(485, 92);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // levyToolStripMenuItem
            // 
            this.levyToolStripMenuItem.Name = "levyToolStripMenuItem";
            this.levyToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.levyToolStripMenuItem.Text = "Apply &Levy Update to Sheets";
            this.levyToolStripMenuItem.Click += new System.EventHandler(this.levyToolStripMenuItem_Click);
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 264);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.updateLabel);
            this.Controls.Add(this.registeredLabel);
            this.Controls.Add(this.optionsGroup);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.addDriverButton);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CabManager - ";
            this.Load += new System.EventHandler(this.MainGUI_Load);
            this.optionsGroup.ResumeLayout(false);
            this.optionsGroup.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label selectMonthLabel;
        private System.Windows.Forms.ComboBox monthCombo;
        private System.Windows.Forms.ComboBox yearCombo;
        private System.Windows.Forms.Label selectDriverLabel;
        private System.Windows.Forms.ComboBox driverCombo;
        private System.Windows.Forms.Button addDriverButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.GroupBox optionsGroup;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem registerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem2;
        private System.Windows.Forms.Label registeredLabel;
        private System.Windows.Forms.ToolStripMenuItem changelogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewOwnerDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.LinkLabel editLink;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.Label updateLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem levyToolStripMenuItem;
    }
}

