using System;
using ImageStore.Services.Database;
using NUnit.Framework;
using VisionCommon.Models;

namespace ImageStoreTest.Services.Database
{
    [TestFixture]
    public class DbHelperTest
    {

        [Test]
        public void AddImageQuery_returns_valid_sql()
        {
            var img = new Image
            {
                ImageId = "1",
                //Filename = "1.jpg",
                EventTime = new DateTime(2019, 4, 26),
                CorrelationId = "1",
                SequenceNumber = 4,
                Source = "Nunit",
                CreatedOn = DateTime.Now
            };

            var result = MySqlDbHelper.AddImageQuery(img);

            // Assert
            var d = img.CreatedOn.ToString("yyyy-MM-dd hh-mm-ss.ffffff");
            string s = $"INSERT INTO Image (ImageId,EventTime,CorrelationId,SequenceNumber,Source,CreatedOn)" +
                       $" VALUES('1','2019-04-26','1',4,'Nunit','{d}')";

            Assert.True(result.Equals(s,StringComparison.InvariantCultureIgnoreCase));
        }


    }
}
