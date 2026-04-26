using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace True.TECH.Dtos.Request.Products
{
    public class SetProductPriceDto
    {
        [Required]
        public Guid PriceTypeId { get; set; }
        [Range(0, (double)decimal.MaxValue)]
        public decimal Amount { get; set; }
    }

}
