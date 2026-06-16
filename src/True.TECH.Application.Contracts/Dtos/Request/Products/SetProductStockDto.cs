using System;
using System.ComponentModel.DataAnnotations;

namespace True.TECH.Dtos.Request.Products
{
    public class SetProductStockDto
    {
        [Required]
        public Guid StoreId { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
