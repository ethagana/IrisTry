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
using System.Runtime.InteropServices;
using iDataIrisAxLib;
using System.Collections;
using CSharpSample;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace IRISTRY
{
    public partial class frmPartReg : DevExpress.XtraEditors.XtraForm
    {
        private TiltStatus DeviceTiltStatus { get; set; }

        [DllImport("FreeImageConverter.dll")]
        private static extern int ConvertRawToBMP(byte[] pucRawImage, int iRawImageWidth, int iRawImageHeight,
                                                   ref IntPtr pucBMPImage, ref int lImageSize);

        [DllImport("FreeImageConverter.dll")]
        private static extern int ReleaseMemory(IntPtr pucImageData);

        private ArrayList iris_temps = new ArrayList();
        private ArrayList uupids = new ArrayList();

        private readonly iCAMT10ControlWrapper _iCAMT10Control;
        private const int MAX_IMAGES_PER_SESSION = 8;
        private readonly ImageMgr _imageMgr;
        private State _appState;
        private int _lastCapturedMode;
        private int CaptureMode = 2; //Primary for identification
        private readonly IiDataIris _iDataIris = new iDataIrisClass();

        private Image imgLeftEye = null;
        private Image imgRightEye = null;

        private ArrayList enrollment_temps = new ArrayList();

        private Boolean le_capture = false;
        private Boolean re_capture = false;

        public frmPartReg()
        {
            InitializeComponent();

            _iCAMT10Control = new iCAMT10ControlWrapper();
            _imageMgr = new ImageMgr(MAX_IMAGES_PER_SESSION);
            SetState(State.DEVICE_CLOSED);

            _iCAMT10Control.OnGetStatus += _iCAMT10Control_OnGetStatus;
            _iCAMT10Control.OnGetIrisImage += _iCAMT10Control_OnGetIrisImage;
            _iCAMT10Control.OnGetLiveInfo += _iCAMT10Control_OnGetLiveInfo;
            _iCAMT10Control.OnGetCompressedIrisImage += _iCAMT10Control_OnGetCompressedIrisImage;
        }

        internal void loadTemps(ArrayList e_temps,Image LE, Image RE){
            enrollment_temps = e_temps;
            imgIRIS.Image = IRISTRY.Properties.Resources.iris_icon;
            imgLeftEye = LE;
            imgRightEye = RE;
        }

        private Bitmap ConvertByteArrayToBitmap(byte[] bmpBytes, Size imageSize)
        {
            Bitmap bmp;
            try
            {
                bmp = new Bitmap(imageSize.Width, imageSize.Height, PixelFormat.Format8bppIndexed);
                ColorPalette pal = bmp.Palette;

                for (int i = 0; i < pal.Entries.Length; i++)
                    pal.Entries[i] = Color.FromArgb(i, i, i);
                bmp.Palette = pal;

                BitmapData bData = bmp.LockBits(new Rectangle(new Point(), bmp.Size),
                                                ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

                // Copy the bytes to the bitmap object
                Marshal.Copy(bmpBytes, 0, bData.Scan0, bmpBytes.Length);
                bmp.UnlockBits(bData);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                return null;
            }

            return (bmp);
        }

        private void ShowMessage(string strMessage)
        {
            MessageBox.Show(strMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private enum State
        {
            DEVICE_CLOSED = 1,
            DEVICE_OPENED = 2,
            CAPTURE_IN_PROGRESS = 3,
            CAPTURE_SUCCESS = 4,
            CAPTURE_FAILED = 5
        };


        private enum TiltStatus
        {
            UpsideDown = 0,
            Normal = 1
        }

        private void SetState(State state)
        {
            switch (state)
            {
                case State.DEVICE_CLOSED:
                    _appState = state;
                    OnDeviceClosed();
                    break;
                case State.DEVICE_OPENED:
                    _appState = state;
                    OnDeviceOpened(true);
                    break;
                case State.CAPTURE_IN_PROGRESS:
                    _appState = state;
                    //OnCaptureInProgress();
                    break;
                case State.CAPTURE_SUCCESS:
                    _appState = State.DEVICE_OPENED;
                    //OnCaptured(true);
                    break;
                case State.CAPTURE_FAILED:
                    _appState = State.DEVICE_OPENED;
                    //OnCaptured(false);
                    break;
            }
        }

        private void DisplayError(int iErrorCode)
        {
            switch (iErrorCode)
            {
                case ErrorCodes.ERR_T10_AX_OPEN:
                    ShowMessage("Failed to open T10 camera.\nERROR CODE: " + iErrorCode);
                    break;
                case ErrorCodes.ERR_T10_AX_NOT_INIT:
                    ShowMessage("Failed to initialize SDK.\nERROR CODE: " + iErrorCode);
                    break;
                case ErrorCodes.ERR_T10_AX_PARAMETER:
                    ShowMessage("Parameter error.\nERROR CODE: " + iErrorCode);
                    break;
                case ErrorCodes.ERR_T10_AX_INVALID_STATE:
                    ShowMessage("Invalid state.\nERROR CODE: " + iErrorCode);
                    break;
                case ErrorCodes.ERR_T10_AX_CB_EVENT:
                    ShowMessage("Error in callback event.\nERROR CODE: " + iErrorCode);
                    break;
                case ErrorCodes.ERR_T10_AX_API_FAIL:
                    ShowMessage("API failed.\nERROR CODE: " + iErrorCode);
                    break;

                default:
                    ShowMessage("Unknown error.\nERROR CODE: " + iErrorCode);
                    break;
            }
        }

        private void OnDeviceOpened(bool p)
        {
            txtReaderStatus.Text = "Reader Status - Connected";
            cmdEnroll.Enabled = p;


            _iCAMT10Control.SetBeeper(true); //Enable beeping
            _iCAMT10Control.SetIllumination(true); //Enable the background white light - does not work

            XtraMessageBox.Show("Have the Participant remove their glasses if they are wearing any", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void OnDeviceClosed()
        {
            txtReaderStatus.Text = "Reader Status - Disconnected";
            cmdEnroll.Enabled = false;

        }

        private void OnTiltStatus(TiltStatus status)
        {
            if (status == TiltStatus.UpsideDown)
            {
                txtReaderStatus.Text = @"Orientation Sensor: Upside Down";
                txtReaderStatus.ForeColor = Color.Red;


                cmdEnroll.Enabled = false;
            }

            if (status == TiltStatus.Normal)
            {
                txtReaderStatus.Text = @"Orientation Sensor: Normal";
                txtReaderStatus.ForeColor = Color.Black;


                cmdEnroll.Enabled = true;

                if (_appState == State.CAPTURE_IN_PROGRESS)
                {

                    cmdEnroll.Enabled = false;
                }

            }
        }

        private void _iCAMT10Control_OnGetLiveInfo(long eye, long imageWidth, long imageHeight, object liveImage,
                                                  long imageSize)
        {
            switch (eye)
            {
                case Constants.SDK_EYE_RIGHT:
                    imgRightEye = ConvertByteArrayToBitmap((byte[])liveImage,
                                                                   new Size((int)imageWidth, (int)imageHeight));
                    break;
                case Constants.SDK_EYE_LEFT:
                    imgLeftEye = ConvertByteArrayToBitmap((byte[])liveImage,
                                                                  new Size((int)imageWidth, (int)imageHeight));
                    break;
            }
        }

        private void _iCAMT10Control_OnGetIrisImage(long eye, long frameNumber, long imageWidth, long imageHeight,
                                                    object irisImage, long imageSize)
        {
            SetState(State.CAPTURE_SUCCESS);

            try
            {
                Image imgIrisImage = ConvertByteArrayToBitmap((byte[])irisImage, new Size((int)imageWidth, (int)imageHeight));
                _imageMgr.AddImage((int)frameNumber, (int)eye, CaptureMode, imgIrisImage, null, Constants.SDK_GET_VGA_IRIS_IMAGE, 0);



                switch (eye)
                {
                    case Constants.SDK_EYE_RIGHT:
                        imgRightEye = imgIrisImage;
                        re_capture = true;
                        break;
                    case Constants.SDK_EYE_LEFT:
                        imgLeftEye = imgIrisImage;
                        le_capture = true;
                        break;
                }

                if (re_capture && le_capture)
                {
                    //Get the templates from both eyes
                    int igalleryImageWidth;
                    int igalleryImageHeight;

                    byte[] byGalleryImageRawR = ImageHelper.ImageToRaw8BitByteArray(imgRightEye, out igalleryImageWidth,
                                                                             out igalleryImageHeight);

                    byte[] byGalleryImageRawL = ImageHelper.ImageToRaw8BitByteArray(imgLeftEye, out igalleryImageWidth,
                                                                             out igalleryImageHeight);

                    //Ensure that the user has no contact lens

                    if (!hasContactLens(byGalleryImageRawL, byGalleryImageRawR, igalleryImageWidth, igalleryImageHeight))
                    {

                        byte[] RE_temp = getIRISTemp(byGalleryImageRawR, "R", igalleryImageWidth, igalleryImageHeight, imgRightEye);
                        byte[] LE_temp = getIRISTemp(byGalleryImageRawL, "L", igalleryImageWidth, igalleryImageHeight, imgLeftEye);

                        //Verify that user has not been enrolled before
                        verifyFirstEnrollment(RE_temp, LE_temp);
                    }
                }


            }
            catch
            {

            }
        }

        private bool hasContactLens(byte[] byGalleryImageRawL, byte[] byGalleryImageRawR, int width, int height)
        {
            int iContactLensType = int.MinValue;
            int iContactLensScore = int.MinValue;
            int iResult;
            bool left_eye_clear = false;
            bool right_eye_clear = false;

            iResult = _iDataIris.GetContactLensInformation(Constants.IRIS_IMAGE_RECT, byGalleryImageRawL, width, height, out iContactLensType,
                                       out iContactLensScore);
            if (iResult != 0 && Constants.IAIRIS_ERROR_CONTACT_LENS != iResult)
            {
                if (iResult == Constants.IAIRIS_LICENCE_EXPIRED)
                {
                    MessageBox.Show(@"License expired!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

            }
            if (iContactLensScore == 0)
            {
                left_eye_clear = true;
            }
            else
            {
                XtraMessageBox.Show("Left Eye has a Contact Lens, Kindly remove it before proceeding", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            iResult = _iDataIris.GetContactLensInformation(Constants.IRIS_IMAGE_RECT, byGalleryImageRawR, width, height, out iContactLensType,
                                       out iContactLensScore);
            if (iResult != 0 && Constants.IAIRIS_ERROR_CONTACT_LENS != iResult)
            {
                if (iResult == Constants.IAIRIS_LICENCE_EXPIRED)
                {
                    MessageBox.Show(@"License expired!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

            }
            if (iContactLensScore == 0)
            {
                right_eye_clear = true;
            }
            else
            {
                XtraMessageBox.Show("Right Eye has a Contact Lens, Kindly remove it before proceeding", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (left_eye_clear && right_eye_clear)
            {
                return false;
            }
            return true;
        }

        private void verifyFirstEnrollment(byte[] RE_temp, byte[] LE_temp)
        {
            for (int i = 0; i < iris_temps.Count; i++)
            {
                int matchedIndex = 0;
                float hd;

                int iResult = _iDataIris.MatchByLongIrisCode(Constants.MATCHING_MODE_STANDARD, RE_temp, iris_temps[i],
                                                    1, 0.32f, ref matchedIndex, out hd);

                if (iResult != 0 && iResult != Constants.IAIRIS_NOT_MATCHED)
                {
                    if (iResult == Constants.IAIRIS_LICENCE_EXPIRED)
                        Debug.WriteLine(@"License expired!" + Environment.NewLine);
                    else
                        Debug.WriteLine(string.Format(
                        @"MatchByLongIrisCode failed with error  0x{0:X}{1}", iResult, Environment.NewLine));
                    return;
                }

                if (matchedIndex == 0)
                {
                    //Match Found
                    XtraMessageBox.Show("Participant is already registered as " + uupids[i], this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

            }

            //So place in readyness for registration
            enrollment_temps.Add(RE_temp);
            enrollment_temps.Add(LE_temp);

            imgIRIS.Image = IRISTRY.Properties.Resources.iris_icon;

            XtraMessageBox.Show("IRIS Templates Registered", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private byte[] getIRISTemp(byte[] byGalleryImageRaw, string eye, int igalleryImageWidth, int igalleryImageHeight, Image imgRightEye)
        {
            object oTemplateIrisCode = null;
            ImageInfo stGalleryImageInfo;
            byte[] g_ProcessedImageBytes = byGalleryImageRaw;
            int g_ProcessedImageWidth = igalleryImageWidth;
            int ProcessedImageHeight = igalleryImageHeight;

            int iResult = _iDataIris.CreateLongIrisCode(Constants.IRIS_IMAGE_RECT, g_ProcessedImageBytes,
                    g_ProcessedImageWidth, ProcessedImageHeight, out oTemplateIrisCode, out stGalleryImageInfo);

            if (iResult != 0)
            {
                if (iResult == Constants.IAIRIS_LICENCE_EXPIRED)
                    MessageBox.Show(@"License expired!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(string.Format(
                    @"CreateIrisCode failed with error  0x{0:X}", iResult),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return null;
            }

            return (byte[])oTemplateIrisCode;
        }

        private void _iCAMT10Control_OnGetCompressedIrisImage(int Status, int Eye, int FrameNumber, int RawIrisImageWidth, int RawIrisImageHeight,
                                                            object vRawIrisImage, int RawIrisImageSize, int CompressedIrisImageWidth,
                                                            int CompressedIrisImageHeight, object vCompressedIrisImage, int CompressedIrisImageSize)
        {
            SetState(State.CAPTURE_SUCCESS);
            int iRet = 0;
            try
            {
                IntPtr pucRawImage = IntPtr.Zero;
                int iimageSize = 0;

                if (Status != Constants.SDK_ERR_SUCCESS)
                {
                    ShowMessage("Get IrisImage Error" + Environment.NewLine + Status.ToString());
                    return;
                }

                iRet = ConvertRawToBMP((byte[])vCompressedIrisImage, CompressedIrisImageWidth, CompressedIrisImageHeight, ref pucRawImage, ref iimageSize);

                if (iRet == 0)
                {
                    byte[] pucRawImage1 = new byte[iimageSize];
                    Marshal.Copy(pucRawImage, pucRawImage1, 0, iimageSize);

                    ReleaseMemory(pucRawImage);
                    if (pucRawImage1 != null)
                    {
                        Image imgIrisImage = Image.FromStream(new MemoryStream(pucRawImage1));

                        _imageMgr.AddImage((int)FrameNumber, (int)Eye, CaptureMode, imgIrisImage, (byte[])vCompressedIrisImage, (1), CompressedIrisImageSize);

                        switch (Eye)
                        {
                            case Constants.SDK_EYE_RIGHT:
                                imgRightEye = imgIrisImage;
                                break;
                            case Constants.SDK_EYE_LEFT:
                                imgLeftEye = imgIrisImage;
                                break;
                        }

                    } if (pucRawImage1 == null)
                    {
                        ShowMessage("Bitmap data is null" + Environment.NewLine);
                    }
                }
                else
                {
                    ShowMessage("Convertion error" + Environment.NewLine + iRet.ToString());
                }

            }
            catch (Exception ex)
            {
                ShowMessage("Convertion error" + Environment.NewLine + ex.Message);
            }
        }

        private void _iCAMT10Control_OnGetStatus(int statusType, int statusValue)
        {
            switch (statusType)
            {
                case Constants.SDK_EVENT_DEVICE_TILT:
                    if (Constants.SDK_TILT_OFF == statusValue)
                    {
                        DeviceTiltStatus = TiltStatus.UpsideDown;
                        OnTiltStatus(TiltStatus.UpsideDown);
                    }
                    else
                    {
                        DeviceTiltStatus = TiltStatus.Normal;
                        OnTiltStatus(TiltStatus.Normal);
                    }

                    break;
                case Constants.SDK_EVENT_SURPRISE_REMOVE:
                    _iCAMT10Control.Close();
                    OnDeviceClosed();

                    ShowMessage(string.Format("Device removed.\nEVENT CODE: {0} \nEVENT VALUE: {1}", statusType,
                                              statusValue));
                    break;
                case Constants.SDK_EVENT_TXN_TIMEOUT:
                    SetState(State.CAPTURE_FAILED);
                    ShowMessage(string.Format("Captue transaction timed out.\nEVENT CODE: {0} \nEVENT VALUE: {1}",
                                              statusType, statusValue));
                    break;
                case Constants.SDK_EVENT_TXN_ERROR:
                    SetState(State.CAPTURE_FAILED);
                    ShowMessage(string.Format("Transaction error.\nEVENT CODE: {0} \nEVENT VALUE: {1}", statusType,
                                              statusValue));

                    break;
            }
        }

        private static string GeneratePronounceableName(int length)
        {
            const string vowels = "aeiou";
            const string consonants = "bcdfghjklmnpqrstvwxyz";

            var rnd = new Random();

            length = length % 2 == 0 ? length : length + 1;

            var name = new char[length];

            for (var i = 0; i < length; i += 2)
            {
                name[i] = vowels[rnd.Next(vowels.Length)];
                name[i + 1] = consonants[rnd.Next(consonants.Length)];
            }

            return new string(name);
        }

        private void frmPartReg_Shown(object sender, EventArgs e)
        {
            //Open the IRIS Scanner
            int retval = _iCAMT10Control.Open();
            if (retval != ErrorCodes.ERR_T10_AX_SUCCESS)
            {
                ShowMessage("Failed to open T10 camera.\nERROR CODE: " + retval);
                XtraMessageBox.Show("Reconnect the scanner and try again", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                this.Close();
            }

            SetState(State.DEVICE_OPENED);

            GlobalVar.reader_active = true;
        }

        private void frmPartReg_FormClosing(object sender, FormClosingEventArgs e)
        {
            int retval = _iCAMT10Control.Close();

            if (retval != ErrorCodes.ERR_T10_AX_SUCCESS)
            {
                //DisplayError(retval);
                DisplayError(retval);
            }

            SetState(State.DEVICE_CLOSED);

            GlobalVar.reader_active = false;
        }

        private void cmdEnroll_Click(object sender, EventArgs e)
        {
            
            imgIRIS.Image = null;
            imgLeftEye = null;
            imgRightEye = null;
            _imageMgr.Clear();

            enrollment_temps.Clear();

            OnTiltStatus(DeviceTiltStatus);
            SetState(State.CAPTURE_IN_PROGRESS);

            int retval = _iCAMT10Control.SetOutputIrisImageType(1, 1);
            if (retval != ErrorCodes.ERR_T10_AX_SUCCESS)
            {
                SetState(State.CAPTURE_FAILED);
                _lastCapturedMode = -1;
                DisplayError(retval);
            }
            retval = _iCAMT10Control.StartIrisCapture(CaptureMode, Constants.SDK_CAPTURE_TYPE_AUTO, 3, 0,
                                                          30); //Using Speed for identification
            if (retval != ErrorCodes.ERR_T10_AX_SUCCESS)
            {
                SetState(State.CAPTURE_FAILED);
                _lastCapturedMode = -1;
                DisplayError(retval);
            }
            //_bReadyState = false;
            _lastCapturedMode = CaptureMode;

            XtraMessageBox.Show("Have the Participant look into the Scanner", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void cmdRegPart_Click(object sender, EventArgs e)
        {
            if (valData())
            {
                getPsedoName();
                regData();
            }
        }

        private void getPsedoName()
        {
            string part_nme = GeneratePronounceableName(8);

            //Check if the name has been used
            do
            {
                //nothing
            } while (partnmeExists(part_nme));

            txtPartNmes.Text = GlobalVar.ToTitleCase(part_nme);
        }

        private bool partnmeExists(string pnme)
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM part_reg WHERE part_name = @name";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@name", pnme);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (!mysql_datareader.HasRows)
            {
                mysql_conn.Close();

                return false;

                
            }
            

            mysql_conn.Close();
            return true;
        }

        private void regData()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "INSERT INTO part_reg (part_name,dob,pob,left_eye,right_eye) "
                + "VALUES(@part_nme,@dob,@pob,@left_eye,@right_eye)";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@part_nme", txtPartNmes.Text);
            mysql_cmd.Parameters.AddWithValue("@dob", dteDOB.DateTime.ToString("yyyy-MM-dd"));
            mysql_cmd.Parameters.AddWithValue("@pob", txtPOB.Text);
            mysql_cmd.Parameters.AddWithValue("@left_eye", Convert.ToBase64String((byte[])enrollment_temps[1]));
            mysql_cmd.Parameters.AddWithValue("@right_eye", Convert.ToBase64String((byte[])enrollment_temps[0]));

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.RecordsAffected > 0)
            {

                XtraMessageBox.Show("Participant Registered Successfully", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                genUUPID(mysql_cmd.LastInsertedId);

                loadParticipants();

                DialogResult drslt = XtraMessageBox.Show("Place Participant into a Study?", this.Text,
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (drslt == DialogResult.Yes)
                {
                    //Open the study placement form
                    frmStudySel sel_study_frm = new frmStudySel();
                    sel_study_frm.loadUUPID(lblUUPID.Text);
                    sel_study_frm.ShowDialog();
                }

                clearText();
            }
            else
            {
                XtraMessageBox.Show("Participant Not Registered Try again later", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            mysql_conn.Close();
        }

        private void genUUPID(long p)
        {
            lblUUPID.Text = "MSMC/" + getProgName(GlobalVar.sel_IP) + "/" + p.ToString() + "/" + GlobalVar.sel_IP_fac+ "/" + GlobalVar.user_id + "/" + DateTime.Now.Year;

            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "UPDATE part_reg SET uupid = @uupid WHERE id = @id";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@uupid", lblUUPID.Text);
            mysql_cmd.Parameters.AddWithValue("@id", p);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.RecordsAffected > 0)
            {
                regIRISImgs();

                XtraMessageBox.Show("Participant UUPID Registered Successfully", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                
            }
            else
            {
                XtraMessageBox.Show("Participant Not Registered Try again later", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            mysql_conn.Close();
        }

        private void regIRISImgs()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "INSERT INTO bio_imgs (uuid,l_eye,r_eye) VALUES(@uupid,@LE,@RE)";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@uupid", lblUUPID.Text);
            mysql_cmd.Parameters.AddWithValue("@LE", Convert.ToBase64String(EncryptBytes(imgtoByteArray(imgLeftEye), "MSMC2019!", "MSMC2019!")));
            mysql_cmd.Parameters.AddWithValue("@RE", Convert.ToBase64String(EncryptBytes(imgtoByteArray(imgRightEye), "MSMC2019!", "MSMC2019!")));

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.RecordsAffected > 0)
            {
                XtraMessageBox.Show("IRIS Images Backed up Successfully", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            mysql_conn.Close();
        }

        private static byte[] imgtoByteArray(Image x)
        {
            MemoryStream ms = new MemoryStream();
            x.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }

        private static byte[] EncryptBytes(byte[] inputBytes, string passPhrase, string saltValue)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            RijndaelCipher.Mode = CipherMode.CBC;
            byte[] salt = Encoding.ASCII.GetBytes(saltValue);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, salt, "SHA1", 2);

            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(password.GetBytes(32), password.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            return CipherBytes;
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
                while (mysql_datareader.Read())
                {
                    string p_nme = mysql_datareader["prog_name"].ToString();
                    mysql_conn.Close();

                    return p_nme;
                }


            }


            mysql_conn.Close();
            return null;
        }

        private void clearText()
        {
            imgIRIS.Image = null;

            enrollment_temps.Clear();

            txtPartNmes.Text = "";

            txtPOB.Text = "";

            dteDOB.EditValue = null;
        }

        private void loadParticipants()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM part_reg";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                uupids.Clear();
                iris_temps.Clear();

                DataTable tblPart = new DataTable();
                tblPart.Columns.Add("Names", typeof(object));
                tblPart.Columns.Add("DOB", typeof(object));
                tblPart.Columns.Add("Place_of_birth", typeof(object));

                while (mysql_datareader.Read())
                {
                    uupids.Add(mysql_datareader["uupid"]);
                    iris_temps.Add(Convert.FromBase64String(mysql_datareader["left_eye"].ToString()));
                    
                    uupids.Add(mysql_datareader["uupid"]);
                    iris_temps.Add(Convert.FromBase64String(mysql_datareader["right_eye"].ToString()));

                    tblPart.Rows.Add(mysql_datareader["part_name"], mysql_datareader["dob"], mysql_datareader["pob"]);
                }

                grdPart.DataSource = tblPart;

                grdPart.RefreshDataSource();

                XtraMessageBox.Show("Participants Loaded", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            mysql_conn.Close();
        }

        private bool valData()
        {
            if (dteDOB.EditValue != null)
            {
                if (!String.IsNullOrEmpty(txtPOB.Text))
                {
                    txtPOB.Text = GlobalVar.ToTitleCase(txtPOB.Text);
                    return true;
                }
                else
                {
                    XtraMessageBox.Show("Kindly enter the participants Place of Birth", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                XtraMessageBox.Show("Kindly enter the participants Date of Birth", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }
    }
}