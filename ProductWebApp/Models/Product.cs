using System.ComponentModel.DataAnnotations;

namespace ProductWebApp.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
