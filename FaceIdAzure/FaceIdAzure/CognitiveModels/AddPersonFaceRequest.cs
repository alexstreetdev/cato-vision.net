

namespace FaceIdAzure.CognitiveModels
{
    public class AddPersonFaceRequest
    {
        /// <summary>
        /// Specifying the person group containing the target person.
        /// </summary>
        /// <remarks>Ignored as using single group in this solution</remarks>
        public string PersonGroupId { get; set; }

        /// <summary>
        /// Target person that the face is added to.
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// User-specified data about the target face to add for any purpose. The maximum length is 1KB.
        /// </summary>
        public string UserData { get; set; }

        /// <summary>
        /// A face rectangle to specify the target face to be added to a person, in the format of "targetFace=left,top,width,height".
        /// E.g. "targetFace=10,10,100,100".
        /// If there is more than one face in the image, targetFace is required to specify which face to add.
        /// No targetFace means there is only one face detected in the entire image.
        /// </summary>
        public string TargetFace { get; set; }
    }
}
