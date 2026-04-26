using System;
using Volo.Abp.Application.Dtos;

namespace True.TECH.Dtos.Response.Stores
{
    public class StoreDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
