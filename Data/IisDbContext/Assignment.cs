using System;
using System.Collections.Generic;

namespace IISprojekt3.Data.IisDbContext
{
    public partial class Assignment
    {
        public Assignment()
        {
            BindAsistentAssignment = new HashSet<BindAsistentAssignment>();
            QuestionGroup = new HashSet<QuestionGroup>();
            Test = new HashSet<Test>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public int Author { get; set; }
        public string Description { get; set; }
        public DateTime Beginning { get; set; }
        public int Duration { get; set; }

        public virtual User AuthorNavigation { get; set; }
        public virtual ICollection<BindAsistentAssignment> BindAsistentAssignment { get; set; }
        public virtual ICollection<QuestionGroup> QuestionGroup { get; set; }
        public virtual ICollection<Test> Test { get; set; }
    }
}
