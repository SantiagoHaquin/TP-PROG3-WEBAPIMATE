using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        
       public int ClientId { get; set; } 
        
 
    }
}
