namespace Driving_Licenses_Managment_Presentation_Layer.Licenses.Local_License
{
    partial class frmPersonLicensesHistory
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
            this.btnClose = new System.Windows.Forms.Button();
            this.picLicenseHisotry = new System.Windows.Forms.PictureBox();
            this.ctrlLicensesHistory1 = new Driving_Licenses_Managment_Presentation_Layer.Licenses.ctrlLicensesHistory();
            this.ctrlPersonInfoWithFillter1 = new Driving_Licenses_Managment_Presentation_Layer.PeoplePresentationLayer.CtrlPersonInfoWithFillter();
            ((System.ComponentModel.ISupportInitialize)(this.picLicenseHisotry)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.Close_321;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1090, 704);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(154, 33);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // picLicenseHisotry
            // 
            this.picLicenseHisotry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLicenseHisotry.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.PersonLicenseHistory_512;
            this.picLicenseHisotry.Location = new System.Drawing.Point(12, 36);
            this.picLicenseHisotry.Name = "picLicenseHisotry";
            this.picLicenseHisotry.Size = new System.Drawing.Size(202, 285);
            this.picLicenseHisotry.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLicenseHisotry.TabIndex = 2;
            this.picLicenseHisotry.TabStop = false;
            // 
            // ctrlLicensesHistory1
            // 
            this.ctrlLicensesHistory1.Location = new System.Drawing.Point(12, 429);
            this.ctrlLicensesHistory1.Name = "ctrlLicensesHistory1";
            this.ctrlLicensesHistory1.Size = new System.Drawing.Size(1055, 308);
            this.ctrlLicensesHistory1.TabIndex = 1;
            // 
            // ctrlPersonInfoWithFillter1
            // 
            this.ctrlPersonInfoWithFillter1.FilterEnabled = true;
            this.ctrlPersonInfoWithFillter1.Location = new System.Drawing.Point(256, 0);
            this.ctrlPersonInfoWithFillter1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ctrlPersonInfoWithFillter1.Name = "ctrlPersonInfoWithFillter1";
            this.ctrlPersonInfoWithFillter1.ShowAddPerson = true;
            this.ctrlPersonInfoWithFillter1.Size = new System.Drawing.Size(994, 421);
            this.ctrlPersonInfoWithFillter1.TabIndex = 0;
            // 
            // frmPersonLicensesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 749);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.picLicenseHisotry);
            this.Controls.Add(this.ctrlLicensesHistory1);
            this.Controls.Add(this.ctrlPersonInfoWithFillter1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPersonLicensesHistory";
            this.Text = "frmPersonLicensesHistory";
            this.Activated += new System.EventHandler(this.frmPersonLicensesHistory_Activated);
            this.Load += new System.EventHandler(this.frmPersonLicensesHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLicenseHisotry)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PeoplePresentationLayer.CtrlPersonInfoWithFillter ctrlPersonInfoWithFillter1;
        private ctrlLicensesHistory ctrlLicensesHistory1;
        private System.Windows.Forms.PictureBox picLicenseHisotry;
        private System.Windows.Forms.Button btnClose;
    }
}