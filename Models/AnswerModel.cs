using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IISprojekt3.Models
{
  public class AnswerModel
  {
    public bool IsSaved { get; set; }
    public string ListGroupItemClass { get; set; }
    public int Id { get; set; }
    public int IdQuestion { get; set; }
    public bool? Bool { get; set; }
    public string YesBoolClass { get; set; }
    public string NoBoolClass { get; set; }
    public int? EvaluatedBy { get; set; }
    public string EvaluationText { get; set; }
    public int IdTest { get; set; }
    public float? Score { get; set; }
    public decimal? Number { get; set; }
    public string Text { get; set; }
    public EAnswerType AnswerType { get; set; }
    public float MaxScore { get; set; }
    public string QuestionText { get; set; }
    public string QuestionImage { get; set; }

    public AnswerModel(Answer answer, float maxScore)
    {
      Id = answer.Id;
      IdQuestion = answer.IdQuestion;
      SetBool(answer.Bool is null ? (bool?)null : Convert.ToBoolean(answer.Bool));
      EvaluatedBy = answer.EvaluatedBy;
      EvaluationText = answer.EvaluationText;
      IdTest = answer.IdTest;
      Score = answer.Score;
      Number = answer.Number;
      Text = answer.Text;
      AnswerType = (EAnswerType)answer.IdQuestionNavigation.AnswerType;
      MaxScore = maxScore;
      QuestionText = answer.IdQuestionNavigation.Text;
      QuestionImage = answer.IdQuestionNavigation.Image;
      if (Bool != null || Text != null || Number != null)
        SetIsSaved(true);
      else
        SetIsSaved(false);
    }

    public void UpdateAnswer(Answer answer)
    {
      answer.EvaluatedBy = EvaluatedBy;
      answer.EvaluationText = EvaluationText;
      answer.Bool = Bool is null ? (byte?)null : Convert.ToByte(Bool);
      answer.Score = Score;
      answer.Number = Number;
      answer.Text = Text;
    }

    public void SetIsSaved(bool isSaved)
    {
      IsSaved = isSaved;
      if (IsSaved)
        ListGroupItemClass = "list-group-item-primary";
      else
        ListGroupItemClass = "list-group-item-warning";
    }

    public void SetBool(bool? value)
    {
      Bool = value;
      if (value is null)
      {
        YesBoolClass = NoBoolClass = "btn btn-secondary";
      }
      else if (value == true)
      {
        YesBoolClass = "btn btn-primary";
        NoBoolClass = "btn btn-secondary";
      } 
      else
      {
        YesBoolClass = "btn btn-secondary";
        NoBoolClass = "btn btn-primary";
      }
    }
  }
}
