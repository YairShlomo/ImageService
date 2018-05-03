using System;
namespace ImageServiceGUI.Communication

{
    public interface Icommand
    {
        string Execute(string[] args, TcpClient client = null);
    }
}