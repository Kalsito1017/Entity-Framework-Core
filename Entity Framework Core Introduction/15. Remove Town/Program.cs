using _03._Employees_Full_Information.Data.Models;
using System.Text;

namespace _15._Remove_Town
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext sf = new SoftUniContext();
            Console.WriteLine(RemoveTown(sf));
        }
        public static string RemoveTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var townToGetDeleted = context
                .Towns
                .Where(t => t.Name == "Seattle")
                .FirstOrDefault();

            //referenced addresses to the town with Seattles's Id
            var refferedAddresses = context
                .Addresses.Where(a => a.TownId == townToGetDeleted.TownId)
                .ToList();

            foreach (var e in context.Employees)
            {
                if (refferedAddresses.Any(r => r.AddressId == e.AddressId))
                {
                    e.AddressId = null;
                }
            }

            var deletedAddressesCnt = refferedAddresses.Count;

            context.Addresses.RemoveRange(refferedAddresses);
            context.Towns.Remove(townToGetDeleted);

            sb.AppendLine($"{deletedAddressesCnt} addresses in Seattle were deleted");

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
    }
}