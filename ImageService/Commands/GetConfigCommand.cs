using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using System.Net.Sockets;
using ImageService;
namespace ImageService.Commands

{
    class GetConfigCommand : ICommand
    {
        public string Execute(string[] args,out bool result, TcpClient client = null)
        {
            try
            {
                result = true;
                TcpMessages tcpMessages = new TcpMessages();
                string[] dirPaths = ConfigurationManager.AppSettings["Handler"].Split(';');
                int sizeDirPath = dirPaths.Length;
                tcpMessages.args = new string[4+sizeDirPath];
                tcpMessages.args[0] = ConfigurationManager.AppSettings.Get("OutputDir");
                tcpMessages.args[1] = ConfigurationManager.AppSettings.Get("SourceName");
                tcpMessages.args[2] = ConfigurationManager.AppSettings.Get("LogName");
                tcpMessages.args[3] = ConfigurationManager.AppSettings.Get("ThumbnailSize");
                for (int i=0; i< sizeDirPath;i++)
                {
                    tcpMessages.args[4 + i] = dirPaths[i];
                }
                return JsonConvert.SerializeObject(tcpMessages);
            }
            catch (Exception ex)
            {
                result = false;
                return ex.ToString();
            }
        }
    }
}

