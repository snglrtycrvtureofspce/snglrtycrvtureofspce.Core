using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using snglrtycrvtureofspce.Core.Microservices.Core.Infrastructure.Authorization;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace snglrtycrvtureofspce.Core.Microservices.Core.ServerMiddleware;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSnglrtycrvtureofspceAuthorization(this IServiceCollection collection)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        collection.AddAuthentication((Action<AuthenticationOptions>) (options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        })).AddJwtBearer((Action<JwtBearerOptions>) (cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = AuthOptions.JwtIssuer,
                ValidAudience = AuthOptions.JwtIssuer,
                IssuerSigningKey = (SecurityKey) new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.JwtKey)),
                ClockSkew = TimeSpan.Zero
            };
        }));
        return collection;
    }

    public static IServiceCollection AddSwaggerConf(this IServiceCollection collection, string assemblyName)
    {
        collection.AddSwaggerGen((Action<SwaggerGenOptions>) (c =>
        {
            c.SwaggerDoc("v3", new OpenApiInfo
            {
                Title = assemblyName + " API",
                Version = "v3"
            });
            var baseDirectory = AppContext.BaseDirectory;
            c.IncludeXmlComments(baseDirectory + assemblyName + ".xml");
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. " +
                              "Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header
            });
            c.OperationFilter<SecurityRequirementsOperationFilter>();
        }));
        return collection;
    }
}