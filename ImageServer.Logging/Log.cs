﻿using System;
using ImageService.Infrastructure.Modal;

namespace ImageService.Logging
{
    public class Log
    {
        private MessageTypeEnum type;
        public string Type
        {
            get { return Enum.GetName(typeof(MessageTypeEnum), type); }
            set { type = (MessageTypeEnum)Enum.Parse(typeof(MessageTypeEnum), value); }
        }
        public string Message { get; set; }

    }
}