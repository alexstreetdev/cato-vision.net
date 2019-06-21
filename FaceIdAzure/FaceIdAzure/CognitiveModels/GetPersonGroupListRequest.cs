
namespace FaceIdAzure.CognitiveModels
{
    public class GetPersonGroupListRequest
    {
        public string Start { get; set; }
        public string Top { get; set; }

        public GetPersonGroupListRequest() : this(string.Empty, string.Empty) { }

        public GetPersonGroupListRequest(string start, string top)
        {
            Start = start;
            Top = top;
        }
    }
}
