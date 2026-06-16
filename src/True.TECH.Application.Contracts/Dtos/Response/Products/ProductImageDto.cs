using System;

namespace True.TECH.Dtos.Response.Products
{
    public class ProductImageDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public bool IsMain { get; set; }
    }
}
