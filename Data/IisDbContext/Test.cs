using System;
using System.Collections.Generic;

namespace IISprojekt3.Data.IisDbContext
{
    public partial class Test
    {
        public Test()
        {
            Answer = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public int IdAssignment { get; set; }
        public int Author { get; set; }
        public float? Score { get; set; }
        public string Evaluation { get; set; }
        public int? EvaluatedBy { get; set; }
        public string EvaluationText { get; set; }
        public int State { get; set; }
        public DateTime? Beginning { get; set; }
        public DateTime? End { get; set; }

        public virtual User AuthorNavigation { get; set; }
        public virtual User EvaluatedByNavigation { get; set; }
        public virtual Assignment IdAssignmentNavigation { get; set; }
        public virtual TestState StateNavigation { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }
    }
}
