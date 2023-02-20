using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CdnApp.Abstracts;
using CdnApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CdnApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JwtController : BaseController
{
    private AppSettings _settings;

    public JwtController(IOptions<AppSettings> options)
    {
        _settings = options.Value;
    }

    [HttpPost]
    public ActionResult<UserInfo> Post([FromBody] UserInfo value)
    {
        var jwt = _settings.Jwt;
        var claims = new List<Claim>
        {
            new Claim("jti", value.Username),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims, expires: DateTime.Now.AddDays(1), signingCredentials: signingCredentials);
        var user = value with { };

        user.Token = new JwtSecurityTokenHandler().WriteToken(token);

        return user;
    }
}