using CSharpSample;
using DevExpress.XtraEditors;
using iDataIrisAxLib;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace IRISTRY
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private TiltStatus DeviceTiltStatus { get; set; }

        [DllImport("FreeImageConverter.dll")]
        private static extern int ConvertRawToBMP(byte[] pucRawImage, int iRawImageWidth, int iRawImageHeight,
                                                   ref IntPtr pucBMPImage, ref int lImageSize);
        [DllImport("FreeImageConverter.dll")]
        private static extern int ConvertJpeg2kToBMP(byte[] pucJpeg2kImage, int iJpeg2kSize,
                                                   ref IntPtr pucBMPImage, ref int lImageSize);
        [DllImport("FreeImageConverter.dll")]
        private static extern int ReleaseMemory(IntPtr pucImageData);

        private const int MAX_IMAGES_PER_SESSION = 8;

        private readonly iCAMT10ControlWrapper _iCAMT10Control;
        private readonly ImageMgr _imageMgr;
        private State _appState;
        private int _lastCapturedMode;
        private int CaptureMode = 0; //Primary for identification
        private bool _bReadyState = false;
        private string EnrollmentFolder = null;
        private string RecognitionFolder = null;
        private bool imgs_saved = false;
        private ArrayList temps = new ArrayList();
        private object[] uuids = null;
        private Boolean match_found = false;
        private Boolean cancel_enrollment = false;
        private Boolean le_capture = false;
        private Boolean re_capture = false;

        private readonly IiDataIris _iDataIris = new iDataIrisClass();

        private static string connectionstring = "server=localhost;database=iris_rsrch;uid=root;pwd=mysql2017;convert zero datetime=True";

        private void CreateImageFolders()
        {
            string strSpecialFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Directory.CreateDirectory(strSpecialFolderPath + @"\iCAM T10 SDK\Enrollment");
            Directory.CreateDirectory(strSpecialFolderPath + @"\iCAM T10 SDK\Recognition");

            EnrollmentFolder = strSpecialFolderPath + @"\iCAM T10 SDK\Enrollment";
            RecognitionFolder = strSpecialFolderPath + @"\iCAM T10 SDK\Recognition";
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

        private void ShowMessage(string strMessage)
        {
            MessageBox.Show(strMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public Form1()
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

        private void Form1_Shown(object sender, EventArgs e)
        {
            //Check if the IRIS scanner is connected
            int retval = _iCAMT10Control.Open();
            if (retval != ErrorCodes.ERR_T10_AX_SUCCESS)
            {
                ShowMessage("Failed to open T10 camera.\nERROR CODE: " + retval);
                return;
            }

            SetState(State.DEVICE_OPENED);
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

        private void OnDeviceOpened(bool p)
        {
            lblReaderStatus.Text = "Reader Status - Connected";
            cmdEnroll.Enabled = p;
            cmdID.Enabled = p;

            _iCAMT10Control.SetBeeper(true); //Enable beeping
            _iCAMT10Control.SetIllumination(true); //Enable the background white light
        }

        private void OnDeviceClosed()
        {
            lblReaderStatus.Text = "Reader Disconnected";
            cmdEnroll.Enabled = false;
            cmdID.Enabled = false;
            imgLEye.Image = null;
            imgREye.Image = null;
        }

        private void OnTiltStatus(TiltStatus status)
        {
            if (status == TiltStatus.UpsideDown)
            {
                lblReaderStatus.Text = @"Orientation Sensor: Upside Down";
                lblReaderStatus.ForeColor = Color.Red;

                cmdID.Enabled = false;
                cmdEnroll.Enabled = false;
            }

            if (status == TiltStatus.Normal)
            {
                lblReaderStatus.Text = @"Orientation Sensor: Normal";
                lblReaderStatus.ForeColor = Color.Black;

                cmdID.Enabled = true;
                cmdEnroll.Enabled = true;

                if (_appState == State.CAPTURE_IN_PROGRESS)
                {
                    cmdID.Enabled = false;
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
                    imgREye.Image = ConvertByteArrayToBitmap((byte[])liveImage,
                                                                   new Size((int)imageWidth, (int)imageHeight));
                    break;
                case Constants.SDK_EYE_LEFT:
                    imgLEye.Image = ConvertByteArrayToBitmap((byte[])liveImage,
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

                int igalleryImageWidth;
                int igalleryImageHeight;

                byte[] byGalleryImageRaw = ImageHelper.ImageToRaw8BitByteArray(imgIrisImage, out igalleryImageWidth,
                                                                         out igalleryImageHeight);

                switch (eye)
                {
                    case Constants.SDK_EYE_RIGHT:
                        imgREye.Image = imgIrisImage;
                        build_Hexes(byGalleryImageRaw, "R",igalleryImageWidth,igalleryImageHeight,imgIrisImage);
                        re_capture = true;
                        break;
                    case Constants.SDK_EYE_LEFT:
                        imgLEye.Image = imgIrisImage;
                        build_Hexes(byGalleryImageRaw, "L",igalleryImageWidth,igalleryImageHeight,imgIrisImage);
                        le_capture = true;
                        break;
                }

                if (!cancel_enrollment)
                {
                    if (CaptureMode == 2 && re_capture && le_capture)
                    {
                        
                        //If capture mode is enroll encrypt image and save it in the database
                        string img_LE_hex = Convert.ToBase64String(ImageHelper.ImageToRaw8BitByteArray(imgLEye.Image, out igalleryImageWidth,
                                                                             out igalleryImageHeight));
                        string img_RE_hex = Convert.ToBase64String(ImageHelper.ImageToRaw8BitByteArray(imgREye.Image, out igalleryImageWidth,
                                                                             out igalleryImageHeight));
                        saveImgs(img_LE_hex, img_RE_hex);

                        le_capture = false;

                        re_capture = false;
                    }
                }

            }
            catch
            {

            }
        }

        private void build_Hexes(byte[] img_arry, string eye,int iris_pic_width,int iris_pic_height, Image iris_img)
        {
            //Check that we have not enrolled this pic before
            object oTemplateIrisCode = null;
            ImageInfo stGalleryImageInfo;
            object oTemplateProcessedImage = null;
            int iTemplateImageQuality = 0;
            byte[] g_ProcessedImageBytes = img_arry;
            int g_ProcessedImageWidth = iris_pic_width;
            int ProcessedImageHeight = iris_pic_height;

            if (CaptureMode == 2 && !cancel_enrollment)
            {
                //int iResult = _iDataIris.CreateIrisCode(Constants.IRIS_IMAGE_RECT, g_ProcessedImageBytes, g_ProcessedImageWidth,
                //                               ProcessedImageHeight, out oTemplateIrisCode, out oTemplateProcessedImage,
                //                               out iTemplateImageQuality, out stGalleryImageInfo);

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
                    return;
                }
                else
                {
                    //Ensure client has not been enrolled first
                    if (!client_Exists(oTemplateIrisCode))
                    {
                        //iResult = _iDataIris.CreateIrisCode(Constants.IRIS_IMAGE_RECT, g_ProcessedImageBytes, g_ProcessedImageWidth,
                        //                               ProcessedImageHeight, out oTemplateIrisCode, out oTemplateProcessedImage,
                        //                        out iTemplateImageQuality, out stGalleryImageInfo);

                        if (iResult == 0)
                        {
                            string irishex = Convert.ToBase64String((byte[])oTemplateIrisCode);

                            saveHex(irishex, eye);
                        }
                    }
                    
                }
                //temps.Add(img_arry);

            }
            else if (CaptureMode == 1)
            {
                int iprobeImageHeight;
                int iprobeImageWidth;
                byte[] byProbeImageRaw = ImageHelper.ImageToRaw8BitByteArray(iris_img, out iprobeImageWidth,
                                                                           out iprobeImageHeight);

                int iResult = _iDataIris.CreateLongIrisCode(Constants.IRIS_IMAGE_RECT, byProbeImageRaw,
                    iprobeImageWidth, iprobeImageHeight, out oTemplateIrisCode, out stGalleryImageInfo);

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
                    return;
                }
                else
                {
                    idUsr(oTemplateIrisCode);

                }

                    //idUsr(oTemplateIrisCode);
            }





            //Run identification

            //int id_rslt = _iDataIris.MatchByLongIrisCode(Constants.MATCHING_MODE_EXPRESS,oTemplateIrisCode,)


        }

        private bool client_Exists(object oTemplateIrisCode)
        {
            for (int i = 0; i < temps.Count; i++)
            {
                int matchedIndex = 0;
                float hd;

                int iResult = _iDataIris.MatchByLongIrisCode(Constants.MATCHING_MODE_STANDARD, oTemplateIrisCode, temps[i],
                                                    1,
                                                    0.32f, ref matchedIndex, out hd);

                //int iResult = _iDataIris.VerifyByLongIrisCode(Constants.MATCHING_MODE_EXPRESS, oTemplateIrisCode, temps[0],
                //                                    0.32f, out hd);

                if (iResult != 0 && iResult != Constants.IAIRIS_NOT_MATCHED)
                {
                    if (iResult == Constants.IAIRIS_LICENCE_EXPIRED)
                        Debug.WriteLine(@"License expired!" + Environment.NewLine);
                    else
                        Debug.WriteLine(string.Format(
                        @"MatchByLongIrisCode failed with error  0x{0:X}{1}", iResult, Environment.NewLine));
                    return false;
                }

                if (matchedIndex == 0)
                {
                    //Match found
                    cancel_enrollment = true;

                    XtraMessageBox.Show("Client found as UUID " + uuids[i].ToString(), this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }

            }
            return false;
        }

        private void idUsr(object oTemplateIrisCode)
        {
            
            ArrayList iris_temps = new ArrayList();
            
            int iResult = 0;

            if (!match_found)
            {
                for (int i = 0; i < temps.Count; i++)
                {
                    int matchedIndex = 0;
                    float hd;

                    iResult = _iDataIris.MatchByLongIrisCode(Constants.MATCHING_MODE_STANDARD, oTemplateIrisCode, temps[i],
                                                        1,
                                                        0.32f, ref matchedIndex, out hd);

                    //int iResult = _iDataIris.VerifyByLongIrisCode(Constants.MATCHING_MODE_EXPRESS, oTemplateIrisCode, temps[0],
                    //                                    0.32f, out hd);

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
                        //Match found
                        match_found = true;

                        XtraMessageBox.Show("Client found as UUID " + uuids[i].ToString(), this.Text,
                          MessageBoxButtons.OK, MessageBoxIcon.Information);

                        break;
                    }

                }
            }

            if (!match_found)
            {
                XtraMessageBox.Show("No Match Found", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
                                imgREye.Image = imgIrisImage;
                                break;
                            case Constants.SDK_EYE_LEFT:
                                imgLEye.Image = imgIrisImage;
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

        private void saveImgs(string img_LE_hex, string img_RE_hex)
        {
            MySqlConnection mysql_conn = new MySqlConnection(connectionstring);

            string query = "INSERT INTO bio_imgs (uuid,l_eye,r_eye) VALUES('785','" + img_LE_hex + "','" + img_RE_hex + "')";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.RecordsAffected > 0)
            {
                XtraMessageBox.Show("IRIS Images Backed up Successfully", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            mysql_conn.Close();

        }

        private void saveHex(string irishex, string eye)
        {
            MySqlConnection mysql_conn = new MySqlConnection(connectionstring);

            string query = "INSERT INTO bio_templates (uuid,temp,eye) VALUES('786','" + irishex + "','" + eye + "')";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            //mysql_cmd.Parameters.AddWithValue("@otemp", o_temp);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.RecordsAffected > 0)
            {
                if (eye.Equals("R"))
                {
                    eye = "RIGHT";
                }
                else
                {
                    eye = "LEFT";
                }
                XtraMessageBox.Show("Biometric Template Registered for " + eye + " Eye", this.Text,
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            mysql_conn.Close();
        }

        private static string ByteArrayToHex(byte[] barray)
        {
            StringBuilder hex = new StringBuilder(barray.Length * 2);
            foreach (byte b in barray)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
            //char[] c = new char[barray.Length * 2];
            //byte b;
            //for (int i = 0; i < barray.Length; ++i)
            //{
            //    b = ((byte)(barray[i] >> 4));
            //    c[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            //    b = ((byte)(barray[i] & 0xF));
            //    c[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            //}
            //return new string(c);
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

        private void cmdID_Click(object sender, EventArgs e)
        {
            CaptureMode = 1;//We are Identifying
            startIRISscan(CaptureMode);
        }

        private void cmdEnroll_Click(object sender, EventArgs e)
        {
            CaptureMode = 2; //We are enrolling
            cancel_enrollment = false;
            startIRISscan(CaptureMode);
        }

        private void startIRISscan(int CaptureMode)
        {
            imgLEye.Image = null;
            imgREye.Image = null;
            imgs_saved = false;
            _imageMgr.Clear();
            match_found = false;

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
            _bReadyState = false;
            _lastCapturedMode = CaptureMode;


        }

        private static byte[] imgtoByteArray(Image x)
        {
            //ImageConverter _imageConverter = new ImageConverter();
            //byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            //return xByte;
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

        public static byte[] DecryptBytes(byte[] encryptedBytes, string passPhrase, string saltValue)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            RijndaelCipher.Mode = CipherMode.CBC;
            byte[] salt = Encoding.ASCII.GetBytes(saltValue);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, salt, "SHA1", 2);

            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(password.GetBytes(32), password.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream(encryptedBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
            byte[] plainBytes = new byte[encryptedBytes.Length];

            int DecryptedCount = cryptoStream.Read(plainBytes, 0, plainBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            return plainBytes;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load the templates in the database
            getTemplates();
           
        }

        

        private void getTemplates()
        {
            //ArrayList ir_temps = new ArrayList();
            ArrayList ir_uuids = new ArrayList();

            //Load the templates into an array for processing
            MySqlConnection mysql_conn = new MySqlConnection(connectionstring);

            string query = "SELECT * FROM bio_templates";

            mysql_conn.Open();

            MySqlCommand mysql_cmd = new MySqlCommand(query, mysql_conn);

            MySqlDataReader mysql_datareader = mysql_cmd.ExecuteReader();

            if (mysql_datareader.HasRows)
            {
                while (mysql_datareader.Read())
                {
                    ir_uuids.Add(mysql_datareader["uuid"]);
                    temps.Add(Convert.FromBase64String(mysql_datareader["temp"].ToString()));
                    
                }

                
                uuids = ir_uuids.ToArray(typeof(object)) as object[];
            }

            mysql_conn.Close();
        }

        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
            //return Enumerable.Range(0, hex.Length)
            //                 .Where(x => x % 2 == 0)
            //                 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            //                 .ToArray();
        }

        public Image ImageFromBuffer(Byte[] bytes)
        {
            Image x = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));

            return x;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            int retval = _iCAMT10Control.Close();

            if (retval != ErrorCodes.ERR_T10_AX_SUCCESS)
            {
                DisplayError(retval);
            }

            SetState(State.DEVICE_CLOSED);
        }
    }
}
