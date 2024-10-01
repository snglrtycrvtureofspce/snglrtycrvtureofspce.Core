using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using snglrtycrvtureofspce.Core.Base.ComplexTypes;
using snglrtycrvtureofspce.Core.Microservices.Core.Infrastructure.Authorization;
using snglrtycrvtureofspce.Core.Microservices.Core.JwtAuth.Models;

namespace snglrtycrvtureofspce.Core.Microservices.Core.JwtAuth;

public static class JwtTokenProvider
{
    public static string GenerateJwtToken(this UserEntity user, IEnumerable<IdentityRole<Guid>> roles)
    {
        List<Claim> claimList = new List<Claim>
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim("Username", user.UserName),
            new Claim("Email", user.Email),
            new Claim("Role", string.Join(" ", roles.Select(x => x.Name))),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.JwtKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.Now.AddDays(30);

        var token = new JwtSecurityToken(
            issuer: "snglrtycrvtureofspce",
            audience: "snglrtycrvtureofspce",
            claims: claimList,
            notBefore: null,
            expires: expires,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);}

    public static string GenerateSystemToken()
    {
        return JwtTokenProvider.GenerateSystemToken(SystemUsersEnumeration.SystemMessage);
    }

    public static string GenerateSystemToken(SystemUsersEnumeration user)
    {
        return GenerateJwtToken(new UserEntity { UserName = "System" }, Enumerable.Empty<IdentityRole<Guid>>());
    }
}