using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VisionCommon.RabbitMq;
using VisionRules.Commands;
using VisionRules.Events;
using VisionRules.MessageClient;

namespace VisionRules.EventHandlers
{
    public class FaceDetectedHandler : IEventHandler
    {
        private readonly IRabbitMqClient _client;
        public FaceDetectedHandler(IRabbitMqClient client)
        {
            _client = client;
        }

        public EventHandlerResult Handle(MqMessage<string> msg)
        {
            Console.WriteLine("FACE !!!");

            var model = JsonConvert.DeserializeObject<FaceDetectedEvent>(msg.Payload);

            var cmd = new IdentifyFaceCmd()
            {
                ImageId = model.ImageId,
                ImageUrl = model.ImageUrl,
                X = model.X,
                Y = model.Y,
                Width = model.Width,
                Height = model.Height
            };

            IBasicProperties props = _client.Model.CreateBasicProperties();
            _client.Model.BasicPublish("msg_gateway", "vision.cmd.identify-face", true, props, RabbitMqMessageHelper.GetModelAsMessagePayload(cmd));

            _client.AckMessage(msg.DeliveryTag);

            return new EventHandlerResult(){Success = true, MessageAcked = true};
        }
    }
}
