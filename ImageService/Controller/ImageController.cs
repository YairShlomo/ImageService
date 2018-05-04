using ImageService.Commands;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        private IImageServiceModal m_modal;                      // The Modal Object
        private Dictionary<int, ICommand> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageController"/> class.
        /// </summary>
        /// <param name="modal">The modal.</param>
        public ImageController(IImageServiceModal modal)
        {
            m_modal = modal;                    // Storing the Modal Of The System
            CommandEnum y = CommandEnum.NewFileCommand;
            CommandEnum c = CommandEnum.CloseCommand;
            CommandEnum g = CommandEnum.GetConfigCommand;
            CommandEnum l = CommandEnum.LogCommand;

            commands = new Dictionary<int, ICommand>()
            {
                // For Now will contain NEW_FILE_COMMAND
                {(int)y, new NewFileCommand(m_modal) },
                {(int)c, new CloseCommand(m_modal)},
                 {(int)g, new GetConfigCommand()},
                 { (int)l, new LogCommand()}
        };
        }
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandID">The command identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="resultSuccesful">if set to <c>true</c> [result succesful].</param>
        /// <returns></returns>
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
            ICommand commandObj = commands[commandID];
            return commandObj.Execute(args, out resultSuccesful);
        }
    }
}