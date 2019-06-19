using System;
using System.Collections.Generic;
using System.Text;

namespace VisionRules.Commands
{
    public abstract class CommandBase : ICommand
    {
        public string CommandId { get; private set; }
        public string CommandName { get; private set; }

        protected CommandBase(string name)
        {
            CommandId = Guid.NewGuid().ToString();
            CommandName = name;
        }
    }
}
