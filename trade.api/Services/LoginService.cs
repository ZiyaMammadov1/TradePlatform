using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using trade.api.Constants;
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

    public LoginGetDto SignIn(LoginDto loginDto)
    {
        User user = Resource.users.FirstOrDefault(x => x.UserName == loginDto.UserName);

        if (user != null)
        {
            if (user.Password == EncryptService.Encrypt(loginDto.Password))
            {
                return new LoginGetDto
                {
                    UserName = loginDto.UserName,
                    Token = _jwtService.Create(user),
                    Deposit = user.Deposit
                };
            }
        }
        throw new Exception("Username or password is incorrect.");
    }
}
