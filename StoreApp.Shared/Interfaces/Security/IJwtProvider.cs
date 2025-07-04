using StoreApp.Shared.Dtos;

namespace StoreApp.Shared.Interfaces.Security;

public interface IJwtProvider
{
    string GenerateRefreshToken();
    string GenerateToken(UserTokenDto user);
}