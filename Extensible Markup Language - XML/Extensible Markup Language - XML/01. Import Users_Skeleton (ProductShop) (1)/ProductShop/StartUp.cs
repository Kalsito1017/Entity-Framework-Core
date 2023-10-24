
using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.Models;
using System.Xml;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
       
        public static void Main()
        {
            var context = new ProductShopContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
           
            var products = File.ReadAllText("../../../Datasets/users.xml");
            
            Console.WriteLine(ImportUsers(context, products));

        }
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var dtoUsers = new XmlSerializer(typeof(UsersInputModel[]), new XmlRootAttribute("Users"));
            var textreader = new StringReader(inputXml);
            var users = context.Users
                .Select(x => new User
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age
                })
                .ToList();
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count}";
        }
    }
}