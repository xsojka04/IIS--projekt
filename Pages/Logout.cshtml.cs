using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IISprojekt3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IISprojekt3.Pages
{
  [Authorize]
  public class LogoutModel : PageModel
  {
    private UserManager _userManager;
    public LogoutModel(UserManager userManager)
    {
      _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync()
    {
      await _userManager.SignOut(this.HttpContext);
      return Redirect("/Index");
    }
  }
}
