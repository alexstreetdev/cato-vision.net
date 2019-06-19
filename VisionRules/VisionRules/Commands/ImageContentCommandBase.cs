using System;
using System.Collections.Generic;
using System.Text;

namespace VisionRules.Commands
{
    public abstract class ImageContentCommandBase : CommandBase
    {
        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        protected ImageContentCommandBase(string commandName) : base(commandName)
        {

        }

    }
}
