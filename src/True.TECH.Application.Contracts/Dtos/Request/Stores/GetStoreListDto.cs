using Volo.Abp.Application.Dtos;

namespace True.TECH.Dtos.Request.Stores
{

    public class GetStoreListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
