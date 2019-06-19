using System;
using System.Collections.Generic;
using System.Text;

namespace VisionRules.Commands
{
    /// <summary>
    /// Check area within image for a face
    /// </summary>
    public class DetectFaceCmd : ImageContentCommandBase
    {

        public DetectFaceCmd() : base("DetectFaceCmd")
        {

        }

    }
}
