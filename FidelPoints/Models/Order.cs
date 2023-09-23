using System.ComponentModel.DataAnnotations;

namespace FidelPoints.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SellerId { get; set; }
        public Seller Sellers { get; set; }
 //       [Required]
//        public int ProductId { get; set; }
 //       public Product Products { get; set; }
        [Required]
        public int ClientId { get; set; }
        public Client Clients { get; set; }
        public int Point { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;







    }
}
