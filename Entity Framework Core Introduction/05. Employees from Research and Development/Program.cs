using _03._Employees_Full_Information.Data.Models;
using System.Text;

namespace _05._Employees_from_Research_and_Development
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees.OrderBy(x => x.Salary).OrderByDescending(x => x.FirstName);
            foreach (var item in employees)
            {
                sb.AppendLine();
            }
            return sb.ToString().Trim();
            
        }
    }
}