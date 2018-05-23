using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using ImageService.Controller;
using Newtonsoft.Json;
using ImageService.Server;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;
using System.Configuration;
using ImageService.Logging;
using System.Threading;
using ImageServer.Infrastructure.Modal.Event;
using ImageServer.Infrastructure.Modal;

namespace ImageService.Communication
{
    class ClientHandler : IClientHandler
    {
        ImageController imageController { get; set; }
        ILoggingService Logging { get; set; }
        private Debug_program debug;
        BinaryReader reader;
        BinaryWriter writer;
        /// <summary>
        /// ClientHandler constructor.
        /// </summary>
        /// <param name="imageController">IImageController obj</param>
        /// <param name="logging">ILoggingService obj</param>
        public ClientHandler(ImageController m_imageController, ILoggingService logging)//, ImageServer imageServer)
        {
            this.imageController = m_imageController;
            this.Logging = logging;
            this.Logging.MessageRecieved += send;
            debug = new Debug_program();
        }
        private bool isStopped = false;
        public static Mutex Mutex { get; set; }
        /// <summary>
        /// HandleClient function.
        /// handles the client-server communication.
        /// </summary>
        /// <param name="client">specified client</param>
        /// <param name="clients">list of all current clients</param>
        public void HandleClient(TcpClient client, List<TcpClient> clients)
        {
            try
            {
                new Task(() =>
                {
                    try
                    {
                        while (!isStopped)
                        {
                            NetworkStream stream = client.GetStream();
                            reader = new BinaryReader(stream);
                            writer = new BinaryWriter(stream);
                            string commandLine = reader.ReadString();
                            debug.write("after reading");
                            Logging.Log("ClientHandler got command: " + commandLine, MessageTypeEnum.INFO);
                            CommandRecievedEventArgs commandRecievedEventArgs = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandLine);
                            if (commandRecievedEventArgs.CommandID == (int)CommandEnum.CloseClient)
                            {
                                clients.Remove(client);
                                client.Close();
                                break;
                            }
                            Console.WriteLine("Got command: {0}", commandLine);
                            bool r;
                           // imageServer.GuiCommands(commandRecievedEventArgs);
                             string result = imageController.ExecuteCommand((int)commandRecievedEventArgs.CommandID,
                             commandRecievedEventArgs.Args, out r);
                            debug.write("ExecutedCommand"+ (int)commandRecievedEventArgs.CommandID);
                            // string result = handleCommand(commandRecievedEventArgs);
                            Mutex.WaitOne();
                            writer.Write(result);
                            debug.write("send " + result);
                            Mutex.ReleaseMutex();
                        }
                    }
                    catch (Exception ex)
                    {
                        clients.Remove(client);
                        Logging.Log(ex.ToString(), MessageTypeEnum.FAIL);
                        client.Close();
                    }
                }).Start();
            }
            catch (Exception ex)
            {
                Logging.Log(ex.ToString(), MessageTypeEnum.FAIL);

            }
        }
        public void send(object o, MessageRecievedEventArgs dirArgs)
        {
            MessageTypeEnum s = dirArgs.Status;
            string[] Args= { Convert.ToString((int)dirArgs.Status), dirArgs.Message };
            CommandRecievedEventArgs cre = new CommandRecievedEventArgs((int)CommandEnum.AddLog, Args, null);
            string jsonCommand = JsonConvert.SerializeObject(dirArgs);
            writer.Write(jsonCommand);
        }
    }
}
