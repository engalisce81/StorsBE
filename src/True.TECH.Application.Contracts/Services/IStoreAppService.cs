using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using True.TECH.Dtos.Request.Stores;
using True.TECH.Dtos.Response.Stores;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace True.TECH.Services
{
    public interface IStoreAppService : IApplicationService
    {
        Task<StoreDto> GetAsync(Guid id);
        Task<PagedResultDto<StoreDto>> GetListAsync(GetStoreListDto input);
        Task<StoreDto> CreateAsync(CreateStoreDto input);
        Task<StoreDto> UpdateAsync(Guid id, UpdateStoreDto input);
        Task DeleteAsync(Guid id);
    }
}
