using Microsoft.Extensions.Caching.Memory;
using trade.api.Constants;
using trade.api.Models.Common;
using trade.api.Models.DTOs.LoginDTOs;
using trade.api.Models.Entities;
using trade.api.Utils;

namespace trade.api.Services;

public class LoginService
{
    private readonly JwtService _jwtService;
    private readonly IMemoryCache _memoryCache;


    public LoginService(JwtService jwtService, IMemoryCache memoryCache)
    {
        _jwtService = jwtService;
        _memoryCache = memoryCache;
    }

    public ApiResponse<LoginGetDto> SignIn(LoginDto loginDto)
    {
        User user = Resource.Users.FirstOrDefault(x => x.UserName == loginDto.UserName);

        if (user != null)
        {
            if (user.Password == EncryptService.Encrypt(loginDto.Password))
            {
                LoginGetDto dto = new LoginGetDto
                {
                    UserName = loginDto.UserName,
                    Token = _jwtService.Create(user),
                    Deposit = user.Deposit
                };

                _memoryCache.Set("deposit", user.Deposit);

                return new ApiResponse<LoginGetDto> { Data = dto, Success = true, Message = null };
            }
        }
        return new ApiResponse<LoginGetDto> { Data = null, Success = true, Message = new List<string>() { "Məlumatlar doğru deyil" } };
    }
}
