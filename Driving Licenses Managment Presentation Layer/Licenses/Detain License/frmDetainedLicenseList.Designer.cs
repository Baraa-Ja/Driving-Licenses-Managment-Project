namespace Driving_Licenses_Managment_Presentation_Layer.Detain
{
    partial class frmDetainedLicenseList
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtFillter = new System.Windows.Forms.TextBox();
            this.cbDetainedLicenseFillter = new System.Windows.Forms.ComboBox();
            this.lblRecordsNumber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDetainedLicense = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPersonDetailesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseDetailesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseDetainedLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMode = new System.Windows.Forms.Label();
            this.picRelease = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.picDetaine = new System.Windows.Forms.PictureBox();
            this.cbIsReleased = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicense)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRelease)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDetaine)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtFillter
            // 
            this.txtFillter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFillter.Location = new System.Drawing.Point(338, 243);
            this.txtFillter.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txtFillter.Multiline = true;
            this.txtFillter.Name = "txtFillter";
            this.txtFillter.Size = new System.Drawing.Size(229, 27);
            this.txtFillter.TabIndex = 20;
            this.txtFillter.Visible = false;
            this.txtFillter.TextChanged += new System.EventHandler(this.txtFillter_TextChanged);
            this.txtFillter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFillter_KeyPress);
            // 
            // cbDetainedLicenseFillter
            // 
            this.cbDetainedLicenseFillter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDetainedLicenseFillter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDetainedLicenseFillter.FormattingEnabled = true;
            this.cbDetainedLicenseFillter.Items.AddRange(new object[] {
            "None",
            "Detain ID",
            "Is Released",
            "National Number",
            "Full Name",
            "Release Application ID"});
            this.cbDetainedLicenseFillter.Location = new System.Drawing.Point(128, 244);
            this.cbDetainedLicenseFillter.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.cbDetainedLicenseFillter.Name = "cbDetainedLicenseFillter";
            this.cbDetainedLicenseFillter.Size = new System.Drawing.Size(186, 26);
            this.cbDetainedLicenseFillter.TabIndex = 19;
            this.cbDetainedLicenseFillter.SelectedIndexChanged += new System.EventHandler(this.cbLDLApplicationsFillter_SelectedIndexChanged);
            // 
            // lblRecordsNumber
            // 
            this.lblRecordsNumber.AutoSize = true;
            this.lblRecordsNumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsNumber.Location = new System.Drawing.Point(148, 641);
            this.lblRecordsNumber.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblRecordsNumber.Name = "lblRecordsNumber";
            this.lblRecordsNumber.Size = new System.Drawing.Size(0, 16);
            this.lblRecordsNumber.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 641);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "# Records: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 243);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Fillter By: ";
            // 
            // dgvDetainedLicense
            // 
            this.dgvDetainedLicense.AllowUserToAddRows = false;
            this.dgvDetainedLicense.AllowUserToDeleteRows = false;
            this.dgvDetainedLicense.AllowUserToOrderColumns = true;
            this.dgvDetainedLicense.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetainedLicense.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDetainedLicense.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetainedLicense.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvDetainedLicense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetainedLicense.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvDetainedLicense.GridColor = System.Drawing.Color.Gray;
            this.dgvDetainedLicense.Location = new System.Drawing.Point(-1, 283);
            this.dgvDetainedLicense.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.dgvDetainedLicense.Name = "dgvDetainedLicense";
            this.dgvDetainedLicense.ReadOnly = true;
            this.dgvDetainedLicense.Size = new System.Drawing.Size(1062, 342);
            this.dgvDetainedLicense.TabIndex = 12;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonDetailesToolStripMenuItem,
            this.showLicenseDetailesToolStripMenuItem,
            this.showPersonLicenseHistoryToolStripMenuItem,
            this.releaseDetainedLicenseToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(273, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // showPersonDetailesToolStripMenuItem
            // 
            this.showPersonDetailesToolStripMenuItem.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.PersonDetails_321;
            this.showPersonDetailesToolStripMenuItem.Name = "showPersonDetailesToolStripMenuItem";
            this.showPersonDetailesToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.showPersonDetailesToolStripMenuItem.Text = "Show Person Detailes";
            this.showPersonDetailesToolStripMenuItem.Click += new System.EventHandler(this.showPersonDetailesToolStripMenuItem_Click);
            // 
            // showLicenseDetailesToolStripMenuItem
            // 
            this.showLicenseDetailesToolStripMenuItem.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.License_View_321;
            this.showLicenseDetailesToolStripMenuItem.Name = "showLicenseDetailesToolStripMenuItem";
            this.showLicenseDetailesToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.showLicenseDetailesToolStripMenuItem.Text = "Show License Detailes";
            this.showLicenseDetailesToolStripMenuItem.Click += new System.EventHandler(this.showLicenseDetailesToolStripMenuItem_Click);
            // 
            // showPersonLicenseHistoryToolStripMenuItem
            // 
            this.showPersonLicenseHistoryToolStripMenuItem.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.License_View_321;
            this.showPersonLicenseHistoryToolStripMenuItem.Name = "showPersonLicenseHistoryToolStripMenuItem";
            this.showPersonLicenseHistoryToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.showPersonLicenseHistoryToolStripMenuItem.Text = "Show Person License History";
            this.showPersonLicenseHistoryToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicenseHistoryToolStripMenuItem_Click);
            // 
            // releaseDetainedLicenseToolStripMenuItem
            // 
            this.releaseDetainedLicenseToolStripMenuItem.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.Release_Detained_License_322;
            this.releaseDetainedLicenseToolStripMenuItem.Name = "releaseDetainedLicenseToolStripMenuItem";
            this.releaseDetainedLicenseToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.releaseDetainedLicenseToolStripMenuItem.Text = "Release Detained License";
            this.releaseDetainedLicenseToolStripMenuItem.Click += new System.EventHandler(this.releaseDetainedLicenseToolStripMenuItem_Click);
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Maroon;
            this.lblMode.Location = new System.Drawing.Point(400, 160);
            this.lblMode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(342, 37);
            this.lblMode.TabIndex = 13;
            this.lblMode.Text = "Detained License List";
            // 
            // picRelease
            // 
            this.picRelease.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picRelease.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picRelease.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.Release_Detained_License_512;
            this.picRelease.Location = new System.Drawing.Point(846, 214);
            this.picRelease.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.picRelease.Name = "picRelease";
            this.picRelease.Size = new System.Drawing.Size(84, 63);
            this.picRelease.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picRelease.TabIndex = 21;
            this.picRelease.TabStop = false;
            this.picRelease.Click += new System.EventHandler(this.picRelease_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.Detain_512;
            this.pictureBox1.Location = new System.Drawing.Point(416, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(314, 136);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(853, 632);
            this.btnClose.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(185, 36);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // picDetaine
            // 
            this.picDetaine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picDetaine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDetaine.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.Detain_321;
            this.picDetaine.Location = new System.Drawing.Point(955, 214);
            this.picDetaine.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.picDetaine.Name = "picDetaine";
            this.picDetaine.Size = new System.Drawing.Size(83, 63);
            this.picDetaine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDetaine.TabIndex = 15;
            this.picDetaine.TabStop = false;
            this.picDetaine.Click += new System.EventHandler(this.picDetaine_Click);
            // 
            // cbIsReleased
            // 
            this.cbIsReleased.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsReleased.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIsReleased.FormattingEnabled = true;
            this.cbIsReleased.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbIsReleased.Location = new System.Drawing.Point(338, 243);
            this.cbIsReleased.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.cbIsReleased.Name = "cbIsReleased";
            this.cbIsReleased.Size = new System.Drawing.Size(110, 26);
            this.cbIsReleased.TabIndex = 22;
            this.cbIsReleased.Visible = false;
            this.cbIsReleased.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // frmDetainedLicenseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 670);
            this.Controls.Add(this.cbIsReleased);
            this.Controls.Add(this.picRelease);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtFillter);
            this.Controls.Add(this.cbDetainedLicenseFillter);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecordsNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picDetaine);
            this.Controls.Add(this.dgvDetainedLicense);
            this.Controls.Add(this.lblMode);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Name = "frmDetainedLicenseList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDetainedLicenseList";
            this.Load += new System.EventHandler(this.frmDetainedLicenseList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicense)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picRelease)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDetaine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtFillter;
        private System.Windows.Forms.ComboBox cbDetainedLicenseFillter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecordsNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picDetaine;
        private System.Windows.Forms.DataGridView dgvDetainedLicense;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.PictureBox picRelease;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showPersonDetailesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseDetailesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseDetainedLicenseToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbIsReleased;
    }
}