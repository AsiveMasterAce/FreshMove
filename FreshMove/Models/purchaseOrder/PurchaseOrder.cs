using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FreshMove.Constants;
using FreshMove.Models.actors;

namespace FreshMove.Models.purchaseOrder
{
    public class PurchaseOrder
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";
        [Required]
        [StringLength(10)]
        public string? PONumber { get; set; }
        [Required]
     
        public string? OrderType { get; set; }
        [Required]
        [ForeignKey("Supplier")]
        public string? SupplierID { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }=DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        [Required]
        [DisplayName("Order Status")]
        public PurchaseOrderStatus status { get; set; }
        public virtual Supplier Supplier { get; set; }

    }
}
