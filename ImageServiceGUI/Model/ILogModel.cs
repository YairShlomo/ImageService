using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ImageService.Logging;

namespace ImageServiceGUI.Model
{
    interface ILogModel : INotifyPropertyChanged
    {
        // ObservableCollection<Tuple<MessageTypeEnum, string>> LogMessages { get; set; }
        /// <summary>
        /// Gets the logs.
        /// </summary>
        /// <value>
        /// The logs.
        /// </value>
        ObservableCollection<Log> logs { get; }
    }
}
