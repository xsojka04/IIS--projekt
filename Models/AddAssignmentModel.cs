using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IISprojekt3.Models
{
  public class AddAssignmentModel
  {
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Beginning { get; set; }
    [Required]
    public int Duration { get; set; }

    public AddAssignmentModel()
    {

    }

    public Assignment ToAssignment(int authorId)
    {
      return new Assignment()
      {
        Created = DateTime.Now,
        Name = Name,
        Author = authorId,
        Description = Description,
        Beginning = DateTime.Parse(Beginning),
        Duration = Duration
      };
    }

    public Assignment UpdateAssignment(Assignment assignment, int authorId)
    {
      assignment.Name = Name;
      assignment.Author = authorId;
      assignment.Duration = Duration;
      assignment.Description = Description;
      assignment.Beginning = DateTime.Parse(Beginning);
      return assignment;
    }

  }
}
