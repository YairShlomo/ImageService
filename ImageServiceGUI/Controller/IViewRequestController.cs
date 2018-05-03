using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace ImageServiceGUI.Controller
{
    public interface IViewRequestController
    {
        string ExecuteCommand(string[] args, TcpClient client = null);         
    }
}
