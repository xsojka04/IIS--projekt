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
  [Authorize(Roles = "ADMIN, PROFESOR")]
  public class AssignmentUsersModel : PageModel
  {
    private IisdbContext _iisdb;

    [BindProperty(SupportsGet = true)]
    public int? AssignmentId { get; set; }
    public Test Test { get; set; }
    public List<Answer> EvaluatedAnswers { get; set; }

    public AssignmentUsersModel(IisdbContext iisdb, IHttpContextAccessor httpContextAccessor) : base()
    {
      _iisdb = iisdb;
    }

    public IActionResult OnGet()
    {
      if (AssignmentId is null)
        return Redirect("/");
      var assignment = _iisdb.Assignment.FirstOrDefault(x => x.Id == AssignmentId);
      if (assignment is null)
        return Redirect("/");
      return Page();
    }
  }
}
