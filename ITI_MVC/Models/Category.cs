using System.ComponentModel.DataAnnotations;

namespace ITI_MVC.Models
{
    public class Category
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        [RegularExpression("^[^0-9]*$", ErrorMessage = "Category name cannot contain numbers.")]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
