using System.Security.Cryptography.X509Certificates;

namespace FreshMove.Models.Cart
{
    public class CartItemViewModel
    {

        public string ProductID { get; set; }
        public string productName { get; set; }
        public string productImage { get; set; }

        public int Quantity { get; set; }

        public double TotalPrice { get; set; }

    }
}
