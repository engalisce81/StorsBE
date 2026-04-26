using Volo.Abp.Application.Dtos;

namespace True.TECH.Dtos.Request.PriceTypes
{
    public class GetPriceTypeListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
