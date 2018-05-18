/*
using System;
using ImageService.Modal;

namespace ImageServiceGUI

{
    public interface IISClient
    {
        CommandRecievedEventArgs Send(CommandRecievedEventArgs commandRecievedEventArgs);
        void Close();
    }
    
}
*/
using System;

using ImageService.Modal;

namespace ImageServiceGUI

{
    public delegate void ExecuteReceivedMessage(CommandRecievedEventArgs arrivedMessage);

    public interface IGuiClient
    {
        event ExecuteReceivedMessage ExecuteReceived;
        void Send(CommandRecievedEventArgs commandRecievedEventArgs);
        void Recieve();

        void Close();
        bool Connected { get; set; }
    }

}
