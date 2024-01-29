using System;
using System.Collections.Generic;

namespace IISprojekt3.Data.IisDbContext
{
    public partial class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public int IdQuestionGroup { get; set; }
        public string Text { get; set; }
        public int AnswerType { get; set; }
        public string Image { get; set; }

        public virtual AnswerType AnswerTypeNavigation { get; set; }
        public virtual QuestionGroup IdQuestionGroupNavigation { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }
    }
}
