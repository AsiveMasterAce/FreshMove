using System.ComponentModel.DataAnnotations;
namespace FreshMove.Constants
{
  public enum Gender
  {
    [Display(Name ="Female")]
     Female,
    [Display(Name = "Male")]
     Male,
    [Display(Name = "Other")]
     Other
  }
  public enum Race
  {
    [Display(Name = "African")]
     African,
    [Display(Name = "White")]
     White,
    [Display(Name = "Colored")]
     Colored,
    [Display(Name = "Indian")]
     Indian,
    [Display(Name = "Other")]
     Other
  }

}
