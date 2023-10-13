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
using System.IO;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;

namespace IRISTRY
{
    public partial class frmFacReg : DevExpress.XtraEditors.XtraForm
    {
        private static string pid;
        private static double[] gps = new double[2];
        private ArrayList pids = new ArrayList();
       
        public frmFacReg()
        {
            InitializeComponent();
        }

        internal void setIP_ID(string id)
        {
            pid = id;
        }

        internal void setFacGPS(double lat, double lon)
        {
            gps[0] = lat;
            gps[1] = lon;

            txtLoc.Text += "("+lat.ToString() +","+ lon.ToString()+")";
        }

        private void frmFacReg_Load(object sender, EventArgs e)
        {
            getFacilities();
        }

        private void getFacilities()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT programs.`logo`, programs.`prog_name`, facilities.`facility` "+
                           "FROM programs JOIN facilities ON programs.`id` = facilities.`prog_id`";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                DataTable tblProgs = new DataTable();
                tblProgs.Columns.Add("Logo", typeof(Image));
                tblProgs.Columns.Add("Implementing Partner", typeof(string));
                tblProgs.Columns.Add("Facilities", typeof(string));

                
                while (mysql_datareader.Read())
                {
                    tblProgs.Rows.Add(hextoImage(mysql_datareader["logo"].ToString()), 
                        mysql_datareader["prog_name"].ToString(),mysql_datareader["facility"].ToString());
                    

                }

                grdFacilities.DataSource = tblProgs;

                grdFacilities.RefreshDataSource();


            }


            mysql_conn.Close();
        }

        private Image hextoImage(String heximg)
        {
            Image image = IRISTRY.Properties.Resources.MSM_Corn_logo;
            try
            {
                byte[] imageBytes = Convert.FromBase64String(heximg);
                MemoryStream ms = new MemoryStream(imageBytes, 0,
                  imageBytes.Length);

                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                image = Image.FromStream(ms, true);
            }
            catch { }
            return image;
        }

        private void frmFacReg_Shown(object sender, EventArgs e)
        {
            getIPs();
        }

        private void getIPs()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM programs";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                
                cboIPs.Properties.Items.Clear();
                ImageCollection imgCol = new ImageCollection();
                int pos = 0;
                pids.Clear();

                while (mysql_datareader.Read())
                {
                    pids.Add(mysql_datareader["id"]);
                    imgCol.AddImage(hextoImage(mysql_datareader["logo"].ToString()));
                    cboIPs.Properties.Items.Add(new ImageComboBoxItem(mysql_datareader["prog_name"].ToString(),pos));
                    pos++;

                }

                cboIPs.Properties.LargeImages = imgCol;

                if (pid != null)
                {
                    cboIPs.SelectedIndex = Convert.ToInt32(pid);
                }
            }


            mysql_conn.Close();
        }

        private void txtFNme_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtFNme.Text))
            {
                txtFNme.Text = GlobalVar.ToTitleCase(txtFNme.Text);
            }
        }

        private void txtLoc_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtLoc.Text))
            {
                //Load the Map
                txtLoc.Text = GlobalVar.ToTitleCase(txtLoc.Text);
                SplashScreenManager.ShowForm(this,typeof(WaitForm1));
                SplashScreenManager.Default.SetWaitFormDescription("Loading Map");
                bgwGetLoc.RunWorkerAsync(txtLoc.Text);
            }
        }

        private void bgwGetLoc_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var client = new System.Net.WebClient();
                client.Encoding = System.Text.Encoding.UTF8;
                String result = null;
                try
                {
                    String qry = "https://swop-kenya.org/idall/websrvc.php?task=getLatlon&Qry=" + Uri.EscapeDataString(e.Argument.ToString() + "");
                    result = client.DownloadString(qry);
                }
                catch { }

                Debug.Write(result, "Geocoding Results");

                e.Result = result;


            }
            catch { }
        }

        private void bgwGetLoc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SplashScreenManager.CloseForm();

            string result = e.Result.ToString();

            string loc = null;

            if (result != null)
            {
                var arrRslt = JArray.Parse(result);

                string addrs = arrRslt[0].Value<String>("display_name");
                loc = arrRslt[0].Value<String>("lat") + ","+arrRslt[0].Value<String>("lon");

                frmMap map_frm = new frmMap();
                map_frm.Show();
                map_frm.place_on_Map(addrs, loc, txtFNme.Text);
            }
        }

        private void frmFacReg_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalVar.fac_reg_frm = null;
        }

        private void cmdReg_Click(object sender, EventArgs e)
        {
            if (valData())
            {
                regData();
            }
        }

        private void regData()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "INSERT INTO facilities (prog_id,facility,location,contact_1,contact_2,gps,con_1_email,"
                +"con_1_tel,con_2_email,con_2_tel) "+
                "VALUES(@pid,@fac,@loc,@con1,@con2,@gps,@con1_email,@con1_tel,@con2_email,@con2_tel)";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@pid", pids[cboIPs.SelectedIndex]);
            mysql_cmd.Parameters.AddWithValue("@fac", txtFNme.Text);
            mysql_cmd.Parameters.AddWithValue("@loc", txtLoc.Text);
            mysql_cmd.Parameters.AddWithValue("@con1", txtCon1.Text);
            mysql_cmd.Parameters.AddWithValue("@con2", txtCon2.Text);
            mysql_cmd.Parameters.AddWithValue("@gps", gps[0].ToString()+","+gps[1].ToString());
            mysql_cmd.Parameters.AddWithValue("@con1_email", txtCP1Email.Text);
            mysql_cmd.Parameters.AddWithValue("@con1_tel", txtCP1Tel.Text);
            mysql_cmd.Parameters.AddWithValue("@con2_email", txtCP2Email.Text);
            mysql_cmd.Parameters.AddWithValue("@con2_tel", txtCP2Tel.Text);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.RecordsAffected > 0)
            {
                XtraMessageBox.Show("Facility Registered Successfully", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Refresh the List
                getFacilities();
                
                //Clear Text
                clearText();
            }
            else
            {
                XtraMessageBox.Show("Facility Not Registered Try again later", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            mysql_conn.Close();
        }

        private void clearText()
        {
            txtCon1.Text = "";
            txtCon2.Text = "";
            txtCP1Email.Text = "";
            txtCP1Tel.Text = "";
            txtCP2Email.Text = "";
            txtCP2Tel.Text = "";
            txtFNme.Text = "";
            txtLoc.Text = "";
            gps = new double[2];
            pid = null;
        }

        private bool valData()
        {
            if (!String.IsNullOrEmpty(txtFNme.Text))
            {
                if (!String.IsNullOrEmpty(txtLoc.Text))
                {
                    if (!String.IsNullOrEmpty(txtCon1.Text))
                    {
                        if (!String.IsNullOrEmpty(txtCon2.Text))
                        {
                            if (!String.IsNullOrEmpty(txtCP1Email.Text))
                            {
                                if (!String.IsNullOrEmpty(txtCP2Email.Text))
                                {
                                    if (GlobalVar.IsValidEmail(txtCP1Email.Text))
                                    {
                                        if (GlobalVar.IsValidEmail(txtCP2Email.Text))
                                        {
                                            if (!String.IsNullOrEmpty(txtCP1Tel.Text))
                                            {
                                                if (!String.IsNullOrEmpty(txtCP2Tel.Text))
                                                {
                                                    return true;
                                                }
                                                else
                                                {
                                                    XtraMessageBox.Show("Please enter the facility Contact 2 Telephone Number", this.Text,
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                }
                                            }
                                            else
                                            {
                                                XtraMessageBox.Show("Please enter the facility Contact 1 Tel Number", this.Text,
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            }
                                        }
                                        else
                                        {
                                            XtraMessageBox.Show("Please enter the facility Contact 2 VALID Email", this.Text,
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        }
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show("Please enter the facility Contact 1 VALID Email", this.Text,
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                                else
                                {
                                    XtraMessageBox.Show("Please enter the facility Contact 2 Email", this.Text,
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show("Please enter the facility Contact 1 Email", this.Text,
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("Please enter the facility Contact 2 Name", this.Text,
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Please enter the facility Contact 1 Name", this.Text,
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Please enter the facility location", this.Text,
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                XtraMessageBox.Show("Please enter the facility name", this.Text,
                          MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }

        private void txtCon1_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCon1.Text))
            {
                txtCon1.Text = GlobalVar.ToTitleCase(txtCon1.Text);
            }
        }

        private void txtCon2_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCon2.Text))
            {
                txtCon2.Text = GlobalVar.ToTitleCase(txtCon2.Text);
            }
        }
    }
}