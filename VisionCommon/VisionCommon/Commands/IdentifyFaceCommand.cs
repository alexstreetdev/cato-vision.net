
namespace VisionCommon.Commands
{
    public class IdentifyFaceCommand : CommandBase
    {
        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public IdentifyFaceCommand() : base("IdentifyFaceCommand")
        {

        }
    }
}
