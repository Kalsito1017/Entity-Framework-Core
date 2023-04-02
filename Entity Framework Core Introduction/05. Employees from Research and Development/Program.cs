using _03._Employees_Full_Information.Data.Models;
using System;
using System.Text;

namespace _05._Employees_from_Research_and_Development
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext vs = new SoftUniContext();
            Console.WriteLine(GetEmployeesFromResearchAndDevelopment(vs));
        }
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees
                .OrderBy(x => x.Salary)
                .OrderByDescending(x => x.FirstName)
                .Where(x => x.Department.Name == "Research and Development");
            foreach (var item in employees)
            {
                sb.AppendLine($"{item.FirstName} {item.LastName} from Research and Development - ${item.Salary:F2}");
            }
            return sb.ToString().Trim();

        }
    }
}