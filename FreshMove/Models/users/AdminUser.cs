using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FreshMove.Constants;

namespace FreshMove.Models.users
{
    public class AdminUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        [Required]
        public string UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string PhysicalAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool Archived { get; set; }
        public Gender Gender { get; set; }

        public Race Race { get; set; }
    }
}
