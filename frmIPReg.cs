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
using System.Collections;

namespace IRISTRY
{
    public partial class frmIPReg : DevExpress.XtraEditors.XtraForm
    {
        private static string imgHex = null;

        private ArrayList ids = new ArrayList();
        private ArrayList p_name = new ArrayList();
        private ArrayList logo = new ArrayList();
        private ArrayList locs = new ArrayList();
        private ArrayList contact = new ArrayList();

        public frmIPReg()
        {
            InitializeComponent();
        }

        private void frmIPReg_Load(object sender, EventArgs e)
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
                DataTable tblProgs = new DataTable();
                tblProgs.Columns.Add("Logo", typeof(Image));
                tblProgs.Columns.Add("Implementing Partner", typeof(string));

                ids.Clear();
                p_name.Clear();
                logo.Clear();
                locs.Clear();
                contact.Clear();

                while (mysql_datareader.Read())
                {
                    tblProgs.Rows.Add(hextoImage(mysql_datareader["logo"].ToString()),mysql_datareader["prog_name"].ToString());
                    ids.Add(mysql_datareader["id"]);
                    p_name.Add(mysql_datareader["prog_name"]);
                    logo.Add(mysql_datareader["logo"]);
                    contact.Add(mysql_datareader["contact_person"]);
                    
                }

                grdRegPrtnr.DataSource = tblProgs;

                grdRegPrtnr.RefreshDataSource();
               
                
            }


            mysql_conn.Close();
        }

        private void cboLogo_Click(object sender, EventArgs e)
        {
            DialogResult drslt = ofdSelLogo.ShowDialog();

            if (drslt == DialogResult.OK)
            {
                imgLogo.Image = Image.FromFile(ofdSelLogo.FileName);
                imgHex = Convert.ToBase64String(File.ReadAllBytes(ofdSelLogo.FileName));
            }
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

        private void txtipNme_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtipNme.Text))
            {
                txtipNme.Text = GlobalVar.ToTitleCase(txtipNme.Text);
            }
        }

        private void txtipLoc_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtipLoc.Text))
            {
                txtipLoc.Text = GlobalVar.ToTitleCase(txtipLoc.Text);
            }
        }

        private void txtipCNme_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtipCNme.Text))
            {
                txtipCNme.Text = GlobalVar.ToTitleCase(txtipCNme.Text);
            }
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

            string query = "INSERT INTO programs (prog_name,logo,location,contact_person,email,tel) "
                + "VALUES(@prog_name,@logo,@location,@cp,@email,@tel)";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@prog_name", txtipNme.Text);
            mysql_cmd.Parameters.AddWithValue("@logo", imgHex);
            mysql_cmd.Parameters.AddWithValue("@location", txtipLoc.Text);
            mysql_cmd.Parameters.AddWithValue("@cp", txtipCNme.Text);
            mysql_cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            mysql_cmd.Parameters.AddWithValue("@tel", txtTel.Text);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.RecordsAffected > 0)
            {

                XtraMessageBox.Show("Implementing Partner Registered Successfully", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                getIPs();

                
                DialogResult drslt = XtraMessageBox.Show("Register "+txtipCNme.Text+ " Facilities", this.Text,
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (drslt == DialogResult.Yes)
                {
                    //Open the facility registration form
                    GlobalVar.fac_reg_frm = new frmFacReg();
                    GlobalVar.fac_reg_frm.setIP_ID(mysql_cmd.LastInsertedId.ToString());
                    GlobalVar.fac_reg_frm.ShowDialog();
                }

                clearText();
            }
            else
            {
                XtraMessageBox.Show("Implementing Partner Not Registered Try again later", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            mysql_conn.Close();
        }

        private void clearText()
        {
            txtipCNme.Text = "";
            txtipLoc.Text = "";
            txtipNme.Text = "";
            txtEmail.Text = "";
            txtTel.Text = "";
            imgHex = null;
            imgLogo.Image = null;
        }

        private bool valData()
        {
            if (!String.IsNullOrEmpty(txtipNme.Text))
            {
                if (imgHex != null)
                {
                    if (!String.IsNullOrEmpty(txtipLoc.Text))
                    {
                        if (!String.IsNullOrEmpty(txtipCNme.Text))
                        {
                            if (!String.IsNullOrEmpty(txtEmail.Text))
                            {
                                if (GlobalVar.IsValidEmail(txtEmail.Text))
                                {
                                    if (!String.IsNullOrEmpty(txtTel.Text))
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show("Please enter the partner contact person Telephone Number", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                                else
                                {
                                    XtraMessageBox.Show("Please enter the partner contact person Valid Email", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show("Please enter the partner contact person email", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("Please enter the partner contact person", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Please enter the partner's location", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Please select the partner Logo", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                XtraMessageBox.Show("Please enter the Partner Name", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            clearText();
        }
    }
}