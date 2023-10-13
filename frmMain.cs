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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Collections;
using DevExpress.Utils;
using System.Net;
using System.Collections.Specialized;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace IRISTRY
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private ArrayList study_descs = new ArrayList();
        private ArrayList sids = new ArrayList();

        
        private Boolean synch_running = false;
        public frmMain()
        {
            InitializeComponent();
        }

        private void registerUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsrReg usr_reg_frm = new frmUsrReg();
            usr_reg_frm.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Place form center top
            this.Left = (Screen.PrimaryScreen.WorkingArea.Right / 2) - (this.Width / 2);
            this.Top = 0;

            
        }

        private void getValidStudies()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM studies WHERE DATEDIFF(end_date,CURDATE()) > 0";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                DataTable tblStudies = new DataTable();
                tblStudies.Columns.Add("Study Name", typeof(string));
                study_descs.Clear();
                sids.Clear();

                while (mysql_datareader.Read())
                {
                    sids.Add(mysql_datareader["id"]);
                    tblStudies.Rows.Add(mysql_datareader["study"]);
                    study_descs.Add(mysql_datareader["study_desc"]);
                }

                grdStudies.DataSource = tblStudies;

                grdStudies.RefreshDataSource();


                flyoutPanel1.ShowBeakForm();


            }
            else
            {
                XtraMessageBox.Show("No studies registered", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult drslt = XtraMessageBox.Show("Do you wish to register a Study", this.Text,
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (drslt == DialogResult.Yes)
                {
                    frmStudyReg study_reg_frm = new frmStudyReg();
                    study_reg_frm.ShowDialog();
                    mysql_conn.Close();
                    getValidStudies();
                }
            }


            mysql_conn.Close();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            //Check for Registered valid studies
            bgwGetFacility.RunWorkerAsync();
            getValidStudies();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            txtSelStudy.Text = gridView1.GetRowCellValue(e.RowHandle, gridView1.Columns["Study Name"]).ToString();
            GlobalVar.sel_study_id = sids[e.RowHandle].ToString();

            flyoutPanel1.HideBeakForm();
        }

        private void txtSelStudy_TextChanged(object sender, EventArgs e)
        {
            if (txtSelStudy.Text.Length > 0)
            {
                cmdID.Enabled = true;
            }
            else
            {
                cmdID.Enabled = false;
            }
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.Info == null && e.SelectedControl == grdStudies)
            {
                GridView view = grdStudies.FocusedView as GridView;
                GridHitInfo info = view.CalcHitInfo(e.ControlMousePosition);
                if (info.InRowCell)
                {
                    string text = study_descs[info.RowHandle].ToString();
                    string cellKey = info.RowHandle.ToString() + " - " + info.Column.ToString();
                    e.Info = new DevExpress.Utils.ToolTipControlInfo(cellKey, text, "Study Description", ToolTipIconType.Information);

                }
            }
        }

        private void cmdSelStudy_Click(object sender, EventArgs e)
        {
            getValidStudies();
        }

        private void logOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdRegStudy_Click(object sender, EventArgs e)
        {
            frmStudyReg study_reg_frm = new frmStudyReg();
            study_reg_frm.ShowDialog();
        }

        private void cmdID_Click(object sender, EventArgs e)
        {
            //Check if any participant is registered
            checkforRegPart();
        }

        private void checkforRegPart()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM part_reg";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                //Open the Biometric Identification Module
                frmID id_frm = new frmID();
                id_frm.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("No participants are registered", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult drslt = XtraMessageBox.Show("Do you wish to register a Participant", this.Text,
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (drslt == DialogResult.Yes)
                {
                    frmPartReg part_reg = new frmPartReg();
                    part_reg.ShowDialog();

                }
            }


            mysql_conn.Close();
        }

        private void tmrSynchData_Tick(object sender, EventArgs e)
        {
            if (!synch_running)
            {
                if (!GlobalVar.reader_active) //Only perform when the reader is not active
                {
                    //Check for a connection to the server
                    if (GlobalVar.checkConn())
                    {
                        synch_running = true;
                        bgwRunSync.RunWorkerAsync();
                        
                    }
                }
            }
        }

        private void bgwRunSync_DoWork(object sender, DoWorkEventArgs e)
        {
            start_Synch();
        }

        private void start_Synch()
        {
            try
            {
                //Synch the Data
                synchIRISImgs();
                synchPartReg();
                synchStudies();
                synchStudyPlacemnt();
                synchVisits();
            }
            catch { }
            synch_running = false;
        }

        private void synchStudyPlacemnt()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM study_placement WHERE uupid LIKE @progid ORDER BY reg_on DESC";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@progid", "%"+getProgName(GlobalVar.sel_IP)+"%");

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                int synch_cnt = 0;

                while (mysql_datareader.Read())
                {
                    using (WebClient client = new WebClient())
                    {

                        byte[] response =
                        client.UploadValues(GlobalVar.conn_url, new NameValueCollection()
                           {
                               {"task", "UploadStudyPlacement"},
                               {"UUPID",mysql_datareader["uupid"].ToString()},
                               {"SID",mysql_datareader["sid"].ToString()},
                               {"IP",mysql_datareader["ip"].ToString()},
                               {"REG_ON",Convert.ToDateTime(mysql_datareader["reg_on"]).ToString("yyyy-MM-dd HH:mm:ss")}
                           });

                        string rslt = System.Text.Encoding.UTF8.GetString(response);


                        if (rslt.Equals("1"))
                        {
                            synch_cnt++;
                        }


                    }


                }

                if (synch_cnt > 0)
                {
                    //Display Card
                    this.Invoke((MethodInvoker)delegate
                    {
                        alertControl1.Show(this, "Study Placement Sync", synch_cnt.ToString() +
                            " Study Placment Records Uploaded to Cloud", "", IRISTRY.Properties.Resources.db_synch);
                    });
                }

                
            }

            mysql_conn.Close();

            //Get other participant data from the cloud
            getStudyParticipants();
        }

        private void getStudyParticipants()
        {
            using (WebClient client = new WebClient())
            {

                byte[] response =
                client.UploadValues(GlobalVar.conn_url, new NameValueCollection()
                           {
                               {"task", "getStudyPlacements"},
                               {"IP",GlobalVar.sel_IP}
                           });

                string rslt = System.Text.Encoding.UTF8.GetString(response);


                if (!rslt.Equals("0"))
                {
                    //Process the result
                    var arrRslt = JArray.Parse(rslt);

                    int synch_cnt = 0;

                    for (int i = 0; i < arrRslt.Count; i++)
                    {
                        MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

                        string query = "INSERT INTO study_placement (uupid,sid,ip,reg_on) "
                            + "VALUES(@uupid,@sid,@ip,@reg_on)";

                        mysql_conn.Open();

                        MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

                        mysql_cmd.Parameters.AddWithValue("@uupid", arrRslt[i].Value<string>("uupid"));
                        mysql_cmd.Parameters.AddWithValue("@sid", arrRslt[i].Value<string>("sid"));
                        mysql_cmd.Parameters.AddWithValue("@ip", arrRslt[i].Value<string>("site"));
                        mysql_cmd.Parameters.AddWithValue("@reg_on", arrRslt[i].Value<string>("reg_on"));

                        MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

                        if (mysql_datareader.RecordsAffected > 0)
                        {
                            synch_cnt++;
                        }


                        mysql_conn.Close();
                    }

                    if (synch_cnt > 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            alertControl1.Show(this, "Study Placement Sync", synch_cnt.ToString() +
                                " Study Placements Downloaded from Cloud", "", IRISTRY.Properties.Resources.db_synch);
                        });
                    }
                }


            }
        }

        private void synchVisits()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM visits ORDER BY reg_on DESC";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            //mysql_cmd.Parameters.AddWithValue("@site", GlobalVar.sel_study_id);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                int synch_cnt = 0;

                while (mysql_datareader.Read())
                {
                    using (WebClient client = new WebClient())
                    {

                        byte[] response =
                        client.UploadValues(GlobalVar.conn_url, new NameValueCollection()
                           {
                               {"task", "UploadVisits"},
                               {"UUPID",mysql_datareader["uupid"].ToString()},
                               {"SID",mysql_datareader["sid"].ToString()},
                               {"STUDY",mysql_datareader["study"].ToString()},
                               {"FAC_ID",mysql_datareader["fac_id"].ToString()},
                               {"DOV",Convert.ToDateTime(mysql_datareader["dov"]).ToString("yyyy-MM-dd HH:mm:ss")},
                               {"REG_ON",Convert.ToDateTime(mysql_datareader["reg_on"]).ToString("yyyy-MM-dd HH:mm:ss")}
                           });

                        string rslt = System.Text.Encoding.UTF8.GetString(response);
                        
                        if (rslt.Equals("1"))
                        {
                            synch_cnt++;
                        }


                    }


                }

                if (synch_cnt > 0)
                {
                    //Display Card
                    this.Invoke((MethodInvoker)delegate
                    {
                        alertControl1.Show(this, "Visit Date Sync", synch_cnt.ToString() +
                            " Visits Uploaded to Cloud", "", IRISTRY.Properties.Resources.db_synch);
                    });
                }

                
            }

            mysql_conn.Close();

            //Get other participant data from the cloud
            getVisits();
        }

        private void getVisits()
        {
            using (WebClient client = new WebClient())
            {

                byte[] response =
                client.UploadValues(GlobalVar.conn_url, new NameValueCollection()
                           {
                               {"task", "getVisits"},
                               {"SITE",GlobalVar.sel_IP}
                           });

                string rslt = System.Text.Encoding.UTF8.GetString(response);


                if (!rslt.Equals("0"))
                {
                    //Process the result
                    var arrRslt = JArray.Parse(rslt);

                    int synch_cnt = 0;

                    for (int i = 0; i < arrRslt.Count; i++)
                    {
                        if (visitDoesnotExist(arrRslt[i].Value<string>("uupid"),
                            arrRslt[i].Value<string>("sid"), arrRslt[i].Value<string>("fac_id")))
                        {

                            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

                            string query = "INSERT INTO visits (uupid,sid,site,dov,reg_on) "
                                + "VALUES(@uupid,@sid,@site,@dov,@reg_on)";

                            mysql_conn.Open();

                            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

                            mysql_cmd.Parameters.AddWithValue("@uupid", arrRslt[i].Value<string>("uupid"));
                            mysql_cmd.Parameters.AddWithValue("@sid", arrRslt[i].Value<string>("sid"));
                            mysql_cmd.Parameters.AddWithValue("@site", arrRslt[i].Value<string>("site"));
                            mysql_cmd.Parameters.AddWithValue("@dov", arrRslt[i].Value<string>("dov"));
                            mysql_cmd.Parameters.AddWithValue("@reg_on", arrRslt[i].Value<string>("reg_on"));

                            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

                            if (mysql_datareader.RecordsAffected > 0)
                            {
                                synch_cnt++;
                            }


                            mysql_conn.Close();
                        }
                    }

                    if (synch_cnt > 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            alertControl1.Show(this, "Visits Sync", synch_cnt.ToString() +
                                " Visits Downloaded from Cloud", "", IRISTRY.Properties.Resources.db_synch);
                        });
                    }
                }


            }
        }

        private bool visitDoesnotExist(string uupid, string sid, string site)
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM visits WHERE uupid = @uupid AND sid = @sid AND fac_id = @site";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@uupid", uupid);
            mysql_cmd.Parameters.AddWithValue("@sid", sid);
            mysql_cmd.Parameters.AddWithValue("@site", site);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {

                while (mysql_datareader.Read())
                {
                    mysql_conn.Close();

                    return false;
                }

            }


            mysql_conn.Close();

            return true;
        }

        private void synchStudies()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM studies";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                int synch_cnt = 0;

                while (mysql_datareader.Read())
                {
                    using (WebClient client = new WebClient())
                    {

                        byte[] response =
                        client.UploadValues(GlobalVar.conn_url, new NameValueCollection()
                           {
                               {"task", "UploadStudies"},
                               {"STUDY",mysql_datareader["study"].ToString()},
                               {"SDESC",mysql_datareader["study_desc"].ToString()},
                               {"SDATE",Convert.ToDateTime(mysql_datareader["start_date"]).ToString("yyyy-MM-dd")},
                               {"EDATE",Convert.ToDateTime(mysql_datareader["end_date"]).ToString("yyyy-MM-dd")},
                               {"REG_ON",Convert.ToDateTime(mysql_datareader["reg_on"]).ToString("yyyy-MM-dd HH:mm:ss")}
                           });

                        string rslt = System.Text.Encoding.UTF8.GetString(response);


                        if (rslt.Equals("1"))
                        {
                            synch_cnt++;
                        }


                    }


                }

                if (synch_cnt > 0)
                {
                    //Display Card
                    this.Invoke((MethodInvoker)delegate
                    {
                        alertControl1.Show(this, "Study Data Sync", synch_cnt.ToString() +
                            " Studies Uploaded to Cloud", "", IRISTRY.Properties.Resources.db_synch);
                    });
                }

               
            }

            mysql_conn.Close();

            //Get other participant data from the cloud
            getStudies();
        }

        private void getStudies()
        {
            using (WebClient client = new WebClient())
            {

                byte[] response =
                client.UploadValues(GlobalVar.conn_url, new NameValueCollection()
                           {
                               {"task", "getStudies"}
                           });

                string rslt = System.Text.Encoding.UTF8.GetString(response);


                if (!rslt.Equals("0"))
                {
                    //Process the result
                    var arrRslt = JArray.Parse(rslt);

                    int synch_cnt = 0;

                    for (int i = 0; i < arrRslt.Count; i++)
                    {
                        if (studyDoesNotExist(arrRslt[i].Value<string>("study"), arrRslt[i].Value<string>("start_date")))
                        {
                            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

                            string query = "INSERT INTO studies (study,study_desc,start_date,end_date) "
                                + "VALUES(@study,@desc,@start_date,@end_date)";

                            mysql_conn.Open();

                            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

                            mysql_cmd.Parameters.AddWithValue("@study", arrRslt[i].Value<string>("study"));
                            mysql_cmd.Parameters.AddWithValue("@desc", arrRslt[i].Value<string>("study_desc"));
                            mysql_cmd.Parameters.AddWithValue("@start_date", arrRslt[i].Value<string>("start_date"));
                            mysql_cmd.Parameters.AddWithValue("@end_date", arrRslt[i].Value<string>("end_date"));

                            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

                            if (mysql_datareader.RecordsAffected > 0)
                            {
                                synch_cnt++;
                            }
                            

                            mysql_conn.Close();
                        }
                    }

                    if (synch_cnt > 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            alertControl1.Show(this, "Study Registration Sync", synch_cnt.ToString() +
                                " Studies Downloaded from Cloud", "", IRISTRY.Properties.Resources.db_synch);
                        });
                    }
                }


            }
        }

        private bool studyDoesNotExist(string study, string start_dte)
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM studies WHERE study = @study AND start_date = @start_date";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@study", study);
            mysql_cmd.Parameters.AddWithValue("@start_date", start_dte);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                
                while (mysql_datareader.Read())
                {
                    mysql_conn.Close();

                    return false;
                }

            }


            mysql_conn.Close();

            return true;
        }

        private void synchPartReg()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM part_reg WHERE uupid LIKE @prog ORDER BY reg_on DESC";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@prog", "%" + getProgName(GlobalVar.sel_IP) + "%");

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                int synch_cnt = 0;

                while (mysql_datareader.Read())
                {
                    using (WebClient client = new WebClient())
                    {

                        byte[] response =
                        client.UploadValues(GlobalVar.conn_url, new NameValueCollection()
                           {
                               {"task", "UploadBioData"},
                               {"UUPID",mysql_datareader["uupid"].ToString()},
                               {"PNME",mysql_datareader["part_name"].ToString()},
                               {"PDOB",Convert.ToDateTime(mysql_datareader["dob"]).ToString("yyyy-MM-dd")},
                               {"PPOB",mysql_datareader["pob"].ToString()},
                               {"LE_TEMP",mysql_datareader["left_eye"].ToString()},
                               {"RE_TEMP",mysql_datareader["right_eye"].ToString()},
                               {"REG_ON",Convert.ToDateTime(mysql_datareader["reg_on"]).ToString("yyyy-MM-dd HH:mm:ss")}
                           });

                        string rslt = System.Text.Encoding.UTF8.GetString(response);


                        if (rslt.Equals("1"))
                        {
                            synch_cnt++;
                        }


                    }


                }

                if (synch_cnt > 0)
                {
                    //Display Card
                    this.Invoke((MethodInvoker)delegate
                    {
                        alertControl1.Show(this, "Participant Data Sync", synch_cnt.ToString() +
                            " Participants Uploaded to Cloud", "", IRISTRY.Properties.Resources.db_synch);
                    });
                }

                
            }

            mysql_conn.Close();

            //Get other participant data from the cloud
            getOtherParticipants();
        }

        private void getOtherParticipants()
        {

            using (WebClient client = new WebClient())
            {

                byte[] response =
                client.UploadValues(GlobalVar.conn_url, new NameValueCollection()
                           {
                               {"task", "getBioData"},
                               {"PID",getProgName(GlobalVar.sel_IP)}
                           });

                string rslt = System.Text.Encoding.UTF8.GetString(response);


                if (!rslt.Equals("0"))
                {
                    //Process the result
                    var arrRslt = JArray.Parse(rslt);

                    int synch_cnt = 0;

                    for (int i = 0; i < arrRslt.Count; i++)
                    {
                        MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

                        string query = "INSERT INTO part_reg (part_name,dob,pob,left_eye,right_eye) "
                            + "VALUES(@part_nme,@dob,@pob,@left_eye,@right_eye)";

                        mysql_conn.Open();

                        MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

                        mysql_cmd.Parameters.AddWithValue("@part_nme", arrRslt[i].Value<string>("part_name"));
                        mysql_cmd.Parameters.AddWithValue("@dob", arrRslt[i].Value<string>("dob"));
                        mysql_cmd.Parameters.AddWithValue("@pob", arrRslt[i].Value<string>("pob"));
                        mysql_cmd.Parameters.AddWithValue("@left_eye", arrRslt[i].Value<string>("left_eye"));
                        mysql_cmd.Parameters.AddWithValue("@right_eye", arrRslt[i].Value<string>("right_eye"));

                        MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

                        if (mysql_datareader.RecordsAffected > 0)
                        {
                            synch_cnt++;

                        }
                        

                        mysql_conn.Close();
                    }

                    if (synch_cnt > 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            alertControl1.Show(this, "Participant Data Sync", synch_cnt.ToString() +
                                " Participants Downloaded from Cloud", "", IRISTRY.Properties.Resources.db_synch);
                        });
                    }
                }


            }
        }

        private string getProgName(string pid)
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT prog_name FROM programs WHERE id = @id";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@id", pid);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                string p_nme = null;

                while (mysql_datareader.Read())
                {
                    p_nme = mysql_datareader["prog_name"].ToString();
                }

                mysql_conn.Close();

                return p_nme;


            }


            mysql_conn.Close();
            return null;
        }

        private void synchIRISImgs()
        {
            //Upload data to cloud
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM bio_imgs ORDER BY reg_on DESC";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                int synch_cnt = 0;

                while (mysql_datareader.Read())
                {
                    using (WebClient client = new WebClient())
                    {

                        byte[] response =
                        client.UploadValues(GlobalVar.conn_url, new NameValueCollection()
                           {
                               {"task", "UploadIRISImgs"},
                               {"UUPID",mysql_datareader["uuid"].ToString()},
                               {"LE_TEMP",mysql_datareader["l_eye"].ToString()},
                               {"RE_TEMP",mysql_datareader["r_eye"].ToString()},
                               {"Reg_On",Convert.ToDateTime(mysql_datareader["reg_on"]).ToString("yyyy-MM-dd HH:mm:ss")}
                           });

                        string rslt = System.Text.Encoding.UTF8.GetString(response);


                        if (rslt.Equals("1"))
                        {
                            synch_cnt++;
                        }


                    }


                }

                if (synch_cnt > 0)
                {
                    //Display Card
                    this.Invoke((MethodInvoker)delegate
                    {
                        alertControl1.Show(this, "IRIS Image Sync", synch_cnt.ToString() +
                            " IRIS Images Uploaded to Cloud", "", IRISTRY.Properties.Resources.db_synch);
                    });
                }
            }



            mysql_conn.Close();
        }

        private void registerImplementingPartnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIPReg ip_reg_frm = new frmIPReg();
            ip_reg_frm.ShowDialog();
        }

        private void addFacilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFacReg fac_reg_frm = new frmFacReg();
            GlobalVar.fac_reg_frm = fac_reg_frm;
            fac_reg_frm.Show();
        }

        private void bgwGetFacility_DoWork(object sender, DoWorkEventArgs e)
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT programs.`prog_name`, facilities.`facility` "+
            "FROM programs JOIN facilities ON programs.`id` = facilities.`prog_id` "+
            "WHERE facilities.id = @id";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@id", GlobalVar.sel_IP_fac);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                
                while (mysql_datareader.Read())
                {

                    this.Invoke((MethodInvoker)delegate
                    {
                        this.Text = "Integrated Study Participant Identity Manager - "+mysql_datareader["prog_name"]+" "+mysql_datareader["facility"];
                    });
                }

               
            }



            mysql_conn.Close();
        }

        
    }
}