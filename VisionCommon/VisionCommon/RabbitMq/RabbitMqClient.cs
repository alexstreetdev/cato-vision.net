

using RabbitMQ.Client;

namespace VisionCommon.RabbitMq
{
    public class RabbitMqClient
    {
        protected IConnection _mQconn;
        protected IModel _mQchannel;


        protected RabbitMqClient()
        {

        }

        public void Connect(MqHost q)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = q.UserName,
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
