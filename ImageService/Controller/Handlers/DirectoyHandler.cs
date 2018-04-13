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
        private Debug_program debug;
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        private string[] extensionsToListen = { "*.jpg", "*.gif", "*.png", "*.bmp" };   // List for valid extensions.
        #endregion
        public DirectoyHandler(string dirPath, ILoggingService loggingService, IImageController imageController)
        {
            debug = new Debug_program();
            m_path = dirPath;
            m_logging = loggingService;
            m_controller = imageController;
            StartHandleDirectory(dirPath);
        }

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;             // The Event That Notifies that the Directory is being closed

        public void StartHandleDirectory(string dirPath)
        {
            debug.write("StartHandleDirectory\n");
            string startMessage = "Handling directory: " + dirPath;
            m_logging.Log(startMessage, MessageTypeEnum.INFO);
            InitializeWatcher(dirPath);
           // m_dirWatcher.Created += StartWatching;
        }
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            debug.write("on command recived");
            bool result;
            string message= m_controller.ExecuteCommand(e.CommandID, e.Args, out result);
            m_logging.Log(message, MessageTypeEnum.INFO);
            if (string.Compare(message,"closeDriectory")==0)
            {
                DirectoryCloseEventArgs close = new DirectoryCloseEventArgs(m_path, e.Args[1]);
                DirectoryClose.Invoke(this, close);
            }
        }
        public void StopWatcher()
        {
            debug.write("stopeed watcher");
            m_dirWatcher.EnableRaisingEvents = false;
        }
        public void InitializeWatcher(string dirPath)
        {
            m_dirWatcher = new FileSystemWatcher();
            m_dirWatcher.Path = dirPath;
            m_dirWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            m_dirWatcher.Filter = "*.*";
            m_dirWatcher.Created += new FileSystemEventHandler(OnChanged);
            m_dirWatcher.EnableRaisingEvents = true;

        }
        //checks if extension exist in extensionsToListen. if yes-use CommandRecievedEventArgs to notice.
        private void OnChanged(object o, FileSystemEventArgs comArgs)
        {
            debug.write("OnChanged");
            string argsFullPath = comArgs.FullPath;
            string[] args = { comArgs.FullPath };
            string fileExtension = Path.GetExtension(argsFullPath);
            debug.write(fileExtension);
            if (extensionsToListen.Contains("*" + fileExtension))
            {
                CommandRecievedEventArgs commandArgs = new CommandRecievedEventArgs((int)CommandEnum.NewFileCommand, args, fileExtension);
                OnCommandRecieved(this, commandArgs);
            }
        }
       
    }
}
