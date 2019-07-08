
using RabbitMQ.Client;

namespace VisionCommon.RabbitMq
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private IConnection _mQconn;
        private IModel _mQchannel;
        private readonly MqQos _mqQos;

        public IConnection Connection => _mQconn;
        public IModel Model => _mQchannel;

        public string Test { get; set; }

        public RabbitMqClient() : this(new MqQos())
        {

        }

        public RabbitMqClient(MqQos qos)
        {
            _mqQos = qos;
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
            _mQchannel.BasicQos(_mqQos.PrefetchSize, _mqQos.PrefetchCount, _mqQos.Global);
        }

        public void AckMessage(ulong deliveryTag)
        {
            _mQchannel.BasicAck(deliveryTag, false);
        }
    }
}
