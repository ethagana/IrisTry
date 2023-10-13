namespace IRISTRY
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.registerUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerImplementingPartnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFacilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdID = new DevExpress.XtraEditors.SimpleButton();
            this.cmdRegStudy = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSelStudy = new DevExpress.XtraEditors.TextEdit();
            this.cmdSelStudy = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.flyoutPanel1 = new DevExpress.Utils.FlyoutPanel();
            this.fpcStudySel = new DevExpress.Utils.FlyoutPanelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdStudies = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tmrSynchData = new System.Windows.Forms.Timer(this.components);
            this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.bgwGetFacility = new System.ComponentModel.BackgroundWorker();
            this.bgwRunSync = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelStudy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanel1)).BeginInit();
            this.flyoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpcStudySel)).BeginInit();
            this.fpcStudySel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdStudies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registerUserToolStripMenuItem,
            this.registerImplementingPartnerToolStripMenuItem,
            this.addFacilitiesToolStripMenuItem,
            this.logOffToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(661, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // registerUserToolStripMenuItem
            // 
            this.registerUserToolStripMenuItem.Name = "registerUserToolStripMenuItem";
            this.registerUserToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.registerUserToolStripMenuItem.Text = "Register User";
            this.registerUserToolStripMenuItem.Click += new System.EventHandler(this.registerUserToolStripMenuItem_Click);
            // 
            // registerImplementingPartnerToolStripMenuItem
            // 
            this.registerImplementingPartnerToolStripMenuItem.Name = "registerImplementingPartnerToolStripMenuItem";
            this.registerImplementingPartnerToolStripMenuItem.Size = new System.Drawing.Size(180, 20);
            this.registerImplementingPartnerToolStripMenuItem.Text = "Register Implementing Partner";
            this.registerImplementingPartnerToolStripMenuItem.Click += new System.EventHandler(this.registerImplementingPartnerToolStripMenuItem_Click);
            // 
            // addFacilitiesToolStripMenuItem
            // 
            this.addFacilitiesToolStripMenuItem.Name = "addFacilitiesToolStripMenuItem";
            this.addFacilitiesToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.addFacilitiesToolStripMenuItem.Text = "Add Facilities";
            this.addFacilitiesToolStripMenuItem.Click += new System.EventHandler(this.addFacilitiesToolStripMenuItem_Click);
            // 
            // logOffToolStripMenuItem
            // 
            this.logOffToolStripMenuItem.Name = "logOffToolStripMenuItem";
            this.logOffToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.logOffToolStripMenuItem.Text = "Log Off";
            this.logOffToolStripMenuItem.Click += new System.EventHandler(this.logOffToolStripMenuItem_Click);
            // 
            // cmdID
            // 
            this.cmdID.Enabled = false;
            this.cmdID.Location = new System.Drawing.Point(12, 106);
            this.cmdID.Name = "cmdID";
            this.cmdID.Size = new System.Drawing.Size(314, 53);
            this.cmdID.TabIndex = 4;
            this.cmdID.Text = "Identify Participant";
            this.cmdID.Click += new System.EventHandler(this.cmdID_Click);
            // 
            // cmdRegStudy
            // 
            this.cmdRegStudy.Location = new System.Drawing.Point(332, 106);
            this.cmdRegStudy.Name = "cmdRegStudy";
            this.cmdRegStudy.Size = new System.Drawing.Size(317, 53);
            this.cmdRegStudy.TabIndex = 4;
            this.cmdRegStudy.Text = "Register Study";
            this.cmdRegStudy.Click += new System.EventHandler(this.cmdRegStudy_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(98, 49);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Study Selected";
            // 
            // txtSelStudy
            // 
            this.txtSelStudy.Location = new System.Drawing.Point(176, 51);
            this.txtSelStudy.Name = "txtSelStudy";
            this.txtSelStudy.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSelStudy.Properties.Appearance.Options.UseFont = true;
            this.txtSelStudy.Properties.ReadOnly = true;
            this.txtSelStudy.Size = new System.Drawing.Size(315, 26);
            this.txtSelStudy.TabIndex = 6;
            this.txtSelStudy.TextChanged += new System.EventHandler(this.txtSelStudy_TextChanged);
            // 
            // cmdSelStudy
            // 
            this.cmdSelStudy.Location = new System.Drawing.Point(509, 49);
            this.cmdSelStudy.Name = "cmdSelStudy";
            this.cmdSelStudy.Size = new System.Drawing.Size(140, 28);
            this.cmdSelStudy.TabIndex = 7;
            this.cmdSelStudy.Text = "Select Study";
            this.cmdSelStudy.Click += new System.EventHandler(this.cmdSelStudy_Click);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit1.EditValue = global::IRISTRY.Properties.Resources.MSM_Corn_logo;
            this.pictureEdit1.Location = new System.Drawing.Point(12, 179);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.DarkGray;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.Size = new System.Drawing.Size(637, 45);
            this.pictureEdit1.TabIndex = 2;
            // 
            // flyoutPanel1
            // 
            this.flyoutPanel1.Controls.Add(this.fpcStudySel);
            this.flyoutPanel1.Location = new System.Drawing.Point(265, 27);
            this.flyoutPanel1.Name = "flyoutPanel1";
            this.flyoutPanel1.OwnerControl = this.pictureEdit1;
            this.flyoutPanel1.Size = new System.Drawing.Size(194, 197);
            this.flyoutPanel1.TabIndex = 8;
            // 
            // fpcStudySel
            // 
            this.fpcStudySel.Controls.Add(this.groupBox1);
            this.fpcStudySel.Controls.Add(this.labelControl2);
            this.fpcStudySel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpcStudySel.FlyoutPanel = this.flyoutPanel1;
            this.fpcStudySel.Location = new System.Drawing.Point(0, 0);
            this.fpcStudySel.Name = "fpcStudySel";
            this.fpcStudySel.Size = new System.Drawing.Size(194, 197);
            this.fpcStudySel.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdStudies);
            this.groupBox1.Location = new System.Drawing.Point(6, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 157);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Valid Studies";
            // 
            // grdStudies
            // 
            this.grdStudies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdStudies.Location = new System.Drawing.Point(3, 17);
            this.grdStudies.MainView = this.gridView1;
            this.grdStudies.Name = "grdStudies";
            this.grdStudies.Size = new System.Drawing.Size(177, 137);
            this.grdStudies.TabIndex = 0;
            this.grdStudies.ToolTipController = this.toolTipController1;
            this.grdStudies.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdStudies;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            // 
            // toolTipController1
            // 
            this.toolTipController1.AppearanceTitle.Image = ((System.Drawing.Image)(resources.GetObject("toolTipController1.AppearanceTitle.Image")));
            this.toolTipController1.AppearanceTitle.Options.UseImage = true;
            this.toolTipController1.ShowBeak = true;
            this.toolTipController1.ToolTipAnchor = DevExpress.Utils.ToolTipAnchor.Cursor;
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController1_GetActiveObjectInfo);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(55, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Select a Study";
            // 
            // tmrSynchData
            // 
            this.tmrSynchData.Enabled = true;
            this.tmrSynchData.Interval = 30000;
            this.tmrSynchData.Tick += new System.EventHandler(this.tmrSynchData_Tick);
            // 
            // alertControl1
            // 
            this.alertControl1.FormLocation = DevExpress.XtraBars.Alerter.AlertFormLocation.TopLeft;
            // 
            // bgwGetFacility
            // 
            this.bgwGetFacility.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwGetFacility_DoWork);
            // 
            // bgwRunSync
            // 
            this.bgwRunSync.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwRunSync_DoWork);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 232);
            this.Controls.Add(this.flyoutPanel1);
            this.Controls.Add(this.cmdSelStudy);
            this.Controls.Add(this.txtSelStudy);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmdRegStudy);
            this.Controls.Add(this.cmdID);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Integrated Study Participant Identity Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelStudy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanel1)).EndInit();
            this.flyoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpcStudySel)).EndInit();
            this.fpcStudySel.ResumeLayout(false);
            this.fpcStudySel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdStudies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem registerUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerImplementingPartnerToolStripMenuItem;
        private DevExpress.XtraEditors.SimpleButton cmdID;
        private DevExpress.XtraEditors.SimpleButton cmdRegStudy;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSelStudy;
        private DevExpress.XtraEditors.SimpleButton cmdSelStudy;
        private DevExpress.Utils.FlyoutPanel flyoutPanel1;
        private DevExpress.Utils.FlyoutPanelControl fpcStudySel;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl grdStudies;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private System.Windows.Forms.ToolStripMenuItem logOffToolStripMenuItem;
        private System.Windows.Forms.Timer tmrSynchData;
        private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
        private System.Windows.Forms.ToolStripMenuItem addFacilitiesToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bgwGetFacility;
        private System.ComponentModel.BackgroundWorker bgwRunSync;
    }
}