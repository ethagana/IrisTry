namespace IRISTRY
{
    partial class frmFacReg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFacReg));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboIPs = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtCon2 = new DevExpress.XtraEditors.TextEdit();
            this.txtCon1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtLoc = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtFNme = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdClear = new DevExpress.XtraEditors.SimpleButton();
            this.cmdReg = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdFacilities = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bgwGetLoc = new System.ComponentModel.BackgroundWorker();
            this.txtCP1Email = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtCP1Tel = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtCP2Email = new DevExpress.XtraEditors.TextEdit();
            this.txtCP2Tel = new DevExpress.XtraEditors.TextEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboIPs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCon2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCon1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFNme.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFacilities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCP1Email.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCP1Tel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCP2Email.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCP2Tel.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboIPs);
            this.groupBox1.Controls.Add(this.txtCon2);
            this.groupBox1.Controls.Add(this.txtCP2Tel);
            this.groupBox1.Controls.Add(this.txtCP2Email);
            this.groupBox1.Controls.Add(this.txtCP1Tel);
            this.groupBox1.Controls.Add(this.txtCP1Email);
            this.groupBox1.Controls.Add(this.txtCon1);
            this.groupBox1.Controls.Add(this.labelControl9);
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.labelControl7);
            this.groupBox1.Controls.Add(this.labelControl8);
            this.groupBox1.Controls.Add(this.txtLoc);
            this.groupBox1.Controls.Add(this.labelControl6);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.txtFNme);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 415);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Facility Details";
            // 
            // cboIPs
            // 
            this.cboIPs.Location = new System.Drawing.Point(7, 50);
            this.cboIPs.Name = "cboIPs";
            this.cboIPs.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboIPs.Size = new System.Drawing.Size(311, 20);
            this.cboIPs.TabIndex = 3;
            // 
            // txtCon2
            // 
            this.txtCon2.Location = new System.Drawing.Point(10, 317);
            this.txtCon2.Name = "txtCon2";
            this.txtCon2.Size = new System.Drawing.Size(309, 20);
            this.txtCon2.TabIndex = 2;
            this.txtCon2.Leave += new System.EventHandler(this.txtCon2_Leave);
            // 
            // txtCon1
            // 
            this.txtCon1.Location = new System.Drawing.Point(9, 212);
            this.txtCon1.Name = "txtCon1";
            this.txtCon1.Size = new System.Drawing.Size(309, 20);
            this.txtCon1.TabIndex = 2;
            this.txtCon1.Leave += new System.EventHandler(this.txtCon1_Leave);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(10, 297);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(83, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Contact Person 2";
            // 
            // txtLoc
            // 
            this.txtLoc.Location = new System.Drawing.Point(9, 157);
            this.txtLoc.Name = "txtLoc";
            this.txtLoc.Size = new System.Drawing.Size(309, 20);
            this.txtLoc.TabIndex = 2;
            this.txtLoc.Leave += new System.EventHandler(this.txtLoc_Leave);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(9, 192);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(74, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Contact Person";
            // 
            // txtFNme
            // 
            this.txtFNme.Location = new System.Drawing.Point(10, 104);
            this.txtFNme.Name = "txtFNme";
            this.txtFNme.Size = new System.Drawing.Size(309, 20);
            this.txtFNme.TabIndex = 2;
            this.txtFNme.Leave += new System.EventHandler(this.txtFNme_Leave);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 137);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(111, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Location - City or Town";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 84);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Facility Name";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(103, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Implementing Partner";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdClear);
            this.groupBox2.Controls.Add(this.cmdReg);
            this.groupBox2.Location = new System.Drawing.Point(12, 434);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(345, 137);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Commands";
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(10, 79);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(309, 52);
            this.cmdClear.TabIndex = 0;
            this.cmdClear.Text = "Clear Text";
            // 
            // cmdReg
            // 
            this.cmdReg.Location = new System.Drawing.Point(10, 21);
            this.cmdReg.Name = "cmdReg";
            this.cmdReg.Size = new System.Drawing.Size(309, 52);
            this.cmdReg.TabIndex = 0;
            this.cmdReg.Text = "Register Facility";
            this.cmdReg.Click += new System.EventHandler(this.cmdReg_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdFacilities);
            this.groupBox3.Location = new System.Drawing.Point(365, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(290, 558);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Facilities";
            // 
            // grdFacilities
            // 
            this.grdFacilities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFacilities.Location = new System.Drawing.Point(3, 17);
            this.grdFacilities.MainView = this.gridView1;
            this.grdFacilities.Name = "grdFacilities";
            this.grdFacilities.Size = new System.Drawing.Size(284, 538);
            this.grdFacilities.TabIndex = 0;
            this.grdFacilities.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdFacilities;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            // 
            // bgwGetLoc
            // 
            this.bgwGetLoc.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwGetLoc_DoWork);
            this.bgwGetLoc.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwGetLoc_RunWorkerCompleted);
            // 
            // txtCP1Email
            // 
            this.txtCP1Email.Location = new System.Drawing.Point(10, 262);
            this.txtCP1Email.Name = "txtCP1Email";
            this.txtCP1Email.Size = new System.Drawing.Size(147, 20);
            this.txtCP1Email.TabIndex = 2;
            this.txtCP1Email.Leave += new System.EventHandler(this.txtCon1_Leave);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(10, 242);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 13);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "Email";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(163, 242);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(14, 13);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "Tel";
            // 
            // txtCP1Tel
            // 
            this.txtCP1Tel.Location = new System.Drawing.Point(163, 262);
            this.txtCP1Tel.Name = "txtCP1Tel";
            this.txtCP1Tel.Size = new System.Drawing.Size(156, 20);
            this.txtCP1Tel.TabIndex = 2;
            this.txtCP1Tel.Leave += new System.EventHandler(this.txtCon1_Leave);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(10, 349);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(24, 13);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "Email";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(163, 349);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(14, 13);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "Tel";
            // 
            // txtCP2Email
            // 
            this.txtCP2Email.Location = new System.Drawing.Point(10, 369);
            this.txtCP2Email.Name = "txtCP2Email";
            this.txtCP2Email.Size = new System.Drawing.Size(147, 20);
            this.txtCP2Email.TabIndex = 2;
            this.txtCP2Email.Leave += new System.EventHandler(this.txtCon1_Leave);
            // 
            // txtCP2Tel
            // 
            this.txtCP2Tel.Location = new System.Drawing.Point(163, 369);
            this.txtCP2Tel.Name = "txtCP2Tel";
            this.txtCP2Tel.Size = new System.Drawing.Size(156, 20);
            this.txtCP2Tel.TabIndex = 2;
            this.txtCP2Tel.Leave += new System.EventHandler(this.txtCon1_Leave);
            // 
            // frmFacReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 583);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmFacReg";
            this.Text = "Facility Registration";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFacReg_FormClosed);
            this.Load += new System.EventHandler(this.frmFacReg_Load);
            this.Shown += new System.EventHandler(this.frmFacReg_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboIPs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCon2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCon1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFNme.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFacilities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCP1Email.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCP1Tel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCP2Email.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCP2Tel.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCon2;
        private DevExpress.XtraEditors.TextEdit txtCon1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtLoc;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtFNme;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton cmdClear;
        private DevExpress.XtraEditors.SimpleButton cmdReg;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraGrid.GridControl grdFacilities;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cboIPs;
        private System.ComponentModel.BackgroundWorker bgwGetLoc;
        private DevExpress.XtraEditors.TextEdit txtCP2Tel;
        private DevExpress.XtraEditors.TextEdit txtCP2Email;
        private DevExpress.XtraEditors.TextEdit txtCP1Tel;
        private DevExpress.XtraEditors.TextEdit txtCP1Email;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}