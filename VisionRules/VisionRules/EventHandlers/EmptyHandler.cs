using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VisionCommon.RabbitMq;

namespace VisionRules.EventHandlers
{
    /// <summary>
    /// If no handler found use this to just ack the message
    /// </summary>
    public class EmptyHandler : IEventHandler
    {
        public EventHandlerResult Handle(MqMessage<string> msg)
        {
            //channel.BasicAck(bdea.DeliveryTag, false);
            return new EventHandlerResult(){Success = true, MessageAcked = false};
        }
    }
}
