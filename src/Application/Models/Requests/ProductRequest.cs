using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        public int StockAvailable { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
