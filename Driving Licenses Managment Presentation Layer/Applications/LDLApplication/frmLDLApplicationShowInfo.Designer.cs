namespace Driving_Licenses_Managment_Presentation_Layer.LDLApplication
{
    partial class frmLDLApplicationShowInfo
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
            this.btnclose = new System.Windows.Forms.Button();
            this.ctrlLDLApplicationInfo1 = new Driving_Licenses_Managment_Presentation_Layer.LDLApplication.ctrlLDLApplicationInfo();
            this.SuspendLayout();
            // 
            // btnclose
            // 
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.Close_32;
            this.btnclose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnclose.Location = new System.Drawing.Point(740, 378);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(128, 35);
            this.btnclose.TabIndex = 1;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // ctrlLDLApplicationInfo1
            // 
            this.ctrlLDLApplicationInfo1.Location = new System.Drawing.Point(36, 32);
            this.ctrlLDLApplicationInfo1.Name = "ctrlLDLApplicationInfo1";
            this.ctrlLDLApplicationInfo1.Size = new System.Drawing.Size(888, 340);
            this.ctrlLDLApplicationInfo1.TabIndex = 0;
            this.ctrlLDLApplicationInfo1.Load += new System.EventHandler(this.ctrlLDLApplicationInfo1_Load);
            // 
            // frmLDLApplicationShowInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 425);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.ctrlLDLApplicationInfo1);
            this.Name = "frmLDLApplicationShowInfo";
            this.Text = "frmLDLApplicationShowInfo";
            this.Load += new System.EventHandler(this.frmLDLApplicationShowInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlLDLApplicationInfo ctrlLDLApplicationInfo1;
        private System.Windows.Forms.Button btnclose;
    }
}