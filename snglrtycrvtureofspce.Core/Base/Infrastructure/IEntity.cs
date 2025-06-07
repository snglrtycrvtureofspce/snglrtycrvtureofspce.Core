using System;

namespace snglrtycrvtureofspce.Core.Base.Infrastructure;

/// <summary>
/// Represents the base entity with audit timestamps.
/// </summary>
public interface IEntity
{
    /// <summary>Entity identifier.</summary>
    Guid Id { get; set; }
    
    /// <summary>Date when the entity was created (UTC).</summary>
    DateTime CreatedDate { get; set; }
    
    /// <summary>Date when the entity was last modified (UTC).</summary>
    DateTime ModificationDate { get; set; }
}