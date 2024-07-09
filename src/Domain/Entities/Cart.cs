using System.Collections.Generic;

namespace Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
