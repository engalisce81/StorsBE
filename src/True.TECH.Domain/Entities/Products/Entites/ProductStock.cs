using System;
using System.ComponentModel.DataAnnotations.Schema;
using True.TECH.Entities.Stores.Entites;
using Volo.Abp.Domain.Entities;

namespace True.TECH.Entities.Products.Entites
{
    public class ProductStock : Entity<Guid>
    {
        public Guid ProductId { get; private set; }
        public Guid StoreId { get; private set; }
        public int Quantity { get; private set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; private set; }
        [ForeignKey(nameof(StoreId))]
        public Store Store { get; private set; }

        protected ProductStock() { }

        internal ProductStock(Guid productId, Guid storeId, int quantity)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            StoreId = storeId;
            Quantity = quantity;
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity cannot be negative");
            Quantity = quantity;
        }
    }
}