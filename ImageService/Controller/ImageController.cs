﻿using ImageService.Commands;
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

        public ImageController(IImageServiceModal modal)
        {
            m_modal = modal;                    // Storing the Modal Of The System
            CommandEnum y = CommandEnum.NewFileCommand;
            commands = new Dictionary<int, ICommand>()
            {
                // For Now will contain NEW_FILE_COMMAND
                {(int)y, new NewFileCommand(m_modal) }

        };
        }
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
            ICommand commandObj = commands[commandID];
            return commandObj.Execute(args, out resultSuccesful);
        }
    }
}