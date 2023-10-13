namespace IRISTRY
{
    partial class frmPartReg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPartReg));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblUUPID = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdPart = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtPOB = new DevExpress.XtraEditors.TextEdit();
            this.dteDOB = new DevExpress.XtraEditors.DateEdit();
            this.txtPartNmes = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmdEnroll = new DevExpress.XtraEditors.SimpleButton();
            this.imgIRIS = new DevExpress.XtraEditors.PictureEdit();
            this.txtReaderStatus = new DevExpress.XtraEditors.TextEdit();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmdClearText = new DevExpress.XtraEditors.SimpleButton();
            this.cmdRegPart = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOB.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPartNmes.Properties)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIRIS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReaderStatus.Properties)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblUUPID);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unique Universal Participant ID";
            // 
            // lblUUPID
            // 
            this.lblUUPID.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUUPID.Appearance.Options.UseFont = true;
            this.lblUUPID.Location = new System.Drawing.Point(6, 38);
            this.lblUUPID.Name = "lblUUPID";
            this.lblUUPID.Size = new System.Drawing.Size(63, 23);
            this.lblUUPID.TabIndex = 0;
            this.lblUUPID.Text = "UUPID";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdPart);
            this.groupBox2.Location = new System.Drawing.Point(383, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 596);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Participants";
            // 
            // grdPart
            // 
            this.grdPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPart.Location = new System.Drawing.Point(3, 17);
            this.grdPart.MainView = this.gridView1;
            this.grdPart.Name = "grdPart";
            this.grdPart.Size = new System.Drawing.Size(266, 576);
            this.grdPart.TabIndex = 0;
            this.grdPart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdPart;
            this.gridView1.Name = "gridView1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtPOB);
            this.groupBox3.Controls.Add(this.dteDOB);
            this.groupBox3.Controls.Add(this.txtPartNmes);
            this.groupBox3.Controls.Add(this.labelControl3);
            this.groupBox3.Controls.Add(this.labelControl2);
            this.groupBox3.Controls.Add(this.labelControl1);
            this.groupBox3.Location = new System.Drawing.Point(13, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(364, 196);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Participant Details";
            // 
            // txtPOB
            // 
            this.txtPOB.Location = new System.Drawing.Point(9, 151);
            this.txtPOB.Name = "txtPOB";
            this.txtPOB.Size = new System.Drawing.Size(349, 20);
            this.txtPOB.TabIndex = 3;
            // 
            // dteDOB
            // 
            this.dteDOB.EditValue = null;
            this.dteDOB.Location = new System.Drawing.Point(8, 99);
            this.dteDOB.Name = "dteDOB";
            this.dteDOB.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDOB.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDOB.Size = new System.Drawing.Size(350, 20);
            this.dteDOB.TabIndex = 2;
            // 
            // txtPartNmes
            // 
            this.txtPartNmes.Location = new System.Drawing.Point(8, 50);
            this.txtPartNmes.Name = "txtPartNmes";
            this.txtPartNmes.Properties.ReadOnly = true;
            this.txtPartNmes.Size = new System.Drawing.Size(350, 20);
            this.txtPartNmes.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 131);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(63, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Place of Birth";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 79);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(61, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Date of Birth";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(173, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Participant Names - Auto Generated";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmdEnroll);
            this.groupBox4.Controls.Add(this.imgIRIS);
            this.groupBox4.Controls.Add(this.txtReaderStatus);
            this.groupBox4.Location = new System.Drawing.Point(13, 323);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(364, 131);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Biometrics";
            // 
            // cmdEnroll
            // 
            this.cmdEnroll.Location = new System.Drawing.Point(115, 21);
            this.cmdEnroll.Name = "cmdEnroll";
            this.cmdEnroll.Size = new System.Drawing.Size(243, 70);
            this.cmdEnroll.TabIndex = 2;
            this.cmdEnroll.Text = "Enroll Biometrics";
            this.cmdEnroll.Click += new System.EventHandler(this.cmdEnroll_Click);
            // 
            // imgIRIS
            // 
            this.imgIRIS.Location = new System.Drawing.Point(9, 21);
            this.imgIRIS.Name = "imgIRIS";
            this.imgIRIS.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.imgIRIS.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.imgIRIS.Size = new System.Drawing.Size(100, 96);
            this.imgIRIS.TabIndex = 0;
            // 
            // txtReaderStatus
            // 
            this.txtReaderStatus.Location = new System.Drawing.Point(115, 97);
            this.txtReaderStatus.Name = "txtReaderStatus";
            this.txtReaderStatus.Properties.ReadOnly = true;
            this.txtReaderStatus.Size = new System.Drawing.Size(243, 20);
            this.txtReaderStatus.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmdClearText);
            this.groupBox5.Controls.Add(this.cmdRegPart);
            this.groupBox5.Location = new System.Drawing.Point(13, 461);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(364, 148);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Commands";
            // 
            // cmdClearText
            // 
            this.cmdClearText.Location = new System.Drawing.Point(3, 82);
            this.cmdClearText.Name = "cmdClearText";
            this.cmdClearText.Size = new System.Drawing.Size(355, 54);
            this.cmdClearText.TabIndex = 0;
            this.cmdClearText.Text = "Clear Text";
            // 
            // cmdRegPart
            // 
            this.cmdRegPart.Location = new System.Drawing.Point(3, 17);
            this.cmdRegPart.Name = "cmdRegPart";
            this.cmdRegPart.Size = new System.Drawing.Size(355, 54);
            this.cmdRegPart.TabIndex = 0;
            this.cmdRegPart.Text = "Register Participant";
            this.cmdRegPart.Click += new System.EventHandler(this.cmdRegPart_Click);
            // 
            // frmPartReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 621);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmPartReg";
            this.Text = "Participant Registration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPartReg_FormClosing);
            this.Shown += new System.EventHandler(this.frmPartReg_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOB.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDOB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPartNmes.Properties)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgIRIS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReaderStatus.Properties)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl lblUUPID;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl grdPart;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.TextEdit txtPOB;
        private DevExpress.XtraEditors.DateEdit dteDOB;
        private DevExpress.XtraEditors.TextEdit txtPartNmes;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraEditors.SimpleButton cmdEnroll;
        private DevExpress.XtraEditors.PictureEdit imgIRIS;
        private DevExpress.XtraEditors.TextEdit txtReaderStatus;
        private System.Windows.Forms.GroupBox groupBox5;
        private DevExpress.XtraEditors.SimpleButton cmdClearText;
        private DevExpress.XtraEditors.SimpleButton cmdRegPart;
    }
}