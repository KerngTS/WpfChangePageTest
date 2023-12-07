using System;
using System.Collections.Generic;
using System.Text;

namespace KEventAggregator
{
    public class TestEvent:IKEvent
    {
        public string Filter { get; set; }
        public string Message { get; set; }
    }
}
