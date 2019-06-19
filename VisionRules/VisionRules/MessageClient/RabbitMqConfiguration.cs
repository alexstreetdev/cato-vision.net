using System;
using System.Collections.Generic;
using System.Text;

namespace VisionRules.MessageClient
{
    public class RabbitMqConfiguration
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public string SourceQueue { get; set; }
        public string TargetExchange { get; set; }
    }
}
