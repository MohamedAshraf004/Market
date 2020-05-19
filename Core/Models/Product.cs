using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = " Price should be greater than ${1}")]
        public double Price { get; set; }
        public bool IsProductOfTheWeek { get; set; }
        public int Quantity { get; set; }

    }
}
