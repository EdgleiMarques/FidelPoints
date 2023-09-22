using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FidelPoints.Models
{
    
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Point { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
//        public Order Orders { get; set; }


    }
}
