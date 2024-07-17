using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string EmailClient { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }

        public static OrderDto Create(Order order)
        {
            return new OrderDto { 
                Id = order.Id,
                ProductName = order.ProductName,
                DateTime = order.DateTime, 
                EmailClient = order.EmailClient,
                Quantity = order.Quantity
            };
        }
        public static List<OrderDto> CreateList(IEnumerable<Order> orders)
        {
            return orders.Select(Create).ToList();
        }
    }
}
