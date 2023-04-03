using _03._Employees_Full_Information.Data.Models;
using System.Text;

namespace _11._Find_Latest_10_Projects
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext sf = new SoftUniContext();
            Console.WriteLine(GetLatestProjects(sf));
        }
        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var solution = context.Projects
                .Take(10)
                .OrderBy(x => x.Name)
                .ThenByDescending(x => x.StartDate)
                .Select(x => new {x.Name, x.Description, x.StartDate})
                .ToList();
            foreach (var item in solution)
            {
                sb.AppendLine(item.Name);
                sb.AppendLine(item.Description);
                sb.AppendLine(item.StartDate.ToString());
            }
            return sb.ToString().Trim();
        }
    }
}