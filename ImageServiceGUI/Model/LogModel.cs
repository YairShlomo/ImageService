using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ImageService.Logging.Modal;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;
using System.Windows.Data;
using ImageService.Logging;

using System.Windows;

using Newtonsoft.Json;
namespace ImageServiceGUI.Model
{
    class LogModel : ILogModel
    {
        public LogModel()
        {
            client = GuiClient.Instance;
            client.Recieve();
            client.ExecuteReceived += ExecuteReceived;
            InitData();
        }
        #region Notify Changed
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        private GuiClient client { get; set; }



        public ObservableCollection<Log> logs
        {
            get
            {
                return logs;
            }
            set => throw new NotImplementedException();
        }

        private void InitData()
        {
            try
            {
                logs = new ObservableCollection<Log>();
                Object thisLock = new Object();
                BindingOperations.EnableCollectionSynchronization(logs, thisLock);
                CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, "");
                client.Send(commandRecievedEventArgs);
            }
            catch (Exception ex)
            {

            }
        }

        private void ExecuteReceived(CommandRecievedEventArgs arrivedMessage)
        {
            try
            {
                if (arrivedMessage != null)
                {
                    switch (arrivedMessage.CommandID)
                    {
                        case (int)CommandEnum.LogCommand:
                            Update(arrivedMessage);
                            break;
                        case (int)CommandEnum.AddLog:
                            AddLog(arrivedMessage);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Update(CommandRecievedEventArgs arrivedMessage)
        {
            try
            {
                foreach (Log log in JsonConvert.DeserializeObject<ObservableCollection<Log>>(arrivedMessage.Args[0]))
                {
                    this.logs.Add(log);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AddLog(CommandRecievedEventArgs responseObj)
        {
            try
            {
                Log newLogEntry = new Log { Type = responseObj.Args[0], Message = responseObj.Args[1] };
                logs.Insert(0,newLogEntry);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

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
