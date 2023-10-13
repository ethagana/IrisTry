using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace IRISTRY
{
    class GlobalVar
    {
        public static frmPartReg part_reg_frm = null;
        public static frmUsrReg usr_reg_frm = null;
        public static frmPartDetails part_det_frm = null;
        public static frmFacReg fac_reg_frm = null;

        public static string connectionstring = "server=localhost;database=iris_rsrch;uid=root;pwd=mysql2017;convert zero datetime=True";
        public static string conn_url = "https://swop-kenya.org/bio_rsrch/iris_websrvc.php";
        public static string sel_study_id = null;
        public static string sel_IP = null;
        public static string sel_IP_fac = null;
        public static string user_id = null;

        public static bool reader_active = false;

                

        public static Dictionary<string, byte[]> iris_temps = new Dictionary<string, byte[]>();

        public static Boolean checkConn()
        {
            try
            {
                Ping x = new Ping();
                PingReply reply = x.Send(IPAddress.Parse("169.239.252.83"), 100000);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
            }

            catch { }
            return false;
        }

        public static bool IsValidEmail(string strIn)
        {
            try
            {
                string address = new MailAddress(strIn).Address;

                return true;
            }
            catch (FormatException)
            {
                //address is invalid
            }

            return false;
        }

        public static string ToTitleCase(string title)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title);
        }
    }
}
