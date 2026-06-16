using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace True.TECH.Dtos.Request.Stores
{
    public class UpdateStoreDto
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Location { get; set; }
    }
}
