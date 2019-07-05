
using RabbitMQ.Client;

namespace VisionCommon.RabbitMq
{
    public interface IRabbitMqClient
    {
        IConnection Connection { get; }
        IModel Model { get; }

        void Connect(MqHost q);

        void AckMessage(ulong deliveryTag);

    }
}
