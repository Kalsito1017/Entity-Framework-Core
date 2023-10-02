using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.DTOs;
using ProductShop.Models;
using System.Text.Json;

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
            string inputJson = File.ReadAllText("../../../Datasets/users.json");
            var result = ImportUsers(context, inputJson);
            Console.WriteLine(result);
        }
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var Dtousers = JsonConvert.DeserializeObject<IEnumerable<UserInputModel[]>>(inputJson);
            var users = mapper.Map<IEnumerable<User>>(Dtousers);
            context.Users.AddRange(users);
            context.SaveChanges();
             return $"Successfully imported {users.Count()}";

        }
    }
}