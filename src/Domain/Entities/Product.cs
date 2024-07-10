using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockAvailable { get; set; }
        public string Category { get; set; }
        public int SellerId { get; set; }
        
        [ForeignKey("SellerId")]
        public Seller Seller { get; set; }

        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
