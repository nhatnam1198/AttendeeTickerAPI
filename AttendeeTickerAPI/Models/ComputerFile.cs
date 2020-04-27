using System;
using System.Collections.Generic;

namespace AttendeeTickerAPI.Models
{
    public partial class ComputerFile
    {
        public int FileID { get; set; }
        public string FileUri { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string StudentID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Student Student { get; set; }
    }
}
