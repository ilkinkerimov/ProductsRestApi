global using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsRestApi.Models
{
    public class Product : ISoftDelete
    {
     

        [Key]
        public int Id { get; set;}
        [Required]
        public string Title { get; set;}
        [Required]
        public decimal Price { get; set;}

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set;}

        public string? Description { get; set;}

        public Category Category { get; set; }
       
        public bool IsDeleted { get; set; }
    }
}

