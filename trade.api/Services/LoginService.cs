using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using trade.api.Constants;
using trade.api.Models.Common;
using trade.api.Models.DTOs.LoginDTOs;
using trade.api.Models.Entities;
using trade.api.Utils;

namespace trade.api.Services;

public class LoginService
{
    private readonly JwtService _jwtService;

    public LoginService(JwtService jwtService)
    {
        _jwtService = jwtService;
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

                return new ApiResponse<LoginGetDto>  { Data = dto, Success = true, Message = null };
            }
        }
        return new ApiResponse<LoginGetDto> { Data = null, Success = true, Message = new List<string>() { "Username or password is incorrect." } };
    }
}
