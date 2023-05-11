using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace FreshMove.Models.categories
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        [Required]
        [Display(Name = "Name")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(100)]
        public string Description { get; set; }

        public bool Archived { get; set; }

     
       
        public string categoryImage { get; set; }



    }
}
