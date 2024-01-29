using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace IISprojekt3.Pages
{
  [Authorize]
  public class TestDetailModel : PageModel
  {
    private IisdbContext _iisdb;

    [BindProperty(SupportsGet = true)]
    public int? TestId { get; set; }
    public int UserId { get; }
    public Test Test { get; set; }
    public List<Answer> EvaluatedAnswers { get;  set; }

    public TestDetailModel(IisdbContext iisdb, IHttpContextAccessor httpContextAccessor): base()
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
      //TODO validate stav
      LoadData(test);
      return Page();
    }

    private void LoadData(Test test)
    {
      test.IdAssignmentNavigation = _iisdb.Assignment.FirstOrDefault(x => x.Id == test.IdAssignment);
      test.IdAssignmentNavigation.AuthorNavigation = _iisdb.User.FirstOrDefault(x => x.Id == test.IdAssignmentNavigation.Author);
      test.IdAssignmentNavigation.QuestionGroup = _iisdb.QuestionGroup.Where(x => x.IdAssingment == test.IdAssignment).ToList();
      switch ((ETestState)test.State)
      {
        case ETestState.Zazadano:
        case ETestState.Error:
        case ETestState.Odmitnuto:
        case ETestState.Schvaleno:
          break;
        case ETestState.Vyplneno:
          test.Answer = LoadAnswersAsync(test).ConfigureAwait(false).GetAwaiter().GetResult();
          break;
        case ETestState.Ohodnoceno:
          test.Answer = LoadAnswersAsync(test).ConfigureAwait(false).GetAwaiter().GetResult();
          break;
        default:
          Redirect("/");
          return;
      }
      this.Test = test;
    }

    public float MaxScore()
    {
      float count = 0;
      foreach (var questionGroup in Test.IdAssignmentNavigation.QuestionGroup)
      {
        count += questionGroup.Score * questionGroup.Amount;
      }
      return count;
    }

    public int QuestionsCount()
    {
      int count = 0;
      foreach (var questionGroup in Test.IdAssignmentNavigation.QuestionGroup)
      {
        count += questionGroup.Amount;
      }
      return count;
    }

    public bool IsContinueTest()
    {
      if (IsTestTime() && Test.Beginning != null && Test.State == (int)ETestState.Schvaleno)
        return true;
      return false;
    }

    private bool IsTestTime()
    {
      if (DateTime.Now > Test.IdAssignmentNavigation.Beginning && DateTime.Now < Test.IdAssignmentNavigation.Beginning.AddMinutes(Test.IdAssignmentNavigation.Duration))
        return true;
      return false;
    }

    public bool IsStartTest()
    {
      if (IsTestTime() && Test.Beginning is null && Test.State == (int)ETestState.Schvaleno)
        return true;
      return false;
    }

    private async Task<List<Answer>> LoadAnswersAsync(Test test)
    {
      var answers = _iisdb.Answer.Where(x => x.IdTest == test.Id).ToList();
      foreach (var answer in answers)
      {
        answer.IdQuestionNavigation = await _iisdb.Question.FirstOrDefaultAsync(x => x.Id == answer.IdQuestion);
      }
      return answers;
    }

    public async Task<bool> IsEvaluatedAsync()
    {
      if (Test.State == (int)ETestState.Ohodnoceno)
      {
        EvaluatedAnswers = _iisdb.Answer.Where(x => x.IdTest == Test.Id).ToList();
        Test.EvaluatedByNavigation = await _iisdb.User.FirstOrDefaultAsync(x => x.Id == Test.EvaluatedBy);
        foreach (var answer in EvaluatedAnswers)
        {
          answer.EvaluatedByNavigation = await _iisdb.User.FirstOrDefaultAsync(x => x.Id == answer.EvaluatedBy);
          answer.IdQuestionNavigation = await _iisdb.Question.FirstOrDefaultAsync(x => x.Id == answer.IdQuestion);
          answer.IdQuestionNavigation.IdQuestionGroupNavigation = await _iisdb.QuestionGroup.FirstOrDefaultAsync(x => x.Id == answer.IdQuestionNavigation.IdQuestionGroup);
        }

        return true;
      }
      return false;
    }

    public bool IsBeforeTest()
    {
      if (Test.IdAssignmentNavigation.Beginning > DateTime.Now && Test.State == (int)ETestState.Schvaleno)
        return true;
      return false;
    }
  }
}
