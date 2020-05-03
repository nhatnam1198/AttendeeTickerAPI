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
        public DateTime? ShiftStart { get; set; }
        public DateTime? ShiftEnd { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
