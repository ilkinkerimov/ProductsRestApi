global using System.ComponentModel.DataAnnotations;

namespace ProductsRestApi.Models
{
    public class Product
    {
        public Product(string title, string description, string category, decimal price)
        {
            Title = title;
            Description = description;
            Category = category;
            Price = price;
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }
    }
}
