using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CartProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public static CartProductDto Create(CartProduct cartItem)
        {
            return new CartProductDto
            {
                ProductId = cartItem.ProductId,
                Name = cartItem.Product.Name,
                Price = cartItem.Product.Price,
                Quantity = cartItem.Quantity
            };
        }

        public static IEnumerable<CartProductDto> CreateList(IEnumerable<CartProduct> cartItems)
        {
            return cartItems.Select(Create);
        }
    }

}
