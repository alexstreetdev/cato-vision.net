

namespace FaceIdAzure.CognitiveModels
{
    public class IdentifyFaceRequest
    {
        public string PersonGroupId { get; set; }
        public string[] FaceIds { get; set; }

    }
}
