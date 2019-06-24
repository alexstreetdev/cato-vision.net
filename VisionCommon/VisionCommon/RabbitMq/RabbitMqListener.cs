using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace VisionCommon.RabbitMq
{
    /// <summary>
    /// Use when all messages on the queue are of the same type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RabbitMqListener<T> : RabbitMqClient
    {
        private bool isListening;
        public delegate void ExampleEventRecd(T ev, RabbitMqClient m);
        public event ExampleEventRecd CommandReceived;

        private Func<BasicDeliverEventArgs, T> _mapper;


        public RabbitMqListener()
        {
            isListening = false;
        }


        public string StartListening(string sourceQueue, Func<BasicDeliverEventArgs, T> mapper)
        {
            if (isListening) throw new Exception("Already listening");
            var consumer = new EventingBasicConsumer(_mQchannel);
            consumer.Received += ConsumerOnReceived;
            string consumerTag = _mQchannel.BasicConsume(sourceQueue, false, consumer);
            _mapper = mapper;
            return consumerTag;
        }

        private void ConsumerOnReceived(object sender, BasicDeliverEventArgs bdea)
        {
            if (CommandReceived == null) return;

            var x = _mapper(bdea);
            if (x == null) return;

            CommandReceived(x, this);

        }

    }
}
