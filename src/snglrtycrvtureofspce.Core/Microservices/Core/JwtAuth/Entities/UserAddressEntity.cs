using System;
using snglrtycrvtureofspce.Core.Contracts.Base.Infrastructure;

namespace snglrtycrvtureofspce.Core.Microservices.Core.JwtAuth.Entities;

public sealed class UserAddressEntity : IEntity
{
    #region IEntity

    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModificationDate { get; set; }

    #endregion

    public string Country { get; set; }

    public string City { get; set; }

    public string Street1 { get; set; }

    public string Street2 { get; set; }

    public string State { get; set; }

    public string PostalCode { get; set; }

    public string? AddressType { get; set; }

    public Guid UserId { get; set; }

    public UserEntity User { get; set; }
}
