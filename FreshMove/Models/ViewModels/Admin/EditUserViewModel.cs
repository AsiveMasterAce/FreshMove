﻿using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using FreshMove.Constants;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;
using Microsoft.AspNetCore.Identity;

namespace FreshMove.Models.ViewModels.Admin
{
    public class EditUserViewModel: IdentityUser
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(120, ErrorMessage = "The {0} must be at least {2} and at a max {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(120, ErrorMessage = "The {0} must be at least {2} and at a max {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        public string PhysicalAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [Display(Name = "User Role")]
        public UserRole UserRole { get; set; }

        public bool Archived { get; set; }
        public Gender Gender { get; set; }

        public Race Race { get; set; }
    }
}
