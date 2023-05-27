namespace FreshMove.Models.Cart
{
    public class CartViewModel
    {
        public string CartId { get; set; }
        public double CartTotal { get; set; }

        public List<CartItemViewModel> CartItems { get; set; }


    }
}
