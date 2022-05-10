using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductsRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
             _context = context;    
        }
        [HttpGet("GetAllProducts")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _context.Products;
            return products;
        }


        [HttpGet("GetSingleProduct/{productId}")]
        public ActionResult<Product> GetProductById([FromRoute] int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return NotFound("Product is not found");

            }
            return product;
       }
        [HttpPost("AddNewProduct")]
        public ActionResult<Product> AddNewProduct([FromBody] Product product)
        {
            
            if (product.Id==0)
            {
              var   productDb = new Product(product.Title, product.Description,
                    product.Category, product.Price);
                _context.Products.Add(productDb);
                _context.SaveChanges();
                return Ok(productDb);
            }
            return BadRequest("Please delete id :d"); 

        }



        [HttpPut("UpdateProduct")]
        public ActionResult<Product> UpdateProduct([FromBody] Product product )
        {
            var productDb = _context.Products.Find(product.Id);
            if (productDb == null)
            {
                return NotFound("Product is not found");
            }
            productDb.Title = product.Title;
            productDb.Description = product.Description;
            productDb.Category = product.Category;
            productDb.Price = product.Price;

            _context.SaveChanges();
            return Ok(productDb);
        }

    
       [HttpDelete("DeleteProduct")]
       public ActionResult DeleteProduct([FromForm] int id)
        {
            var productDb = _context.Products.Find(id);
            if(productDb == null)
            {
                return NotFound("product is not found");
            }
            _context.Products.Remove(productDb);
            _context.SaveChanges();
            return Ok();
        }
        
    }

}
