using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FreshMove.Models.products
{
    public class AddProductViewModel
    {
        [Required]
        public string productCode { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public IFormFile productImage { get; set; }

        [Display(Name = "Supplier")]
        public string SupplierID { get; set; }
        [Required]
 
        [Display(Name = "Categories")]
        public string CategoryID { get; set; }
    }
}
