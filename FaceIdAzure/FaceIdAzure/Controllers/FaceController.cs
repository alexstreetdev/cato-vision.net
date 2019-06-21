
using System.IO;
using System.Threading.Tasks;
using FaceIdAzure.CognitiveModels;
using Microsoft.AspNetCore.Mvc;

namespace FaceIdAzure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaceController : ControllerBase
    {

        private readonly ICognitiveHttpClient _azureClient;

        public FaceController(ICognitiveHttpClient azc)
        {
            _azureClient = azc;
        }

        [HttpPost]
        [Route("Detect")]
        public async Task<DetectFaceResponse[]> Detect()
        {
            DetectFaceResponse[] response;

            using (var stream = new MemoryStream())
            {
                Request.Body.CopyTo(stream);
                var az = new AzureCognitive(_azureClient);
                response = await az.DetectFace(new DetectFaceRequest(), stream);
            }

            return response;
        }

        [HttpPost]
        [Route("Identify")]
        public async Task<IdentifyFaceResponse[]> Identify([FromBody] IdentifyFaceRequest faces)
        {
            var az = new AzureCognitive(_azureClient);
            var response = await az.IdentifyFace(faces);
            return response;
        }

    }
}