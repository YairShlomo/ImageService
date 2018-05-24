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
        public SettingVM(ISettingModel model)
        {            
            this.model = model;
            this.model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e) {
                    this.PropertyChanged?.Invoke(this, e);
                };
        }
        public ObservableCollection<string> Handlers
        {
            get { return model.Handlers; }
        }
        public string SourceName
        {
            get { return model.SourceName; }
        }
        public string LogName
        {
            get { return model.LogName; }
        }
        public string OutputDirectory
        {
            get { return model.OutputDirectory; }
        }
        public string TumbnailSize
        {
            get { return model.TumbnailSize; }
        }
        public void CloseHandler(string handler)
        {
            model.CloseHandler(handler);
        }


    }
}
