using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

using ImageService.Infrastructure.Enums;
using System.Windows.Data;
using ImageService.Infrastructure.Modal;
using ImageService.Infrastructure.Modal.Event;
using ImageService.Communication;
using Newtonsoft.Json;

namespace ImageServiceGUI.Model
{
    class SettingModel : ISettingModel
    {
        private string m_tumbnailSize;
        private string m_outputDirectory;
        private string m_sourceName;
        private string m_logName;
        private ObservableCollection<string> m_Handlers;
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingModel"/> class.
        /// </summary>
        public SettingModel()
        {
            client = GuiClient.Instance;
            //client.Recieve();
            client.ExecuteReceived += ExecuteReceived;
            InitData();
        }
        #region Notify Changed
        
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public GuiClient client { get; set; }
        /// <summary>
        /// Initializes the data.
        /// </summary>
        private void InitData()
        {
            try
            {
                OutputDirectory = string.Empty;
                SourceName = string.Empty;
                LogName = string.Empty;
                TumbnailSize = string.Empty;
                m_Handlers = new ObservableCollection<string>();
                Object thisLock = new Object();
                BindingOperations.EnableCollectionSynchronization(Handlers, thisLock);
                string[] Args = new string[5];
                CommandRecievedEventArgs request = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, Args, "");
                client.Send(request);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Executes the received.
        /// </summary>
        /// <param name="arrivedMessage">The <see cref="CommandRecievedEventArgs"/> instance containing the event data.</param>
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
                        case (int)CommandEnum.CloseHandlerCommand:
                            CloseHandler(arrivedMessage);
                            break;
                        default:
                            //client.Close();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Updates the specified arrived message.
        /// </summary>
        /// <param name="arrivedMessage">The <see cref="CommandRecievedEventArgs"/> instance containing the event data.</param>
        private void Update(CommandRecievedEventArgs arrivedMessage)
        {
            try
            {
                TcpMessages tcpMessages = JsonConvert.DeserializeObject < TcpMessages > (arrivedMessage.Args[0]);
                OutputDirectory = tcpMessages.Args[0];
                SourceName = tcpMessages.Args[1];
                LogName = tcpMessages.Args[2];
                TumbnailSize = tcpMessages.Args[3];
                for (int i = 4; i < tcpMessages.Args.Length; i++)
                {
                    Handlers.Add(tcpMessages.Args[i]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("exception in update-settingmodel" + e.Message);

            }
        }
        /// <summary>
        /// Closes the handler.
        /// </summary>
        /// <param name="arrivedMessage">The <see cref="CommandRecievedEventArgs"/> instance containing the event data.</param>
        private void CloseHandler(CommandRecievedEventArgs arrivedMessage)
        {
            if (Handlers != null && Handlers.Count > 0 && arrivedMessage.Args != null
                                 && Handlers.Contains(arrivedMessage.Args[0]))
            {
                Handlers.Remove(arrivedMessage.Args[0]);
            }
        }
        /// <summary>
        /// Gets or sets the output directory.
        /// </summary>
        /// <value>
        /// The output directory.
        /// </value>
        public string OutputDirectory
        {
            get { return m_outputDirectory; }
            set
            {
                m_outputDirectory = value;
                OnPropertyChanged("OutputDirectory");
            }
        }
        /// <summary>
        /// Gets or sets the name of the source.
        /// </summary>
        /// <value>
        /// The name of the source.
        /// </value>
        public string SourceName
        {
            get { return m_sourceName; }
            set
            {
                m_sourceName = value;
                OnPropertyChanged("SourceName");
            }
        }
        /// <summary>
        /// Gets or sets the name of the log.
        /// </summary>
        /// <value>
        /// The name of the log.
        /// </value>
        public string LogName
        {
            get { return m_logName; }
            set
            {
                m_logName = value;
                OnPropertyChanged("LogName");
            }
        }
        /// <summary>
        /// Gets or sets the size of the tumbnail.
        /// </summary>
        /// <value>
        /// The size of the tumbnail.
        /// </value>
        public string TumbnailSize
        {
            get { return m_tumbnailSize; }
            set
            {
                m_tumbnailSize = value;
                OnPropertyChanged("TumbnailSize");
            }
        }

        /// <summary>
        /// Gets the handlers.
        /// </summary>
        /// <value>
        /// The handlers.
        /// </value>
        public ObservableCollection<string> Handlers {
            get
            {
                return m_Handlers;
            }
        }
        /// <summary>
        /// Closes the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void CloseHandler(string handler)
        {
            Console.WriteLine(handler);
            string[] Args= {handler };
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseHandlerCommand, Args, handler);
            client.Send(commandRecievedEventArgs);
        }

    }
}

    

