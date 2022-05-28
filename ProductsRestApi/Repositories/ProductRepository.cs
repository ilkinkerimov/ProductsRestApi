using Microsoft.AspNetCore.Mvc;
using ProductsRestApi.Models.Dto;

namespace ProductsRestApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public void Save()
        {
            
            _context.SaveChanges();
            
        }

        public List<ProductDto> GetProductByName(string title)
        {
            return JoinTables().Where(dto => dto.Title ==title).ToList();
        }

        public List<ProductDto> GetAllProductsRep()
        {

            var query = JoinTables();

            return query.ToList();
        }

        public  ProductDto GetProductByIdRep(int productId)
        {
            var productDto = JoinTables().FirstOrDefault(pr=> pr.Id == productId);
            
            return productDto;
        }


        public ProductDto UpdateProduct(ProductDto product,
            Category category )
        {
            var productDb = _context.Products.Find(product.Id);

            productDb.Title = product.Title;
            productDb.Description = product.Description;
            productDb.CategoryId = category.Id;
            productDb.Price = product.Price;

            
            var updatedProductDto = new ProductDto
            {
                Id = productDb.Id,
                Title = product.Title,
                Description = product.Description,
                CategoryName = product.CategoryName,
                Price = product.Price,
            };
            _context.SaveChanges();
            return updatedProductDto;
        }


        public void AddNewProduct( ProductDto product, Category category)
        {
            
            var productDb = new Product
            {
                Title = product.Title,
                Description = product.Description,
                CategoryId = category.Id,
                Price = product.Price
            };
            _context.Add(productDb);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var product = _context.Products.Find(id);
           if(product == null)
            {
                return false;
            }
            product.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }


        public IQueryable<ProductDto> JoinTables()
        {
            var query = from pr in _context.Products
                        join ct in _context.Categories
            on pr.CategoryId equals ct.Id
            where pr.IsDeleted == false
                        select new ProductDto
                        {
                            Id = pr.Id,
                            Title = pr.Title,
                            Description = pr.Description,
                            Price = pr.Price,
                            CategoryName = ct.Name
                        };

            return query;
        }

      

    }
}
