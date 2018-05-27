using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    class MainWindowModel
    {
        private GuiClient client;
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowModel"/> class.
        /// </summary>
        public MainWindowModel()
        {
            client = GuiClient.Instance;
        }
        /// <summary>
        /// Gets a value indicating whether this instance is conneted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is conneted; otherwise, <c>false</c>.
        /// </value>
        public bool isConneted
        {
            get
            {
                return client.Connected;
            }
        }
    }
}
