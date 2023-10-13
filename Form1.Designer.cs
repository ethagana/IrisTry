namespace IRISTRY
{
    partial class Form1
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
            this.lblReaderStatus = new DevExpress.XtraEditors.LabelControl();
            this.imgLEye = new DevExpress.XtraEditors.PictureEdit();
            this.imgREye = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cmdID = new DevExpress.XtraEditors.SimpleButton();
            this.cmdEnroll = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.imgLEye.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgREye.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReaderStatus
            // 
            this.lblReaderStatus.Location = new System.Drawing.Point(272, 28);
            this.lblReaderStatus.Name = "lblReaderStatus";
            this.lblReaderStatus.Size = new System.Drawing.Size(69, 13);
            this.lblReaderStatus.TabIndex = 0;
            this.lblReaderStatus.Text = "Reader Status";
            // 
            // imgLEye
            // 
            this.imgLEye.Location = new System.Drawing.Point(12, 64);
            this.imgLEye.Name = "imgLEye";
            this.imgLEye.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.imgLEye.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.imgLEye.Size = new System.Drawing.Size(166, 162);
            this.imgLEye.TabIndex = 1;
            // 
            // imgREye
            // 
            this.imgREye.Location = new System.Drawing.Point(221, 64);
            this.imgREye.Name = "imgREye";
            this.imgREye.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.imgREye.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.imgREye.Size = new System.Drawing.Size(166, 162);
            this.imgREye.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(79, 243);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Left Eye";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(282, 243);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(46, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Right Eye";
            // 
            // cmdID
            // 
            this.cmdID.Location = new System.Drawing.Point(433, 64);
            this.cmdID.Name = "cmdID";
            this.cmdID.Size = new System.Drawing.Size(164, 44);
            this.cmdID.TabIndex = 2;
            this.cmdID.Text = "Identify";
            this.cmdID.Click += new System.EventHandler(this.cmdID_Click);
            // 
            // cmdEnroll
            // 
            this.cmdEnroll.Location = new System.Drawing.Point(433, 114);
            this.cmdEnroll.Name = "cmdEnroll";
            this.cmdEnroll.Size = new System.Drawing.Size(164, 44);
            this.cmdEnroll.TabIndex = 2;
            this.cmdEnroll.Text = "Enroll";
            this.cmdEnroll.Click += new System.EventHandler(this.cmdEnroll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 278);
            this.Controls.Add(this.cmdEnroll);
            this.Controls.Add(this.cmdID);
            this.Controls.Add(this.imgREye);
            this.Controls.Add(this.imgLEye);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lblReaderStatus);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.imgLEye.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgREye.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblReaderStatus;
        private DevExpress.XtraEditors.PictureEdit imgLEye;
        private DevExpress.XtraEditors.PictureEdit imgREye;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton cmdID;
        private DevExpress.XtraEditors.SimpleButton cmdEnroll;

    }
}

