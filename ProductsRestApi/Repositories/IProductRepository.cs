using Microsoft.AspNetCore.Mvc;
using ProductsRestApi.Models.Dto;

namespace ProductsRestApi.Repositories
{
    public interface IProductRepository
    {
        public List<ProductDto> GetAllProductsRep();

        public void Save();

        public ProductDto GetProductByIdRep(int productId);

        public ProductDto UpdateProduct(ProductDto product,
            Category category);
        public void AddNewProduct(ProductDto product, Category category);

        public bool Delete(int id);



    }
}