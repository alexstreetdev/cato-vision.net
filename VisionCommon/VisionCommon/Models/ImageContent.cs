using System;


namespace VisionCommon.Models
{
    public class ImageContent
    {
        public string ContentId { get; set; }
        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ContentDescription { get; set; }
        public string ContentData { get; set; }
        public string Source { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
