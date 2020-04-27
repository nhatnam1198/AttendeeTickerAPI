using System;
using System.Collections.Generic;

namespace AttendeeTickerAPI.Models
{
    public partial class Shift
    {
        public Shift()
        {
            Event = new HashSet<Event>();
        }

        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan? ShiftStart { get; set; }
        public TimeSpan? ShiftEnd { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
