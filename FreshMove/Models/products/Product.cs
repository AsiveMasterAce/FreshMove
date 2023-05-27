using FreshMove.Models.actors;
using FreshMove.Models.categories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreshMove.Models.products
{
    public class Product
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

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
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }
        [Required]
        public string productImage { get; set; }

        [ForeignKey("Supplier")]
        public string SupplierID { get; set; }
        [Required]
        [ForeignKey("Category")]
        public string CategoryID { get; set; }  
        public bool Archive { get; set; }

        public virtual Supplier Supplier { get; set; }  
        public virtual Category Category { get; set; }  



    }
}
