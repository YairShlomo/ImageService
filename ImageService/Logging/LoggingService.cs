
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageServer.Infrastructure.Modal;


namespace ImageService.Logging
{
    public class LoggingService : ILoggingService
    {
        public ObservableCollection<Log> ListLog {get; set;}
        public LoggingService()
            {
            ListLog = new ObservableCollection<Log>();
            }
        /// <summary>
        /// Occurs when [message recieved].
        /// </summary>
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        public void Log(string message, MessageTypeEnum type)
        {
            string s = Convert.ToString((int)type);
            ListLog.Add(new Log{ Type = (s), Message = message });
            MessageRecievedEventArgs eventArgs = new MessageRecievedEventArgs();
            eventArgs.Message = message;
            eventArgs.Status = type;

            MessageRecieved?.Invoke(this, eventArgs);
        }
    }
}
