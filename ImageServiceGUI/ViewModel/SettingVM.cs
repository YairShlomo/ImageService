using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ImageServiceGUI.Model;
using System.Collections.ObjectModel;

namespace ImageServiceGUI.ViewModel
{
    class SettingVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ISettingModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SettingVM(ISettingModel model)
        {            
            this.model = model;
            this.model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e) {
                    this.PropertyChanged?.Invoke(this, e);
                };
        }
        /// <summary>
        /// Gets the handlers.
        /// </summary>
        /// <value>
        /// The handlers.
        /// </value>
        public ObservableCollection<string> Handlers
        {
            get { return model.Handlers; }
        }
        /// <summary>
        /// Gets the name of the source.
        /// </summary>
        /// <value>
        /// The name of the source.
        /// </value>
        public string SourceName
        {
            get { return model.SourceName; }
        }
        /// <summary>
        /// Gets the name of the log.
        /// </summary>
        /// <value>
        /// The name of the log.
        /// </value>
        public string LogName
        {
            get { return model.LogName; }
        }
        /// <summary>
        /// Gets the output directory.
        /// </summary>
        /// <value>
        /// The output directory.
        /// </value>
        public string OutputDirectory
        {
            get { return model.OutputDirectory; }
        }
        /// <summary>
        /// Gets the size of the tumbnail.
        /// </summary>
        /// <value>
        /// The size of the tumbnail.
        /// </value>
        public string TumbnailSize
        {
            get { return model.TumbnailSize; }
        }
        /// <summary>
        /// Closes the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void CloseHandler(string handler)
        {
            model.CloseHandler(handler);
        }


    }
}
