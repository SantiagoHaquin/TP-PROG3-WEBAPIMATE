using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly IRepositoryBase<Cart> _cartRepository;
        private readonly IRepositoryBase<Product> _productRepository;
        private readonly IRepositoryBase<Order> _orderRepository;
        private readonly IRepositoryBase<User> _userRepository;
        public CartService(IRepositoryBase<Cart> cartRepository, IRepositoryBase<Product> productRepository, IRepositoryBase<Order> orderRepository, IRepositoryBase<User> userRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;       
        }

        public async Task AddProductToCart(int clientId, int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            if (product.StockAvailable <= 0)
            {
                throw new Exception("Product out of stock.");
            }

            var clientCart = (await _cartRepository.ListAsync(
                query => query.Include(c => c.Products)))
                .FirstOrDefault(c => c.ClientId == clientId);

            if (clientCart == null)
            {
                clientCart = new Cart
                {
                    ClientId = clientId,
                    Products = new List<Product> { product }
                };

                await _cartRepository.AddAsync(clientCart);
            }
            else
            {
                // Verifica que el producto no esté ya en el carrito
                if (!clientCart.Products.Any(p => p.Id == productId))
                {
                    clientCart.Products.Add(product);
                    await _cartRepository.UpdateAsync(clientCart);
                }
            }

            product.StockAvailable--;
          
            await _productRepository.UpdateAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetCartProducts(int clientId)
        {
            var clientCart = (await _cartRepository.ListAsync(
                query => query.Include(c => c.Products)))
                .FirstOrDefault(c => c.ClientId == clientId);

            if (clientCart == null || !clientCart.Products.Any())
            {
                return new List<ProductDto>();
            }

            return ProductDto.CreateList(clientCart.Products);
        }

        public async Task PurchaseCart(int clientId)
        {
            var clientCart = (await _cartRepository.ListAsync(
                query => query.Include(c => c.Products)))
                .FirstOrDefault(c => c.ClientId == clientId);
            if(clientCart == null || !clientCart.Products.Any())
            {
                throw new Exception("CART IS EMPTY");
            }
            var client = await _userRepository.GetByIdAsync(clientId);
            var order = clientCart.Products.Select(product => new Order
            {
                ProductId = product.Id,
                ClientId = clientId,
                SellerId = product.SellerId,
                DateTime = DateTime.UtcNow,
                EmailClient = client.Email,

            }).ToList();
            
            foreach(var item in order)
            {
                await _orderRepository.AddAsync(item);
            }

            clientCart.Products.Clear();
            await _cartRepository.UpdateAsync(clientCart);   
        }
    }
}
