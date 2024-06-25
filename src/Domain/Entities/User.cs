using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public abstract class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ?UserName { get; set; }
        [Required]
        public string ?Password { get; set; }
       
        public string ?Email { get; set; }

        [Required]
        public string ?UserType { get; set; } 
    }
}
