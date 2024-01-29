using System;
using System.Collections.Generic;

namespace IISprojekt3.Data.IisDbContext
{
    public partial class Answer
    {
        public int Id { get; set; }
        public int IdTest { get; set; }
        public int IdQuestion { get; set; }
        public string Text { get; set; }
        public decimal? Number { get; set; }
        public byte? Bool { get; set; }
        public float? Score { get; set; }
        public int? EvaluatedBy { get; set; }
        public string EvaluationText { get; set; }

        public virtual User EvaluatedByNavigation { get; set; }
        public virtual Question IdQuestionNavigation { get; set; }
        public virtual Test IdTestNavigation { get; set; }
    }
}
