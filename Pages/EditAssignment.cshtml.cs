using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IISprojekt3.Data.IisDbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IISprojekt3.Pages
{
  [Authorize(Roles = "ADMIN, PROFESOR")]
  public class EditAssignmentModel : PageModel
  {
    private IisdbContext _iisdbContext;

    [BindProperty(SupportsGet = true)]
    public int AssignmentId { get; set; }
    public EditAssignmentModel(IisdbContext iisdbContext)
    {
      _iisdbContext = iisdbContext;
    }

    public IActionResult OnGet()
    {
      if (AssignmentId == 0)
      {
        return Redirect("/ManageAssignments");
      }

      return Page();
    }


    public void OnPost()
    {

    }
  }
}
