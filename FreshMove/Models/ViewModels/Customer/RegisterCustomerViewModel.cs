using FreshMove.Constants;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FreshMove.Models.ViewModels.Customer
{
    public class RegisterCustomerViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(120, ErrorMessage = "The {0} must be at least {2} and at a max {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(120, ErrorMessage = "The {0} must be at least {2} and at a max {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Physical Address")]
        public string PhysicalAddress { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10, ErrorMessage ="Number has to be 10 digits")]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public bool Archived { get; set; }
        public Gender Gender { get; set; }

        public Race Race { get; set; }
    }
}
