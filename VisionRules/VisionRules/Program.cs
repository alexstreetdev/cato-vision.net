using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Unity;
using Unity.Injection;
using VisionRules.EventHandlers;
using VisionRules.MessageClient;

namespace VisionRules
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IConfiguration cfg = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var container = ConfigureContainer(cfg);


            var rabbitMqConfig = cfg.GetSection("RabbitMQ").Get<RabbitMqConfiguration>();

            var listener = new MqEventListener(container, rabbitMqConfig);
            listener.Run();


            Console.WriteLine("See Ya!");
            Console.ReadLine();
        }


        private static IUnityContainer ConfigureContainer(IConfiguration cfg)
        {
            var c = new UnityContainer();

            c.RegisterType<IEventHandler, EmptyHandler>("empty");
            c.RegisterType<IEventHandler, MovementDetectedHandler>("vision.evt.detected-movement");


            return c;
        }
    }
}
