using System;
using System.Collections.Generic;

namespace AttendeeTickerAPI.Models
{
    public partial class Event
    {
        public int EventID { get; set; }
        public int? ShiftID { get; set; }
        public int? SubjectClassID { get; set; }
        public DateTime? DateTime { get; set; }
        public int? TeacherID { get; set; }

        public virtual Shift Shift { get; set; }
        public virtual SubjectClass SubjectClass { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}