using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraMap;

namespace IRISTRY
{
    public partial class frmMap : DevExpress.XtraEditors.XtraForm
    {
        private string fac_nme;
        public frmMap()
        {
            InitializeComponent();
        }

        internal void place_on_Map(string addrs, string loc, string poc)
        {
            mapItemStorage1.Items.Clear();

            fac_nme = poc;

            string[] latlon = loc.Split(',');
            mapFacLoc.CenterPoint = new GeoPoint(Double.Parse(latlon[0]), Double.Parse(latlon[1]));
            mapFacLoc.ZoomLevel = 16;

            //var customElement = new MapCustomElement() { Location = new GeoPoint(Double.Parse(latlon[0]),Double.Parse(latlon[1]))
            //    , Text = addrs_1 };

            mapItemStorage1.Items.Add(new MapPushpin()
            {
                Location = new GeoPoint(Double.Parse(latlon[0]), Double.Parse(latlon[1]))
                ,
                Text = poc,
                ToolTipPattern = poc + "\n" + addrs
            });
        }

        private void mapFacLoc_MapItemClick(object sender, MapItemClickEventArgs e)
        {
            
        }

        private void mapFacLoc_MouseClick(object sender, MouseEventArgs e)
        {
            
            

        }

        private void mapFacLoc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point mapPoint = e.Location;
            GeoPoint geoPoint = (GeoPoint)mapFacLoc.ScreenPointToCoordPoint(mapPoint);

            DialogResult drslt = XtraMessageBox.Show("Confirm this is the Location of the Facility " + fac_nme, this.Text,
                          MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (drslt == DialogResult.Yes)
            {
                mapItemStorage1.Items.Add(new MapPushpin()
                {
                    Location = geoPoint
                    ,
                    Text = fac_nme
                });


                GlobalVar.fac_reg_frm.setFacGPS(geoPoint.Latitude, geoPoint.Longitude);

                XtraMessageBox.Show("You can close this Form Now", this.Text,
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }
    }
}