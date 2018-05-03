using System;

namespace ImageServiceGUI.Communication

{
    public interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}
