using System;
using System.Collections.Generic;

namespace AttendeeTickerAPI.Models
{
    public partial class Attendance
    {
        public string StudentID { get; set; }
        public int? SubjectClassID { get; set; }
        public int AttendanceID { get; set; }

        public virtual Student Student { get; set; }
        public virtual SubjectClass SubjectClass { get; set; }
    }
}
