using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime SaleDate { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public Client CLient { get; set; }

        public decimal TotalAmount { get; set; }

        public ICollection<SaleLine> SaleLine { get; set; }
    }
}
