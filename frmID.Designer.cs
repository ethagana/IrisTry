namespace IRISTRY
{
    partial class frmID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmID));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtReaderStatus = new DevExpress.XtraEditors.TextEdit();
            this.imgRE = new DevExpress.XtraEditors.PictureEdit();
            this.imgLE = new DevExpress.XtraEditors.PictureEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.prgProgress = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.tmrImgUpdate = new System.Windows.Forms.Timer(this.components);
            this.bgwID = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReaderStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgRE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLE.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prgProgress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtReaderStatus);
            this.groupBox1.Controls.Add(this.imgRE);
            this.groupBox1.Controls.Add(this.imgLE);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IRIS Scans";
            // 
            // txtReaderStatus
            // 
            this.txtReaderStatus.Location = new System.Drawing.Point(7, 124);
            this.txtReaderStatus.Name = "txtReaderStatus";
            this.txtReaderStatus.Size = new System.Drawing.Size(225, 20);
            this.txtReaderStatus.TabIndex = 1;
            // 
            // imgRE
            // 
            this.imgRE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgRE.Location = new System.Drawing.Point(132, 21);
            this.imgRE.Name = "imgRE";
            this.imgRE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.imgRE.Properties.Appearance.Options.UseBackColor = true;
            this.imgRE.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.imgRE.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.imgRE.Size = new System.Drawing.Size(100, 96);
            this.imgRE.TabIndex = 0;
            // 
            // imgLE
            // 
            this.imgLE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgLE.Location = new System.Drawing.Point(7, 21);
            this.imgLE.Name = "imgLE";
            this.imgLE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.imgLE.Properties.Appearance.Options.UseBackColor = true;
            this.imgLE.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.imgLE.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.imgLE.Size = new System.Drawing.Size(100, 96);
            this.imgLE.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.prgProgress);
            this.groupBox2.Controls.Add(this.lblStatus);
            this.groupBox2.Location = new System.Drawing.Point(261, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(317, 160);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Instructions";
            // 
            // prgProgress
            // 
            this.prgProgress.Location = new System.Drawing.Point(14, 114);
            this.prgProgress.Name = "prgProgress";
            this.prgProgress.Properties.ShowTitle = true;
            this.prgProgress.Size = new System.Drawing.Size(292, 30);
            this.prgProgress.TabIndex = 1;
            this.prgProgress.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Appearance.Options.UseFont = true;
            this.lblStatus.Location = new System.Drawing.Point(14, 63);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(292, 29);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Please look into Scanner";
            // 
            // tmrImgUpdate
            // 
            this.tmrImgUpdate.Enabled = true;
            this.tmrImgUpdate.Tick += new System.EventHandler(this.tmrImgUpdate_Tick);
            // 
            // bgwID
            // 
            this.bgwID.WorkerReportsProgress = true;
            this.bgwID.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwID_DoWork);
            this.bgwID.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwID_ProgressChanged);
            this.bgwID.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwID_RunWorkerCompleted);
            // 
            // frmID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 185);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmID";
            this.Text = "Participant Identification";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmID_FormClosing);
            this.Load += new System.EventHandler(this.frmID_Load);
            this.Shown += new System.EventHandler(this.frmID_Shown);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtReaderStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgRE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLE.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prgProgress.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.PictureEdit imgLE;
        private DevExpress.XtraEditors.TextEdit txtReaderStatus;
        private DevExpress.XtraEditors.PictureEdit imgRE;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private System.Windows.Forms.Timer tmrImgUpdate;
        private DevExpress.XtraEditors.ProgressBarControl prgProgress;
        private System.ComponentModel.BackgroundWorker bgwID;
    }
}