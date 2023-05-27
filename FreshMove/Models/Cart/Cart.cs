using System.ComponentModel.DataAnnotations.Schema;
using FreshMove.Models.products;
using FreshMove.Models.users;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FreshMove.Models.Cart
{
    public class Cart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";
        
        [ForeignKey("Customer")]
        public string CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<CartItem> CartItems { get; set; }



    }
}
