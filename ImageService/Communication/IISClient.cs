using System;

using ImageService.Modal;

namespace ImageServiceGUI

{
    public delegate void UpdateResponseArrived(CommandRecievedEventArgs responseObj);

    public interface IISClient
    {
        event UpdateResponseArrived UpdateResponse;
        void Send(CommandRecievedEventArgs commandRecievedEventArgs);
        void Recieve();

        void Close();
        bool Connected { get; set; }
    }
    
}
