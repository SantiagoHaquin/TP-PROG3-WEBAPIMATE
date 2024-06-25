using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockAvailable { get; set; }
        public string Category { get; set; }
        public int UserId { get; set; }

        public static ProductDto Create(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                StockAvailable = product.StockAvailable,
                Category = product.Category,
                UserId = product.UserId,
            };
        }

        public static List<ProductDto> CreateList(IEnumerable<Product> products)
        {
            return products.Select(Create).ToList();
        }
    }
}
