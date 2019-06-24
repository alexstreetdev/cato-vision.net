
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FaceIdAzure.CognitiveModels;
using Newtonsoft.Json;


namespace FaceIdAzure
{
    public class AzureCognitive
    {
        
        private static string personGroupId = "cato-group";

        private readonly ICognitiveHttpClient _client;

        public AzureCognitive(ICognitiveHttpClient client)
        {
            _client = client;
        }

#region PersonGroup
        public async Task<GetPersonGroupResponse[]> GetPersonGroupList()
        {
            var hrm = await _client.GetAsync("/personGroups");
            string result = await hrm.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<GetPersonGroupResponse[]>(result);

            return dto;
        }

        public async Task<string> CreatePersonGroup(CreatePersonGroupRequest group)
        {
            string url = $"/personGroups/{group.PersonGroupId}";
            
            var bodyText = JsonConvert.SerializeObject(group);
            var httpContent = new StringContent(bodyText, Encoding.UTF8, "application/json");

            HttpResponseMessage hrm = await _client.PutAsync(url, httpContent);

            return hrm.StatusCode.ToString();
        }

        public async Task<string> TrainPersonGroup()
        {
            string url = $"/personGroups/{personGroupId}/train";
            HttpResponseMessage hrm = await _client.PostAsync(url, new StringContent(string.Empty));

            return hrm.StatusCode.ToString();
        }

        public async Task<PersonGroupTrainingStatusResult> GetTrainingStatus()
        {
            var hrm = await _client.GetAsync($"/personGroups/{personGroupId}/training");
            var reply = await hrm.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PersonGroupTrainingStatusResult>(reply);

            return result;
        }

        #endregion PersonGroup

#region Person
        public async Task<CreatePersonResponse> CreatePerson(CreatePersonRequest cp)
        {
            string url = $"/personGroups/{personGroupId}/persons";

            var bodyText = JsonConvert.SerializeObject(cp);
            var httpContent = new StringContent(bodyText, Encoding.UTF8, "application/json");

            HttpResponseMessage hrm = await _client.PostAsync(url, httpContent);
            string result = await hrm.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<CreatePersonResponse>(result);
            return response;
        }

        public async Task<GetPersonResponse> GetPerson(string personId)
        {
            var person = new GetPersonRequest()
            {
                PersonGroupId = personGroupId,
                PersonId = personId
            };

            var hrm = await _client.GetAsync($"/personGroups/{personGroupId}/persons/{person.PersonId}");
            string result = await hrm.Content.ReadAsStringAsync();
            var personResponse = JsonConvert.DeserializeObject<GetPersonResponse>(result);

            return personResponse;
        }

        public async Task<GetPersonResponse[]> GetPersonList()
        {
            var hrm = await _client.GetAsync($"/personGroups/{personGroupId}/persons");

            string result = await hrm.Content.ReadAsStringAsync();
            var personResponse = JsonConvert.DeserializeObject<GetPersonResponse[]>(result);

            return personResponse;
        }

        public async Task<AddPersonFaceResponse> AddPersonFace(AddPersonFaceRequest request, MemoryStream image)
        {
            request.PersonGroupId = personGroupId;

            string query = $"/{request.PersonGroupId}/persons/{request.PersonId}/persistedfaces";
            string optionalparam = string.Empty;
            if(request.UserData != string.Empty) optionalparam = $"userData={request.UserData}";
            if (request.TargetFace != string.Empty)
            {
                optionalparam = optionalparam == string.Empty ? $"?targetFace={request.TargetFace}" : $"?{optionalparam}&targetFace={request.TargetFace}";
            }

            string finalUrl = $"/persongroups{query}{optionalparam}";
            var content = new ByteArrayContent(image.ToArray());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            HttpResponseMessage hrm = await _client.PostAsync(finalUrl, content);
            string result = await hrm.Content.ReadAsStringAsync();
            var detectResponse = JsonConvert.DeserializeObject<AddPersonFaceResponse>(result);

            return detectResponse;
        }

#endregion Person

        public async Task<DetectFaceResponse[]> DetectFace(DetectFaceRequest detectRequest, MemoryStream image)
        {
            var content = new ByteArrayContent(image.ToArray());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            HttpResponseMessage hrm = await _client.PostAsync("/detect?returnFaceAttributes=age,gender", content);
            string result = await hrm.Content.ReadAsStringAsync();
            var detectResponse = JsonConvert.DeserializeObject<DetectFaceResponse[]>(result);

            return detectResponse;
        }

        public async Task<IdentifyFaceResponse[]> IdentifyFace(IdentifyFaceRequest request)
        {
            request.PersonGroupId = personGroupId;
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var hrm = await _client.PostAsync("/identify", content);
            string result = await hrm.Content.ReadAsStringAsync();
            var detectResponse = JsonConvert.DeserializeObject<IdentifyFaceResponse[]>(result);

            return detectResponse;
        }

    }
}