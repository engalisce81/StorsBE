using System;
using Volo.Abp.Application.Dtos;

namespace True.TECH.Dtos.Response.PriceTypes
{
    public class PriceTypeDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
