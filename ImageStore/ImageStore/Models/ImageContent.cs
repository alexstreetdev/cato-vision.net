
using System;

namespace ImageStore.Models
{
    public class ImageContent
    {
        public long ContentId {get; set;}
        public string ImageId {get; set;}
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ContentDescription { get; set; }
        public DateTime? CreatedOn { get; set; }

    }
}
