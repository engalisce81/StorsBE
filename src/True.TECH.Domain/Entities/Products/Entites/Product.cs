using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;

namespace True.TECH.Entities.Products.Entites
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public virtual ICollection<ProductPrice> Prices { get; private set; }
        public virtual ICollection<ProductStock> Stocks { get; private set; }
        public virtual ICollection<ProductImage> Images { get; private set; }

        protected Product()
        {
            Prices = new Collection<ProductPrice>();
            Stocks = new Collection<ProductStock>();
            Images = new Collection<ProductImage>();
        }

        public Product(Guid id, string name, string description = null) : base(id)
        {
            SetName(name);
            SetDescription(description); // تعيين الوصف عند الإنشاء
            Prices = new Collection<ProductPrice>();
            Images = new Collection<ProductImage>();
            Stocks = new Collection<ProductStock>();
        }

        public void SetName(string name) =>
            Name = Volo.Abp.Check.NotNullOrWhiteSpace(name, nameof(name), maxLength: 256);

    
        // ميثود جديدة للتحكم في الوصف
        public void SetDescription(string description) =>
            Description = description;

        // ── Prices ───────────────────────────────────────
        public void AddOrUpdatePrice(Guid priceTypeId, decimal amount)
        {
            var existing = Prices.FirstOrDefault(p => p.PriceTypeId == priceTypeId);
            if (existing != null)
                existing.UpdateAmount(amount);
            else
                Prices.Add(new ProductPrice(Id, priceTypeId, amount));
        }

        public void RemovePrice(Guid priceTypeId)
        {
            var price = Prices.FirstOrDefault(p => p.PriceTypeId == priceTypeId);
            if (price != null) Prices.Remove(price);
        }

        // ── Stocks ───────────────────────────────────────
        public void AddOrUpdateStock(Guid storeId, int quantity)
        {
            var existing = Stocks.FirstOrDefault(s => s.StoreId == storeId);
            if (existing != null)
                existing.UpdateQuantity(quantity);
            else
                Stocks.Add(new ProductStock(Id, storeId, quantity));
        }

        // ── Images ───────────────────────────────────────
        public void AddImage(Guid imageId, string fileName, bool isMain = false)
        {
            if (isMain)
            {
                foreach (var img in Images) img.SetAsMain(false);
            }

            Images.Add(new ProductImage(imageId, Id, fileName, isMain));
        }

        public void RemoveImage(string fileName)
        {
            var image = Images.FirstOrDefault(i => i.FileName == fileName);
            if (image != null) Images.Remove(image);
        }

        public void SetMainImage(string fileName)
        {
            foreach (var img in Images)
            {
                img.SetAsMain(img.FileName == fileName);
            }
        }
    }
}