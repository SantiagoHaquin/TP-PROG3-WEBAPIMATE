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

        public static OrderDto Create(Order order)
        {
            return new OrderDto { 
                Id = order.Id, 
                DateTime = order.DateTime, 
                EmailClient = order.EmailClient  
            };

            
        }
        public static List<OrderDto> CreateList(IEnumerable<Order> orders)
        {
            return orders.Select(Create).ToList();
        }
    }
}
