using _03._Employees_Full_Information.Data.Models;
using System.Text;

namespace _04._Employees_with_Salary_Over_50_000;
class Program
{
    static void Main(string[] args)
    {
        SoftUniContext context = new SoftUniContext();
        string output = GetEmployeesWithSalaryOver50000(context);
        Console.WriteLine(output);
    }
    public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
    {
        StringBuilder sb = new StringBuilder();
        var employees = context.Employees.Where(x => x.Salary > 50000).OrderBy(x => x.FirstName).ToList();
        foreach (var employee in employees)
        {
            sb.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
        }
        return sb.ToString().Trim();
    }
}
