﻿namespace DapperProject.Dtos.ProductDots
{
    public class GetByIdProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
