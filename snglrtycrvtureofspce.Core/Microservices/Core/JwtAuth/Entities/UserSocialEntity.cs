using System;
using snglrtycrvtureofspce.Core.Base.Infrastructure;

namespace snglrtycrvtureofspce.Core.Microservices.Core.JwtAuth.Entities;

public sealed class UserSocialEntity : IEntity
{
    #region IEntity
    
    public Guid Id { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime ModificationDate { get; set; }
    
    #endregion
    
    public string? Site { get; set; }
    
    public string? FacebookLink { get; set; }
    
    public string? TwitterLink { get; set; }
    
    public string? InstagramLink { get; set; }
    
    public string? SkypeLink { get; set; }
    
    public string? LinkedIn { get; set; }
    
    public string? VkLink { get; set; }
}