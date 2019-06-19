using System;
using System.Collections.Generic;
using System.Text;

namespace VisionRules.Commands
{
    public interface ICommand
    {
        string CommandId { get; }
        string CommandName { get; }
    }
}
