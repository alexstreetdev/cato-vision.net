
namespace FaceIdAzure.CognitiveModels
{
    /// <summary>
    /// Data required for Create PersonGroup request
    /// </summary>
    public class CreatePersonGroupRequest
    {
        public string PersonGroupId { get; set; }
        public string Name { get; set; }
        public string UserData { get; set; }
    }
}