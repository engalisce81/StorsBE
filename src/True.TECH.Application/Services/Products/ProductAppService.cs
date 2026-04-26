using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using True.TECH.Dtos.Request.Products;
using True.TECH.Dtos.Response.Products;
using True.TECH.Entities.Products.Entites;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace True.TECH.Services.Products
{
    [Authorize]
    [Route("api/app/products")]
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _repo;

        public ProductAppService(IRepository<Product, Guid> repo)
            => _repo = repo;

        [HttpGet("{id}")]
        public async Task<ProductDto> GetAsync(Guid id)
        {
            var query = await _repo.GetQueryableAsync();

            var dto = await query
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CreationTime = p.CreationTime,
                    Prices = p.Prices.Select(pr => new ProductPriceDto
                    {
                        PriceTypeId = pr.PriceTypeId,
                        PriceTypeName = pr.PriceType.Name,
                        Amount = pr.Amount
                    }).ToList(),
                    Stocks = p.Stocks.Select(s => new ProductStockDto
                    {
                        StoreId = s.StoreId,
                        StoreName = s.Store.Name,
                        Quantity = s.Quantity
                    }).ToList(),
                    Images = p.Images
                        .OrderByDescending(i => i.IsMain)
                        .Select(i => new ProductImageDto
                        {
                            Id = i.Id,
                            FileName = i.FileName,
                            IsMain = i.IsMain
                        }).ToList()
                })
                .FirstOrDefaultAsync()
                ?? throw new EntityNotFoundException(typeof(Product), id);

            return dto;
        }

        [HttpGet]
        public async Task<PagedResultDto<ProductListDto>> GetListAsync(
            [FromQuery] GetProductListDto input)
        {
            var query = await _repo.GetQueryableAsync();

            query = query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                         p => p.Name.Contains(input.Filter)
                           || p.Description.Contains(input.Filter))
                .WhereIf(input.StoreId.HasValue,
                         p => p.Stocks.Any(s => s.StoreId == input.StoreId));

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.Name)
                .PageBy(input)
                .Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    TotalStock = p.Stocks.Sum(s => s.Quantity),
                    MainImage = p.Images.FirstOrDefault(i => i.IsMain).FileName
                })
                .ToListAsync();

            return new PagedResultDto<ProductListDto>(total, items);
        }

        [HttpPost]
        public async Task<ProductDto> CreateAsync([FromBody] CreateProductDto input)
        {
            var product = new Product(
                GuidGenerator.Create(), input.Name, input.Description);

            foreach (var p in input.Prices)
                product.AddOrUpdatePrice(p.PriceTypeId, p.Amount);

            foreach (var s in input.Stocks)
                product.AddOrUpdateStock(s.StoreId, s.Quantity);

            foreach (var img in input.Images)
                product.AddImage(GuidGenerator.Create(), img.FileName, img.IsMain);

            await _repo.InsertAsync(product, autoSave: true);
            return await GetAsync(product.Id);
        }

        [HttpPut("{id}")]
        public async Task<ProductDto> UpdateAsync(Guid id, [FromBody] UpdateProductDto input)
        {
            var product = await (await _repo.GetQueryableAsync())
                .Include(p => p.Prices)
                .Include(p => p.Stocks)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new EntityNotFoundException(typeof(Product), id);

            product.SetName(input.Name);
            product.SetDescription(input.Description);

            foreach (var p in input.Prices)
                product.AddOrUpdatePrice(p.PriceTypeId, p.Amount);

            foreach (var s in input.Stocks)
                product.AddOrUpdateStock(s.StoreId, s.Quantity);

            await _repo.UpdateAsync(product, autoSave: true);
            return await GetAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
            => await _repo.DeleteAsync(id, autoSave: true);


        [HttpPut("{id}/prices")]
        public async Task<ProductDto> SetPriceAsync(Guid id, [FromBody] SetProductPriceDto input)
        {
            var product = await (await _repo.GetQueryableAsync())
                .Include(p => p.Prices)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new EntityNotFoundException(typeof(Product), id);

            product.AddOrUpdatePrice(input.PriceTypeId, input.Amount);
            await _repo.UpdateAsync(product, autoSave: true);
            return await GetAsync(id);
        }

        [HttpDelete("{id}/prices/{priceTypeId}")]
        public async Task RemovePriceAsync(Guid id, Guid priceTypeId)
        {
            var product = await (await _repo.GetQueryableAsync())
                .Include(p => p.Prices)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new EntityNotFoundException(typeof(Product), id);

            product.RemovePrice(priceTypeId);
            await _repo.UpdateAsync(product, autoSave: true);
        }



        [HttpPut("{id}/stocks")]
        public async Task<ProductDto> SetStockAsync(Guid id, [FromBody] SetProductStockDto input)
        {
            var product = await (await _repo.GetQueryableAsync())
                .Include(p => p.Stocks)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new EntityNotFoundException(typeof(Product), id);

            product.AddOrUpdateStock(input.StoreId, input.Quantity);
            await _repo.UpdateAsync(product, autoSave: true);
            return await GetAsync(id);
        }

       
        
        
        
        [HttpPost("{id}/images")]
        public async Task<ProductDto> AddImageAsync(Guid id, [FromBody] AddProductImageDto input)
        {
            var product = await (await _repo.GetQueryableAsync())
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new EntityNotFoundException(typeof(Product), id);

            if (product.Images.Any(i => i.FileName == input.FileName))
                throw new UserFriendlyException(
                    $"Image '{input.FileName}' already exists for this product.");

            product.AddImage(GuidGenerator.Create(), input.FileName, input.IsMain);
            await _repo.UpdateAsync(product, autoSave: true);
            return await GetAsync(id);
        }

        [HttpPut("{id}/images/{fileName}/main")]
        public async Task<ProductDto> SetMainImageAsync(Guid id, string fileName)
        {
            var product = await (await _repo.GetQueryableAsync())
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new EntityNotFoundException(typeof(Product), id);

            if (!product.Images.Any(i => i.FileName == fileName))
                throw new UserFriendlyException(
                    $"Image '{fileName}' not found for this product.");

            product.SetMainImage(fileName);
            await _repo.UpdateAsync(product, autoSave: true);
            return await GetAsync(id);
        }

        [HttpDelete("{id}/images/{fileName}")]
        public async Task RemoveImageAsync(Guid id, string fileName)
        {
            var product = await (await _repo.GetQueryableAsync())
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new EntityNotFoundException(typeof(Product), id);

            product.RemoveImage(fileName);
            await _repo.UpdateAsync(product, autoSave: true);
        }
    }
}