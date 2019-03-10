using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doremi.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventDes { get; set; }
    }
}
