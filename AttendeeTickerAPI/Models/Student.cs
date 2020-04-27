using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendeeTickerAPI.Models
{
    public partial class Student
    {
        public Student()
        {
            Attendance = new HashSet<Attendance>();
            ComputerFile = new HashSet<ComputerFile>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StudentID { get; set; }
        public string StudentLastName { get; set; }
        public string StudentFirstName { get; set; }
        public DateTime? Dob { get; set; }
        public string ClassName { get; set; }
        public string Nationality { get; set; }
        public string Phone { get; set; }
        public string PersonID { get; set; }

        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual ICollection<ComputerFile> ComputerFile { get; set; }
    }
}
