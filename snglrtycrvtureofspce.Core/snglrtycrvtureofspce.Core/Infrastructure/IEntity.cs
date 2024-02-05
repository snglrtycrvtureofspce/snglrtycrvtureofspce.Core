namespace snglrtycrvtureofspce.Core.Infrastructure;

/// <summary>
/// Base entity interface
/// </summary>
public interface IEntity
{
    #region IEntity
    Guid Id { get; set; }
    
    DateTime CreatedDate { get; set; }
    
    DateTime ModificationDate { get; set; }
    #endregion
}