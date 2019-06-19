using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VisionRules.Commands;
using VisionRules.Events;
using VisionRules.MessageClient;

namespace VisionRules.EventHandlers
{
    public class MovementDetectedHandler : IEventHandler
    {
        public MovementDetectedHandler()
        {
            
        }

        public string Handle(IModel channel, BasicDeliverEventArgs bdea)
        {
            Console.WriteLine("Movement");

            var bodystring = Encoding.UTF8.GetString(bdea.Body);
            var model = JsonConvert.DeserializeObject<MovementDetectedEvent>(bodystring);

            // send updates
            var detectface = new DetectFaceCmd()
            {
                ImageId = model.ImageId,
                ImageUrl = model.ImageUrl,
                X = model.X,
                Y = model.Y,
                Width = model.Width,
                Height = model.Height
            };
            IBasicProperties props = channel.CreateBasicProperties();
            channel.BasicPublish("msg_gateway", "vision.cmd.detectface", true, props, RabbitMqMessageHelper.GetModelAsMessagePayload(detectface));

            var detectbody = new DetectBodyCmd()
            {
                ImageId = model.ImageId,
                ImageUrl = model.ImageUrl,
                X = model.X,
                Y = model.Y,
                Width = model.Width,
                Height = model.Height
            };
            channel.BasicPublish("msg_gateway", "vision.cmd.detectbody", true, props, RabbitMqMessageHelper.GetModelAsMessagePayload(detectbody));

            channel.BasicAck(bdea.DeliveryTag, false);

            return "result";
        }
    }
}
