using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using True.TECH.Dtos.Request.PriceTypes;
using True.TECH.Dtos.Response.PriceTypes;
using True.TECH.Entities.PriceTypes.Entites;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace True.TECH.Services.PriceTypes
{
    [Authorize]
    [Route("api/app/price-types")]
    public class PriceTypeAppService
          : ApplicationService, IPriceTypeAppService
    {
        private readonly IRepository<PriceType, Guid> _repo;

        public PriceTypeAppService(IRepository<PriceType, Guid> repo)
            => _repo = repo;

        [HttpGet("{id}")]
        public async Task<PriceTypeDto> GetAsync(Guid id)
        {
            var query = await _repo.GetQueryableAsync();
            return query
                .Where(x => x.Id == id)
                .Select(e => new PriceTypeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description
                })
                .First();
        }

        [HttpGet]
        public async Task<PagedResultDto<PriceTypeDto>> GetListAsync(
            [FromQuery] GetPriceTypeListDto input)
        {
            var query = await _repo.GetQueryableAsync();

            query = query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                         x => x.Name.Contains(input.Filter));

            var total = query.Count();

            var items = query
                .OrderBy(x => x.Name)
                .PageBy(input)
                .Select(e => new PriceTypeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description
                })
                .ToList();

            return new PagedResultDto<PriceTypeDto>(total, items);
        }

        [HttpPost]
        public async Task<PriceTypeDto> CreateAsync([FromBody] CreatePriceTypeDto input)
        {
            PriceType.CheckNameDuplicate(input.Name, await _repo.GetQueryableAsync());

            var entity = new PriceType(
                GuidGenerator.Create(),
                input.Name,
                input.Description);

            await _repo.InsertAsync(entity, autoSave: true);

            return new PriceTypeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }

        [HttpPut("{id}")]
        public async Task<PriceTypeDto> UpdateAsync(Guid id, [FromBody] UpdatePriceTypeDto input)
        {
            var entity = await _repo.GetAsync(id);

            entity.CheckNameDuplicate(await _repo.GetQueryableAsync(), entity.Id);

            entity.Name = input.Name;
            entity.Description = input.Description;

            await _repo.UpdateAsync(entity, autoSave: true);

            return new PriceTypeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
            => await _repo.DeleteAsync(id, autoSave: true);
    }
}