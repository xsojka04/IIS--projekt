using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace IISprojekt3.Pages
{
  [Authorize(Roles = "ADMIN, PROFESOR")]
  public class ManageUsersModel : PageModel
  {
    private IisdbContext _iisdbContext;

    public ManageUsersModel(IisdbContext iisdbContext)
    {
      _iisdbContext = iisdbContext;
    }

    public void OnGet()
    {
    }
  }
}
