using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ImageServiceGUI.Communication;
using ImageService.Logging.Modal;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;
using System.Windows.Data;

namespace ImageServiceGUI.Model
{
    class SettingModel : ISettingModel
    {
        public SettingModel()
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
        public GuiClient client { get; set; }
        // private ObservableCollection<string> Handlers = new ObservableCollection<string>();



        private void InitData()
        {
            try
            {
                OutputDirectory = string.Empty;
                SourceName = string.Empty;
                LogName = string.Empty;
                TumbnailSize = string.Empty;
                Handlers = new ObservableCollection<string>();
                Object thisLock = new Object();
                BindingOperations.EnableCollectionSynchronization(Handlers, thisLock);
                string[] arr = new string[5];
                CommandRecievedEventArgs request = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, arr, "");
                client.Send(request);
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
                        case (int)CommandEnum.GetConfigCommand:
                            Update(arrivedMessage);
                            break;
                        case (int)CommandEnum.CloseHandler:
                            CloseHandler(arrivedMessage);
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
                OutputDirectory = arrivedMessage.Args[0];
                SourceName = arrivedMessage.Args[1];
                LogName = arrivedMessage.Args[2];
                TumbnailSize = arrivedMessage.Args[3];
                string[] handlers = arrivedMessage.Args[4].Split(';');
                foreach (string handler in handlers)
                {
                    Handlers.Add(handler);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CloseHandler(CommandRecievedEventArgs arrivedMessage)
        {
            if (Handlers != null && Handlers.Count > 0 && arrivedMessage.Args != null
                                 && Handlers.Contains(arrivedMessage.Args[0]))
            {
                Handlers.Remove(arrivedMessage.Args[0]);
            }
        }

        private string m_outputDirectory;
        public string OutputDirectory
        {
            get { return m_outputDirectory; }
            set
            {
                m_outputDirectory = value;
                OnPropertyChanged("OutputDirectory");
            }
        }
        private string m_sourceName;
        public string SourceName
        {
            get { return m_sourceName; }
            set
            {
                m_sourceName = value;
                OnPropertyChanged("SourceName");
            }
        }
        private string m_logName;
        public string LogName
        {
            get { return m_logName; }
            set
            {
                m_logName = value;
                OnPropertyChanged("LogName");
            }
        }
        private string m_tumbnailSize;
        public string TumbnailSize
        {
            get { return m_tumbnailSize; }
            set
            {
                m_tumbnailSize = value;
                OnPropertyChanged("TumbnailSize");
            }
        }

        public ObservableCollection<string> Handlers { get; set; }

    }
}

    

