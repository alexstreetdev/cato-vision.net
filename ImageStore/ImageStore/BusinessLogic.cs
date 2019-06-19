using System;
using System.Threading.Tasks;
using ImageStore.Services.Database;
using VisionCommon.Models;

namespace ImageStore
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IDbClient _db;

        public BusinessLogic(IDbClient dbClient)
        {
            _db = dbClient;
        }

        public async Task<string> AddImageAsync(Image img)
        {
            if (img.ExpiryOn < DateTime.Now)
            {
                var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                img.ExpiryOn = dt.AddDays(30);
            }

            string id = await _db.AddImageAsync(img);
            return id;
        }

        public async Task<string> AddImageContentAsync(ImageContent imgcon)
        {
            var ic = await _db.AddImageContentAsync(imgcon);
            return ic.ContentId.ToString();
        }

        public async Task<int> DeleteImageData(string imageid)
        {
            int i = await _db.DeleteImageAsync(imageid);

            return i;
        }

    }
}
