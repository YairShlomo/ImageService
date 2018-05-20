using ImageService.Logging;
using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
{
    public sealed class MyListBoxItem
    {
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
    }
    class LogVM : INotifyPropertyChanged
    {
        public ObservableCollection<MyListBoxItem> Items { get; private set; }
        private ILogModel model;
        public LogVM(ILogModel model)
        {
            Items = new ObservableCollection<MyListBoxItem>();
            Items.Add(new MyListBoxItem { Field1 = "WARNING", Field2 = "Two", Field3 = "Three" });
            Items.Add(new MyListBoxItem { Field1 = "INFO", Field2 = "fdshfdh", Field3 = "dfshfdhfhfghs" });
            Items.Add(new MyListBoxItem { Field1 = "ERROR", Field2 = "fdshfdh", Field3 = "dfshfdhfhfghs" });
            this.model = model;
            this.model.PropertyChanged += 
                delegate(Object sender, PropertyChangedEventArgs e) {
                    this.PropertyChanged?.Invoke(this, e);
                };
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Log> logs
        {
            get { return model.logs; }
        }
    }
}
