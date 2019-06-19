using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VisionRules.Commands;
using VisionRules.Events;
using VisionRules.MessageClient;

namespace VisionRules.EventHandlers
{
    public class FaceDetectedHandler : IEventHandler
    {
        public string Handle(IModel channel, BasicDeliverEventArgs bdea)
        {
            Console.WriteLine("FACE !!!");

            var model = RabbitMqMessageHelper.GetMessagePayloadModel<FaceDetectedEvent>(bdea.Body);

            var cmd = new IdentifyFaceCmd()
            {
                ImageId = model.ImageId,
                ImageUrl = model.ImageUrl,
                X = model.X,
                Y = model.Y,
                Width = model.Width,
                Height = model.Height
            };

            IBasicProperties props = channel.CreateBasicProperties();
            channel.BasicPublish("msg_gateway", "vision.cmd.detectface", true, props, RabbitMqMessageHelper.GetModelAsMessagePayload(cmd));

            channel.BasicAck(bdea.DeliveryTag, false);

            return "ok";
        }
    }
}
