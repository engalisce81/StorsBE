using System.ComponentModel.DataAnnotations;

namespace True.TECH.Dtos.Request.PriceTypes
{
    public class UpdatePriceTypeDto
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Description { get; set; }
    }
}
