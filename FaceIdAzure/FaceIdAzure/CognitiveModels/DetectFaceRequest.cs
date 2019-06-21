

namespace FaceIdAzure.CognitiveModels
{
    /// <summary>
    /// Parameters to call the api to
    /// Detect human faces in an image, return face rectangles, and optionally with faceIds, landmarks, and attributes.
    /// </summary>
    public class DetectFaceRequest
    {
        /// <summary>
        /// (optional)
        /// Return faceIds of the detected faces or not.The default value is true.
        /// </summary>
        public bool ReturnFaceId { get; set; }

        /// <summary>
        /// (optional)
        /// Return face landmarks of the detected faces or not. The default value is false.
        /// </summary>
        public bool ReturnFaceLandmarks { get; set; }

        /// <summary>
        /// (optional)
        /// Analyze and return the one or more specified face attributes in the comma-separated string like
        /// "returnFaceAttributes=age,gender".
        /// Supported face attributes include age, gender, headPose, smile, facialHair, glasses, emotion,
        /// hair, makeup, occlusion, accessories, blur, exposure and noise.
        /// Face attribute analysis has additional computational and time cost.
        /// </summary>
        public string ReturnFaceAttributes { get; set; }



        public DetectFaceRequest()
        {
            ReturnFaceId = true;
            ReturnFaceLandmarks = false;
            ReturnFaceAttributes = string.Empty;
        }
    }
}
