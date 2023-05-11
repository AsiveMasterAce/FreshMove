using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FreshMove.Models.categories
{
    public class CategoryViewModel
    {
        [Display(Name = "Name")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(100)]
        public string Description { get; set; }
        [NotMapped]
        [Display(Name = "Category Image")]
        public IFormFile categoryImage { get; set; }

    }
}
