using System.ComponentModel.DataAnnotations;

namespace FidelPoints.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
