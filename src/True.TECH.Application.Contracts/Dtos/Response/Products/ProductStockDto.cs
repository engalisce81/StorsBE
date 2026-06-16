using System;
using System.Collections.Generic;
using System.Text;

namespace True.TECH.Dtos.Response.Products
{
    public class ProductStockDto
    {
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public int Quantity { get; set; }
    }

}
