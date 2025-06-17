using StoreApp.Shared.Dtos;

namespace StoreApp.Shared.Services;

public interface IJwtProvider
{
    string GenerateRefreshToken();
    string GenerateToken(UserTokenDto user);
}