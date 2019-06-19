using System;
using ImageStore.Services.Database;
using Newtonsoft.Json;
using NUnit.Framework;
using VisionCommon.Models;

namespace ImageStoreTest.Services.Database
{
    [TestFixture()]
    public class Temp
    {

        [Test]
        public void Run()
        {
            var dbs = new DbSettings(){ConnString = ""};
            var db = new MySqlDbClient(dbs);
            db.InitialiseDatabase();
        }

        [Test]
        public void JsonIt()
        {
            var img = new Image
            {
                ImageId = "test_1.jpg",
                SequenceNumber = 1,
                CorrelationId = "test",
                EventTime = DateTime.UtcNow,
                ImageUrl = "http://here",
                Source = "test"
            };
            var imgjson = JsonConvert.SerializeObject(img);

            var c = new ImageContent()
            {
                ImageId = "test_1.jpg",
                X = 100,
                Y = 100,
                Width = 150,
                Height = 75,
                ContentDescription = "womble"
            };
            var contentjson = JsonConvert.SerializeObject(c);
        }

    }
}
