
using RabbitMQ.Client;

namespace VisionCommon.RabbitMq
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private IConnection _mQconn;
        private IModel _mQchannel;

        public IConnection Connection => _mQconn;
        public IModel Model => _mQchannel;

        public string Test { get; set; }

        public RabbitMqClient()
        {

        }

        public void Connect(MqHost q)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = q.Username,
                Password = q.Password,
                HostName = q.HostName
            };

            _mQconn = factory.CreateConnection();
            _mQchannel = _mQconn.CreateModel();

        }

        public void AckMessage(ulong deliveryTag)
        {
            _mQchannel.BasicAck(deliveryTag, false);
        }
    }
}
