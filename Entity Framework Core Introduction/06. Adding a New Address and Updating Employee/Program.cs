using _03._Employees_Full_Information.Data.Models;
using System.Text;

namespace _06._Adding_a_New_Address_and_Updating_Employee
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext sb = new SoftUniContext();
            Console.WriteLine(AddNewAddressToEmployee(sb));
        }
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };
            var nakovEmployee = context.Employees.First(e => e.LastName == "Nakov");
            nakovEmployee.Address = address;
            context.SaveChanges();
            StringBuilder sb = new StringBuilder();
            var adreses = context.Addresses
                .OrderByDescending(x => x.AddressId)
                .Select(x => new {x.AddressText})
                .Take(10);
            foreach (var item in adreses)
            {
                sb.AppendLine(item.AddressText);
            }
            return sb.ToString().Trim();
               
        }
    }
}