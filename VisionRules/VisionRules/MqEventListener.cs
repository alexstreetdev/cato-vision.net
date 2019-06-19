using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Unity;
using VisionRules.EventHandlers;
using VisionRules.MessageClient;

namespace VisionRules
{
    public class MqEventListener
    {
        private readonly IUnityContainer _container;
        private IConnection _mQconn;
        private IModel _mQchannel;
        private readonly RabbitMqConfiguration _mqConfig;

        public MqEventListener(IUnityContainer container, RabbitMqConfiguration cfg)
        {
            _container = container;
            _mqConfig = cfg;
        }

        public void Run()
        {
            var cfg = new RabbitMqConfiguration();
            //var c = new RabbitMqClient(cfg);

            Connect();

            Listen();


            //Disconnect();
        }

        private void ConsumerOnReceived(object sender, BasicDeliverEventArgs bdea)
        {

            IEventHandler handler = ResolveHandler(bdea.RoutingKey);
            handler.Handle(_mQchannel, bdea);

        }

        private IEventHandler ResolveHandler(string routingKey)
        {
            IEventHandler handler;
            try
            {
                handler = _container.Resolve<IEventHandler>(routingKey);
                return handler;
            }
            catch (ResolutionFailedException)
            {
                try
                {
                    handler = _container.Resolve<IEventHandler>("empty");
                    return handler;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void Connect()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = _mqConfig.UserName,
                Password = _mqConfig.Password,
                HostName = _mqConfig.HostName
            };

            _mQconn = factory.CreateConnection();
            _mQchannel = _mQconn.CreateModel();
        }

        private void Listen()
        {
            var consumer = new EventingBasicConsumer(_mQchannel);
            consumer.Received += ConsumerOnReceived;
            String consumerTag = _mQchannel.BasicConsume(_mqConfig.SourceQueue, false, consumer);
        }

        //private void Disconnect()
        //{
        //    _mQchannel.Close();
        //    _mQconn.Close();
        //}


    }
}
