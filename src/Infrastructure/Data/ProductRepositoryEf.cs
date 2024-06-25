using Domain;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepositoryEf : EfRepository<Product>, IProductRepository
    {

        public ProductRepositoryEf(ApplicationDbContext context) : base(context) { }
        public Product GetByName(string name)
        {
            return _context.Products.SingleOrDefault(p => p.Name == name);
        }

    }
}

