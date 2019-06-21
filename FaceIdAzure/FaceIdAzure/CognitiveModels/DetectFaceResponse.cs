
namespace FaceIdAzure.CognitiveModels
{
    public class DetectFaceResponse
    {
        public string FaceId { get; set; }
        public FaceRectangle FaceRectangle { get; set; }

        // Not included faceLandmarks or faceAttributes yet
    }
}
