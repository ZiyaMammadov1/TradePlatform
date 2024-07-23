using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using trade.api.Models.Entities;

namespace trade.api.Services;

public class JwtService
{
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _http;

    public JwtService(IConfiguration config, IHttpContextAccessor http)
    {
        _config = config;
        _http = http;
    }

    public string Create(User user)
    {
        var issuer = _config.GetSection("Jwt:Issuer").Value;
        var audience = _config.GetSection("Jwt:Audience").Value;
        var key = Encoding.ASCII.GetBytes(_config.GetSection("Jwt:Key").Value);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName)
            }),
            Expires = DateTime.UtcNow.AddHours(5),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }

    public string Read()
    {
        string token = _http.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        var tokenHandler = new JwtSecurityTokenHandler();

        var validatedToken = tokenHandler.ReadJwtToken(token);

        var claims = validatedToken.Claims;
        foreach (var claim in claims)
        {
            Console.WriteLine($"{claim.Type}: {claim.Value}");
        }

        var username = validatedToken.Claims.FirstOrDefault(c => c.Type == "unique_name").Value;
        return username;
    }


}
