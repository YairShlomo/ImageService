using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Modal;
using System.Collections.ObjectModel;

namespace ImageService.Logging
{
    //public delegate void NewLog(CommandRecievedEventArgs newLog);
    public interface ILoggingService
    {
         ObservableCollection<Log> ListLog { get; set; }

        event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        void Log(string message, MessageTypeEnum type);           // Logging the Message
    }
}
