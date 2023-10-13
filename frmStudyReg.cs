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
    public partial class frmStudyReg : DevExpress.XtraEditors.XtraForm
    {
        DataTable tblStudies = new DataTable();

        ArrayList fac_ids = new ArrayList();

        private string sel_facs;

        public frmStudyReg()
        {
            InitializeComponent();

            tblStudies.Columns.Add("Study Name", typeof(string));
            tblStudies.Columns.Add("Is Valid", typeof(object));

            grdStudies.DataSource = tblStudies;

            grdStudies.RefreshDataSource();
        }

        private void frmStudyReg_Load(object sender, EventArgs e)
        {
            //Load the registered studies
            loadStudies();
            //Load the facilities
            loadFacilities();
        }

        private void loadFacilities()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT Facilities.id ,CONCAT_WS('-',programs.`prog_name`,facilities.`facility`) AS Facility FROM facilities "+
                           "JOIN programs ON facilities.`prog_id` = programs.`id` WHERE Facilities.prog_id = @id";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@id", GlobalVar.sel_IP);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                cboFacs.Properties.Items.Clear();
                fac_ids.Clear();

                while (mysql_datareader.Read())
                {
                    cboFacs.Properties.Items.Add(mysql_datareader["facility"]);
                    fac_ids.Add(mysql_datareader["id"]);
                }

            }
            else
            {
                XtraMessageBox.Show("No Facilities registered, Register a facility and try again", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
            }


            mysql_conn.Close();
        }

        private void loadStudies()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM studies";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);
                        
            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                tblStudies.Rows.Clear();

                while (mysql_datareader.Read())
                {
                    string validity = null;

                    if (is_Valid(mysql_datareader["end_date"]) > 0)
                    {
                        validity = "Yes";
                    }
                    else
                    {
                        validity = "No";
                    }

                    tblStudies.Rows.Add(mysql_datareader["study"], validity);
                }

                
                grdStudies.RefreshDataSource();

                XtraMessageBox.Show("Studies Loaded", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information); 

                
            }
             

            mysql_conn.Close();
        }

        private double is_Valid(object p)
        {
          return (DateTime.Parse(p.ToString()) - DateTime.Now).TotalDays;
        }

        private void cmdReg_Click(object sender, EventArgs e)
        {
            if (valData())
            {
                regStudy();
            }
        }

        private void regStudy()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "INSERT INTO studies (study,study_desc,start_date,end_date,fac_part) "
                + "VALUES(@study,@desc,@start_date,@end_date,@facpart)";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@study", txtStudyName.Text);
            mysql_cmd.Parameters.AddWithValue("@desc", txtDesc.Text);
            mysql_cmd.Parameters.AddWithValue("@start_date", dteStart.DateTime.ToString("yyyy-MM-dd"));
            mysql_cmd.Parameters.AddWithValue("@end_date", dteEnd.DateTime.ToString("yyyy-MM-dd"));
            mysql_cmd.Parameters.AddWithValue("@facpart", sel_facs);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.RecordsAffected > 0)
            {
                
                XtraMessageBox.Show("Study Registered Successfully", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                loadStudies();

                clearText();
            }
            else
            {
                XtraMessageBox.Show("Study Not Registered Try again later", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            mysql_conn.Close();
        }

        private void clearText()
        {
            txtStudyName.Text = "";
            txtDesc.Text = "";
            dteStart.EditValue = null;
            dteEnd.EditValue = null;
            cboFacs.SetEditValue(null);
        }

        private bool valData()
        {
            if (!String.IsNullOrEmpty(txtStudyName.Text))
            {
                if (!String.IsNullOrEmpty(txtDesc.Text))
                {
                    if (dteStart.EditValue != null)
                    {
                        if (dteEnd.EditValue != null)
                        {
                            if (sel_facs != null)
                            {
                                return true;
                            }
                            else
                            {
                                XtraMessageBox.Show("Please select the facilities the study is to take place", this.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("Please enter the Study End Date", this.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Please enter the Study Start Date", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Please enter a short desription of the study", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                XtraMessageBox.Show("Please enter the Study Name", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }

        private void txtStudyName_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtStudyName.Text))
            {
                txtStudyName.Text = GlobalVar.ToTitleCase(txtStudyName.Text);
            }
        }

        private void txtDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtDesc.Text.Length > 250)
            {
                XtraMessageBox.Show("A study description can only be a maximum of 250 characters", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                e.Handled = true;
            }
        }

        private void txtStudyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtStudyName.Text.Length > 100)
            {
                XtraMessageBox.Show("A study name can only be a maximum of 100 characters", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                e.Handled = true;
            }
        }

        private void cboFacs_EditValueChanged(object sender, EventArgs e)
        {
            ArrayList sel_fac_ids = new ArrayList();

            for (int i = 0; i < cboFacs.Properties.Items.Count; i++)
            {
                if (cboFacs.Properties.Items[i].CheckState == CheckState.Checked)
                {
                    sel_fac_ids.Add(fac_ids[i]);
                }
            }

            if (sel_fac_ids.Count > 0)
            {
                sel_facs = String.Join(",", sel_fac_ids.ToArray());
            }
        }
    }
}