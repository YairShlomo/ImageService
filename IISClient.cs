using System;

namespace ImageServiceGUI.Communication

{
    public class ISClient
    {
        public interface ISClient
        {
            void Send();
            void Close();
        }
    }
}
