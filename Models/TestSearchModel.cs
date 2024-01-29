using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IISprojekt3.Models
{
  public class TestSearchModel
  {
    public int TestId { get; set; }
    public int UserId { get; set; }
    public string AssignmentName { get; set; }
    public string UserName { get; set; }
    public ETestState TestState { get; set; }
    public float Score { get; set; }
    public float MaxScore { get; set; }

    public TestSearchModel(Test test, Assignment assignment, User user, IisdbContext iisdb)
    {
      TestId = test.Id;
      AssignmentName = assignment.Name;
      UserId = user.Id;
      UserName = user.Name;
      TestState = (ETestState)test.State;
      Score = iisdb.Answer.Where(x => x.IdTest == TestId).ToList().Sum(x => x.Score) ?? 0;
      MaxScore = iisdb.QuestionGroup.Where(x => x.IdAssingment == assignment.Id).ToList().Sum(x => x.Score * x.Amount);
    }
  }
}
