using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
{
    class MainWindowVM
    {
        private MainWindowModel model;
        public MainWindowVM()
        {
            this.model = new MainWindowModel();
        }
        public bool isConnected
        {
            get { return model.isConneted; }
        }
    }
}
