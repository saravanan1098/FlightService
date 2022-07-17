using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Model
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }
        public string Discountcode { get; set; }

        public int Discountamount { get; set; }
    }
}
