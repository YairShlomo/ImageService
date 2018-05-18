using System;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Collections.Generic;

using System.IO;
using Newtonsoft.Json;
using ImageService.Modal;
namespace ImageService.Communication
{
    class ISServer
    {
        ILoggingService Logging { get; set; }

        private int port;
        private TcpListener listener;
        private IClientHandler ch;
        int Port { get; set; }
        TcpListener Listener { get; set; }
        IClientHandler Ch { get; set; }
        private List<TcpClient> clients = new List<TcpClient>();
        public ISServer(int port, ILoggingService logging, IClientHandler ch)
        {
            this.port = port;
            this.Logging = logging;
            this.ch = ch;
        }
        public void Start()
        {
            try
            {


                IPEndPoint ep = new
                IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                listener = new TcpListener(ep);

                listener.Start();
                //Console.WriteLine("Waiting for connections...");
                Logging.Log("Waiting for client connections...", MessageTypeEnum.INFO);

                Task task = new Task(() =>
                {
                    while (true)
                    {
                        try
                        {
                            TcpClient client = listener.AcceptTcpClient();
                            Console.WriteLine("Got new connection");
                            clients.Add(client);
                            ch.HandleClient(client, clients);
                        }
                        catch (SocketException)
                        {
                            break;
                        }
                    }
                    Logging.Log("Server stopped", MessageTypeEnum.INFO);
                });
                task.Start();


            }
            catch (Exception ex)
            {
                Logging.Log(ex.ToString(), MessageTypeEnum.FAIL);
            }
        }


        public void NotifyClients(CommandRecievedEventArgs commandRecievedEventArgs)
        {
            try
            {
                foreach (TcpClient client in clients)
                {
                    new Task(() =>
                    {
                        using (NetworkStream stream = client.GetStream())
                        using (BinaryReader reader = new BinaryReader(stream))
                        using (BinaryWriter writer = new BinaryWriter(stream))
                        {
                            string jsonCommand = JsonConvert.SerializeObject(commandRecievedEventArgs);
                            writer.Write(jsonCommand);
                        }
                        client.Close();
                    }).Start();
                }
            }
            catch (Exception ex)
            {
                Logging.Log(ex.ToString(), MessageTypeEnum.FAIL);

            }
        }


        public void Stop()
        {
            listener.Stop();
        }
    }
}