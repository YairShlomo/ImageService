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
        private ISettingModel model;
        private ObservableCollection<string> handlers;
        public SettingVM(ISettingModel model)
        {
            this.handlers = new ObservableCollection<string>();
            this.model = model;
            this.model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e) {
                    this.PropertyChanged?.Invoke(this, e);
                };
        }
        public ObservableCollection<string> getHandlers
        {
            get { return handlers; }
        }
        public string getSourceName
        {
            get { return model.SourceName; }
        }
        public string getLogName
        {
            get { return model.LogName; }
        }
        public string getOutputDirectory
        {
            get { return model.OutputDirectory; }
        }
        public string getTumbnailSize
        {
            get { return model.TumbnailSize; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
