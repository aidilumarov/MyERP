using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.Entities
{
    public class Order : BaseEntity
    {
        public decimal Price { get; set; }

        public DateTime Date { get; set; }
    }
}
