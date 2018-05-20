using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ImageServiceGUI.Model
{
    interface ISettingModel : INotifyPropertyChanged
    {
        string OutputDirectory { get; set; }
        string SourceName { get; set; }
        string LogName { get; set; }
        string TumbnailSize { get; set; }
        ObservableCollection<string> Handlers { get; }
        void CloseMessage(string handler);

    }
}
