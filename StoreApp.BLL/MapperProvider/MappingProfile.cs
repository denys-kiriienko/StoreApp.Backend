using AutoMapper;
using StoreApp.DAL.Entities;
using StoreApp.Shared.Dtos;
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

            CreateMap<CredentialsDto, UserModel>();
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserEntity, UserTokenDto>();
            CreateMap<ProductEntity, ProductModel>().ReverseMap();
            CreateMap<CartItemEntity, CartItemModel>().ReverseMap();
        }
    }
}
