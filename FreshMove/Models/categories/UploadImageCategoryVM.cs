using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FreshMove.Models.categories
{
    public class UploadImageCategoryVM
    {
        [NotMapped]
        [Display(Name = "Category Image")]
        public IFormFile categoryImageUpload { get; set; }
    }
}
