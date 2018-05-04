using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure.Enums;
using Newtonsoft.Json;

namespace ImageService
{
    class TcpMessages
    {
        public CommandEnum CommandID { get; set; }
        public string[] args { get; set; }
    }
}
