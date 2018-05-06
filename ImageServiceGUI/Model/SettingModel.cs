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
namespace ImageServiceGUI.Model
{
    class SettingModel : ISettingModel
    {
        public SettingModel()
        {
            //client = new ISClient();
            CommandRecievedEventArgs commandArgs = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, null);
            //client.Send(commandArgs);
        }
        #region Notify Changed
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        private IISClient client;
        // private ObservableCollection<string> Handlers = new ObservableCollection<string>();

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

        public ObservableCollection<string> Handlers
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

    

