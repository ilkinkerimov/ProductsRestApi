using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsRestApi.Models.Dto;
using ProductsRestApi.Repositories;

namespace ProductsRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private  readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<ProductController> _logger;



        public ProductController(ApplicationDbContext context, ILogger<ProductController> logger, 
            IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
           _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }


        [HttpGet("GetAllProducts")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            var user = HttpContext.User;
            _logger.LogInformation("Request accepted at {date}", DateTime.Now);
           
           
                _logger.LogInformation("Request completed successfully at {date}", DateTime.Now);
            return _productRepository.GetAllProductsRep();
        }

        
        [HttpGet("GetSingleProduct/{productId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<ProductDto>> GetProductById([FromRoute] int productId)
        {
            var product = _productRepository.GetProductByIdRep(productId);
            if(product == null)
            {
                _logger.LogError("Wrong product id entered");
                return NotFound("Product is not found");
            }
            _logger.LogInformation("User get information about product with id:" + productId);
            return Ok(product);
           
       }

  

        
        [HttpPost("AddNewProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDto>> AddNewProduct( ProductDto product)
        {
            var category = _categoryRepository.GetCategory(product.CategoryName);
            if (product.Id==0)
            {
                _productRepository.AddNewProduct(product, category);
                _logger.LogInformation("Product created with id:" + product.Id);
                return Ok(product);
            }
            return BadRequest("You can not add id, it is auto generated"); 
        }



        [HttpPut("UpdateProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDto>> UpdateProduct([FromBody] ProductDto product)

        {
            _logger.LogInformation("Trying to update product with id:" + product.Id);
            var productDb = _productRepository.GetProductByIdRep(product.Id);
            if (productDb == null)
            {
                return NotFound("Product is not found");
            }
            var category = _categoryRepository.GetCategory(product.CategoryName);
            if (category == null)
            {
                return NotFound("Category is not found");
            }

            var updatedProductDtp =  _productRepository.UpdateProduct(product, category);
            _logger.LogWarning(product.Title + "is updated");

            return Ok(updatedProductDtp);
        }

    
       [HttpDelete("DeleteProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteProduct([FromForm] int id)
        {
            _logger.LogInformation("Request accepted to delete Product with {id} id", id);
            if (_productRepository.Delete(id)) {
                return Ok("Product is deleted");
            }
            _logger.LogWarning("Product with  {id} id is deleted ", id);
            return BadRequest("Product is not found");

        }
        
    }

}
