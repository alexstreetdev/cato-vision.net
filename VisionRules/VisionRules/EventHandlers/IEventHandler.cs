using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace VisionRules.EventHandlers
{
    public interface IEventHandler
    {
        string Handle(IModel channel, BasicDeliverEventArgs bdea);

    }
}
