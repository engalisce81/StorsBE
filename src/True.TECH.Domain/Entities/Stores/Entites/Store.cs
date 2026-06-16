using System;
using System.Collections.Generic;
using System.Linq;
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
            SetLocation(location);
            Products = new List<ProductStock>();
        }

        public void SetName(string name)
        {
            Name = Volo.Abp.Check.NotNullOrWhiteSpace(name, nameof(name));
        }

        public void SetLocation(string location)
        {
            Location = location;
        }

        public static void CheckNameDuplicate(string name, IQueryable<Store> existingStores)
        {
            if (existingStores.Any(x => x.Name == name))
                throw new Volo.Abp.UserFriendlyException(
                    $"A store named '{name}' already exists.");
        }

        public void CheckNameDuplicate(IQueryable<Store> existingStores, Guid? excludeId = null)
        {
            var query = existingStores.Where(x => x.Name == Name);

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            if (query.Any())
                throw new Volo.Abp.UserFriendlyException(
                    $"A store named '{Name}' already exists.");
        }
    }
}