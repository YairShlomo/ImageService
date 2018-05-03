using System;

using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace ImageService
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
        public void Send()
        {
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Send data to server
                Console.Write("Please enter a number: ");
                int num = int.Parse(Console.ReadLine());
                writer.Write(num);
                // Get result from server
                int result = reader.ReadInt32();
                Console.WriteLine("Result = {0}", result);
            }
        }
        public void Close()
        {
            client.Close();
        }
    }
}