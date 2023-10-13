namespace IRISTRY
{
    partial class frmPartDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPartDetails));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblYOB = new DevExpress.XtraEditors.LabelControl();
            this.lblPsedoNme = new DevExpress.XtraEditors.LabelControl();
            this.lblUUPID = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdDelfromStudy = new DevExpress.XtraEditors.SimpleButton();
            this.cmdRegVisit = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdVisits = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblYOB);
            this.groupBox1.Controls.Add(this.lblPsedoNme);
            this.groupBox1.Controls.Add(this.lblUUPID);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(502, 169);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Participant Identifiers";
            // 
            // lblYOB
            // 
            this.lblYOB.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYOB.Appearance.Options.UseFont = true;
            this.lblYOB.Location = new System.Drawing.Point(6, 121);
            this.lblYOB.Name = "lblYOB";
            this.lblYOB.Size = new System.Drawing.Size(182, 33);
            this.lblYOB.TabIndex = 0;
            this.lblYOB.Text = "labelControl1";
            // 
            // lblPsedoNme
            // 
            this.lblPsedoNme.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPsedoNme.Appearance.Options.UseFont = true;
            this.lblPsedoNme.Location = new System.Drawing.Point(6, 71);
            this.lblPsedoNme.Name = "lblPsedoNme";
            this.lblPsedoNme.Size = new System.Drawing.Size(182, 33);
            this.lblPsedoNme.TabIndex = 0;
            this.lblPsedoNme.Text = "labelControl1";
            // 
            // lblUUPID
            // 
            this.lblUUPID.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUUPID.Appearance.Options.UseFont = true;
            this.lblUUPID.Location = new System.Drawing.Point(6, 20);
            this.lblUUPID.Name = "lblUUPID";
            this.lblUUPID.Size = new System.Drawing.Size(182, 33);
            this.lblUUPID.TabIndex = 0;
            this.lblUUPID.Text = "labelControl1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdDelfromStudy);
            this.groupBox2.Controls.Add(this.cmdRegVisit);
            this.groupBox2.Location = new System.Drawing.Point(13, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(502, 95);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // cmdDelfromStudy
            // 
            this.cmdDelfromStudy.Location = new System.Drawing.Point(267, 20);
            this.cmdDelfromStudy.Name = "cmdDelfromStudy";
            this.cmdDelfromStudy.Size = new System.Drawing.Size(229, 61);
            this.cmdDelfromStudy.TabIndex = 0;
            this.cmdDelfromStudy.Text = "Remove from Study";
            // 
            // cmdRegVisit
            // 
            this.cmdRegVisit.Enabled = false;
            this.cmdRegVisit.Location = new System.Drawing.Point(7, 21);
            this.cmdRegVisit.Name = "cmdRegVisit";
            this.cmdRegVisit.Size = new System.Drawing.Size(229, 61);
            this.cmdRegVisit.TabIndex = 0;
            this.cmdRegVisit.Text = "Register Visit";
            this.cmdRegVisit.Click += new System.EventHandler(this.cmdRegVisit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdVisits);
            this.groupBox3.Location = new System.Drawing.Point(13, 290);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(502, 177);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Past Visits";
            // 
            // grdVisits
            // 
            this.grdVisits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdVisits.Location = new System.Drawing.Point(3, 17);
            this.grdVisits.MainView = this.gridView1;
            this.grdVisits.Name = "grdVisits";
            this.grdVisits.Size = new System.Drawing.Size(496, 157);
            this.grdVisits.TabIndex = 0;
            this.grdVisits.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdVisits;
            this.gridView1.Name = "gridView1";
            // 
            // frmPartDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 479);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmPartDetails";
            this.Text = "Participant Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPartDetails_FormClosing);
            this.Shown += new System.EventHandler(this.frmPartDetails_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl lblYOB;
        private DevExpress.XtraEditors.LabelControl lblPsedoNme;
        private DevExpress.XtraEditors.LabelControl lblUUPID;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton cmdRegVisit;
        private DevExpress.XtraEditors.SimpleButton cmdDelfromStudy;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraGrid.GridControl grdVisits;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}