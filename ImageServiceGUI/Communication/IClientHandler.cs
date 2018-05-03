using System;
using System.Net.Sockets;

namespace ImageServiceGUI.Communication

{
    public interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}
