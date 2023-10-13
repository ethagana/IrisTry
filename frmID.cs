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
using System.Collections;
using iDataIrisAxLib;
using CSharpSample;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using MySql.Data.MySqlClient;

namespace IRISTRY
{
    public partial class frmID : DevExpress.XtraEditors.XtraForm
    {
        private TiltStatus DeviceTiltStatus { get; set; }

        [DllImport("FreeImageConverter.dll")]
        private static extern int ConvertRawToBMP(byte[] pucRawImage, int iRawImageWidth, int iRawImageHeight,
                                                   ref IntPtr pucBMPImage, ref int lImageSize);

        [DllImport("FreeImageConverter.dll")]
        private static extern int ReleaseMemory(IntPtr pucImageData);

        private ArrayList le_temps = new ArrayList();
        private ArrayList re_temps = new ArrayList();
        private ArrayList p_nmes = new ArrayList();
        private ArrayList p_dob = new ArrayList();
        private ArrayList p_pob = new ArrayList();
        private ArrayList uupids = new ArrayList();

        private readonly iCAMT10ControlWrapper _iCAMT10Control;
        private const int MAX_IMAGES_PER_SESSION = 20;
        private readonly ImageMgr _imageMgr;
        private State _appState;
        private int _lastCapturedMode;
        private int CaptureMode = 1; //Primary for identification
        private readonly IiDataIris _iDataIris = new iDataIrisClass();

        private Image imgLeftEye = null;
        private Image imgRightEye = null;

        private ArrayList enrollment_temps = new ArrayList();

        private Boolean le_capture = false;
        private Boolean re_capture = false;

        public frmID()
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
            txtReaderStatus.Text = "Reader Status - Connected";
            //cmdEnroll.Enabled = p;


            _iCAMT10Control.SetBeeper(true); //Enable beeping
            _iCAMT10Control.SetIllumination(true); //Enable white led lights - does not work

            XtraMessageBox.Show("Have the Participant remove their glasses if they are wearing any", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void OnDeviceClosed()
        {
            txtReaderStatus.Text = "Reader Status - Disconnected";
            //cmdEnroll.Enabled = false;

        }

        private void OnTiltStatus(TiltStatus status)
        {
            if (status == TiltStatus.UpsideDown)
            {
                txtReaderStatus.Text = @"Orientation Sensor: Upside Down";
                txtReaderStatus.ForeColor = Color.Red;


                //cmdEnroll.Enabled = false;
            }

            if (status == TiltStatus.Normal)
            {
                txtReaderStatus.Text = @"Orientation Sensor: Normal";
                txtReaderStatus.ForeColor = Color.Black;


                //cmdEnroll.Enabled = true;

                if (_appState == State.CAPTURE_IN_PROGRESS)
                {

                    //cmdEnroll.Enabled = false;
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
                    //TODO: Check if we can put a cross hair on this image
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

                    imgLE.BackgroundImage = IRISTRY.Properties.Resources.crosshair_green;
                    imgRE.BackgroundImage = IRISTRY.Properties.Resources.crosshair_green;

                    byte[] byGalleryImageRawR = ImageHelper.ImageToRaw8BitByteArray(imgRightEye, out igalleryImageWidth,
                                                                             out igalleryImageHeight);

                    byte[] byGalleryImageRawL = ImageHelper.ImageToRaw8BitByteArray(imgLeftEye, out igalleryImageWidth,
                                                                             out igalleryImageHeight);

                    //Ensure that the user has no contact lens

                    if (!hasContactLens(byGalleryImageRawL, byGalleryImageRawR, igalleryImageWidth, igalleryImageHeight))
                    {

                        byte[] RE_temp = getIRISTemp(byGalleryImageRawR, "R", igalleryImageWidth, igalleryImageHeight, imgRightEye);
                        byte[] LE_temp = getIRISTemp(byGalleryImageRawL, "L", igalleryImageWidth, igalleryImageHeight, imgLeftEye);

                        //Used to run the identification process
                        object[] eye_temps = new object[2];
                        eye_temps[0] = RE_temp;
                        eye_temps[1] = LE_temp;

                        prgProgress.Visible = true;

                        //So place in readyness for registration
                        enrollment_temps.Add(RE_temp);
                        enrollment_temps.Add(LE_temp);

                        bgwID.RunWorkerAsync(eye_temps);

                        //verifyFirstEnrollment(RE_temp, LE_temp);
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

        private void tmrImgUpdate_Tick(object sender, EventArgs e)
        {
            if (imgLeftEye != null && imgRightEye != null)
            {
                imgLE.BackgroundImage = imgLeftEye;
                imgLE.Image = IRISTRY.Properties.Resources.crosshair_pick_red;
                imgRE.BackgroundImage = imgRightEye;
                imgRE.Image = IRISTRY.Properties.Resources.crosshair_pick_red;
            }
            else
            {
                imgLE.BackgroundImage = null;
                imgRE.BackgroundImage = null;
            }
        }

        private void frmID_Load(object sender, EventArgs e)
        {
            loadParticipants();
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
                le_temps.Clear();
                re_temps.Clear();
                p_dob.Clear();
                p_nmes.Clear();
                p_pob.Clear();

                while (mysql_datareader.Read())
                {
                    uupids.Add(mysql_datareader["uupid"]);
                    le_temps.Add(Convert.FromBase64String(mysql_datareader["left_eye"].ToString()));
                    re_temps.Add(Convert.FromBase64String(mysql_datareader["right_eye"].ToString()));
                    p_dob.Add(mysql_datareader["dob"]);
                    p_nmes.Add(mysql_datareader["part_name"]);
                    p_pob.Add(mysql_datareader["pob"]);


                }

                prgProgress.Properties.Maximum = uupids.Count;

                XtraMessageBox.Show("Ready to Scan", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            mysql_conn.Close();
        }

        private void frmID_Shown(object sender, EventArgs e)
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

            openforID();
        }

        private void openforID()
        {
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
                                                          0); //Using Speed for identification
            if (retval != ErrorCodes.ERR_T10_AX_SUCCESS)
            {
                SetState(State.CAPTURE_FAILED);
                _lastCapturedMode = -1;
                DisplayError(retval);
            }
            //_bReadyState = false;
            _lastCapturedMode = CaptureMode;
        }

        private void frmID_FormClosing(object sender, FormClosingEventArgs e)
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

        private void bgwID_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] eye_temps = (object[])e.Argument;

            byte[] RE_temp = (byte[])eye_temps[0];

            byte[] LE_temp = (byte[])eye_temps[1];

            for (int i = 0; i < uupids.Count; i++)
            {
                int matchedIndex = 0;
                float hd;

                int iResult = _iDataIris.MatchByLongIrisCode(Constants.MATCHING_MODE_STANDARD, RE_temp, re_temps[i],
                                                    1, 0.32f, ref matchedIndex, out hd);

                if (iResult != 0 && iResult != Constants.IAIRIS_NOT_MATCHED)
                {
                    if (iResult == Constants.IAIRIS_LICENCE_EXPIRED)
                        Debug.WriteLine(@"License expired!" + Environment.NewLine);

                }

                if (matchedIndex == 0)
                {
                    //Match Found

                    e.Result = i;

                    return;

                }

                iResult = _iDataIris.MatchByLongIrisCode(Constants.MATCHING_MODE_STANDARD, LE_temp, le_temps[i],
                                                    1, 0.32f, ref matchedIndex, out hd);

                if (iResult != 0 && iResult != Constants.IAIRIS_NOT_MATCHED)
                {
                    if (iResult == Constants.IAIRIS_LICENCE_EXPIRED)
                        Debug.WriteLine(@"License expired!" + Environment.NewLine);

                }

                if (matchedIndex == 0)
                {
                    //Match Found

                    e.Result = i;

                    return;

                    //BeginInvoke(new Action(() =>
                    //{
                    //    frmPartDetails part_dets_frm = new frmPartDetails();
                    //    part_dets_frm.getDetails(uupids[i].ToString(), p_nmes[i].ToString(), p_dob[i].ToString(), p_pob[i].ToString());
                    //    part_dets_frm.Show();
                    //    this.Close();
                    //}));


                }

                bgwID.ReportProgress(i);

            }

            e.Result = -1;
        }

        private void bgwID_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgProgress.Properties.Step = e.ProgressPercentage;
        }

        private void bgwID_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            prgProgress.Visible = false;

            int i = Convert.ToInt32(e.Result.ToString());

            if (i >= 0)
            {
                //Match Found
                frmPartDetails part_dets_frm = new frmPartDetails();
                GlobalVar.part_det_frm = part_dets_frm;
                part_dets_frm.getDetails(uupids[i].ToString(), p_nmes[i].ToString(), p_dob[i].ToString(), p_pob[i].ToString());
                part_dets_frm.Show();
                this.Close();
            }
            else if (i == -1)
            {

                //imgIRIS.Image = IRISTRY.Properties.Resources.iris_icon;

                DialogResult drslt = XtraMessageBox.Show("Participant not found, Register participant?", this.Text,
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (drslt == DialogResult.Yes)
                {
                    //Open the participant registration form
                    frmPartReg part_reg_form = new frmPartReg();
                    part_reg_form.loadTemps(enrollment_temps, imgLE.Image, imgRE.Image);
                    part_reg_form.Show();
                    this.Close();

                }
                else
                {
                    openforID();
                }
            }
        }
    }
}