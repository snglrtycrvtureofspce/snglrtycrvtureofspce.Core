using System;

namespace snglrtycrvtureofspce.Core.Base.Infrastructure;

public interface IAuditable : IEntity
{
    public Guid CreatedBy { get; set; }
    
    public Guid ModifiedBy { get; set; }
}