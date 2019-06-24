using System;

namespace VisionCommon.Commands
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
