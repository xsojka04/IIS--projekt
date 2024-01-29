using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IISprojekt3.Models
{
  public class EvaluateTestModel
  {
    [Required]
    public string Evaluation { get; set; }
    [Required]
    public string EvaluationText { get; set; }

    public EvaluateTestModel()
    {
      
    }

  }
}
