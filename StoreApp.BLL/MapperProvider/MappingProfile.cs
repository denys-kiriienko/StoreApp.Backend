using AutoMapper;
using StoreApp.DAL.Entities;
using StoreApp.Shared.Dtos;
using StoreApp.Shared.Models;

namespace StoreApp.BLL.MapperProvider;

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
        
        CreateMap<ProductVariant, ProductVariantModel>()
            .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.Color.Name))
            .ForMember(dest => dest.ColorHex, opt => opt.MapFrom(src => src.Color.HexCode))
            .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Size.Name));

        CreateMap<ProductEntity, ProductModel>()
            .ForMember(p => p.Variants, opt => opt.MapFrom(src => src.Variants));
        
        CreateMap<ProductModel, ProductEntity>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.CartItems, opt => opt.Ignore());

        CreateMap<CartItemEntity, CartItemModel>().ReverseMap();

        CreateMap<ReviewEntity, ReviewModel>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Email))
            .ReverseMap();
    }
}
