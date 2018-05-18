using System;
using System.Net.Sockets;
using System.Collections.Generic;
namespace ImageService.Communication

{
    public interface IClientHandler
    {
        void HandleClient(TcpClient client, List<TcpClient> clients);
    }
}
