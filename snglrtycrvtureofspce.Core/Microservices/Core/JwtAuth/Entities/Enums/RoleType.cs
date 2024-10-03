using System.ComponentModel;

namespace snglrtycrvtureofspce.Core.Microservices.Core.JwtAuth.Entities.Enums;

public enum RoleType
{
    [Description("Member")]
    Member = 0,
    
    [Description("Moderator")]
    Moderator = 1,
    
    [Description("Administrator")]
    Administrator = 2
}