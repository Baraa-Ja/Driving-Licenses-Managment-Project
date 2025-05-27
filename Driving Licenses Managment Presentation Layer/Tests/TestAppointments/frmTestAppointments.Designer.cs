namespace Driving_Licenses_Managment_Presentation_Layer.TestAppointments
{
    partial class frmTestAppointments
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
            this.btnCLose = new System.Windows.Forms.Button();
            this.ctrlTestAppointments2 = new Driving_Licenses_Managment_Presentation_Layer.TestAppointments.ctrlTestAppointments();
            this.ctrlTestAppointments1 = new Driving_Licenses_Managment_Presentation_Layer.TestAppointments.ctrlTestAppointments();
            this.SuspendLayout();
            // 
            // btnCLose
            // 
            this.btnCLose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCLose.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.Close_32;
            this.btnCLose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCLose.Location = new System.Drawing.Point(410, 682);
            this.btnCLose.Name = "btnCLose";
            this.btnCLose.Size = new System.Drawing.Size(125, 36);
            this.btnCLose.TabIndex = 1;
            this.btnCLose.Text = "Close";
            this.btnCLose.UseVisualStyleBackColor = true;
            this.btnCLose.Click += new System.EventHandler(this.btnCLose_Click);
            // 
            // ctrlTestAppointments2
            // 
            this.ctrlTestAppointments2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlTestAppointments2.Location = new System.Drawing.Point(12, 2);
            this.ctrlTestAppointments2.Name = "ctrlTestAppointments2";
            this.ctrlTestAppointments2.Size = new System.Drawing.Size(886, 674);
            this.ctrlTestAppointments2.TabIndex = 2;
            // 
            // ctrlTestAppointments1
            // 
            this.ctrlTestAppointments1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlTestAppointments1.Location = new System.Drawing.Point(12, 1);
            this.ctrlTestAppointments1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlTestAppointments1.Name = "ctrlTestAppointments1";
            this.ctrlTestAppointments1.Size = new System.Drawing.Size(912, 703);
            this.ctrlTestAppointments1.TabIndex = 0;
            this.ctrlTestAppointments1.Load += new System.EventHandler(this.ctrlTestAppointments1_Load);
            // 
            // frmTestAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 722);
            this.Controls.Add(this.ctrlTestAppointments2);
            this.Controls.Add(this.btnCLose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTestAppointments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmVisionTestAppointments1";
            this.Load += new System.EventHandler(this.frmTestAppointments_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlTestAppointments ctrlTestAppointments1;
        private System.Windows.Forms.Button btnCLose;
        private ctrlTestAppointments ctrlTestAppointments2;
    }
}