namespace bt33.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation property: 1 Category có nhiều Product
        public List<Product> Products { get; set; } = new();
    }
}
