using Castle.DynamicProxy.Generators.Emitters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.DTOs.Import
{
    public class SuppliersInputModel
    {
        public string Name { get; set; }
        public bool IsImported { get; set; }
    }
}
