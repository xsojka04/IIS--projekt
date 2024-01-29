using System;
using System.Collections.Generic;

namespace IISprojekt3.Data.IisDbContext
{
    public partial class QuestionGroup
    {
        public QuestionGroup()
        {
            Question = new HashSet<Question>();
        }

        public int Id { get; set; }
        public int IdAssingment { get; set; }
        public string Name { get; set; }
        public float Score { get; set; }
        public int Amount { get; set; }

        public virtual Assignment IdAssingmentNavigation { get; set; }
        public virtual ICollection<Question> Question { get; set; }
    }
}
