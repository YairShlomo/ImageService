using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace ImageServiceGUI.Communication
{
    class ClientHandler : IClientHandler
    {
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string commandLine = reader.ReadLine();
                    string[] args = { commandLine };
                    Console.WriteLine("Got command: {0}", commandLine);
                    //ViewRequestController vrc = new ViewRequestController();
                    //string result = vrc.ExecuteCommand(1,args, client);
                    writer.Write("D");
                }
                client.Close();
            }).Start();
        }
    }
}
