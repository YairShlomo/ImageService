﻿using ImageService.Infrastructure;
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
                    DirectoryInfo targetPath = Directory.CreateDirectory(OutputFolder + "\\" + yearOfCreation + "\\" + monthOfCreation);
                    DirectoryInfo targetPathThumbnail = Directory.CreateDirectory(thumbnailPath + "\\" + yearOfCreation + "\\" + monthOfCreation);
                    File.Copy(path, targetPath.ToString());
                    //Save the thumbnail image.
                    Image thumbImage = Image.FromFile(path);
                    thumbImage = thumbImage.GetThumbnailImage(m_thumbnailSize, m_thumbnailSize, () => false, IntPtr.Zero);
                    thumbImage.Save(targetPathThumbnail.ToString() + "\\" + fullNamePath);
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
    }
}
