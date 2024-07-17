﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public int SellerId { get; set; }   
        public DateTime DateTime { get; set; }
        public string EmailClient { get; set; }
        public int Quantity { get; set; }
    }
}
