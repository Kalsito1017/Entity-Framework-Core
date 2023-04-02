using _03._Employees_Full_Information.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace _07._Employees_and_Projects
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext sf = new SoftUniContext();
            Console.WriteLine(GetEmployeesInPeriod(sf));
        }
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employee = context.Employees.Take(10).Include(x => x.Projects).Include(x => x.Manager);
            
            foreach (var item in employee)
            {
                var list = item.Projects.Where(x => x.StartDate.Year > 2001).Where(x => x.StartDate.Year < 2001).ToList();
                sb.AppendLine($"{item.FirstName} {item.LastName} - Manager: {item.Manager.FirstName} {item.Manager.LastName}");
                foreach (var a in list)
                {
                    sb.AppendLine($"--{a.Name} - {a.StartDate} - {a.EndDate}");
                }
            }
            return sb.ToString().Trim();

        }
    }
}