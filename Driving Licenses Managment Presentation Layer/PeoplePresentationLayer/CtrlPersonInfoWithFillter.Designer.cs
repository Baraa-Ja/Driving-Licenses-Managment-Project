namespace Driving_Licenses_Managment_Presentation_Layer.PeoplePresentationLayer
{
    partial class CtrlPersonInfoWithFillter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFillterPersonInfo = new System.Windows.Forms.ComboBox();
            this.gbFillter = new System.Windows.Forms.GroupBox();
            this.btnAddNewPerson = new System.Windows.Forms.Button();
            this.btnFindPerson = new System.Windows.Forms.Button();
            this.txtFillter = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ctrlPersonCard1 = new Driving_Licenses_Managment_Presentation_Layer.ctrlPersonCard();
            this.gbFillter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find By: ";
            // 
            // cbFillterPersonInfo
            // 
            this.cbFillterPersonInfo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFillterPersonInfo.DropDownWidth = 210;
            this.cbFillterPersonInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFillterPersonInfo.FormattingEnabled = true;
            this.cbFillterPersonInfo.Items.AddRange(new object[] {
            "National No",
            "Person ID"});
            this.cbFillterPersonInfo.Location = new System.Drawing.Point(100, 30);
            this.cbFillterPersonInfo.Name = "cbFillterPersonInfo";
            this.cbFillterPersonInfo.Size = new System.Drawing.Size(214, 28);
            this.cbFillterPersonInfo.TabIndex = 4;
            this.cbFillterPersonInfo.SelectedIndexChanged += new System.EventHandler(this.cbFillterPersonInfo_SelectedIndexChanged);
            // 
            // gbFillter
            // 
            this.gbFillter.Controls.Add(this.label1);
            this.gbFillter.Controls.Add(this.btnAddNewPerson);
            this.gbFillter.Controls.Add(this.cbFillterPersonInfo);
            this.gbFillter.Controls.Add(this.btnFindPerson);
            this.gbFillter.Controls.Add(this.txtFillter);
            this.gbFillter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFillter.Location = new System.Drawing.Point(22, 3);
            this.gbFillter.Name = "gbFillter";
            this.gbFillter.Size = new System.Drawing.Size(828, 71);
            this.gbFillter.TabIndex = 7;
            this.gbFillter.TabStop = false;
            this.gbFillter.Text = "Fillter";
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.BackColor = System.Drawing.Color.White;
            this.btnAddNewPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddNewPerson.Cursor = System.Windows.Forms.Cursors.Cross;
            this.btnAddNewPerson.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.AddPerson_32;
            this.btnAddNewPerson.Location = new System.Drawing.Point(654, 21);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.Size = new System.Drawing.Size(44, 37);
            this.btnAddNewPerson.TabIndex = 6;
            this.btnAddNewPerson.UseVisualStyleBackColor = false;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnAddNewPerson_Click);
            // 
            // btnFindPerson
            // 
            this.btnFindPerson.BackColor = System.Drawing.Color.White;
            this.btnFindPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFindPerson.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnFindPerson.Image = global::Driving_Licenses_Managment_Presentation_Layer.Properties.Resources.SearchPerson;
            this.btnFindPerson.Location = new System.Drawing.Point(583, 21);
            this.btnFindPerson.Name = "btnFindPerson";
            this.btnFindPerson.Size = new System.Drawing.Size(44, 37);
            this.btnFindPerson.TabIndex = 5;
            this.btnFindPerson.UseVisualStyleBackColor = false;
            this.btnFindPerson.Click += new System.EventHandler(this.btnFindPerson_Click);
            // 
            // txtFillter
            // 
            this.txtFillter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFillter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFillter.Location = new System.Drawing.Point(346, 30);
            this.txtFillter.Multiline = true;
            this.txtFillter.Name = "txtFillter";
            this.txtFillter.Size = new System.Drawing.Size(214, 26);
            this.txtFillter.TabIndex = 3;
            this.txtFillter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFillter_KeyPress);
            this.txtFillter.Validating += new System.ComponentModel.CancelEventHandler(this._ValidateEmptyTextBox);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ctrlPersonCard1
            // 
            this.ctrlPersonCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlPersonCard1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlPersonCard1.Location = new System.Drawing.Point(22, 80);
            this.ctrlPersonCard1.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlPersonCard1.Name = "ctrlPersonCard1";
            this.ctrlPersonCard1.Size = new System.Drawing.Size(828, 339);
            this.ctrlPersonCard1.TabIndex = 0;
            // 
            // CtrlPersonInfoWithFillter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFillter);
            this.Controls.Add(this.ctrlPersonCard1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CtrlPersonInfoWithFillter";
            this.Size = new System.Drawing.Size(870, 428);
            this.Load += new System.EventHandler(this.CtrlPersonInfoWithFillter_Load);
            this.gbFillter.ResumeLayout(false);
            this.gbFillter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPersonCard ctrlPersonCard1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFillterPersonInfo;
        private System.Windows.Forms.Button btnFindPerson;
        private System.Windows.Forms.Button btnAddNewPerson;
        private System.Windows.Forms.GroupBox gbFillter;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtFillter;
    }
}
