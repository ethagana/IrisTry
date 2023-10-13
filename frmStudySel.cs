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
using MySql.Data.MySqlClient;
using System.Collections;

namespace IRISTRY
{
    public partial class frmStudySel : DevExpress.XtraEditors.XtraForm
    {
        private ArrayList sids = new ArrayList();

        public frmStudySel()
        {
            InitializeComponent();
        }

        internal void loadUUPID(string uupid)
        {
            lblUUPID.Text = uupid;

            loadValidStudies();
        }

        private void frmStudySel_Load(object sender, EventArgs e)
        {
            
        }

        private void loadValidStudies()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT id,study,study_desc,start_date FROM studies "+
                           "WHERE id NOT IN (SELECT sid FROM study_placement WHERE uupid = '') AND DATEDIFF(end_date,CURDATE()) > 0";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@uupid", lblUUPID.Text);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                sids.Clear();

                DataTable tblPart = new DataTable();
                tblPart.Columns.Add("Study Name", typeof(object));
                tblPart.Columns.Add("Study Description", typeof(object));
                tblPart.Columns.Add("Start Date", typeof(object));

                while (mysql_datareader.Read())
                {
                    sids.Add(mysql_datareader["id"]);
                    tblPart.Rows.Add(mysql_datareader["study"], mysql_datareader["study_desc"], mysql_datareader["start_Date"]);
                }

                grdStudies.DataSource = tblPart;

                grdStudies.RefreshDataSource();

                 
            }


            mysql_conn.Close();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            string s_nme = gridView1.GetRowCellValue(e.RowHandle, gridView1.Columns["Study Name"]).ToString();

            DialogResult drslt = XtraMessageBox.Show("Confirm that you have selected "+s_nme+" study?", this.Text,
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (drslt == DialogResult.Yes)
            {
                //Place the participant into the selected study
                placeintoStudy(sids[e.RowHandle].ToString());
                if (GlobalVar.part_det_frm != null)
                {
                    GlobalVar.part_det_frm.setStudy(sids[e.RowHandle].ToString());
                }
                this.Close();
            }
        }

        private void placeintoStudy(string p)
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "INSERT INTO study_placement (uupid,sid,ip) "
                + "VALUES(@uupid,@sid,@ip)";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@uupid", lblUUPID.Text);
            mysql_cmd.Parameters.AddWithValue("@sid", p);
            mysql_cmd.Parameters.AddWithValue("@ip", GlobalVar.sel_IP);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.RecordsAffected > 0)
            {

                XtraMessageBox.Show("Participant Placed into Study Successfully", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                
            }
            else
            {
                XtraMessageBox.Show("Participant Not Placed into Study, Try again later", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            mysql_conn.Close();
        }
    }
}