namespace IRISTRY
{
    partial class frmStudyReg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStudyReg));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dteEnd = new DevExpress.XtraEditors.DateEdit();
            this.dteStart = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDesc = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtStudyName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdClearText = new DevExpress.XtraEditors.SimpleButton();
            this.cmdReg = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdStudies = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cboFacs = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStudyName.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdStudies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFacs.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboFacs);
            this.groupBox1.Controls.Add(this.dteEnd);
            this.groupBox1.Controls.Add(this.dteStart);
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.txtDesc);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.txtStudyName);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 334);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Study Details";
            // 
            // dteEnd
            // 
            this.dteEnd.EditValue = null;
            this.dteEnd.Location = new System.Drawing.Point(7, 226);
            this.dteEnd.Name = "dteEnd";
            this.dteEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEnd.Size = new System.Drawing.Size(278, 20);
            this.dteEnd.TabIndex = 3;
            // 
            // dteStart
            // 
            this.dteStart.EditValue = null;
            this.dteStart.Location = new System.Drawing.Point(8, 174);
            this.dteStart.Name = "dteStart";
            this.dteStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStart.Size = new System.Drawing.Size(278, 20);
            this.dteStart.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(7, 206);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(44, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "End Date";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(8, 92);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Properties.MaxLength = 250;
            this.txtDesc.Size = new System.Drawing.Size(278, 47);
            this.txtDesc.TabIndex = 2;
            this.txtDesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDesc_KeyPress);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 154);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(50, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Start Date";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(7, 73);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Description";
            // 
            // txtStudyName
            // 
            this.txtStudyName.Location = new System.Drawing.Point(8, 44);
            this.txtStudyName.Name = "txtStudyName";
            this.txtStudyName.Properties.MaxLength = 100;
            this.txtStudyName.Size = new System.Drawing.Size(278, 20);
            this.txtStudyName.TabIndex = 1;
            this.txtStudyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStudyName_KeyPress);
            this.txtStudyName.Leave += new System.EventHandler(this.txtStudyName_Leave);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(7, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Study Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdClearText);
            this.groupBox2.Controls.Add(this.cmdReg);
            this.groupBox2.Location = new System.Drawing.Point(13, 353);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 135);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Commands";
            // 
            // cmdClearText
            // 
            this.cmdClearText.Location = new System.Drawing.Point(6, 77);
            this.cmdClearText.Name = "cmdClearText";
            this.cmdClearText.Size = new System.Drawing.Size(287, 40);
            this.cmdClearText.TabIndex = 0;
            this.cmdClearText.Text = "Clear Text";
            // 
            // cmdReg
            // 
            this.cmdReg.Location = new System.Drawing.Point(6, 31);
            this.cmdReg.Name = "cmdReg";
            this.cmdReg.Size = new System.Drawing.Size(287, 40);
            this.cmdReg.TabIndex = 0;
            this.cmdReg.Text = "Register Study";
            this.cmdReg.Click += new System.EventHandler(this.cmdReg_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdStudies);
            this.groupBox3.Location = new System.Drawing.Point(320, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(261, 478);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Studies";
            // 
            // grdStudies
            // 
            this.grdStudies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdStudies.Location = new System.Drawing.Point(3, 17);
            this.grdStudies.MainView = this.gridView1;
            this.grdStudies.Name = "grdStudies";
            this.grdStudies.Size = new System.Drawing.Size(255, 458);
            this.grdStudies.TabIndex = 0;
            this.grdStudies.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdStudies;
            this.gridView1.Name = "gridView1";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(8, 258);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(102, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Participating Facilities";
            // 
            // cboFacs
            // 
            this.cboFacs.Location = new System.Drawing.Point(8, 278);
            this.cboFacs.Name = "cboFacs";
            this.cboFacs.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFacs.Size = new System.Drawing.Size(277, 20);
            this.cboFacs.TabIndex = 4;
            this.cboFacs.EditValueChanged += new System.EventHandler(this.cboFacs_EditValueChanged);
            // 
            // frmStudyReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 503);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmStudyReg";
            this.Text = "Study Registration";
            this.Load += new System.EventHandler(this.frmStudyReg_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStudyName.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdStudies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFacs.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dteEnd;
        private DevExpress.XtraEditors.DateEdit dteStart;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.MemoEdit txtDesc;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtStudyName;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton cmdClearText;
        private DevExpress.XtraEditors.SimpleButton cmdReg;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraGrid.GridControl grdStudies;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cboFacs;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}