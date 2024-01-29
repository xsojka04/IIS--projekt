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
  [Authorize(Roles = "ADMIN, PROFESOR, ASISTENT")]
  public class EvaluateTestModel : PageModel
  {
    private IisdbContext _iisdb;

    [BindProperty(SupportsGet = true)]
    public int? TestId { get; set; }
    public int UserId { get; }

    public EvaluateTestModel(IisdbContext iisdb, IHttpContextAccessor httpContextAccessor) : base()
    {
      _iisdb = iisdb;
      UserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
    }

    public IActionResult OnGet()
    {
      if (TestId is null)
        return Redirect("/");
      var test = _iisdb.Test.FirstOrDefault(x => x.Id == TestId);
      test.IdAssignmentNavigation = _iisdb.Assignment.FirstOrDefault(x => x.Id == test.IdAssignment);
      if (test is null)
        return Redirect("/");
      if (test.State != (int)ETestState.Vyplneno && test.State != (int)ETestState.Ohodnoceno)
        return Redirect("/");

      var user = _iisdb.User.FirstOrDefault(x => x.Id == UserId);
      if (user.UserType == (int)EUserType.Asistent)
      {
        var asignmentsForAssistent = _iisdb.BindAsistentAssignment.Where(x => x.IdAsistent == UserId).ToList();
        List<int> asignmentsId = new List<int>();
        foreach (var asignmentForAssistent in asignmentsForAssistent)
        {
          asignmentsId.Add(asignmentForAssistent.IdAssignment);
        }

        if (!asignmentsId.Contains(test.IdAssignmentNavigation.Id))
        {
          return Redirect("/");
        }
      }

      return Page();
    }
  }
}
