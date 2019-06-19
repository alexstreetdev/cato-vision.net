using System;
using System.Collections.Generic;
using System.Text;

namespace VisionCommon.Models
{
    public class Image
    {
        public string ImageId { get; set; }
        public DateTime EventTime { get; set; }
        public string ImageUrl { get; set; }
        public string CorrelationId { get; set; }
        public int SequenceNumber { get; set; }
        public string Source { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiryOn { get; set; }
        public DateTime DeletedOn { get; set; }
    }
}
