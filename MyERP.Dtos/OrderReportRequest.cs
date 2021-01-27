using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.Dtos
{
    public class OrderReportRequest
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}
