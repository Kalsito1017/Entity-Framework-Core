using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.DTOs.Import
{
    public class SalesInputModel
    {
        public decimal Discount { get; set; }   
        public int CardId { get; set; }
        public int CustomerId { get; set; } 
    }
}
