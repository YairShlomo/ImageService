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
        /// <summary>
        /// Gets or sets the output directory.
        /// </summary>
        /// <value>
        /// The output directory.
        /// </value>
        string OutputDirectory { get; set; }
        /// <summary>
        /// Gets or sets the name of the source.
        /// </summary>
        /// <value>
        /// The name of the source.
        /// </value>
        string SourceName { get; set; }
        /// <summary>
        /// Gets or sets the name of the log.
        /// </summary>
        /// <value>
        /// The name of the log.
        /// </value>
        string LogName { get; set; }
        /// <summary>
        /// Gets or sets the size of the tumbnail.
        /// </summary>
        /// <value>
        /// The size of the tumbnail.
        /// </value>
        string TumbnailSize { get; set; }
        /// <summary>
        /// Gets the handlers.
        /// </summary>
        /// <value>
        /// The handlers.
        /// </value>
        ObservableCollection<string> Handlers { get; }
        /// <summary>
        /// Closes the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        void CloseHandler(string handler);

    }
}
