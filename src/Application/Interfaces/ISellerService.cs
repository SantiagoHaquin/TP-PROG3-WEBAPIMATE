using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISellerService
    {
        Task<IEnumerable<ProductDto>> GetSellerProducts(int sellerId);
        Task<IEnumerable<Order>> GetSellerSales(int sellerId);
    }
}
