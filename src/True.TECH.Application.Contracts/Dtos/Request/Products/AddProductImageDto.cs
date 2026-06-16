using System.ComponentModel.DataAnnotations;

namespace True.TECH.Dtos.Request.Products
{
    public class AddProductImageDto
    {
        [Required]
        [MaxLength(512)]
        public string FileName { get; set; }
        public bool IsMain { get; set; }
    }
}
