using System.ComponentModel.DataAnnotations;
using FreshMove.Constants;

namespace FreshMove.Models.ViewModels
{
    public class newUserViewModel
    {

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public UserRole userRole { get; set; }
    }
}
