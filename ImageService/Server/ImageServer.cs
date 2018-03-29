using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Configuration;
namespace ImageService.Server
{
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        #endregion

        public ImageServer(ILoggingService loggingService, IImageController imageController)
        {
            m_logging = loggingService;
            m_controller = imageController;
            
            string[] dirPaths = ConfigurationManager.AppSettings["Handler"].Split(';');
            foreach (string path in dirPaths)
            {
                //**need to check that dirPath is valid??***
                //**************************************
                CreateHandler(path);
            }
        }

        public void CreateHandler(string dirPath)
        {
            IDirectoryHandler dirHandler = new DirectoyHandler(dirPath, m_logging, m_controller);
            CommandRecieved += dirHandler.OnCommandRecieved;
            dirHandler.DirectoryClose += onClose;

        }
        public void onClose(object o, DirectoryCloseEventArgs dirArgs)
        {
            IDirectoryHandler dirHandler = (IDirectoryHandler)o;
            CommandRecieved -= dirHandler.OnCommandRecieved;
            string closingMessage = "The directory: " + dirArgs.DirectoryPath + "was closed";
            m_logging.Log(closingMessage, Logging.Modal.MessageTypeEnum.INFO);

        }
    }
       
}
