using System;
using System.Collections.Generic;

namespace AttendeeTickerAPI.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Event = new HashSet<Event>();
        }

        public int TeacherID { get; set; }
        public string TeacherName { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
