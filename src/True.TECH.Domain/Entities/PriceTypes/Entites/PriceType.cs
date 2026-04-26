namespace True.TECH.Entities.PriceTypes.Entites
{
    using System;
    using Volo.Abp.Domain.Entities;

    public class PriceType : Entity<Guid> 
    {
        public string Name { get; set; }
        public string Description { get; set; }

        protected PriceType() { }

        public PriceType(Guid id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }
    }
}
