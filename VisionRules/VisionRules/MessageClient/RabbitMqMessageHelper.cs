using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VisionRules.MessageClient
{
    public static class RabbitMqMessageHelper
    {

        public static T GetMessagePayloadModel<T>(byte[] body)
        {
            var bodystring = Encoding.UTF8.GetString(body);
            var model = JsonConvert.DeserializeObject<T>(bodystring);
            return model;
        }

        public static byte[] GetModelAsMessagePayload(object item)
        {
            var serialized = JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(serialized);
        }

    }
}
