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
  [Authorize(Roles = "ADMIN, PROFESOR")]
  public class AssigmentModel : PageModel
  {
    private IisdbContext _iisdb;

    public AssigmentModel(IisdbContext iisdb)
    {
      _iisdb = iisdb;
    }

    public void OnGet()
    {

    }


    public void OnPost()
    {

    }
  }
}
