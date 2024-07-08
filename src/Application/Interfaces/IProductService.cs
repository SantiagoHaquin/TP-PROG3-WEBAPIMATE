using Application.Models.Requests;
using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(int id);
        Task<Product> GetProductEntityById(int id);
        Task<Product> CreateProduct(ProductRequest productRequest, int sellerId);
        Task DeleteProduct(int id);
        Task UpdateProduct(int id, ProductRequest productRequest);
    }
}

