using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Text;

namespace VisionCommon.RabbitMq
{
    public static class MessageMap
    {

        public static MqMessage<T> Map<T>(BasicDeliverEventArgs bdea)
        {
            var m = new MqMessage<T>();
            m.RoutingKey = bdea.RoutingKey;
            m.DeliveryTag = bdea.DeliveryTag;
            var bodystring = Encoding.UTF8.GetString(bdea.Body);
            m.Payload = JsonConvert.DeserializeObject<T>(bodystring);
            return m;
        }

        public static MqMessage<string> Map(BasicDeliverEventArgs bdea)
        {
            var m = new MqMessage<string>();
            m.RoutingKey = bdea.RoutingKey;
            m.DeliveryTag = bdea.DeliveryTag;
            m.Payload = Encoding.UTF8.GetString(bdea.Body);
            return m;
        }

    }
}
