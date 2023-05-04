
using FreshMove.Constants;
using System.ComponentModel.DataAnnotations;

namespace FreshMove.Models.ViewModels.Admin
{
    public class RegisterAdminViewModel
    {

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name ="Physical Address")]
        public string PhysicalAddress { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Race")]
        public Race Gace { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
       
    }
}
