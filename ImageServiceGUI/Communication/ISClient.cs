using System;
using ImageService.Modal;

using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
namespace ImageServiceGUI
{
    public class ISClient : IISClient
    {
        private TcpClient client;
        public ISClient()
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                TcpClient client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("You are connected");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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