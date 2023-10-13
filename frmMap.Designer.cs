namespace IRISTRY
{
    partial class frmMap
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mapFacLoc = new DevExpress.XtraMap.MapControl();
            this.imageLayer1 = new DevExpress.XtraMap.ImageLayer();
            this.openStreetMapDataProvider1 = new DevExpress.XtraMap.OpenStreetMapDataProvider();
            this.vectorItemsLayer1 = new DevExpress.XtraMap.VectorItemsLayer();
            this.mapItemStorage1 = new DevExpress.XtraMap.MapItemStorage();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapFacLoc)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.mapFacLoc);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 480);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select the Facility Location";
            // 
            // mapFacLoc
            // 
            this.mapFacLoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapFacLoc.Layers.Add(this.imageLayer1);
            this.mapFacLoc.Layers.Add(this.vectorItemsLayer1);
            this.mapFacLoc.Location = new System.Drawing.Point(3, 17);
            this.mapFacLoc.Name = "mapFacLoc";
            this.mapFacLoc.Size = new System.Drawing.Size(699, 460);
            this.mapFacLoc.TabIndex = 0;
            this.mapFacLoc.MapItemClick += new DevExpress.XtraMap.MapItemClickEventHandler(this.mapFacLoc_MapItemClick);
            this.mapFacLoc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapFacLoc_MouseClick);
            this.mapFacLoc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mapFacLoc_MouseDoubleClick);
            this.imageLayer1.DataProvider = this.openStreetMapDataProvider1;
            this.openStreetMapDataProvider1.TileUriTemplate = "http://{0}.tile.openstreetmaps.org/{1}/{2}/{3}.png";
            this.vectorItemsLayer1.Data = this.mapItemStorage1;
            // 
            // frmMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 504);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMap";
            this.Text = "Map";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mapFacLoc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraMap.MapControl mapFacLoc;
        private DevExpress.XtraMap.ImageLayer imageLayer1;
        private DevExpress.XtraMap.OpenStreetMapDataProvider openStreetMapDataProvider1;
        private DevExpress.XtraMap.VectorItemsLayer vectorItemsLayer1;
        private DevExpress.XtraMap.MapItemStorage mapItemStorage1;
    }
}