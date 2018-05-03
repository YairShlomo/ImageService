using System;

using System.Net.Sockets;

namespace ImageServiceGUI.Commands
{
    public interface ICommand
    {
        string Execute(string[] args, TcpClient client = null);
    }
}