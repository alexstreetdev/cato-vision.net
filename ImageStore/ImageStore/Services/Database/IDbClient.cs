using System.Threading.Tasks;
using ImageStore.Models;

namespace ImageStore.Services.Database
{
    public interface IDbClient
    {

        string AddImage(Image img);

        Task<string> AddImageAsync(Image img);

        Task<ImageContent> AddImageContentAsync(ImageContent ic);

        Task<int> DeleteImageAsync(string imageid);

    }
}
