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
using System.Configuration;
namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        #endregion
        public ImageServiceModal()
        {
            m_OutputFolder = ConfigurationManager.AppSettings["OutputDir"];
            try
            {
                m_thumbnailSize = Int32.Parse(ConfigurationManager.AppSettings["ThumbnailSize"]);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            // 
        }
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
        

        public string AddFile(string path, out bool result)
        {

            result = true;
            try
            {
                string strResult;
                if (File.Exists(path))
                {

                    Debug_program debug = new Debug_program();
                    debug.write("addfile");
                   

                    //create output directory if doesnt exist
                    Directory.CreateDirectory(OutputFolder);
                    string fullNamePath = Path.GetFileName(path);
                    string thumbnailPath = OutputFolder + "\\Thumbnails";
                    Directory.CreateDirectory(thumbnailPath);
                    DateTime creation = File.GetCreationTime(path);
                    string yearOfCreation = creation.Year.ToString();
                    string monthOfCreation = creation.Month.ToString();
                    Directory.CreateDirectory(OutputFolder + "\\" + yearOfCreation);
                    Directory.CreateDirectory(thumbnailPath + "\\" + yearOfCreation);
                    //Create the directory for the monthOfCreation
                    string targetPathDir = OutputFolder + "\\" + yearOfCreation + "\\" + monthOfCreation;
                    DirectoryInfo dir = Directory.CreateDirectory(targetPathDir);
                    string targetPath = targetPathDir + "\\" + fullNamePath;
                    string targetPathThumbnail= thumbnailPath + "\\" + yearOfCreation + "\\" + monthOfCreation;
                    DirectoryInfo dirThumbnail = Directory.CreateDirectory(thumbnailPath + "\\" + yearOfCreation + "\\" + monthOfCreation);
                    string pathExtension = Path.GetExtension(targetPath);
                    targetPath = isFileExist(targetPath, pathExtension);
                    File.Move(path, targetPath);
                   debug.write(path);
                    debug.write(targetPath);
                   debug.write(targetPathDir);
                    debug.write("1");
                    //Save the thumbnail image.
                    Image thumbImage = Image.FromFile(targetPath);
                    debug.write("2");
                    thumbImage = thumbImage.GetThumbnailImage(m_thumbnailSize, m_thumbnailSize, () => false, IntPtr.Zero);                    
                    thumbImage.Save(targetPathThumbnail.ToString() + "\\" + fullNamePath);
                    debug.write("saved thumbImage ");
                    result = true;
                    return targetPath.ToString() + "\\" + fullNamePath;
                }
                else
                {
                    result = false;
                    strResult = "File didn't added-wrong Image path ";

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

        public string isFileExist(string targetPath, string pathExtension)
        {
            int counter = 1;
            while (File.Exists(targetPath))
            {
                string noExtesnsion = targetPath.Replace(pathExtension, "");
                int numericValue;
                if (Int32.TryParse(noExtesnsion.Substring(noExtesnsion.Length - 1), out numericValue))
                {
                    noExtesnsion = noExtesnsion.Substring(0, noExtesnsion.Length - 1);
                }
                targetPath = noExtesnsion + counter + pathExtension;
                counter++;
            }
            return targetPath;
        }
    }
}
