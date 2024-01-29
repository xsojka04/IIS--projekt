using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Helpers;
using IISprojekt3.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IISprojekt3.Models
{
  public class AddUserModel
  {
    [Required]
    public string Name { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public EUserType UserType { get; set; }

    public AddUserModel()
    {
      UserType = EUserType.Student;
    }

    public User ToUser(int authorId)
    {
      return new User()
      {
        Name = Name,
        Author = authorId,
        Password = UserManager.HashPassword(Password),
        UserType = (int)UserType
      };
    }
  }
}
