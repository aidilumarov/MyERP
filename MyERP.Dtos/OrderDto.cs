using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.Dtos
{
    public class OrderDto : BaseDto
    {
        public decimal Price { get; set; }

        public DateTime Date { get; set; }
    }
}
