using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Helpers;
using IISprojekt3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IISprojekt3.Pages
{
  [Authorize(Roles = "ADMIN, PROFESOR")]
  public class TestSearch : PageModel
  {
    public void OnGet()
    {
    }
  }
}
