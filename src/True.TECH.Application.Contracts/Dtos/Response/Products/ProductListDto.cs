using System;
using Volo.Abp.Application.Dtos;

namespace True.TECH.Dtos.Response.Products
{
    public class ProductListDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalStock { get; set; }
        public string MainImage { get; set; } // FileName of the main image
    }
}
