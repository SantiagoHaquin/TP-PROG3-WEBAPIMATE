using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SellerService : ISellerService
    {
        private readonly IRepositoryBase<Order> _orderRepository;
        private readonly IRepositoryBase<Product> _productRepository;
        public SellerService(IRepositoryBase<Order> orderRepository, IRepositoryBase<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetSellerProducts(int sellerId)
        {
            var products = await _productRepository.ListAsync();
            var sellerProducts = products.Where(products => products.SellerId == sellerId);
            var productDtos = ProductDto.CreateList(sellerProducts);
            return productDtos;
        }

        public async Task<IEnumerable<Order>> GetSellerSales(int sellerId)
        {
            var orders = await _orderRepository.ListAsync();
            var sellerOrders = orders.Where(o => o.SellerId == sellerId).ToList();
            return sellerOrders;
        }
    }
}