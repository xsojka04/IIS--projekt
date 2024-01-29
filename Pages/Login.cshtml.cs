using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IISprojekt3.Pages
{
  public class LoginModel : PageModel
  {
    [BindProperty]
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }


    [DataType(DataType.Password)]
    [Required]
    [BindProperty]
    public string Password { get; set; }

    private readonly ILogger<LoginModel> _logger;
    private IisdbContext _iisdb;
    private UserManager _userManager;
    public string Error { get; set; }
    public string Info { get; set; }

    public LoginModel(ILogger<LoginModel> logger, IisdbContext iisdb, UserManager userManager)
    {
      _logger = logger;
      _iisdb = iisdb;
      _userManager = userManager;
    }

    public void OnGet()
    {
      Error = null;
    }

    public async Task<IActionResult> OnPostAsync()
    {
      Error = null;
      if (ModelState.IsValid)
      {
        if (await _userManager.SignIn(this.HttpContext, _iisdb, Name, Password))
        {
          _logger.LogInformation("Login succ");
          return Redirect("/Index");
        }
      }
      _logger.LogInformation("Login fail");
      Error = "Nesprávné údaje";
      return Page();
    }
  }
}
