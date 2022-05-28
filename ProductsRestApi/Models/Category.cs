using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsRestApi.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }



    }
}
