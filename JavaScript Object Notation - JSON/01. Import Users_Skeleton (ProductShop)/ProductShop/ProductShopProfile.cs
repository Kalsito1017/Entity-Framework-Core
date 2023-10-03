using AutoMapper;
using ProductShop.DTOs;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile() 
        {
            this.CreateMap<UserInputModel, User>();
            this.CreateMap<ProductsInputModel, Product>();
            this.CreateMap<CategoriesInputModel, Category>();
            this.CreateMap<CategoryProductsInputModel, CategoryProduct>();
        }
    }
}
