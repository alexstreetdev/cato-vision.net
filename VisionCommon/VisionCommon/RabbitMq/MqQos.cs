using System;
using System.Collections.Generic;
using System.Text;

namespace VisionCommon.RabbitMq
{
    public class MqQos
    {
        /// <summary>
        /// Believe this isn't supported yet, 0 = unlimited
        /// </summary>
        public uint PrefetchSize => 0;
        public ushort PrefetchCount { get; set; }
        // Not implemented yet
        public bool Global => false;

        public MqQos()
        {
            PrefetchCount = 0;
        }

    }
}
