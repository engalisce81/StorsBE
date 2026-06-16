using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace True.TECH.Dtos.Request.Products
{
    public class UpdateProductDto
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        public List<SetProductPriceDto> Prices { get; set; } = new();
        public List<SetProductStockDto> Stocks { get; set; } = new();
    }
}
