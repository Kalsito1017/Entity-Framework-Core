﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.DTOs.Import
{
    public class PartsInputModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }  
        public int Quantity { get; set; }
        public int SupplierId { get; set; }
    }
}
