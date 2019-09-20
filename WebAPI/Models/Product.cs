using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }
        
        public decimal ProductPrice { get; set; }
        
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}