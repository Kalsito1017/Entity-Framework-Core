using AutoMapper;
using ProductShop.Data;
using ProductShop.Models;
using System.Xml;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        static IMapper mapper;
        public static void Main()
        {
            var context = new ProductShopContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<ProductShopProfile>();
                });
            mapper = config.CreateMapper();
            string inputXML = File.ReadAllText("../../../Datasets/users.xml");
            var result = ImportUsers(context, inputXML);
            Console.WriteLine(result);

        }
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var dtoUsers = XmlConvert.
            return $"Successfully imported {users.Count}";
        }
    }
}