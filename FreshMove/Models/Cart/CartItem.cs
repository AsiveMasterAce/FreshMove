using FreshMove.Models.products;

using System.ComponentModel.DataAnnotations.Schema;

namespace FreshMove.Models.Cart
{
    public class CartItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

        [ForeignKey("Cart")]
        public string CartID { get; set; }
        [ForeignKey("Product")]
        public string ProductID { get; set;}

        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }


    }
}
