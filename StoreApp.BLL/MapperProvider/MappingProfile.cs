using AutoMapper;
using StoreApp.DAL.Entities;
using StoreApp.Shared.Models;

namespace StoreApp.BLL.MapperProvider
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserModel>().ReverseMap();
            CreateMap<ProductEntity, ProductModel>().ReverseMap();
            CreateMap<CartItemEntity, CartItemModel>().ReverseMap();
        }
    }
}
