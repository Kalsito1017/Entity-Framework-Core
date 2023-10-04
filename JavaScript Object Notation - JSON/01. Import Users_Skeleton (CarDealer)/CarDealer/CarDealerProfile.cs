using AutoMapper;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SuppliersInputModel, Supplier>();
            CreateMap<CarsInputModel, Car>();
            CreateMap<SalesInputModel, Sale>();
            CreateMap<PartsInputModel, Part>();
            CreateMap<CustomersInputModel, Customer>();
        }
    }
}
