using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using ImageService.Modal;
using ImageService.Controller;
namespace ImageServiceGUI.Communication
{
    class ClientHandler : IClientHandler
    {
        IImageController ImageController { get; set; }
        public ClientHandler(IImageController imageController)//, ImageServer imageServer)
        {
            ImageController = imageController;

        }
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    bool res;
                    string commandLine = reader.ReadLine();
                    CommandRecievedEventArgs commandRecievedEventArgs = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandLine);
                    string result = ImageController.ExecuteCommand((int)commandRecievedEventArgs.CommandID,
                        commandRecievedEventArgs.Args, out res);
                    string[] args = { commandLine };
                    //Console.WriteLine("Got command: {0}", commandLine);
                    writer.Write(result);
                }
                client.Close();
            }).Start();
        }
    }
}
