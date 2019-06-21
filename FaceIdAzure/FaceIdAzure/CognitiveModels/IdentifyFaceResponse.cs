
namespace FaceIdAzure.CognitiveModels
{
    public class IdentifyFaceResponse
    {
        public string FaceId { get; set; }

        public IdentifiedPersonCandidate[] Candidates { get; set; }


    }
}
