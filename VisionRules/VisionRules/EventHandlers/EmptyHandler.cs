using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace VisionRules.EventHandlers
{
    /// <summary>
    /// If no handler found use this to just ack the message
    /// </summary>
    public class EmptyHandler : IEventHandler
    {
        public string Handle(IModel channel, BasicDeliverEventArgs bdea)
        {
            channel.BasicAck(bdea.DeliveryTag, false);
            return "";
        }
    }
}
