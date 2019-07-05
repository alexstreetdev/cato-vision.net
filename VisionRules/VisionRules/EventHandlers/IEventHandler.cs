
using VisionCommon.RabbitMq;

namespace VisionRules.EventHandlers
{
    public interface IEventHandler
    {
        EventHandlerResult Handle(MqMessage<string> msg);

    }
}
