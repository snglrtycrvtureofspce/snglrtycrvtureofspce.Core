using System;

namespace snglrtycrvtureofspce.Core.Base.ComplexTypes;

public class SystemUsersEnumeration : TypeEnumeration
{
    public static readonly SystemUsersEnumeration SystemMessage =
        new SystemUsersEnumeration(nameof(SystemMessage), Guid.Parse("00000000-0000-0000-0001-000000000000"));

    public Guid Identificator { get; set; }

    public SystemUsersEnumeration(string value, Guid id) : base(value)
    {
        Identificator = id;
    }

    public override bool Equals(object obj)
    {
        return Identificator.Equals(((SystemUsersEnumeration)obj).Identificator);
    }
}