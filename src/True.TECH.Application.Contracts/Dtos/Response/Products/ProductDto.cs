using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace True.TECH.Dtos.Response.Products
{
    public class ProductDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductPriceDto> Prices { get; set; } = new();
        public List<ProductStockDto> Stocks { get; set; } = new();
        public List<ProductImageDto> Images { get; set; } = new();
    }

}
