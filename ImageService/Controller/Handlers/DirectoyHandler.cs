using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Text.RegularExpressions;
//using ImageService.Controller.Handlers;

namespace ImageService.Controller.Handlers
{
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        private string[] extensionsToListen = { "*.jpg", "*.gif", "*.png", "*.bmp" };   // List for valid extensions.
        #endregion
        public DirectoyHandler(string dirPath)
        {
            StartHandleDirectory(dirPath);
        }
        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;             // The Event That Notifies that the Directory is being closed

        // Implement Here!
        public void StartHandleDirectory(string dirPath)
        {
            string startMessage = "Handling directory: " + dirPath;
            m_logging.Log(startMessage, MessageTypeEnum.INFO);
            initializeWatcher(dirPath);
            m_dirWatcher.Created += startWatching;
        }
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            bool result;
            m_controller.ExecuteCommand(e.CommandID, e.Args,out result);
        }

        public void initializeWatcher(string dirPath)
        {
            this.m_dirWatcher = new FileSystemWatcher();
            m_dirWatcher.Path = dirPath;
            m_dirWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            m_dirWatcher.Filter = "*.*";
            m_dirWatcher.Created += new FileSystemEventHandler(startWatching);
            m_dirWatcher.EnableRaisingEvents = true;

        }
        //checks if extension exist in extensionsToListen. if yes-use CommandRecievedEventArgs to notice.
        private void startWatching(object o, FileSystemEventArgs comArgs)
        {
            string argsFullPath = comArgs.FullPath;
            string[] args = { comArgs.FullPath };
            string fileExtension = Path.GetExtension(argsFullPath);
            if (extensionsToListen.Contains(fileExtension))
            {
                CommandRecievedEventArgs commandArgs = new CommandRecievedEventArgs(1, args, fileExtension);
                OnCommandRecieved(this, commandArgs);
            }
        }
    }
}
