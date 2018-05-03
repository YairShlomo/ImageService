using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace ImageService.Commands
{
    class LogCommand : ICommand
    {
        public string Execute(string[] args,out bool result, TcpClient client = null)  
        {
            result = true;
            return "";
        }
    }
}
