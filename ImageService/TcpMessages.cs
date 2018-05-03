using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure.Enums;

namespace ImageService
{
    class TcpMessages
    {
        public CommandEnum CommandID { get; set; }
        public string[] args { get; set; }
    }
}
