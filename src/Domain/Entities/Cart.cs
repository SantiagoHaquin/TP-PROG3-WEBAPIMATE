using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public ICollection<Product> Products = new List<Product>();
    }
}