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

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual Brand Brand { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = " Price should be greater than ${1}")]
        public double Price { get; set; }
        public bool IsProductOfTheWeek { get; set; }
        public int Quantity { get; set; }

        public Status ProductStatus { get; set; }
        public enum Status { Available=1,Not_Available=2}
    }
}
