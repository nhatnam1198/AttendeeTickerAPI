using System;
using System.Collections.Generic;

namespace AttendeeTickerAPI.Models
{
    public partial class AttendanceDetails
    {
        public int AttendanceID { get; set; }
        public int? EventID { get; set; }
        public bool? IsAttended { get; set; }

        public virtual Attendance Attendance { get; set; }
        public virtual Event Event { get; set; }
    }
}
