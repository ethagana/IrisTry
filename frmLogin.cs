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
using CSharpSample;
using System.Drawing.Imaging;
using System.IO;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Diagnostics;

namespace IRISTRY
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        private TiltStatus DeviceTiltStatus { get; set; }

        [DllImport("FreeImageConverter.dll")]
        private static extern int ConvertRawToBMP(byte[] pucRawImage, int iRawImageWidth, int iRawImageHeight,
                                                   ref IntPtr pucBMPImage, ref int lImageSize);

        [DllImport("FreeImageConverter.dll")]
        private static extern int ReleaseMemory(IntPtr pucImageData);

        private ArrayList ids = new ArrayList();
        private ArrayList iris_temps = new ArrayList();

        private readonly iCAMT10ControlWrapper _iCAMT10Control;
        private const int MAX_IMAGES_PER_SESSION = 8;
        private readonly ImageMgr _imageMgr;
        private State _appState;
        private int _lastCapturedMode;
        private int CaptureMode = 2; //Primary for identification
        private readonly IiDataIris _iDataIris = new iDataIrisClass();

        private Image imgLeftEye = null;
        private Image imgRightEye = null;

        private Boolean le_capture = false;
        private Boolean re_capture = false;

        private string pid = null;
        private string fac_id = null;

        public frmLogin()
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
            lblReaderStatus.Text = "Reader Status - Connected";
            cmdLogin.Enabled = p;


            _iCAMT10Control.SetBeeper(true); //Enable beeping
            _iCAMT10Control.SetIllumination(true); //Enable the background white light
        }

        private void OnDeviceClosed()
        {
            lblReaderStatus.Text = "Reader Status - Disconnected";
            cmdLogin.Enabled = false;

        }

        private void OnTiltStatus(TiltStatus status)
        {
            if (status == TiltStatus.UpsideDown)
            {
                lblReaderStatus.Text = @"Orientation Sensor: Upside Down";
                lblReaderStatus.ForeColor = Color.Red;


                cmdLogin.Enabled = false;
            }

            if (status == TiltStatus.Normal)
            {
                lblReaderStatus.Text = @"Orientation Sensor: Normal";
                lblReaderStatus.ForeColor = Color.Black;


                cmdLogin.Enabled = true;

                if (_appState == State.CAPTURE_IN_PROGRESS)
                {

                    cmdLogin.Enabled = false;
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
                        idUsr(RE_temp, LE_temp);
                    }
                }


            }
            catch
            {

            }
        }

        private void idUsr(byte[] RE_temp, byte[] LE_temp)
        {
            for (int i = 0; i < iris_temps.Count; i++)
            {
                
                float hd;

                int iResult = _iDataIris.VerifyByLongIrisCode(Constants.MATCHING_MODE_STANDARD, RE_temp, iris_temps[i],
                                                    0.32f, out hd);

                if (iResult != 0)
                {
                    if (iResult == Constants.IAIRIS_LICENCE_EXPIRED)
                    {
                        Debug.WriteLine(@"License expired!" + Environment.NewLine);
                        //else
                        //    Debug.WriteLine(string.Format(
                        //    @"MatchByLongIrisCode failed with error  0x{0:X}{1}", iResult, Environment.NewLine));
                        return;
                    }
                }

                if (hd < 0.33)
                {
                    //Match found - Login User
                    return;
                }
                else if (hd > 0.33)
                {
                    iResult = _iDataIris.VerifyByLongIrisCode(Constants.MATCHING_MODE_STANDARD, LE_temp, iris_temps[i],
                                                    0.32f, out hd);

                    if (iResult != 0)
                    {
                        if (iResult == Constants.IAIRIS_LICENCE_EXPIRED)
                        {
                            Debug.WriteLine(@"License expired!" + Environment.NewLine);
                            //else
                            //    Debug.WriteLine(string.Format(
                            //    @"MatchByLongIrisCode failed with error  0x{0:X}{1}", iResult, Environment.NewLine));
                            return;
                        }
                    }

                    if (hd < 0.33)
                    {
                        //Match found - Login User
                        XtraMessageBox.Show("Welcome", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                        GlobalVar.user_id = ids[i].ToString();
                        GlobalVar.sel_IP = pid;
                        GlobalVar.sel_IP_fac = fac_id;

                        closeScanner();

                        frmMain main_frm = new frmMain();
                        main_frm.Show();
                        this.Hide();
                        return;
                    }
                }

            }

            XtraMessageBox.Show("You are not user "+txtEmail.Text, this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


        }

        private void closeScanner()
        {
            int retval = _iCAMT10Control.Close();

            if (retval != ErrorCodes.ERR_T10_AX_SUCCESS)
            {
                //DisplayError(retval);
                DisplayError(retval);
            }

            SetState(State.DEVICE_CLOSED);
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

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void loadUsers()
        {
            MySqlConnection mysql_conn = new MySqlConnection(GlobalVar.connectionstring);

            string query = "SELECT * FROM users WHERE Email = @email";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            mysql_cmd.Parameters.AddWithValue("@email", txtEmail.Text);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                iris_temps.Clear();

                while (mysql_datareader.Read())
                {
                    ids.Add(mysql_datareader["id"].ToString());
                    iris_temps.Add(Convert.FromBase64String(mysql_datareader["left_eye"].ToString()));
                    
                    ids.Add(mysql_datareader["id"].ToString());
                    iris_temps.Add(Convert.FromBase64String(mysql_datareader["right_eye"].ToString()));

                    pid = mysql_datareader["pid"].ToString();
                    fac_id = mysql_datareader["fac_id"].ToString();
                }

                XtraMessageBox.Show("Kindly look into the scanner for verification", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Information);

                prepforScan();
               
            }
            else
            {
                XtraMessageBox.Show("No Users registered, Application will now close", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            mysql_conn.Close();
        }

        private void prepforScan()
        {
            
            imgLeftEye = null;
            imgRightEye = null;
            _imageMgr.Clear();

            
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
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            int retval = _iCAMT10Control.Open();
            if (retval != ErrorCodes.ERR_T10_AX_SUCCESS)
            {
                ShowMessage("Failed to open T10 camera.\nERROR CODE: " + retval);
                return;
            }

            SetState(State.DEVICE_OPENED);
        }

        private void cmdLogin_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtEmail.Text))
            {
                if (GlobalVar.IsValidEmail(txtEmail.Text))
                {
                    loadUsers();
                }
                else
                {
                    XtraMessageBox.Show("Kindly enter a Valid Email", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                XtraMessageBox.Show("Kindly enter your Email", this.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}