using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ImageServiceGUI.Model;

namespace ImageServiceGUI.ViewModel
{
    class SettingVM : INotifyPropertyChanged
    {
        private ISettingModel model;
        public SettingVM(ISettingModel model)
        {
            this.model = model;
            this.model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e) {
                    this.PropertyChanged?.Invoke(this, e);
                };
        }
        public LinkedList<string> getHandlers {
            // return model.gethandlers();
            get { return new LinkedList<string>(); }
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
