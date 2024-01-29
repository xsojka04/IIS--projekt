using BlazorInputFile;
using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IISprojekt3.Models
{
  public class GroupModel
  {
    [Required]
    public string Name { get; set; }
    [Required]
    public float Score { get; set; }
    [Required]
    public int Amount { get; set; }
    [Required]
    public List<Question> Questions { get; set; }

    public string Error { get; set; }

    public GroupModel()
    {
        Questions = new List<Question>() { new Question() { AnswerType = 1} };
        Amount = Questions.Count;
    }

    public GroupModel(QuestionGroup questionGroup)
    {
      Name = questionGroup.Name;
      Score = questionGroup.Score;
      Amount = questionGroup.Amount;
      Questions = new List<Question>();
    }
    public QuestionGroup ToQuestionGroup(int idAssignment = 0)
    {
      return new QuestionGroup()
      {
        IdAssingment = idAssignment,
        Name = Name,
        Amount = Amount,
        Score = Score,
        Question = Questions,
      };
    }

    public bool IsError(IFileListEntry file)
    {
      Error = null;
      if (file is null)
      {
        Error = "Soubor se nepodařilo získat";
        return true;
      }
      if (!file.Type.ToLower().Contains("image"))
      {
        Error = "Soubor není obrázek";
        return true;
      }
      return false;
    }

    public bool IsError()
    {
      Error = null;
      if (string.IsNullOrEmpty(Name) || (Questions.Count() == 0))
      {
        Error = "Skupina otázek není správně vyplněna";
        return true;
      }

      foreach (var question in Questions)
      {
        if ((string.IsNullOrEmpty(question.Text) && question.Image is null) || question.AnswerType < 1)
        {
          Error = "Otázka není správně vyplněna";
          return true;
        }
      }
      return false;
    }
  }
}
