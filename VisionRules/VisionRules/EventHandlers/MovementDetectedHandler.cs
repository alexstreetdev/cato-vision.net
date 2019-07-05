using System;
using Newtonsoft.Json;
using RabbitMQ.Client;
using VisionCommon.RabbitMq;
using VisionRules.Commands;
using VisionRules.Events;
using VisionRules.MessageClient;

namespace VisionRules.EventHandlers
{
    public class MovementDetectedHandler : IEventHandler
    {
        private readonly IRabbitMqClient _client;
        public MovementDetectedHandler(IRabbitMqClient client)
        {
            _client = client;
        }
        
        public EventHandlerResult Handle(MqMessage<string> msg)
        {
            Console.WriteLine("Movement");

            var model = JsonConvert.DeserializeObject<MovementDetectedEvent>(msg.Payload);

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
            IBasicProperties props = _client.Model.CreateBasicProperties();
            _client.Model.BasicPublish("msg_gateway", "vision.cmd.detect-face", true, props, RabbitMqMessageHelper.GetModelAsMessagePayload(detectface));

            var detectbody = new DetectBodyCmd()
            {
                ImageId = model.ImageId,
                ImageUrl = model.ImageUrl,
                X = model.X,
                Y = model.Y,
                Width = model.Width,
                Height = model.Height
            };
            _client.Model.BasicPublish("msg_gateway", "vision.cmd.detect-body", true, props, RabbitMqMessageHelper.GetModelAsMessagePayload(detectbody));
            _client.AckMessage(msg.DeliveryTag);

            return new EventHandlerResult(){Success = true, MessageAcked = true};
        }
    }
}
