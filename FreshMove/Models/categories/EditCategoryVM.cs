using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreshMove.Models.categories
{
    public class EditCategoryVM
    {
        public string Id { get;set; }

        [Display(Name = "Name")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(100)]
        public string Description { get; set; }

        public string ExistingImage { get; set; }
        [NotMapped]
        [Display(Name = "Category Image")]
        public IFormFile categoryImage { get; set; }

    }
}
