using System;
using System.Collections.Generic;
using System.Text;

namespace VisionRules.Events
{
    public abstract class EventBase : IEvent
    {
        public string EventName { get; private set; }
    }
}
