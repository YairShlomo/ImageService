using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using ImageService.Logging;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;
using ImageService.Infrastructure.Modal;
using ImageService.Infrastructure.Modal.Event;

namespace ImageService.Commands
{
    class LogCommand : ICommand
    {
        private ILoggingService loggingService;
        public LogCommand(ILoggingService lg)
        {
            loggingService = lg;
        }
        public string Execute(string[] args, out bool result, TcpClient client = null)
        {
            try
            {
                ObservableCollection<Log> logMessages = this.loggingService.ListLog;
                
                string logMessagesString = JsonConvert.SerializeObject(logMessages);
                string[] Args = { logMessagesString };
                CommandRecievedEventArgs crea = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, Args, null);
                result = true;
                string commandSerialized = JsonConvert.SerializeObject(crea);

                return commandSerialized;
            }
            catch (Exception e)
            {
                result = false;
                string failedMessage = "LogCommand.Execute: Failed execute log command";
                loggingService.Log(failedMessage, MessageTypeEnum.FAIL);
                return failedMessage;
            }
        }
    }
}
