using Microsoft.Extensions.Caching.Memory;
using trade.api.Models.Common;
using trade.api.Models.Entities;

namespace trade.api.Services;

public class UserService
{
    private readonly JwtService _jwtService;
    private readonly IMemoryCache _memoryCache;

    public UserService(JwtService jwtService, IMemoryCache memoryCache)
    {
        _jwtService = jwtService;
        _memoryCache = memoryCache;
    }

    public ApiResponse<bool> CheckBudge(double Invest)
    {
        string username = _jwtService.Read();

        User user = Constants.Resource.Users.FirstOrDefault(x => x.UserName == username);

        double deposit = _memoryCache.Get("deposit") != null ? (double)_memoryCache.Get("deposit") : user.Deposit;

        if (user.Deposit > 0 && user.Deposit >= Invest)
        {
            return new ApiResponse<bool>()
            {
                Data = deposit,
                Message = null,
                Success = true
            };
        }

        return new ApiResponse<bool>()
        {
            Data = user,
            Message = new List<string>() { "Depozit miqdarı yetərli deyil" },
            Success = false
        };
    }
}
