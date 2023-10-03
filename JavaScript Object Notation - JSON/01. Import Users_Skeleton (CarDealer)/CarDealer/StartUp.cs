using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Resource;
using Newtonsoft.Json;
using System.Text.Json;

namespace CarDealer
{
    public class StartUp
    {
        static IMapper mapper;
        public static void Main()
        {
            CarDealerContext db = new CarDealerContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });
            mapper = config.CreateMapper();

            //Console.WriteLine(ImportSuppliers(db, jsonSuppliers));
            //Console.WriteLine(ImportParts(db, jsonParts));
            //Console.WriteLine(ImportCars(db, jsonCars));
            //Console.WriteLine(ImportCustomers(db, jsonCustomers));
            //Console.WriteLine(ImportSales(db, jsonSales));
            //Console.WriteLine(GetOrderedCustomers(db));
            //Console.WriteLine(GetCarsFromMakeToyota(db));
            //Console.WriteLine(GetCarsWithTheirListOfParts(db));
            //Console.WriteLine(GetTotalSalesByCustomer(db));
        }
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var dtoSuppliers = JsonConvert.DeserializeObject<IEnumerable<SuppliersInputModel>>(inputJson);
            var supplier = mapper.Map<IEnumerable<Supplier>>(dtoSuppliers);
            context.Suppliers.AddRange(supplier);
            context.SaveChanges();
            return $"Successfully imported {supplier.Count()}.";
        }
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var dtoParts = JsonConvert.DeserializeObject<IEnumerable<PartsInputModel>>(inputJson);
            var parts = mapper.Map<IEnumerable<Part>>(dtoParts);
            context.Parts.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {parts.Count()}.";
        }
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var dtoCars = JsonConvert.DeserializeObject<IEnumerable<CarsInputModel>>(inputJson);
            var cars = mapper.Map<IEnumerable<Car>>(dtoCars);
            context.Cars.AddRange(cars);
            context.SaveChanges();
            return $"Successfully imported {cars.Count()}.";
        }
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var dtoCustomers = JsonConvert.DeserializeObject<IEnumerable<CustomersInputModel>>(inputJson);
            var customers = mapper.Map<IEnumerable<Customer>>(dtoCustomers);
            context.Customers.AddRange(customers);
            context.SaveChanges();
            return $"Successfully imported {customers.Count()}.";
        }
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var dtoSales = JsonConvert.DeserializeObject<IEnumerable<SalesInputModel>>(inputJson);
            var sales = mapper.Map<IEnumerable<Sale>>(dtoSales);
            context.Sales.AddRange(sales);
            context.SaveChanges();
            return $"Successfully imported {sales.Count()}.";
        }
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers.Select(x => new CustomersInputModel
            {
                Name = x.Name,
                BirthDate = x.BirthDate,
                IsYoungDriver = x.IsYoungDriver
            })
                .OrderBy(x => x.BirthDate)
                .ThenBy(x => x.IsYoungDriver)
                .ToList();
            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);
            //File.WriteAllText("/.././customers.json", json);
            return json;
        }
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var carsfromtoyota = context.Cars
                .Where(x => x.Make == "Toyota")
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .ToList();
            var json = JsonConvert.SerializeObject(carsfromtoyota, Formatting.Indented);
            //File.WriteAllText("/.././customers.json", json);
            return json;
        }
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var localSuppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Parts = x.Parts.Count
                })
                .ToList();
            var json = JsonConvert.SerializeObject(localSuppliers, Formatting.Indented);
            //File.WriteAllText("/.././customers.json", json);
            return json;
        }
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carswithlistofParts = context.Cars
                .Select(x => new
                { x.Make, x.Model, x.TravelledDistance, parts = x.PartsCars.Select(x => new { x.Part.Name, x.Part.Price }) })

                .ToArray();
            var json = JsonConvert.SerializeObject(carswithlistofParts, Formatting.Indented);
            //File.WriteAllText("/.././customers.json", json);
            return json;
        }
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var totalsalesbyCustomer = context.Customers
                .Where(x => x.Sales.Count > 0)
                .Select(x => new {fullname = x.Name, boughtcars = x.Sales.Count})
                .ToList();
            var json = JsonConvert.SerializeObject(totalsalesbyCustomer, Formatting.Indented);
            //File.WriteAllText("/.././customers.json", json);
            return json;
        }

    }
}