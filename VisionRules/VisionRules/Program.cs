using System;
using Microsoft.Extensions.Configuration;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using VisionCommon.RabbitMq;
using VisionRules.EventHandlers;

namespace VisionRules
{
    class Program
    {
        static IUnityContainer _container;
        private static IRabbitMqClient _client;
        private static RabbitMqListener<MqMessage<string>> _listener;
        static void Main(string[] args)
        {
            Console.WriteLine("Starting VisionRules");

            IConfiguration cfg = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddCommandLine(args)
                .Build();

            _container = ConfigureContainer(cfg);

            var host = cfg.GetSection("RabbitMQ:MqHost").Get<MqHost>();
            _client.Connect(host);
            Console.WriteLine("Connected");
            _listener.MessageReceived += Recd;
            var listenTo = cfg.GetValue<string>("RabbitMQ:SourceQueue");
            _listener.StartListening(listenTo, MessageMap.Map);
            Console.WriteLine("Listening...");

            
            Console.WriteLine("See Ya!");
            Console.ReadLine();
        }


        private static IUnityContainer ConfigureContainer(IConfiguration cfg)
        {
            var c = new UnityContainer();

            var qos = cfg.GetSection("RabbitMq:MqQos").Get<MqQos>();
            _client = new RabbitMqClient(qos);
            c.RegisterInstance<IRabbitMqClient>(_client);
            _listener = new RabbitMqListener<MqMessage<string>>(_client);
            c.RegisterInstance<IRabbitMqListener>(_listener);

            c.RegisterType<IEventHandler, EmptyHandler>("empty");
            c.RegisterType<IEventHandler, MovementDetectedHandler>("vision.evt.detected-movement",
                                                                new PerResolveLifetimeManager(),
                                                                new InjectionConstructor(c.Resolve<IRabbitMqClient>()));
            c.RegisterType<IEventHandler, FaceDetectedHandler>("vision.evt.detected-face", 
                                                                new PerResolveLifetimeManager(), 
                                                                new InjectionConstructor(c.Resolve<IRabbitMqClient>()));

            return c;
        }

        private static void Recd(MqMessage<string> msg)
        {
            var hdler = GetHandler(msg.RoutingKey);
            hdler?.Handle(msg);
            
        }


        private static IEventHandler GetHandler(string key)
        {
            IEventHandler handler;
            try
            {
                handler = _container.Resolve<IEventHandler>(key);
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
    }
}
