namespace IRISTRY
{
    partial class frmIPReg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIPReg));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboLogo = new DevExpress.XtraEditors.SimpleButton();
            this.imgLogo = new DevExpress.XtraEditors.PictureEdit();
            this.txtipCNme = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtipLoc = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtipNme = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdClear = new DevExpress.XtraEditors.SimpleButton();
            this.cmdReg = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdRegPrtnr = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ofdSelLogo = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtTel = new DevExpress.XtraEditors.TextEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtipCNme.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtipLoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtipNme.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRegPrtnr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboLogo);
            this.groupBox1.Controls.Add(this.imgLogo);
            this.groupBox1.Controls.Add(this.txtTel);
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.txtipCNme);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.txtipLoc);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.txtipNme);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 333);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Partner Details";
            // 
            // cboLogo
            // 
            this.cboLogo.Location = new System.Drawing.Point(76, 71);
            this.cboLogo.Name = "cboLogo";
            this.cboLogo.Size = new System.Drawing.Size(265, 61);
            this.cboLogo.TabIndex = 3;
            this.cboLogo.Text = "Select Partner Logo";
            this.cboLogo.Click += new System.EventHandler(this.cboLogo_Click);
            // 
            // imgLogo
            // 
            this.imgLogo.Location = new System.Drawing.Point(9, 71);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.imgLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.imgLogo.Size = new System.Drawing.Size(60, 61);
            this.imgLogo.TabIndex = 2;
            // 
            // txtipCNme
            // 
            this.txtipCNme.Location = new System.Drawing.Point(7, 221);
            this.txtipCNme.Name = "txtipCNme";
            this.txtipCNme.Size = new System.Drawing.Size(334, 20);
            this.txtipCNme.TabIndex = 1;
            this.txtipCNme.Leave += new System.EventHandler(this.txtipCNme_Leave);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(7, 201);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(74, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Contact Person";
            // 
            // txtipLoc
            // 
            this.txtipLoc.Location = new System.Drawing.Point(7, 169);
            this.txtipLoc.Name = "txtipLoc";
            this.txtipLoc.Size = new System.Drawing.Size(334, 20);
            this.txtipLoc.TabIndex = 1;
            this.txtipLoc.Leave += new System.EventHandler(this.txtipLoc_Leave);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(7, 149);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Location";
            // 
            // txtipNme
            // 
            this.txtipNme.Location = new System.Drawing.Point(7, 41);
            this.txtipNme.Name = "txtipNme";
            this.txtipNme.Size = new System.Drawing.Size(334, 20);
            this.txtipNme.TabIndex = 1;
            this.txtipNme.Leave += new System.EventHandler(this.txtipNme_Leave);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(7, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(133, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Implementing Partner Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdClear);
            this.groupBox2.Controls.Add(this.cmdReg);
            this.groupBox2.Location = new System.Drawing.Point(12, 352);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(352, 158);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Commands";
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(7, 90);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(334, 54);
            this.cmdClear.TabIndex = 0;
            this.cmdClear.Text = "Clear Text";
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdReg
            // 
            this.cmdReg.Location = new System.Drawing.Point(7, 24);
            this.cmdReg.Name = "cmdReg";
            this.cmdReg.Size = new System.Drawing.Size(334, 54);
            this.cmdReg.TabIndex = 0;
            this.cmdReg.Text = "Register Partner";
            this.cmdReg.Click += new System.EventHandler(this.cmdReg_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdRegPrtnr);
            this.groupBox3.Location = new System.Drawing.Point(372, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(306, 497);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Registered Partners";
            // 
            // grdRegPrtnr
            // 
            this.grdRegPrtnr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRegPrtnr.Location = new System.Drawing.Point(3, 17);
            this.grdRegPrtnr.MainView = this.gridView1;
            this.grdRegPrtnr.Name = "grdRegPrtnr";
            this.grdRegPrtnr.Size = new System.Drawing.Size(300, 477);
            this.grdRegPrtnr.TabIndex = 0;
            this.grdRegPrtnr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdRegPrtnr;
            this.gridView1.Name = "gridView1";
            // 
            // ofdSelLogo
            // 
            this.ofdSelLogo.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(6, 251);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(6, 271);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(170, 20);
            this.txtEmail.TabIndex = 1;
            this.txtEmail.Leave += new System.EventHandler(this.txtipCNme_Leave);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(181, 251);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(14, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Tel";
            // 
            // txtTel
            // 
            this.txtTel.Location = new System.Drawing.Point(181, 271);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(160, 20);
            this.txtTel.TabIndex = 1;
            this.txtTel.Leave += new System.EventHandler(this.txtipCNme_Leave);
            // 
            // frmIPReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 515);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmIPReg";
            this.Text = "Register Implementing Partner";
            this.Load += new System.EventHandler(this.frmIPReg_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtipCNme.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtipLoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtipNme.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRegPrtnr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit txtipNme;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtipCNme;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtipLoc;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton cmdReg;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraGrid.GridControl grdRegPrtnr;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton cmdClear;
        private DevExpress.XtraEditors.SimpleButton cboLogo;
        private DevExpress.XtraEditors.PictureEdit imgLogo;
        private DevExpress.XtraEditors.XtraOpenFileDialog ofdSelLogo;
        private DevExpress.XtraEditors.TextEdit txtTel;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}