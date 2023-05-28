namespace FreshMove.Models.Cart
{
    public class CartViewModel
    {
        public string CartId { get; set; }
        public double CartTotal { get; set; }

        public virtual HashSet<CartItemViewModel> CartItems { get; set; } = new HashSet<CartItemViewModel>();
    }
}
