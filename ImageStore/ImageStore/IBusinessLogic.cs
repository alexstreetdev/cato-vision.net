

using System.Threading.Tasks;
using VisionCommon.Models;

namespace ImageStore
{
    public interface IBusinessLogic
    {

        Task<string> AddImageAsync(Image img);

        Task<string> AddImageContentAsync(ImageContent imgContent);


        Task<int> DeleteImageData(string imageid);
    }
}
