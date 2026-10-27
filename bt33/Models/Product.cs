using System.ComponentModel.DataAnnotations;

namespace bt33.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Giá phải >= 0")]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        // Foreign key to Category
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        // Navigation property to Category
        public Category? Category { get; set; }
    }
}
