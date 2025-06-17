using AutoMapper;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Interfaces.Repositories;
using StoreApp.Shared.Dtos;
using StoreApp.Shared.Interfaces.Services;
using StoreApp.Shared.Models;
using StoreApp.Shared.Services;

namespace StoreApp.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
        }

        public async Task<bool> RegisterUserAsync(UserModel user)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return false;
            }

            var userEntity = _mapper.Map<UserEntity>(user);

            var passwordHash = _passwordHasher.Hash(user.Password);
            userEntity.PasswordHash = passwordHash;

            await _userRepository.AddUserAsync(userEntity);

            return true;
        }

        public async Task<(string?, string?)> LoginUserAsync(UserModel user)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser == null)
            {
                return (null, null);
            }

            if (!_passwordHasher.Verify(user.Password, existingUser.PasswordHash))
            {
                return (null, null);
            }

            var tokens = await GenerateAndSaveTokensAsync(existingUser);

            return tokens;  // access, refresh
        }

        public async Task<(string?, string?)> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);
            if (user == null || user.RefreshTokenExpiryTime == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return (null, null);
            }

            var tokens = await GenerateAndSaveTokensAsync(user);

            return tokens;  // access, refresh
        }

        private async Task<(string, string)> GenerateAndSaveTokensAsync(UserEntity user)
        {
            var userTokenDto = _mapper.Map<UserTokenDto>(user);
            var accessToken = _jwtProvider.GenerateToken(userTokenDto);
            var refreshToken = _jwtProvider.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userRepository.UpdateUserAsync(user);

            return (accessToken, refreshToken);
        }
    }
}
