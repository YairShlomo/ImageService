using System;

namespace ImageService

{
    public interface IISClient
    {
        void Send();
        void Close();
    }
    
}
