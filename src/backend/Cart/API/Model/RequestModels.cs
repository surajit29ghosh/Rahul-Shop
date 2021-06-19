using System;

namespace eShop.Cart.API.Models
{
    public class GetCartModel
    {
        public string CartId { get; set; }
    }
    public class AddCartModel
    {
        public string CartId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Units { get; set; }
    }
}