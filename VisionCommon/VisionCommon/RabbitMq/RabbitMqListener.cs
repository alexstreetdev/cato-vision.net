using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace VisionCommon.RabbitMq
{
    public class RabbitMqListener
    {
        private readonly RabbitMqClient _mqClient;
        private bool _isListening;
        public delegate void MessageRecd(MqMessage<string> msg);
        public event MessageRecd MessageReceived;

        public RabbitMqListener(RabbitMqClient client)
        {
            _mqClient = client;
            _isListening = false;
        }


        public string StartListening(string sourceQueue)
        {
            if (_isListening) throw new Exception("Already listening");
            var consumer = new EventingBasicConsumer(_mqClient.Model);
            consumer.Received += ConsumerOnReceived;
            string consumerTag = _mqClient.Model.BasicConsume(sourceQueue, false, consumer);
            _isListening = true;
            return consumerTag;
        }

        private void ConsumerOnReceived(object sender, BasicDeliverEventArgs bdea)
        {
            if (MessageReceived == null) return;

            var msg = MessageMap.Map(bdea);
            MessageReceived(msg);
        }

    }
}
