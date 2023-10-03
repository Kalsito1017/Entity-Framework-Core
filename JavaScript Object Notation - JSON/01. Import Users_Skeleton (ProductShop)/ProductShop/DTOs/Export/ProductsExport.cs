﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShop.DTOs.Export
{
    public class ProductsExport
    {
        public string Name { get; set; }    
        public decimal Price { get; set; }
        public string Seller { get; set; }
    }
}
