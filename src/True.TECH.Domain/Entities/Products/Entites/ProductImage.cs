using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace True.TECH.Entities.Products.Entites
{
    
    public class ProductImage : CreationAuditedEntity<Guid>
    {
        public Guid ProductId { get; internal set; }
        public string FileName { get; private set; }
        public bool IsMain { get; private set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; private set; }

        protected ProductImage() { }

        public ProductImage(Guid id, Guid productId, string fileName, bool isMain = false) : base(id)
        {
            ProductId = productId;
            FileName = fileName;
            IsMain = isMain;
        }

        public void SetAsMain(bool isMain) => IsMain = isMain;
    }
}

