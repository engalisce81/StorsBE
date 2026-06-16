using System;
using Volo.Abp.Application.Dtos;

namespace True.TECH.Dtos.Request.Products
{
    public class GetProductListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public Guid? StoreId { get; set; }
    }
}
