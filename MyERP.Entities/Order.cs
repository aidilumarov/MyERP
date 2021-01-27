using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
