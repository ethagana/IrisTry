namespace IRISTRY
{
    internal class ErrorCodes
    {
        public const int ERR_T10_AX_SUCCESS = 0; /* SDK is not initialized*/
        public const int ERR_T10_AX_NOT_INIT = 101; /* SDK is not initialized*/
        public const int ERR_T10_AX_OPEN = 102; /* open error*/
        public const int ERR_T10_AX_PARAMETER = 103; /* Invalid parameter*/
        public const int ERR_T10_AX_INVALID_STATE = 104; /* SDK not in proper state*/
        public const int ERR_T10_AX_CB_EVENT = 105; /* Error in callback mechanism*/
        public const int ERR_T10_AX_API_FAIL = 106; /* Error in callback mechanism*/
        public const int ERR_T10_AX_UNKNOWN = -1;
        public const int SDK_ERR_INVALID_IMAGE_TYPE = 107;/* Error in expected imagetype  */
        public const int SDK_ERR_IN_MEMORY_CREATION		= 108; /* Error in Memory creation */
        public const int SDK_ERR_IMAGE_SIZE_NOT_IN_RANGE	= 109; /* Error in image size */
        public const int SDK_IAIRIS_NO_MATCH_FOUND		=	110;
        public const int SDK_IAIRIS_ERROR_POOR_FOCUS	=		111;
        public const int SDK_IAIRIS_ERROR_POOR_PUPIL_BOUNDARY =	112;
        public const int SDK_IAIRIS_ERROR_WEAK_LIMBUS		=	113;
        public const int SDK_IAIRIS_ERROR_LOW_TEXTURE		=	114;
        public const int SDK_ERR_INVALID_COMPRESSION_RATIO	=	115;
        /* Transaction Error Types */
        public const int ERR_TRANSACTION_IMAGE = 1;
        public const int ERR_TRANSACTION_ILLUMINATION = 2;
        public const int ERR_TRANSACTION_SHUTTER = 3;
        public const int ERR_TRANSACTION_SDK_ABORT = 4;
    };
}