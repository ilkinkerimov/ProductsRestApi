namespace MvcApp.DataAccess;

using Microsoft.AspNetCore.Mvc;
using Refit;
using WebApplication1.Models;

[Headers("Authorization: Bearer")]
public interface IProductData
{
    [Get("/Product/GetAllProducts")]
    Task<List<ProductModel>> GetProducts();
    
    [Post("/Product/AddNewProduct")]
    Task<ActionResult<ProductModel>> AddNewProduct([Body] ProductModel product);
    
    [Post("/Product/GetProductByName")]
    Task<List<ProductModel>> ProductByName( string title);



}




