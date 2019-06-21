using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FaceIdAzure.CognitiveModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FaceIdAzure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        //private IConfiguration _configuration;
        //private ILogger _logger;
        private readonly ICognitiveHttpClient _azureClient;

        public PersonController(ICognitiveHttpClient azc)
        {
            _azureClient = azc;
        }

        [HttpGet("{id}")]
        public async Task<GetPersonResponse> Get(string id)
        {
            var az = new AzureCognitive(_azureClient);
            var s = await az.GetPerson(id);
            return s;
        }


        // POST api/person
        [HttpPost]
        public async void Post([FromBody] string value)
        {
            var az = new AzureCognitive(_azureClient);
            var cp = JsonConvert.DeserializeObject<CreatePersonRequest>(value);
            var s = await az.CreatePerson(cp);
        }


        [HttpGet]
        [Route("all")]
        public async Task<GetPersonResponse[]> GetAll()
        {
            var az = new AzureCognitive(_azureClient);
            var s = await az.GetPersonList();
            return s;
        }


        /// <summary>
        /// Upload image of person
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddFace/{personId}")]
        public async Task<AddPersonFaceResponse> AddFace(string personId)
        {
            AddPersonFaceResponse response;

            var request = new AddPersonFaceRequest()
            {
                PersonGroupId = string.Empty,   //overwritten
                PersonId = personId
            };

            using (var stream = new MemoryStream())
            {
                Request.Body.CopyTo(stream);
                var az = new AzureCognitive(_azureClient);
                response = await az.AddPersonFace(request, stream);
            }

            return response;
        }
    }
}