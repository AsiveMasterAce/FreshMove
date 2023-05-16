using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace FreshMove.Models.actors
{
    public class Supplier
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "E-mail Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Physical Address")]
        public string PhysicalAddress { get; set; }

        public bool Archived { get; set; }

    }
}
