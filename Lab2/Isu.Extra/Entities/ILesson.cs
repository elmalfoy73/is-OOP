using System.ComponentModel.DataAnnotations;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public interface ILesson
{
    [Required]
    string Subject { get; }
    ClassTime ClassTime { get; }
    [Required]
    string Teacher { get; }
    int ClassRoom { get;  }
}