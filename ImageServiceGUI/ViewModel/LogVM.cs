using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
{

    class LogVM : INotifyPropertyChanged
    {
        private ILogModel model;
        public LogVM(ILogModel model)
        {
            this.model = model;
            this.model.PropertyChanged += 
                delegate(Object sender, PropertyChangedEventArgs e) {
                    this.PropertyChanged?.Invoke(this, e);
                };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        
        public string getLog {
            //return model.getLog();
            
          get {
                
                return " "; }
        }
    }
}
