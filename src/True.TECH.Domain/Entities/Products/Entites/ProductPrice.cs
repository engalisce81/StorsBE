using System;
using System.ComponentModel.DataAnnotations.Schema;
using True.TECH.Entities.PriceTypes.Entites;
using Volo.Abp.Domain.Entities;

namespace True.TECH.Entities.Products.Entites
{
    public class ProductPrice : Entity<Guid>
    {
        public Guid ProductId { get; private set; }
        public Guid  PriceTypeId { get; private set; }
        public decimal Amount { get; private set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product  { get; private set; }
        [ForeignKey(nameof(PriceTypeId))]
        public PriceType PriceType { get; private set; }

        protected ProductPrice() { }

        internal ProductPrice(Guid productId, Guid priceTypeId, decimal amount)
        {
            ProductId = productId;
            PriceTypeId = priceTypeId;
            UpdateAmount(amount);
        }

        internal void UpdateAmount(decimal amount)
        {
            Amount = amount; 
        }

    }
}
