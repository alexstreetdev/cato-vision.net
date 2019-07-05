using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace VisionCommon.RabbitMq
{
    /// <summary>
    /// Use when all messages on the queue are of the same type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RabbitMqListener<T> : IRabbitMqListener
    {
        private readonly IRabbitMqClient _mqClient;
        private bool isListening;
        public delegate void MessageRecd(T ev);
        public event MessageRecd MessageReceived;

        private Func<BasicDeliverEventArgs, T> _mapper;


        public RabbitMqListener(IRabbitMqClient client)
        {
            _mqClient = client;
            isListening = false;
        }


        public string StartListening(string sourceQueue, Func<BasicDeliverEventArgs, T> mapper)
        {
            if (isListening) throw new Exception("Already listening");
            var consumer = new EventingBasicConsumer(_mqClient.Model);
            consumer.Received += ConsumerOnReceived;
            string consumerTag = _mqClient.Model.BasicConsume(sourceQueue, false, consumer);
            _mapper = mapper;
            isListening = true;
            return consumerTag;
        }

        public string Stop()
        {
            return "STOP";
        }

        private void ConsumerOnReceived(object sender, BasicDeliverEventArgs bdea)
        {
            if (MessageReceived == null) return;

            var x = _mapper(bdea);
            MessageReceived(x);
        }

    }

}
