using AutoMapper;
using StoreApp.DAL.Entities;
using StoreApp.Shared.Models;

namespace StoreApp.BLL.MapperProvider
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, UserEntity>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CartItems, opt => opt.Ignore());

            CreateMap<ProductEntity, ProductModel>().ReverseMap();
            CreateMap<CartItemEntity, CartItemModel>().ReverseMap();
        }
    }
}
