using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using True.TECH.Dtos.Request.Stores;
using True.TECH.Dtos.Response.Stores;
using True.TECH.Entities.Stores.Entites;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace True.TECH.Services.Stores
{
    [Authorize]
    [Route("api/app/stores")]
    public class StoreAppService : ApplicationService, IStoreAppService
    {
        private readonly IRepository<Store, Guid> _repo;

        public StoreAppService(IRepository<Store, Guid> repo)
            => _repo = repo;

        [HttpGet("{id}")]
        public async Task<StoreDto> GetAsync(Guid id)
        {
            var query = await _repo.GetQueryableAsync();
            return query
                .Where(x => x.Id == id)
                .Select(e => new StoreDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Location = e.Location,
                    CreationTime = e.CreationTime
                })
                .First();
        }

        [HttpGet]
        public async Task<PagedResultDto<StoreDto>> GetListAsync(
            [FromQuery] GetStoreListDto input)
        {
            var query = await _repo.GetQueryableAsync();

            query = query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                         x => x.Name.Contains(input.Filter)
                           || x.Location.Contains(input.Filter));

            var total = query.Count();

            var items = query
                .OrderBy(x => x.Name)
                .PageBy(input)
                .Select(e => new StoreDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Location = e.Location,
                    CreationTime = e.CreationTime
                })
                .ToList();

            return new PagedResultDto<StoreDto>(total, items);
        }

        [HttpPost]
        public async Task<StoreDto> CreateAsync([FromBody] CreateStoreDto input)
        {
            Store.CheckNameDuplicate(input.Name, await _repo.GetQueryableAsync());

            var entity = new Store(
                GuidGenerator.Create(),
                input.Name,
                input.Location);

            await _repo.InsertAsync(entity, autoSave: true);

            return new StoreDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Location = entity.Location,
                CreationTime = entity.CreationTime
            };
        }

        [HttpPut("{id}")]
        public async Task<StoreDto> UpdateAsync(Guid id, [FromBody] UpdateStoreDto input)
        {
            var entity = await _repo.GetAsync(id);

            entity.CheckNameDuplicate(await _repo.GetQueryableAsync(), entity.Id);

            entity.SetName(input.Name);
            entity.SetLocation(input.Location);

            await _repo.UpdateAsync(entity, autoSave: true);

            return new StoreDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Location = entity.Location,
                CreationTime = entity.CreationTime
            };
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
            => await _repo.DeleteAsync(id, autoSave: true);
    }
}