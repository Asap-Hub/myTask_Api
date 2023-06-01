using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Infrastructure.Utility
{
    public class SMTPSettings
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; } 
        public string user { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
 