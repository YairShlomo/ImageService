using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ImageService.Logging.Modal;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;
namespace ImageServiceGUI.Model
{
    class LogModel : ILogModel
    {
        public LogModel()
        {
            client = new ISClient();
            CommandRecievedEventArgs commandArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, null);
            client.Send(commandArgs);
        }
        #region Notify Changed
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        private IISClient client;
        //{ return m_outputDirectory; }
        private ObservableCollection<Tuple<MessageTypeEnum, string>> messages;
    public ObservableCollection<Tuple<MessageTypeEnum, string>> LogMessages
        {
            get { return messages; }
            set
            {
                messages = value;
                OnPropertyChanged("LogMessages");
            }
        }
    }
}
