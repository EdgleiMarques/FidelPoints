using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FidelPoints.Models
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Point { get; set; }
        public string Cpf { get; set; }
        //       public IList<Order> Orders { get; set; }


    }
}
