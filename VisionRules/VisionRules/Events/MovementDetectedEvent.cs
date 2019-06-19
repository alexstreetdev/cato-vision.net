using System;
using System.Collections.Generic;
using System.Text;

namespace VisionRules.Events
{
    public class MovementDetectedEvent
    {
        public long ContentId { get; set; }
        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ContentDescription { get; set; }
    }
}
