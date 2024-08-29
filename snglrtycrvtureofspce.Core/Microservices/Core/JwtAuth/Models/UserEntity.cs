using System;
using Microsoft.AspNetCore.Identity;

namespace snglrtycrvtureofspce.Core.Microservices.Core.JwtAuth.Models;

public class UserEntity : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string? MiddleName { get; set; }
    
    public DateTime? DateOfBirth { get; set; }

    public string? Country { get; set; }
    
    public string? City { get; set; }
    
    public string? Language { get; set; }
    
    public string? UserPhoto { get; set; }
    
    public string? FacebookLink { get; set; }
    
    public string? InstagramLink { get; set; }
    
    public string? TwitterLink { get; set; }
    
    public string? VkLink { get; set; }
    
    public string? Site { get; set; }
    
    public bool Agreement { get; set; }
    
    public bool? ChangePasswordNotification { get; set; }
    
    public string? DeliveryMethod { get; set; }
    
    public string? ClientType { get; set; }
}