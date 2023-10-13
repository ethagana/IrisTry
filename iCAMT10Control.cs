using CSharpSample;
using iCAMT10ControlAxLib;

namespace IRISTRY
{
    internal enum VersionType
    {
        SDK_VERSION = 0,
        FIRMWARE_VERSION,
        DRIVER_VERSION,
        LIBRARY_VERSION
    };

    internal class iCAMT10ControlWrapper
    {
        #region Delegates

        public delegate void GetCompressedIrisImage(int lStatus, int lEye, int lFrameNumber, int lRawIrisImageWidth, int lRawIrisImageHeight, 
                                        object vRawIrisImage, int lRawIrisImageSize, int lCompressedIrisImageWidth, int lCompressedIrisImageHeight,
                                        object vCompressedIrisImage, int lCompressedIrisImageSize);

        public delegate void GetIrisImage(long eye, long frameNumber, long width, long height, object irisImage, long imageSize);
        
        public delegate void GetLiveInfo(long eye, long width, long height, object liveImage, long imageSize);

        public delegate void GetStatus(int statusType, int statusValue);

        #endregion Delegates

        #region Events

        public event GetStatus OnGetStatus;
        public event GetLiveInfo OnGetLiveInfo;
        public event GetIrisImage OnGetIrisImage;
        public event GetCompressedIrisImage OnGetCompressedIrisImage;

        #endregion Events

        #region Public Properties

        public string SDKVersion
        {
            get
            {
                string strVersion;
                GetVersion(VersionType.SDK_VERSION, out strVersion);
                return strVersion;
            }
        }

        public string FirmwareVersion { get; set; }

        public string LibraryVersion { get; set; }

        public string DriverVersion { get; set; }

        public string SerialNumber { get; set; }

        #endregion Public Properties

        private readonly iCAMT10Control _iCAMT10Control;

        #region Contructor

        public iCAMT10ControlWrapper()
        {
            _iCAMT10Control = new iCAMT10Control();

            _iCAMT10Control.GetStatus += _iCAMT10Control_GetStatus;
            _iCAMT10Control.GetLiveInfo += _iCAMT10Control_GetLiveInfo;
            _iCAMT10Control.GetIrisImage += _iCAMT10Control_GetIrisImage;
            _iCAMT10Control.GetCompressedIrisImage += _iCAMT10Control_GetCompressedIrisImage;

            ResetProperties();
        }

        private void _iCAMT10Control_GetCompressedIrisImage(int lStatus, int lEye, int lFrameNumber, int lRawIrisImageWidth, int lRawIrisImageHeight,
                                                            object vRawIrisImage, int lRawIrisImageSize, int lCompressedIrisImageWidth, 
                                                            int lCompressedIrisImageHeight, object vCompressedIrisImage, int lCompressedIrisImageSize)
        {
            OnGetCompressedIrisImage(lStatus, lEye, lFrameNumber, lRawIrisImageWidth, lRawIrisImageHeight, vRawIrisImage, lRawIrisImageSize,
                                    lCompressedIrisImageWidth, lCompressedIrisImageHeight, vCompressedIrisImage, lCompressedIrisImageSize);
        }

        private void _iCAMT10Control_GetIrisImage(int eye, int frameNumber, int width, int height,
                                                  object irisImage, int imageSize)
        {
            OnGetIrisImage(eye, frameNumber, width, height, irisImage, imageSize);
        }

        private void _iCAMT10Control_GetLiveInfo(int eye, int width, int height, object liveImage, int imageSize)
        {
            OnGetLiveInfo(eye, width, height, liveImage, imageSize);
        }

        private void _iCAMT10Control_GetStatus(int statusType, int value)
        {
            OnGetStatus(statusType, value);
        }
       
        #endregion Contructor

        #region Public Methods

        public int Open()
        {
            int retval = _iCAMT10Control.Open();

            if (retval == ErrorCodes.ERR_T10_AX_SUCCESS)
            {
                string strVersion;
                if (GetVersion(VersionType.LIBRARY_VERSION, out strVersion) == ErrorCodes.ERR_T10_AX_SUCCESS)
                    LibraryVersion = strVersion;

                if (GetVersion(VersionType.FIRMWARE_VERSION, out strVersion) == ErrorCodes.ERR_T10_AX_SUCCESS)
                    FirmwareVersion = strVersion;

                if (GetVersion(VersionType.DRIVER_VERSION, out strVersion) == ErrorCodes.ERR_T10_AX_SUCCESS)
                    DriverVersion = strVersion;

                string strSerialNumber;
                if (GetSerialNumber(out strSerialNumber) == ErrorCodes.ERR_T10_AX_SUCCESS)
                    SerialNumber = strSerialNumber;
            }

            return retval;
        }

        public int Close()
        {
            int retval = _iCAMT10Control.Close();
            if (retval == ErrorCodes.ERR_T10_AX_SUCCESS)
            {
                ResetProperties();
            }

            return retval;
        }

        public int SetOutputIrisImageType(int iIrisImageType, int bVGArequired)
        {
            return _iCAMT10Control.SetOutputIrisImageType(iIrisImageType, bVGArequired);
        }

        public int GetOutputIrisImageType(int iIrisImageType, int bVGArequired)
        {
            return _iCAMT10Control.GetOutputIrisImageType(out iIrisImageType, out bVGArequired);
        }
        public int SetBeeper(bool bEnable)
        {
            return _iCAMT10Control.SetBeeper(bEnable ? Constants.ENABLED : Constants.DISABLED);
        }

        public int SetIllumination(bool bEnable)
        {
            return _iCAMT10Control.SetIllumination(bEnable ? Constants.ENABLED : Constants.DISABLED);
        }

       
        public int StartIrisCapture(int iCaptureMode, int iCaptureType, int iEyeType, int iRequestPriority, int iTimeout)
        {
            return _iCAMT10Control.StartIrisCapture(iCaptureMode, iCaptureType, iEyeType, iRequestPriority, iTimeout);
        }

        public int StopIrisCapture()
        {
            return _iCAMT10Control.AbortIrisCapture();
        }

        public int CaptureIris()
        {
            return _iCAMT10Control.CaptureIris();
        }

        public int SetMoveIrisPosition(int iEyeType, int iX, int iY)
        {
            return _iCAMT10Control.SetMoveIrisPosition(iEyeType, iX, iY);
        }

        #endregion Public Methods

        #region Private Methods

        private int GetVersion(VersionType type, out string strVersion)
        {
            return _iCAMT10Control.GetVersion((int)type, out strVersion);
        }

        private int GetSerialNumber(out string strSerialNumber)
        {
            return _iCAMT10Control.GetSerialNumber(out strSerialNumber);
        }

        private void ResetProperties()
        {
            LibraryVersion = string.Empty;
            FirmwareVersion = string.Empty;
            DriverVersion = string.Empty;
            SerialNumber = string.Empty;
        }

        #endregion Private Methods
    }
}