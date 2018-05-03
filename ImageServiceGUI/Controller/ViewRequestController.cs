using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using ImageServiceGUI.Infrastructure.Enums;
using ImageServiceGUI.Commands;
/*
namespace ImageServiceGUI.Controller
{
   class ViewRequestController : IViewRequestController
   {
       //private IImageServiceModal m_modal;                      // The Modal Object
       private Dictionary<int, ICommand> commands;
       public ViewRequestController()
       {

         //  m_modal = modal;                    // Storing the Modal Of The System
           CommandEnum log = CommandEnum.GetLogCommand;
           CommandEnum setting = CommandEnum.GetSettingsCommand;
           commands = new Dictionary<int, ICommand>()
           {
               // For Now will contain NEW_FILE_COMMAND
               {(int)log, new GetLogCommand() },
               {(int)setting, new GetSettingsCommand()}


       };

       }

       string ExecuteCommand(int commandID,string[] args, TcpClient client = null)
       {
           ICommand commandObj = commands[commandID];
           return commandObj.Execute(args, client);
       }
   }
}
*/
