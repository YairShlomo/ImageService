﻿/*
using System;
using ImageService.Modal;

using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
namespace ImageServiceGUI.Communication
{
    public class GuiClient 
    {
        private static GuiClient instance;
        private TcpClient client;
        private GuiClient()
        {
            try
            {   
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("You are connected");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static GuiClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GuiClient();
                }
                return instance;
            }
        }
        public CommandRecievedEventArgs Send(CommandRecievedEventArgs commandRecievedEventArgs)
        {
            string jsonCommand = JsonConvert.SerializeObject(commandRecievedEventArgs);

            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Send data to server
                Console.Write(jsonCommand);
                int num = int.Parse(Console.ReadLine());
                writer.Write(num);
                // Get result from server
                string strResult = reader.ReadString();
                Console.WriteLine("Result = {0}", strResult);
                return JsonConvert.DeserializeObject<CommandRecievedEventArgs>(strResult);
            }

        }
        public void Close()
        {
            client.Close();
        }
    }
}


/*
public class Singleton
{
    private static Singleton instance;

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }
    
}
*/
using System;
using ImageService.Infrastructure.Modal;
using ImageService.Infrastructure.Modal.Event;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Threading;

using ImageService.Infrastructure.Enums;
namespace ImageServiceGUI
{
    public class GuiClient
    {

        private static GuiClient instance;
        private TcpClient client;
        public bool Connected { get; set; }
        private bool isStopped;
        private static Mutex mutex = new Mutex();
        public event ExecuteReceivedMessage ExecuteReceived;


        private GuiClient()
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                client = new TcpClient();
                client.Connect(ep);
                Connected = true;
                Console.WriteLine("You are connected");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Connected = false;
            }
        }
        public static GuiClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GuiClient();
                }
                return instance;
            }
        }
        public void Send(CommandRecievedEventArgs commandRecievedEventArgs)
        {
            new Task(() =>
            {
                try
                {
                    string jsonCommand = JsonConvert.SerializeObject(commandRecievedEventArgs);
                    NetworkStream stream = client.GetStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    // Send data to server
                    Console.WriteLine($"Send {jsonCommand} to Server");
                    mutex.WaitOne();
                    writer.Write(jsonCommand);
                    mutex.ReleaseMutex();
                }
                catch (Exception ex)
                {

                }
            }).Start();
        }

        public void Recieve()
        {
            new Task(() =>
            {
                try
                {
                    while (!isStopped)
                    {
                        NetworkStream stream = client.GetStream();
                        BinaryReader reader = new BinaryReader(stream);
                        string jsonArrivedMessage = reader.ReadString();
                        Console.WriteLine($"Recieve {jsonArrivedMessage} from Server");
                        CommandRecievedEventArgs arrivedMessage = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(jsonArrivedMessage);
                        ExecuteReceived?.Invoke(arrivedMessage);
                    }
                }
                catch (Exception ex)
                {

                }
            }).Start();
        }
        public void Close()
        {
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseClient, null, "");
            Send(commandRecievedEventArgs);
            client.Close();
            isStopped = true;
        }
    }
}


