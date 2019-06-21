
namespace FaceIdAzure.CognitiveModels
{
    public class IdentifiedPersonCandidate
    {
        public string PersonId { get; set; }
        public string Confidence { get; set; }

        public IdentifiedPersonCandidate()
        {
            PersonId = string.Empty;
            Confidence = string.Empty;
        }
    }
}
