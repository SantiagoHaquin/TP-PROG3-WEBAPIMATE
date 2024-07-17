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
               query => query.Include(c => c.CartProducts)
                              .ThenInclude(p => p.Product)))
                              .FirstOrDefault(c => c.ClientId == clientId);

            if (clientCart == null)
            {
                clientCart = new Cart
                {
                    ClientId = clientId,
                    CartProducts = new List<CartProduct>
                    { new CartProduct 
                        {
                            ProductId = productId,
                            Quantity = 1,
                            Product = product
                        } 
                    }
                };

                await _cartRepository.AddAsync(clientCart);
            }
            else
            {
                var existProducttoCart = clientCart.CartProducts.FirstOrDefault(clientCart => clientCart.ProductId == productId);   
                if (existProducttoCart == null) 
                {
                    clientCart.CartProducts.Add(new CartProduct 
                    { ProductId = productId,
                      Quantity = 1, 
                     Product= product 
                    });
                }
                else 
                { 
                    existProducttoCart.Quantity++;
                }
                await _cartRepository.UpdateAsync(clientCart);
            }
        }

        public async Task<IEnumerable<CartProductDto>> GetCartProducts(int clientId)
        {
            var clientCart = (await _cartRepository.ListAsync(
                query => query.Include(c => c.CartProducts)
                .ThenInclude(p => p.Product)))
                .FirstOrDefault(c => c.ClientId == clientId);

            if (clientCart == null || !clientCart.CartProducts.Any())
            {
                return new List<CartProductDto>();
            }
           
            return CartProductDto.CreateList(clientCart.CartProducts);
        }
        public async Task RemoveProductFromCart(int clientId, int productId)
        {
            var clientCart = (await _cartRepository.ListAsync(
                query => query.Include(c => c.CartProducts)))
                .FirstOrDefault(c => c.ClientId == clientId);

            if (clientCart == null)
            {
                throw new Exception("Cart not found.");
            }

            var cartItem = clientCart.CartProducts.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem == null)
            {
                throw new Exception("Product not found in cart.");
            }

            clientCart.CartProducts.Remove(cartItem);
            await _cartRepository.UpdateAsync(clientCart);
        }

        public async Task PurchaseCart(int clientId)
        {
            var clientCart = (await _cartRepository.ListAsync(
                query => query.Include(c => c.CartProducts)
                              .ThenInclude(ci => ci.Product)))
                .FirstOrDefault(c => c.ClientId == clientId);

            if (clientCart == null || !clientCart.CartProducts.Any())
            {
                throw new Exception("Cart is empty");
            }

            var client = await _userRepository.GetByIdAsync(clientId);

            foreach (var cartItem in clientCart.CartProducts)
            {
                var product = cartItem.Product;
                var productToUpdate = await _productRepository.GetByIdAsync(product.Id);

                if (productToUpdate.StockAvailable < cartItem.Quantity)
                {
                    throw new Exception($"Not enough stock for product {productToUpdate.Name}. Available stock: {productToUpdate.StockAvailable}, requested: {cartItem.Quantity}");
                }

                productToUpdate.StockAvailable -= cartItem.Quantity;
                await _productRepository.UpdateAsync(productToUpdate);

                var order = new Order
                {
                    ProductId = product.Id,
                    ClientId = clientId,
                    ProductName = product.Name,
                    SellerId = product.SellerId,
                    DateTime = DateTime.UtcNow,
                    EmailClient = client.Email,
                    Quantity = cartItem.Quantity
                };

                await _orderRepository.AddAsync(order);
            }

            clientCart.CartProducts.Clear();
            await _cartRepository.UpdateAsync(clientCart);
        }
    }
}
