﻿
namespace FaceIdAzure.CognitiveModels
{
    public class DetectFaceResponse
    {
        public string FaceId { get; set; }
        public FaceRectangle FaceRectangle { get; set; }
        public FaceAttributes FaceAttributes { get; set; }

        // Not included faceLandmarks or most faceAttributes yet
    }
}
