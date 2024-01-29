using System;
using System.Collections.Generic;

namespace IISprojekt3.Data.IisDbContext
{
    public partial class User
    {
        public User()
        {
            Answer = new HashSet<Answer>();
            Assignment = new HashSet<Assignment>();
            BindAsistentAssignmentApprovedByNavigation = new HashSet<BindAsistentAssignment>();
            BindAsistentAssignmentIdAsistentNavigation = new HashSet<BindAsistentAssignment>();
            InverseAuthorNavigation = new HashSet<User>();
            TestAuthorNavigation = new HashSet<Test>();
            TestEvaluatedByNavigation = new HashSet<Test>();
        }

        public int Id { get; set; }
        public int UserType { get; set; }
        public string Name { get; set; }
        public int? Author { get; set; }
        public string Password { get; set; }

        public virtual User AuthorNavigation { get; set; }
        public virtual UserType UserTypeNavigation { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }
        public virtual ICollection<Assignment> Assignment { get; set; }
        public virtual ICollection<BindAsistentAssignment> BindAsistentAssignmentApprovedByNavigation { get; set; }
        public virtual ICollection<BindAsistentAssignment> BindAsistentAssignmentIdAsistentNavigation { get; set; }
        public virtual ICollection<User> InverseAuthorNavigation { get; set; }
        public virtual ICollection<Test> TestAuthorNavigation { get; set; }
        public virtual ICollection<Test> TestEvaluatedByNavigation { get; set; }
    }
}
