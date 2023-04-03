using _03._Employees_Full_Information.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace _09._Employee_147
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(GetEmployee147(context));
        }
        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employee147 = context.Employees
                .Include(x => x.Projects)
                .First(x => x.EmployeeId == 147);
            sb.AppendLine();
            foreach (var item in a.Projects.OrderBy(x => x.Name))
            {
                sb.AppendLine($"{item.Name}");
            }
            return $"{a.FirstName} {a.LastName} {a.JobTitle} {sb}";
        }
    }
}