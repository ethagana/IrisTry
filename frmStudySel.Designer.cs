namespace IRISTRY
{
    partial class frmStudySel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStudySel));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblUUPID = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdStudies = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdStudies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblUUPID);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Participant to Place into Study";
            // 
            // lblUUPID
            // 
            this.lblUUPID.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUUPID.Appearance.Options.UseFont = true;
            this.lblUUPID.Location = new System.Drawing.Point(6, 32);
            this.lblUUPID.Name = "lblUUPID";
            this.lblUUPID.Size = new System.Drawing.Size(63, 23);
            this.lblUUPID.TabIndex = 0;
            this.lblUUPID.Text = "UUPID";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdStudies);
            this.groupBox2.Location = new System.Drawing.Point(13, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 334);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select study to place participant in";
            // 
            // grdStudies
            // 
            this.grdStudies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdStudies.Location = new System.Drawing.Point(3, 17);
            this.grdStudies.MainView = this.gridView1;
            this.grdStudies.Name = "grdStudies";
            this.grdStudies.Size = new System.Drawing.Size(320, 314);
            this.grdStudies.TabIndex = 0;
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
            // frmStudySel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 466);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmStudySel";
            this.Text = "Study Selection";
            this.Load += new System.EventHandler(this.frmStudySel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdStudies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl lblUUPID;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl grdStudies;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}