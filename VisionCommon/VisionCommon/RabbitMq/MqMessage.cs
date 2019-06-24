
namespace VisionCommon.RabbitMq
{
    public class MqMessage<T>
    {
        public string RoutingKey { get; set; }
        public ulong DeliveryTag { get; set; }
        public T Payload { get; set; }

    }
}
