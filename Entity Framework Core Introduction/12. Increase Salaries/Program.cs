using _03._Employees_Full_Information.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace _12._Increase_Salaries
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext sf = new SoftUniContext();
            Console.WriteLine(IncreaseSalaries(sf));

        }
        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            string[] depts = new string[]
            {
                "Engineering",
                "Tool Design",
                "Marketing",
                "Information Services"
            };

            var employees = context
                .Employees
                .Where(e => depts.Contains(e.Department.Name))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToArray();
            foreach (var employee in employees)
            {
                employee.Salary *= 1.12M;
            }

            context.SaveChanges();
            foreach (var e in employees)
            {
                sb
                    .AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
            }
            return sb.ToString().Trim();
        }
    }
}