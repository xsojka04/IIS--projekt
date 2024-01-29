using BlazorInputFile;
using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IISprojekt3.Helpers
{
  public static class Extensions
  {
    public static string GetValidationError(this object o)
    {
      var ctx = new ValidationContext(o);
      var results = new List<ValidationResult>();

      if (!Validator.TryValidateObject(o, ctx, results, true))
        foreach (var errors in results)
          if (errors.ErrorMessage != null)
            return errors.ErrorMessage;
      return null;
    }

    public static string ToUserFriendlyString(this ETestState state)
    {
      switch (state)
      {
        case ETestState.Error:
          return "Chyba";
        case ETestState.Odmitnuto:
          return "Nepřipuštěn";
        case ETestState.Ohodnoceno:
          return "Ohodnoceno";
        case ETestState.Schvaleno:
          return "Připuštěn";
        case ETestState.Vyplneno:
          return "Vyplněno";
        case ETestState.Zazadano:
          return "Zažádáno";
        default:
          throw new NotSupportedException();
      }
    }

    public static List<Answer> ToAnswers(this List<Question> questions, int idTest)
    {
      var answers = new List<Answer>();
      foreach (var question in questions)
      {
        answers.Add(new Answer() { IdQuestion = question.Id, IdTest = idTest });
      }
      return answers;
    }

    public static async Task<string> ToImageStringAsync(this IFileListEntry file) 
    {
      file = await file.ToImageFileAsync(file.Type, 200, 200);
      byte[] data;
      using (var memoryStream = new MemoryStream())
      {
        await file.Data.CopyToAsync(memoryStream);
        data = memoryStream.ToArray();
      }
      return $"data:{file.Type};base64,{Convert.ToBase64String(data)}";
    }
  }
}
