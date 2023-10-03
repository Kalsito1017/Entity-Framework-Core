using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.DTOs;
using ProductShop.DTOs.Export;
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
            var productsresult = ImportProducts(context, inputJson);
            Console.WriteLine(GetProductsInRange);
        }
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var Dtousers = JsonConvert.DeserializeObject<IEnumerable<UserInputModel[]>>(inputJson);
            var users = mapper.Map<IEnumerable<User>>(Dtousers);
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count()}";

        }
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var Dtousers = JsonConvert.DeserializeObject<IEnumerable<ProductsInputModel>>(inputJson);
            var products = mapper.Map<IEnumerable<Product>>(Dtousers);
            context.Products.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count()}";
        }
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var dtoCategories = JsonConvert.DeserializeObject<IEnumerable<CategoriesInputModel>>(inputJson);
            var categories = mapper.Map<IEnumerable<Category>>(dtoCategories);
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count()}";
        }
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var dtoCateroryProducts = JsonConvert.DeserializeObject<IEnumerable<CategoryProductsInputModel>>(inputJson);
            var categoryproducts = mapper.Map<IEnumerable<CategoryProduct>>(dtoCateroryProducts);
            context.CategoriesProducts.AddRange(categoryproducts);
            context.SaveChanges();
            return $"Successfully imported {categoryproducts.Count()}";
        }
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new ProductsExport { Name = x.Name, Price = x.Price, Seller = x.Seller.FirstName + " " + x.Seller.LastName })
                .OrderBy(x => x.Price)
                .ToList();
            var result = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText(@"C:\Users\Kaloyan\Programing\VSCodes\Entity-Framework-Core\Entity-Framework-Core\JavaScript Object Notation - JSON\01. Import Users_Skeleton (ProductShop)\ProductShop\Resultsproducts-in-range.json", result);
            return result;
        }
        public static string GetSoldProducts(ProductShopContext context)
        {
            var soldproducts = context.Users
                .Where(x => x.ProductsSold.Count > 0)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(x => new { FirstName = x.FirstName, LastName = x.LastName, SoldProducts = x.ProductsSold.Select(x => new { Name = x.Name, Price = x.Price, BuyerFirstName = x.Buyer.FirstName, BuyerLastName = x.Buyer.LastName }) })
                .ToList();
            ;
            var result = JsonConvert.SerializeObject(soldproducts, Formatting.Indented);
            return result;
        }
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .OrderByDescending(x => x.CategoriesProducts.Count)
                .Select(x => new
                {
                    name = x.Name,
                    productsCount = x.CategoriesProducts.Count,
                    averagePrice = Math.Round(x.CategoriesProducts.Average(x => x.Product.Price), 2)
                ,
                    Revenue = Math.Round(x.CategoriesProducts.Sum(x => x.Product.Price), 2)
                }
                ).ToList();
            var result = JsonConvert.SerializeObject(categories, Formatting.Indented);
            return result;

        }
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(x => x.ProductsSold.Count > 0)
                .Select(x => new {
                    x.FirstName,
                    x.LastName,
                    x.Age,
                    Products = x.ProductsSold
                .Select(x => new { x.Name, x.Price })
                .Where(x => x.Name != null)
                })
                .OrderByDescending(x => x.Products.Count())
                .ToList();
            var result = JsonConvert.SerializeObject(users, Formatting.Indented);
            return result;
        }

    }
}