﻿namespace ProductsRestApi.Models.Dto
{
    public class ProductDto
    {
        

       
        public int Id { get; set; } 
        public string Title { get; set; }
        
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string CategoryName { get; set; }




    }
}
