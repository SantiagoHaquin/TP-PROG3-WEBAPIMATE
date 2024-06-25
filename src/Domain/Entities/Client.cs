using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace Domain.Entities
{
    public class Client : User
    {
        public Cart Carts { get; set; } 
    }
}
