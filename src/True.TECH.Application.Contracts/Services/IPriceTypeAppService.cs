using System;
using System.Threading.Tasks;
using True.TECH.Dtos.Request.PriceTypes;
using True.TECH.Dtos.Response.PriceTypes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace True.TECH.Services
{
    public interface IPriceTypeAppService : IApplicationService
    {
        Task<PriceTypeDto> GetAsync(Guid id);
        Task<PagedResultDto<PriceTypeDto>> GetListAsync(GetPriceTypeListDto input);
        Task<PriceTypeDto> CreateAsync(CreatePriceTypeDto input);
        Task<PriceTypeDto> UpdateAsync(Guid id, UpdatePriceTypeDto input);
        Task DeleteAsync(Guid id);
    }
}
