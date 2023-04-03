using _03._Employees_Full_Information.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace _10.DepartmentswithMoreThan5Emp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext sf = new SoftUniContext();
            Console.WriteLine(GetDepartmentsWithMoreThan5Employees(sf));
        }
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var emp = context.Departments.Include(x => x.Employees).ThenInclude(x => x.Manager).Where(x => x.Employees.Count() > 5).OrderBy(x => x.Employees.Count()).ThenBy(x => x.Name).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var d in emp)
            {
                sb.AppendLine($"{d.Name} - {d.Manager.FirstName} {d.Manager.LastName}");
                foreach (var e in d.Employees.OrderBy(x => x.FirstName).ThenBy(x => x.LastName))
                {
                    sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }
            }
            return sb.ToString();
        }
    }
}