using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Models.Enums;
using IISprojekt3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace IISprojekt3.Pages
{
  [Authorize]
  public class TestModel : PageModel
  {
    private IisdbContext _iisdb;
    [BindProperty(SupportsGet = true)]
    public int? TestId { get; set; }
    public int UserId { get; }
    public TestModel(IisdbContext iisdb, IHttpContextAccessor httpContextAccessor)
    {
      _iisdb = iisdb;
      UserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
    }

    public IActionResult OnGet()
    {
      if (TestId is null)
        return Redirect("/");

      var test = _iisdb.Test.FirstOrDefault(x => x.Id == TestId);
      if (test is null)
        return Redirect("/");
      if (test.Author != UserId)
        return Redirect("/");
      var assingment = _iisdb.Assignment.FirstOrDefault(x => x.Id == test.IdAssignment);
      if (IsBeforeInterval(assingment))
        return Redirect($"/TestDetail?TestId={test.Id}");
      if (IsPastInterval(assingment))
      {
        if (test.State != (int)ETestState.Vyplneno || test.State != (int)ETestState.Ohodnoceno)
        {
          test.State = (int)ETestState.Vyplneno;
          _iisdb.Test.Update(test);
          _iisdb.SaveChanges();
        }
        return Redirect($"/TestDetail?TestId={test.Id}");
      }

      if (test.Beginning is null)
      {
        StartTest(test);
        CreateTest(test, assingment);
      }
      return Page();
    }

    private void CreateTest(Test test, Assignment assingment)
    {
      var questionGroups = LoadQuestionGroups(assingment);
      var questions = new List<Question>();
      foreach (var questionGroup in questionGroups)
      {
        questions.AddRange(GetQuestions(questionGroup));
      }
      _iisdb.Answer.AddRange(questions.ToAnswers(test.Id));
      _iisdb.SaveChanges();
    }

    private List<Question> GetQuestions(QuestionGroup questionGroup)
    {
      var chosenQuestions = new List<Question>();
      var allQuestions = ToRandomOrderArray(questionGroup.Question);
      for (int i = 0; i < questionGroup.Amount; i++)
        chosenQuestions.Add(allQuestions.ElementAt(i));
      return chosenQuestions;
    }

    private List<QuestionGroup> LoadQuestionGroups(Assignment assingment)
    {
      var questionGroups = _iisdb.QuestionGroup.Where(x => x.IdAssingment == assingment.Id).ToList();
      foreach (var questionGroup in questionGroups)
      {
        questionGroup.Question = _iisdb.Question.Where(x => x.IdQuestionGroup == questionGroup.Id).ToList();
      }
      return questionGroups;
    }

    private void StartTest(Test test)
    {
      test.Beginning = DateTime.Now;
      _iisdb.Test.Update(test);
      _iisdb.SaveChanges();
    }

    private bool IsBeforeInterval(Assignment assingment)
    {
      if (DateTime.Now < assingment.Beginning)
        return true;
      return false;
    }

    private bool IsPastInterval(Assignment assingment)
    {
      if (DateTime.Now > assingment.Beginning.AddMinutes(assingment.Duration))
        return true;
      return false;
    }

    private static T[] ToRandomOrderArray<T>(ICollection<T> collection)
    {
      var rnd = new Random();
      var array = collection.ToArray();
      for (int i = 0; i < collection.Count; i++)
      {
        int j = rnd.Next(collection.Count - 1);
        Swap(array, i, j);
      }
      return array;
    }

    private static void Swap<T>(T[] collection, int i, int j)
    {
      var tmp = collection[i];
      collection[i] = collection[j];
      collection[j] = tmp;
    }
  }
}
