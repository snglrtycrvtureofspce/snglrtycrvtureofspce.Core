using System;

namespace snglrtycrvtureofspce.Core.Base.Infrastructure;

/// <summary>
/// Base auditable interface
/// </summary>
public interface IAuditable : IEntity
{
    #region IAuditable
    
    public Guid CreatedBy { get; set; }
    
    public Guid ModifiedBy { get; set; }
    
    #endregion
}