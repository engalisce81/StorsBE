using System;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;

namespace True.TECH.Entities.PriceTypes.Entites
{
    public class PriceType : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        protected PriceType() { }

        public PriceType(Guid id, string name, string description) : base(id)
        {
            Name = Volo.Abp.Check.NotNullOrWhiteSpace(name, nameof(name));
            Description = description;
        }

        public static void CheckNameDuplicate(string name, IQueryable<PriceType> existingPriceTypes)
        {
            if (existingPriceTypes.Any(x => x.Name == name))
                throw new Volo.Abp.UserFriendlyException(
                    $"A price type named '{name}' already exists.");
        }

        public void CheckNameDuplicate(IQueryable<PriceType> existingPriceTypes, Guid? excludeId = null)
        {
            var query = existingPriceTypes.Where(x => x.Name == Name);

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            if (query.Any())
                throw new Volo.Abp.UserFriendlyException(
                    $"A price type named '{Name}' already exists.");
        }
    }
}