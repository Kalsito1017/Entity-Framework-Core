using _03._Employees_Full_Information.Data.Models;
using System.Text;

namespace _13.esbyFirstNameStartingWithSa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext sf = new SoftUniContext();
            Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(sf));
        }
         public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees.Where(x => x.FirstName.StartsWith("Sa")).OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName);
            foreach (var item in employees)
            {
                sb.AppendLine($"{item.FirstName} {item.LastName} - {item.JobTitle} - (${item.Salary:f2})");
            }
            return sb.ToString().Trim();
        }
    }
}