using System;
using System.Collections.Generic;

namespace AttendeeTickerAPI.Models
{
    public partial class SubjectClass
    {
        public SubjectClass()
        {
            Attendance = new HashSet<Attendance>();
            Event = new HashSet<Event>();
        }

        public int SubjectClassID { get; set; }
        public string SubjectClassName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public int? SubjectID { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual ICollection<Event> Event { get; set; }
    }
}
