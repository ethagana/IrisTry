using CSharpSample;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace IRISTRY
{
    internal class ImageMgr
    {
        #region User Defined Types

        private struct ImageInfo
        {
            public readonly int CaptureMode;
            public readonly int EyeType;
            public readonly Image IrisImage;
            public readonly byte[] jp2IrisImage;
            public readonly int ImageType;
            public readonly int jp2ImageSize;

            public ImageInfo(int eyeType, int captureMode, Image irisImage, byte[] bjp2IrisImage, int iImageType, int ijp2ImageSize)
            {
                EyeType = eyeType;
                CaptureMode = captureMode;
                IrisImage = irisImage;
                jp2IrisImage = bjp2IrisImage;
                ImageType = iImageType;
                jp2ImageSize = ijp2ImageSize;
            }
        }

        #endregion User Defined Types

        #region Properties

        public int MaximumImages { get; set; }

        #endregion Properties

        private readonly Dictionary<int, ImageInfo> _imageInfoLeftEye;
        private readonly Dictionary<int, ImageInfo> _imageInfoRightEye;

        #region Constructor

        public ImageMgr(int iMaxImages)
        {
            MaximumImages = iMaxImages;
            _imageInfoRightEye = new Dictionary<int, ImageInfo>(iMaxImages);
            _imageInfoLeftEye = new Dictionary<int, ImageInfo>(iMaxImages);
        }

        #endregion Constructor

        #region Public Methods

        public void AddImage(int imageNumber, int eyeType, int captureMode, Image irisImage, byte[] bjp2IrisImage, int iImageType, int iJp2ImageSize)
        {
            if (imageNumber > MaximumImages) throw (new Exception("Image count exceeded the maximum limit"));

            if (eyeType == Constants.SDK_EYE_RIGHT) _imageInfoRightEye[imageNumber] = new ImageInfo(eyeType, captureMode, irisImage, bjp2IrisImage, iImageType, iJp2ImageSize);
            else _imageInfoLeftEye[imageNumber] = new ImageInfo(eyeType, captureMode, irisImage, bjp2IrisImage, iImageType, iJp2ImageSize);
        }

        public void Clear()
        {
            _imageInfoRightEye.Clear();
            _imageInfoLeftEye.Clear();
        }

        public void GetImage(int eye, int imageNumber, out Image image, out int captureMode)
        {
            image = null;
            eye = Constants.SDK_EYE_RIGHT;
            captureMode = Constants.SDK_CAPTURE_TYPE_AUTO;

            if (eye == Constants.SDK_EYE_RIGHT)
            {
                if (_imageInfoRightEye.ContainsKey(imageNumber) == false)
                    throw (new Exception("Image is not available for the specified image index"));

                ImageInfo info = _imageInfoRightEye[imageNumber];
                image = info.IrisImage;
                captureMode = info.CaptureMode;
            }
            else if (eye == Constants.SDK_EYE_LEFT)
            {
                if (_imageInfoLeftEye.ContainsKey(imageNumber) == false)
                    throw (new Exception("Image is not available for the specified image index"));

                ImageInfo info = _imageInfoLeftEye[imageNumber];
                image = info.IrisImage;
                captureMode = info.CaptureMode;
            }
            else
                throw (new Exception("Invalid eye type"));
        }

        public void SaveImages(string strDirectory, out string saveMessage)
        {
            saveMessage = string.Empty;

            string imageFileName;

            string guid = Guid.NewGuid().ToString();

            foreach (KeyValuePair<int, ImageInfo> pair in _imageInfoRightEye)
            {
                if (pair.Value.ImageType == Constants.SDK_GET_VGA_IRIS_IMAGE || pair.Value.ImageType == Constants.SDK_GET_KIND7_IRIS_IMAGE)
                {
                    imageFileName = string.Format("{0}_{1}_{2}{3}.bmp", GetCaptureModeCode(pair.Value.CaptureMode),
                                               guid, GetEyeTypeCode(pair.Value.EyeType), pair.Key);
                    pair.Value.IrisImage.Save(strDirectory + "\\" + imageFileName, ImageFormat.Bmp);
                    saveMessage += imageFileName + Environment.NewLine;
                }
                else
                {
                    imageFileName = string.Format("{0}_{1}_{2}{3}.jp2", GetCaptureModeCode(pair.Value.CaptureMode),
                                               guid, GetEyeTypeCode(pair.Value.EyeType), pair.Key);
                    SaveToFile(strDirectory + "\\" + imageFileName, pair.Value.jp2IrisImage, pair.Value.jp2ImageSize);
                    saveMessage += imageFileName + Environment.NewLine;
                }
            }

            foreach (KeyValuePair<int, ImageInfo> pair in _imageInfoLeftEye)
            {
                if (pair.Value.ImageType == Constants.SDK_GET_VGA_IRIS_IMAGE || pair.Value.ImageType == Constants.SDK_GET_KIND7_IRIS_IMAGE)
                {
                    imageFileName = string.Format("{0}_{1}_{2}{3}.bmp", GetCaptureModeCode(pair.Value.CaptureMode),
                                                  guid, GetEyeTypeCode(pair.Value.EyeType), pair.Key);
                    pair.Value.IrisImage.Save(strDirectory + "\\" + imageFileName, ImageFormat.Bmp);
                    saveMessage += imageFileName + Environment.NewLine;
                  }
                else
                {
                    imageFileName = string.Format("{0}_{1}_{2}{3}.JP2", GetCaptureModeCode(pair.Value.CaptureMode),
                                               guid, GetEyeTypeCode(pair.Value.EyeType), pair.Key);
                    SaveToFile(strDirectory + "\\" + imageFileName, pair.Value.jp2IrisImage, pair.Value.jp2ImageSize);
                    saveMessage += imageFileName + Environment.NewLine;
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void SaveToFile(string strFileName, byte[] jp2IrisImage, int iSize)
        {
            using (FileStream fs = new FileStream(strFileName, FileMode.CreateNew, FileAccess.Write))
            {
                fs.Write(jp2IrisImage, 0, iSize);
                fs.Close();
            }
        }

        private string GetEyeTypeCode(int eye)
        {
            string eyeTypeCode = string.Empty;

            switch (eye)
            {
                case Constants.SDK_EYE_RIGHT:
                    eyeTypeCode = "R";
                    break;
                case Constants.SDK_EYE_LEFT:
                    eyeTypeCode = "L";
                    break;
            }

            return eyeTypeCode;
        }

        private string GetCaptureModeCode(int captureMode)
        {
            string captureModeCode = string.Empty;

            switch (captureMode)
            {
                case Constants.SDK_MODE_RECOGNITION:
                    captureModeCode = "R";
                    break;
                case Constants.SDK_MODE_ENROLLMENT:
                    captureModeCode = "E";
                    break;
            }

            return captureModeCode;
        }

        #endregion Private Methods
    }
}