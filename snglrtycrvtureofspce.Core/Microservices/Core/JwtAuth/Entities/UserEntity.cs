using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using snglrtycrvtureofspce.Core.Base.Infrastructure;

namespace snglrtycrvtureofspce.Core.Microservices.Core.JwtAuth.Entities;

public class UserEntity : IdentityUser<Guid>, IEntity
{
    #region IEntity
    public DateTime CreatedDate { get; set; }
    
    public DateTime ModificationDate { get; set; }
    #endregion
    
    public string? RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }
    
    public DateTime? LastLoginAt { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string? MiddleName { get; set; }
    
    public DateTime? DateOfBirth { get; set; }
    
    public string? Country { get; set; }
    
    public string? City { get; set; }
    
    public string? Language { get; set; }
    
    public string? UserPhoto { get; set; }
    
    public bool? ChangePasswordNotification { get; set; }
    
    public string? DeliveryMethod { get; set; }
    
    public string? ClientType { get; set; }
    
    public bool IsActive { get; set; }
    
    public bool Agreement { get; set; }
    
    public Guid? UserSocialId { get; set; }
    
    public UserSocialEntity UserSocial { get; set; }
    
    public List<UserAddressEntity> UserAddresses { get; init; }
}