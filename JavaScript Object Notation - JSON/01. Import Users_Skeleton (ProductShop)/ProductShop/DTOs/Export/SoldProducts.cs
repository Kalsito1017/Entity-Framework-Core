using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShop.DTOs.Export
{
    public class SoldProducts
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }

        public string name { get; set; }
        public decimal price { get; set; }
        public string buyerFirstName { get; set; }  
        public string buyerLastName { get; set;}
    }
}
