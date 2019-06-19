
using ImageStore.Models;
using System.Threading.Tasks;

namespace ImageStore
{
    public interface IBusinessLogic
    {

        Task<string> AddImageAsync(Image img);

        Task<string> AddImageContentAsync(ImageContent imgContent);


        Task<int> DeleteImageData(string imageid);
    }
}
