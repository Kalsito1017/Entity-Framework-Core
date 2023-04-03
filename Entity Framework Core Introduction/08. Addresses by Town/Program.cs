using _03._Employees_Full_Information.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
namespace _08._Addresses_by_Town
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext sf = new SoftUniContext();
            Console.WriteLine(GetAddressesByTown(sf));
        }
        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var em = context.Addresses
                .Include(x => x.Employees)
                .Include(x => x.Town)
                .OrderByDescending(x => x.Employees.Count())
                .ThenBy(x => x.Town.Name)
                .ThenBy(x => x.AddressText)
                .Take(10);
                
            foreach (var address in em)
            {
                sb.AppendLine($"{address.AddressText}, {address.Town.Name} - {address.Employees.Count} employees");
            }
            return sb.ToString().Trim();
        }
    }
}