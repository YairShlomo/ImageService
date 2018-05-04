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
