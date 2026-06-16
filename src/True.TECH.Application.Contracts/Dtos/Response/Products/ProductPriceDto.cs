using System;

namespace True.TECH.Dtos.Response.Products
{
    public class ProductPriceDto
    {
        public Guid PriceTypeId { get; set; }
        public string PriceTypeName { get; set; }
        public decimal Amount { get; set; }
    }

}
