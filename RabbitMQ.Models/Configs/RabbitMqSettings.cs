using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Models.Configs
{
    public class RabbitMqSettings
    {
        public string Uri { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
