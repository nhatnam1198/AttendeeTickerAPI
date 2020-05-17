using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendeeTickerAPI.Models
{
    public class AttendanceDetailsDTO
    {
        public int EventID { get; set; }
        public int SubjectClassID { get; set; }
        public List<Student> StudentList { get; set; }
    }
}
