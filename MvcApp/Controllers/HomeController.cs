using Microsoft.AspNetCore.Mvc;
using MvcApp.DataAccess;
using Refit;
using System.Diagnostics;
using NuGet.Protocol;
using RestSharp;
using RestSharp.Authenticators;
using WebApplication1.Models;
using ParameterType = RestSharp.ParameterType;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductData _productData;
        private readonly RestClient _client;
        private const string url = "https://localhost:7005/api";
        


        public HomeController(ILogger<HomeController> logger)
        {
            _client = new RestClient(url); // for restsharp
            _productData = RestService.For<IProductData>(url, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = (() => 
                        Task.FromResult(GenerateToken()))
            }); //for refit
            _logger = logger;
        }
        
        private  string GenerateToken()
        {
            
            var request = new RestRequest("Auth/Login", Method.Post);
            request.AddHeader("Content-Type", "application/json");

            var credentials = new
            {
                Username = "admin",
                Password = "admin",
                
            };

            request.AddJsonBody(credentials);

            var response = _client.Execute(request);
            var content = response.Content;
            var token = content.Trim('"');
            return token;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Products()
        {
          
            var products =  _productData.GetProducts().Result;
           
            return View(products);
        }
        public ActionResult SearchProduct(string title)
        {
            var products = _productData.ProductByName(title).Result;
            if (products==null)
            {
                return NotFound("Product is not found");
            }
            return View(products);
        }


        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult AddProduct(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                _productData.AddNewProduct(product);
                return RedirectToAction("Products");
            }
            return View(product);
        }

      

        [HttpGet]
        public  ActionResult GetProductById(int productId)
        {
            var token = GenerateToken();
            var  request = new RestRequest("Product/GetSingleProduct/{productId}");
            request.AddUrlSegment("productId", productId);
            _client.Authenticator = new JwtAuthenticator(token);
            var content = _client.Execute<ProductModel>(request).Data;
            if (content == null)
            {
                return NotFound("Product is not found");
            }
          
            return View(content);
        }

        public  ActionResult ProductById()
        {
            return View();
        }

       

        public ActionResult UpdateProduct(ProductModel productModel)
        {
            return View(productModel);
        }
    
        public ActionResult UpdateProductt(ProductModel productModel)
        {
            var token = GenerateToken();
            var request = new RestRequest("Product/UpdateProduct", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(productModel);
            _client.Authenticator = new JwtAuthenticator(token);
            var response =  _client.Execute(request);
            return RedirectToAction("Products");
        }

        
        public  ActionResult DeleteProduct( int id)
        {
            var token = GenerateToken();
            var request = new RestRequest("Product/DeleteProduct", Method.Delete);
            request.AddBody(id);
            _client.Authenticator = new JwtAuthenticator(token);
            var response =  _client.Execute(request);
            return RedirectToAction("Products");

        }


       
    }
}