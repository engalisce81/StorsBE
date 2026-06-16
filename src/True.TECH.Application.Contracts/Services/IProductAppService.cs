using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using True.TECH.Dtos.Request.Products;
using True.TECH.Dtos.Response.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace True.TECH.Services
{
    public interface IProductAppService : IApplicationService
    {
        
        Task<ProductDto> GetAsync(Guid id);
        Task<PagedResultDto<ProductListDto>> GetListAsync(GetProductListDto input);
        Task<ProductDto> CreateAsync(CreateProductDto input);
        Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input);
        Task DeleteAsync(Guid id);


        
        Task<ProductDto> SetPriceAsync(Guid id, SetProductPriceDto input);
        Task RemovePriceAsync(Guid id, Guid priceTypeId);

       
        
        Task<ProductDto> SetStockAsync(Guid id, SetProductStockDto input);



        Task<ProductDto> AddImageAsync(Guid id, AddProductImageDto input);
        Task<ProductDto> SetMainImageAsync(Guid id, string fileName);
        Task RemoveImageAsync(Guid id, string fileName);
    }
}
