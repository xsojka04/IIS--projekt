using System;
using System.Collections.Generic;

namespace IISprojekt3.Data.IisDbContext
{
    public partial class BindAsistentAssignment
    {
        public int IdAssignment { get; set; }
        public int IdAsistent { get; set; }
        public byte IsApproved { get; set; }
        public int? ApprovedBy { get; set; }

        public virtual User ApprovedByNavigation { get; set; }
        public virtual User IdAsistentNavigation { get; set; }
        public virtual Assignment IdAssignmentNavigation { get; set; }
    }
}
