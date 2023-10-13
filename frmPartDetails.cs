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
using DevExpress.XtraGrid.Columns;

namespace IRISTRY
{
    public partial class frmPartDetails : DevExpress.XtraEditors.XtraForm
    {
        private string sid;

        public frmPartDetails()
        {
            InitializeComponent();
        }

        internal void getDetails(string uupid, string psdo_nme, string p_dob, string p_pob)
        {
            lblPsedoNme.Text = psdo_nme;
            lblUUPID.Text = uupid;
            lblYOB.Text = p_dob + " - " + p_pob;
        }

        internal void setStudy(string ssid)
        {
            sid = ssid;
            cmdRegVisit.Enabled = true;
        }

        private void frmPartDetails_Shown(object sender, EventArgs e)
        {
            checkifinStudy(); //Check if the participant is in any study

            this.BringToFront();
        }

        private void checkifinStudy()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM study_placement WHERE uupid = @uupid";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@uupid", lblUUPID.Text);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                cmdRegVisit.Enabled = true;

                loadVisits();
                
            }
            else
            {
                //Place participant into a study
                DialogResult drslt = XtraMessageBox.Show("Place Participant into a Study?", this.Text,
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (drslt == DialogResult.Yes)
                {
                    //Open the study placement form
                    frmStudySel sel_study_frm = new frmStudySel();
                    sel_study_frm.loadUUPID(lblUUPID.Text);
                    sel_study_frm.ShowDialog();
                }
            }

            mysql_conn.Close();
        }

        private void loadVisits()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT visits.`dov`, programs.`prog_name`, studies.`study`, facilities.facility FROM visits "+
                            "JOIN programs ON visits.`sid` = programs.`id` "+
                            "JOIN studies ON visits.`study` = studies.`id` "+
                            "JOIN facilities ON visits.fac_id = facilities.id "+
                            "WHERE visits.`uupid` = @uupid ORDER BY visits.dov DESC";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@uupid", lblUUPID.Text);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                DataTable tblVisits = new DataTable();
                tblVisits.Columns.Add("Date of Visit", typeof(DateTime));
                tblVisits.Columns.Add("Partner Visited", typeof(string));
                tblVisits.Columns.Add("Facility Visited", typeof(string));
                tblVisits.Columns.Add("Study", typeof(string));

                while (mysql_datareader.Read())
                {
                    tblVisits.Rows.Add(Convert.ToDateTime(mysql_datareader["dov"].ToString()),
                        mysql_datareader["prog_name"].ToString(),mysql_datareader["facility"].ToString(),mysql_datareader["study"].ToString());
                }

                grdVisits.DataSource = tblVisits;

                //Sort the Data
                GridColumn grddov = gridView1.Columns["Date of Visit"];
                GridColumn grdprog = gridView1.Columns["Partner Visited"];

                grddov.GroupIndex = 1;
                grdprog.GroupIndex = 2;

                grddov.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

                grdVisits.RefreshDataSource();

            }

            mysql_conn.Close();
        }

        private void frmPartDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVar.part_det_frm = null;
        }

        private void cmdRegVisit_Click(object sender, EventArgs e)
        {
            //Register the visit 
            RegVisits();
        }

        private void RegVisits()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "INSERT INTO visits (uupid,sid,study,dov,fac_id) VALUES(@uupid,@sid,@study,@dov,@fac)";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@uupid", lblUUPID.Text);
            mysql_cmd.Parameters.AddWithValue("@sid", GlobalVar.sel_IP);
            mysql_cmd.Parameters.AddWithValue("@study", GlobalVar.sel_study_id);
            mysql_cmd.Parameters.AddWithValue("@dov", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            mysql_cmd.Parameters.AddWithValue("@fac", GlobalVar.sel_IP_fac);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.RecordsAffected > 0)
            {

                XtraMessageBox.Show("Visit Registered", this.Text,
                          MessageBoxButtons.OK, MessageBoxIcon.Information);

                loadVisits();
            }

            mysql_conn.Close();
        }
    }
}