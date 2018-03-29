using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
//using ImageService.Modal;
using ImageService.Logging;
using ImageService.Logging.Modal;
namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        private ILoggingService ls;

        #endregion
        public string AddFile(string path, out bool result) {
            result = true;
            if (!File.Exists(path))   {
                result = false;
            }
            string strResult;
            if (result) {
                strResult = "adding filed Succesfully";
            }
            else  {
                strResult = "adding filed failed";
            }
            ls.Log(strResult, MessageTypeEnum.INFO);
            return strResult;
        }

    }
}
