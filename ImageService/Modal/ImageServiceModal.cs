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
        public string OutputFolder
        {
            get { return m_OutputFolder; }
            set { m_OutputFolder = value; }
        }
        public int ThumbnailSize
        {
            get { return m_thumbnailSize; }
            set { m_thumbnailSize = value; }
        }
        
        

        #endregion
        public string AddFile(string path, out bool result)
        {
            
            result = true;
            try
            {
                string strResult;

                if (File.Exists(path))
                {
                    if (!Directory.Exists(path))
                    {
                        
                        
                        Directory.CreateDirectory(OutputFolder);
                    }
                    string fullNamePath = Path.GetFileName(path);
                    string thumbnailPath = OutputFolder + "\\Thumbnails";
                    Directory.CreateDirectory(thumbnailPath);
                    DateTime creation = File.GetCreationTime(path);
                    string year = creation.Year.ToString();
                    string month = creation.Month.ToString();
                    Directory.CreateDirectory(OutputFolder + "\\" + year);
                    Directory.CreateDirectory(thumbnailPath + "\\" + year);
                    //Create the directory for the month
                    DirectoryInfo locationToCopy = Directory.CreateDirectory(OutputFolder + "\\" + year + "\\" + month);
                    DirectoryInfo locationToCopyThumbnail = Directory.CreateDirectory(thumbnailPath + "\\" + year + "\\" + month);
                    File.Copy(path, locationToCopy.ToString());
                    //Save the thumbnail image.
                    Image thumbImage = Image.FromFile(path);
                    thumbImage = thumbImage.GetThumbnailImage(m_thumbnailSize, m_thumbnailSize, () => false, IntPtr.Zero);
                    thumbImage.Save(locationToCopyThumbnail.ToString() + "\\" + fullNamePath);

                    result = true;
                    return locationToCopy.ToString() + "\\" + fullNamePath;
                }
                else
                {
                    result = false;
                    strResult = "File didnt added-wrong Image path ";

                }
                
                if (result)
                {
                    strResult = "File added Successfully ";
                }
                else
                {
                }
                return strResult;
            }
            catch (Exception e)
            {
                result = false;
                return e.ToString();
            }

        }
    }
}
