namespace CSharpSample
{
    internal class Constants
    {
        /* Error Codes */
        public const int SDK_ERR_SUCCESS = 0;

        public const int SDK_ERR_NOT_INIT = 101; /* SDK is not initialized */
        public const int SDK_ERR_OPEN = 102; /* open error */
        public const int SDK_ERR_PARAMETER = 103; /* Invalid parameter */
        public const int SDK_ERR_INVALID_STATE = 104; /* SDK not in proper state */
        public const int SDK_ERR_CB_EVENT = 105; /* Error in callback mechanism */
        public const int SDK_ERR_API_FAIL = 106; /* Error in calling internal API */
        public const int SDK_ERR_UNKNOWN = -1;
        public const int  SDK_ERR_INVALID_IMAGE_TYPE	 =	107; /* Error in expected imagetype  */
        public const int  SDK_ERR_IN_MEMORY_CREATION	 =	108; /* Error in Memory creation */
        public const int SDK_ERR_IMAGE_SIZE_NOT_IN_RANGE =	109; /* Error in image size */
        public const int  SDK_IAIRIS_NO_MATCH_FOUND		 =	110;
        public const int SDK_IAIRIS_ERROR_POOR_FOCUS		 =	111;
        public const int SDK_IAIRIS_ERROR_POOR_PUPIL_BOUNDARY	 = 112;
        public const int  SDK_IAIRIS_ERROR_WEAK_LIMBUS			 = 113;
        public const int  SDK_IAIRIS_ERROR_LOW_TEXTURE		 =	114;
        public const int  SDK_ERR_INVALID_COMPRESSION_RATIO	 =	115;

        /* Version Types */
        public const int SDK_VER_SDK = 0;
        public const int SDK_VER_FIRMWARE = 1;
        public const int SDK_VER_DRIVER = 2;
        public const int SDK_VER_LIBRARY = 3;

        /* Capture Modes */
        public const int SDK_MODE_ENROLLMENT = 1;
        public const int SDK_MODE_RECOGNITION = 2;

        /* Capture Types */
        public const int SDK_CAPTURE_TYPE_AUTO = 0;
        public const int SDK_CAPTURE_TYPE_MANUAL = 1;

        /* Eye Types */
        public const int SDK_EYE_RIGHT = 1;
        public const int SDK_EYE_LEFT = 2;
        public const int SDK_EYE_BOTH = 3;
        public const int SDK_EYE_EITHER = 4;

        /* Capture Priority */
        public const int SDK_PRIORITY_SPEED = 0;
        public const int SDK_PRIORITY_QUALITY = 1;
        public const int SDK_PRIORITY_MULTIPLE = 2;

        /* Status Event Types */
        public const int SDK_EVENT_DEVICE_TILT = 1;
        public const int SDK_EVENT_SURPRISE_REMOVE = 2;
        public const int SDK_EVENT_TXN_TIMEOUT = 3;
        public const int SDK_EVENT_TXN_ERROR = 4;

        /* Device Tilt Status */
        public const int SDK_TILT_OFF = 0;
        public const int SDK_TILT_ON = 1;

        /* Transaction Error Types */
        public const int SDK_ERR_TXN_IMAGE = 1;
        public const int SDK_ERR_TXN_ILLUMINATION = 2;
        public const int SDK_ERR_TXN_SHUTTER = 3;
        public const int SDK_ERR_TXN_ABORT = 4;

        /* Enable / Disable setting */
        public const int ENABLED = 1;
        public const int DISABLED = 0;

        /* Constants for setting the expected irisimage type */
        public const int SDK_GET_VGA_IRIS_IMAGE					=		1;
        public const int SDK_GET_KIND7_IRIS_IMAGE				=		2;
        public const int SDK_GET_KIND7_COMPRESSED_LESSTHAN_1DOT5KB_IRIS_IMAGE = 3;
        public const int SDK_GET_KIND7_COMPRESSED_LESSTHAN_2DOT5KB_IRIS_IMAGE = 4;
        public const int SDK_GET_KIND7_COMPRESSED_LESSTHAN_3DOT5KB_IRIS_IMAGE	=	5;
        public const int SDK_GET_KIND7_COMPRESSED_LESSTHAN_5KB_IRIS_IMAGE = 6;

        // Cropping modes
        public const int CROP_TYPE_CROPPED = 400;
        public const int CROP_TYPE_CROP_AND_MASKED = 500;

        // Image Type
        public const int IRIS_IMAGE_RECT = 100;
        public const int IRIS_IMAGE_POLAR_NO_BOUND_POLAR = 200;
        public const int IRIS_IMAGE_POLAR_BOUND = 300;

        //Matching Modes
        public const int MATCHING_MODE_STANDARD = 1;
        public const int MATCHING_MODE_EXPRESS = 2;

        public const int IRIS_IMAGE_HEIGHT = 480;
        public const int IRIS_IMAGE_WIDTH = 640;

        // capture device id(this is user defined)
        public const int DEV_ID_ICAM7000 = 1;
        public const int DEV_ID_TD100 = 2;

        //image transformation
        public const int TRANS_UNDEF = 0;
        public const int TRANS_STD = 1;

        //image properties (this is user defined)
        public const int IMG_PROP_RT_RECT = unchecked(0x0000);
        public const int IMG_PROP_RT_POLAR = unchecked(0x0001);

        //Image formats
        public const int IMAGEFORMAT_MONO_RAW = unchecked(0X0002);
        public const int IMAGEFORMAT_RGB_RAW = unchecked(0X0004);
        public const int IMAGEFORMAT_MONO_JPEG = unchecked(0X0006);
        public const int IMAGEFORMAT_RGB_JPEG = unchecked(0X0008);
        public const int IMAGEFORMAT_MONO_JPEG_LS = unchecked(0X000A);
        public const int IMAGEFORMAT_RGB_JPEG_LS = unchecked(0X000C);
        public const int IMAGEFORMAT_MONO_JPEG2000 = unchecked(0X000E);
        public const int IMAGEFORMAT_RGB_JPEG2000 = unchecked(0X0010);
        public const int IMAGEFORMAT_MONO_BMP = unchecked(0X000B);
        //Image formats for ISO 2011
		public const int ISO2011_IMAGEFORMAT_MONO_RAW =	2;
		public const int ISO2011_IMAGEFORMAT_MONO_JPEG2000 = 10; // 0X0A
		public const int ISO2011_IMAGEFORMAT_MONO_PNG = 14; // 0X0E

        //// ISO Error codes
		public const int IAIRIS_LICENCE_EXPIRED = -2097151994;
		public const int IAIRIS_NOT_MATCHED = -2147414015;
		public const int IAIRIS_ERROR_CONTACT_LENS = -2147418073;
    }

	public enum enmImageFormat
	{
		RAW, JPEG, JPEG2K, PNG, BMP
	};
    
}