using System;
using System.Collections.Generic;
using True.TECH.Entities.Products.Entites;
using Volo.Abp.Domain.Entities.Auditing;

namespace True.TECH.Entities.Stores.Entites
{

    public class Store : FullAuditedAggregateRoot<Guid> 
    {
        public string Name { get; private set; }
        public string Location { get; private set; }

        public virtual ICollection<ProductStock> Products { get; private set; }
        protected Store() 
        {
            Products = new List<ProductStock>();
        }

        public Store(Guid id, string name, string location) : base(id)
        {
            SetName(name);
            Location = location;
            Products = new List<ProductStock>();

        }

        public void SetName(string name)
        {
            Name = Volo.Abp.Check.NotNullOrWhiteSpace(name, nameof(name));
        }
    }
}
