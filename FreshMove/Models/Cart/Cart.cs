using System.ComponentModel.DataAnnotations.Schema;
using FreshMove.Models.products;
using FreshMove.Models.users;

namespace FreshMove.Models.Cart
{
    public class Cart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        [ForeignKey("Product")]
        public string ProductID { get; set; }
        [ForeignKey("Customer")]
        public string CustomerID { get; set; }

        public int quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }


    }
}
