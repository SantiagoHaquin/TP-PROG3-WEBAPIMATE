using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICartService
    {
        Task AddProductToCart(int clientId, int productId);
        Task<IEnumerable<CartProductDto>> GetCartProducts(int clientId);
        Task RemoveProductFromCart(int clientId, int productId);
        Task PurchaseCart(int clientId);
    }
}
