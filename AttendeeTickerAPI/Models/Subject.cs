using System;
using System.Collections.Generic;

namespace AttendeeTickerAPI.Models
{
    public partial class Subject
    {
        public Subject()
        {
            SubjectClass = new HashSet<SubjectClass>();
        }

        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<SubjectClass> SubjectClass { get; set; }
    }
}
