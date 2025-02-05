using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using snglrtycrvtureofspce.Core.Errors;
using snglrtycrvtureofspce.Core.Microservices.Core.JwtAuth.Entities;

namespace snglrtycrvtureofspce.Core.Filters;

public class HasAccessAuthorizationAttribute : ActionFilterAttribute
{
    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var tk = context.HttpContext.Request.Headers.Authorization.FirstOrDefault() ??
                 throw new ValidationException(AuthorizationError.UnableToGetAuthorizationToken());
        
        var token = tk.Split(" ")[1];
        var jwtToken = new JwtSecurityToken(token);
        
        var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Role");
        
        if (roleClaim == null || !roleClaim.Value.Equals(RoleType.Administrator.ToString(), 
                StringComparison.OrdinalIgnoreCase))
        {
            throw new AccessViolationException($"You do not have {RoleType.Administrator} role access");
        }
        
        return next();
    }
}