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
        public MainWindowModel()
        {
            client = GuiClient.Instance;
        }
        public bool isConneted
        {
            get
            {
                return client.Connected;
            }
        }
    }
}
