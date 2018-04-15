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
       // private Debug_program debug;
        private LinkedList<Task> tasks;
        object lockObject = new object();
        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageServer"/> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="imageController">The image controller.</param>
        public ImageServer(ILoggingService loggingService, IImageController imageController)
        {
          //  debug = new Debug_program();
           // tasks = new LinkedList<Task>();
            m_logging = loggingService;
            m_controller = imageController;
            
            string[] dirPaths = ConfigurationManager.AppSettings["Handler"].Split(';');
            foreach (string path in dirPaths)
            {
                //**need to check that dirPath is valid??***
                //**************************************
                // tasks.AddFirst(Task.Factory.StartNew(() => CreateHandler(path)));
                CreateHandler(path);
                //Task.Factory.StartNew(() => CreateHandler(path));
                //CreateHandler(path);
            }
        }

        /// <summary>
        /// Creates the handler.
        /// </summary>
        /// <param name="dirPath">The dir path.</param>
        public void CreateHandler(string dirPath)
        {
           // debug.write("dirPath\n");
            IDirectoryHandler dirHandler = new DirectoyHandler(dirPath, m_logging, m_controller);
            CommandRecieved += dirHandler.OnCommandRecieved;
            dirHandler.DirectoryClose += OnClose;
        }
        /// <summary>
        /// Raises the Close event.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="dirArgs">The <see cref="DirectoryCloseEventArgs"/> instance containing the event data.</param>
        public void OnClose(object o, DirectoryCloseEventArgs dirArgs)
        {
            IDirectoryHandler dirHandler = (IDirectoryHandler)o;
            CommandRecieved -= dirHandler.OnCommandRecieved;
            // CommandRecieved
            dirHandler.StopWatcher();


            string closingMessage = "The directory: " + dirArgs.DirectoryPath + "was closed";
            m_logging.Log(closingMessage, Logging.Modal.MessageTypeEnum.INFO);

        }

        /// <summary>
        /// Closes all the handlers.
        /// </summary>
        public void CloseAll()
        {
            string[] message = { "directory has been closed" };
            CommandRecievedEventArgs cre = new CommandRecievedEventArgs(1, message, null);
            CommandRecieved.Invoke(this, cre);
        }
    }
       
}
