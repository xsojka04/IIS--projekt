using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IISprojekt3.Pages
{
  [Authorize(Roles = "ADMIN, PROFESOR")]
  public class EditUserModel : PageModel
  {
    [BindProperty(SupportsGet = true)]
    public int UserId { get; set; }
    [BindProperty]
    public User UserEntity { get; set; }

    private IisdbContext _iisdbContext;
    public EditUserModel(IisdbContext iisdbContext)
    {
      _iisdbContext = iisdbContext;
      UserEntity = null;
    }

    public void OnGet()
    {
      UserEntity = GetUser(UserId);
      UserEntity.Password = null;
    }

    public IActionResult OnPost()
    {
      if (UserEntity is null)
      {
        this.ViewData["Error"] = "Nepodaøilo se naèíst uivatele";
        return Page();
      }
      return EditUser();
    }

    private IActionResult EditUser()
    {
      try
      {
        var oldUser = GetUser(UserId);
        if (oldUser is null)
          throw(new Exception("Nepodaøilo se upravit uivatele"));
        if (oldUser.Name != UserEntity.Name)
          ChangeName(oldUser);
        oldUser = GetUser(UserId);
        if (!string.IsNullOrEmpty(UserEntity.Password))
          ChangePassword(oldUser);
        oldUser = GetUser(UserId);
        if (oldUser.UserType != UserEntity.UserType)
          ChangeUserType(oldUser);
        return Redirect("/ManageUsers");
      }
      catch(Exception e)
      {
        this.ViewData["Error"] = e.Message;
        return Page();
      }
    }

    private void ChangeUserType(User oldUser)
    {
      oldUser.UserType = UserEntity.UserType;
      _iisdbContext.User.Update(oldUser);
      _iisdbContext.SaveChanges();
      this.ViewData["Info"] = "Uivatel byl upraven";
    }

    private void ChangePassword(User oldUser)
    {
      if (Constants.PASSWORD_MIN_LENHTH > UserEntity.Password.Length || UserEntity.Password.Length > Constants.PASSWORD_MAX_LENHTH)
      {
        throw new Exception($"Heslo musí mít {Constants.PASSWORD_MIN_LENHTH} a {Constants.PASSWORD_MAX_LENHTH} znakù");
      }
      oldUser.Password = UserManager.HashPassword(UserEntity.Password);
      _iisdbContext.User.Update(oldUser);
      _iisdbContext.SaveChanges();
      this.ViewData["Info"] = "Uivatel byl upraven";
    }

    private void ChangeName(User oldUser)
    {
      oldUser.Name = UserEntity.Name;
      if (_iisdbContext.User.FirstOrDefault(item => item.Name == oldUser.Name) != null)
      {
        throw new Exception("Jméno ji je zabrané");
      }
      _iisdbContext.User.Update(oldUser);
      _iisdbContext.SaveChanges();
      this.ViewData["Info"] = "Uivatel byl upraven";
    }

    private User GetUser(int id)
    {
      var user = _iisdbContext.User.FirstOrDefault(x => x.Id == id);
      return user;
    }
  }
}
