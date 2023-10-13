namespace IRISTRY
{
    partial class frmUsrReg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsrReg));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtNmes = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtStaffID = new DevExpress.XtraEditors.TextEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdEnroll = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmdRegUsr = new DevExpress.XtraEditors.SimpleButton();
            this.imgIRIS = new DevExpress.XtraEditors.PictureEdit();
            this.lblReaderStatus = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNmes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStaffID.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIRIS.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtStaffID);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.txtNmes);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 177);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Details";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "User Name";
            // 
            // txtNmes
            // 
            this.txtNmes.Location = new System.Drawing.Point(16, 41);
            this.txtNmes.Name = "txtNmes";
            this.txtNmes.Size = new System.Drawing.Size(283, 20);
            this.txtNmes.TabIndex = 1;
            this.txtNmes.Leave += new System.EventHandler(this.txtNmes_Leave);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 69);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(16, 89);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(283, 20);
            this.txtEmail.TabIndex = 1;
            this.txtEmail.Leave += new System.EventHandler(this.txtEmail_Leave);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 118);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(38, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Staff ID";
            // 
            // txtStaffID
            // 
            this.txtStaffID.Location = new System.Drawing.Point(16, 138);
            this.txtStaffID.Name = "txtStaffID";
            this.txtStaffID.Size = new System.Drawing.Size(283, 20);
            this.txtStaffID.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.imgIRIS);
            this.groupBox2.Controls.Add(this.lblReaderStatus);
            this.groupBox2.Controls.Add(this.cmdEnroll);
            this.groupBox2.Location = new System.Drawing.Point(13, 197);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "User Biometrics";
            // 
            // cmdEnroll
            // 
            this.cmdEnroll.Location = new System.Drawing.Point(110, 18);
            this.cmdEnroll.Name = "cmdEnroll";
            this.cmdEnroll.Size = new System.Drawing.Size(220, 43);
            this.cmdEnroll.TabIndex = 0;
            this.cmdEnroll.Text = "Enroll User IRIS";
            this.cmdEnroll.Click += new System.EventHandler(this.cmdEnroll_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmdRegUsr);
            this.groupBox3.Location = new System.Drawing.Point(13, 304);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(348, 100);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Commands";
            // 
            // cmdRegUsr
            // 
            this.cmdRegUsr.Location = new System.Drawing.Point(16, 29);
            this.cmdRegUsr.Name = "cmdRegUsr";
            this.cmdRegUsr.Size = new System.Drawing.Size(326, 52);
            this.cmdRegUsr.TabIndex = 0;
            this.cmdRegUsr.Text = "Register User";
            this.cmdRegUsr.Click += new System.EventHandler(this.cmdRegUsr_Click);
            // 
            // imgIRIS
            // 
            this.imgIRIS.EditValue = global::IRISTRY.Properties.Resources.iris_icon;
            this.imgIRIS.Location = new System.Drawing.Point(16, 20);
            this.imgIRIS.Name = "imgIRIS";
            this.imgIRIS.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.imgIRIS.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.imgIRIS.Size = new System.Drawing.Size(76, 64);
            this.imgIRIS.TabIndex = 1;
            // 
            // lblReaderStatus
            // 
            this.lblReaderStatus.Location = new System.Drawing.Point(192, 67);
            this.lblReaderStatus.Name = "lblReaderStatus";
            this.lblReaderStatus.Size = new System.Drawing.Size(0, 13);
            this.lblReaderStatus.TabIndex = 0;
            // 
            // frmUsrReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 415);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUsrReg";
            this.Text = "User Registration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUsrReg_FormClosing);
            this.Load += new System.EventHandler(this.frmUsrReg_Load);
            this.Shown += new System.EventHandler(this.frmUsrReg_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNmes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStaffID.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgIRIS.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit txtNmes;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtStaffID;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.PictureEdit imgIRIS;
        private DevExpress.XtraEditors.SimpleButton cmdEnroll;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.SimpleButton cmdRegUsr;
        private DevExpress.XtraEditors.LabelControl lblReaderStatus;
    }
}