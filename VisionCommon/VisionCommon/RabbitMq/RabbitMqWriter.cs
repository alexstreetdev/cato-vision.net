using System;
using System.Collections.Generic;
using System.Text;

namespace VisionCommon.RabbitMq
{
    public class RabbitMqWriter
    {
        private IRabbitMqClient _client;

        public RabbitMqWriter(IRabbitMqClient client)
        {
            _client = client;
        }



    }
}
