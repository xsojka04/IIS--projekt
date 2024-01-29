using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IISprojekt3.Pages
{
  [Authorize]
  public class AccountModel : PageModel
  {
    [BindProperty]
    [DisplayName("Jméno")]
    [Required(ErrorMessage = "Chybí jméno")]
    [MaxLength(50, ErrorMessage = "Jméno mùže mít maximálnì 50 znakù")]
    public string Name { get; set; }

    private readonly ILogger<AccountModel> _logger;
    private IisdbContext _iisdb;
    private UserManager _userManager;

    public AccountModel(ILogger<AccountModel> logger, IisdbContext iisdb, UserManager userManager)
    {
      _logger = logger;
      _iisdb = iisdb;
      _userManager = userManager;
    }

    public async Task OnPostAsync()
    {
      if (ModelState.IsValid)
      {
        await UpdateNameAsync();
      }
      else
      {
        _logger.LogInformation("Change name fail");
        this.ViewData["NameError"] = this.GetValidationError();
      }
    }

    private async Task UpdateNameAsync()
    {
      if (User.Identity.Name == Name)
      {
        this.ViewData["NameInfo"] = "Zadané jméno je aktuální";
        return; 
      }
      if (_iisdb.User.FirstOrDefault(item => item.Name == Name) != null)
      {
        this.ViewData["NameError"] = "Jméno již je zabrané";
        return;
      }

      var userEntity = _iisdb.User.FirstOrDefault(item => item.Id.ToString() == HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
      userEntity.Name = Name;
      _iisdb.SaveChanges();
      await _userManager.UpdateNameAsync(this.HttpContext, Name);
      this.ViewData["NameInfo"] = "Jméno bylo aktualizováno";
    }
  }
}
