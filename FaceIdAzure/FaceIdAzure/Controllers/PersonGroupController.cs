
using System.Threading.Tasks;
using FaceIdAzure.CognitiveModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FaceIdAzure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonGroupController : ControllerBase
    {

        private readonly ICognitiveHttpClient _azureClient;

        public PersonGroupController(ICognitiveHttpClient azc)
        {
            _azureClient = azc;
        }

        
        /// <summary>
        /// Create a PersonGroup
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public async void Post([FromBody] string value)
        {
            var az = new AzureCognitive(_azureClient);
            var model = JsonConvert.DeserializeObject<CreatePersonGroupRequest>(value);
            var s = await az.CreatePersonGroup(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<GetPersonGroupResponse[]> GetList()
        {
            var az = new AzureCognitive(_azureClient);
            var s = await az.GetPersonGroupList();
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Train")]
        public async Task<string> Train()
        {
            var az = new AzureCognitive(_azureClient);
            var s = await az.TrainPersonGroup();
            return s;
        }

        [HttpGet]
        [Route("TrainingStatus")]
        public async Task<PersonGroupTrainingStatusResult> GetTrainingStatus()
        {
            var az = new AzureCognitive(_azureClient);
            var result = await az.GetTrainingStatus();
            return result;
        }

    }
}