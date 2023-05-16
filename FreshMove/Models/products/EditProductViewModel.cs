using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FreshMove.Models.products
{
    public class EditProductViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Name")]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        public string productCode { get; set; }
        [Required]
        [Display(Name = "Description")]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Display(Name = "Product Price")]
        public double Price { get; set; }
        public string ExistingImage { get; set; }

        [NotMapped]
        [Display(Name = "Product Image")]
        public IFormFile productImage { get; set; }

        [Display(Name = "Supplier")]
        public string SupplierID { get; set; }
        [Required]

        [Display(Name = "Categories")]
        public string CategoryID { get; set; }

    }
}
