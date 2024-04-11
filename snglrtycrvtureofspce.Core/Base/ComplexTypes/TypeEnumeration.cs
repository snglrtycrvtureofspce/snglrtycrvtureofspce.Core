using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace snglrtycrvtureofspce.Core.Base.ComplexTypes;

public abstract class TypeEnumeration : IComparable
{
    public string Value { get; set; }

    public TypeEnumeration(string value) => Value = value;

    public override string ToString() => Value;

    public virtual int CompareTo(object obj)
    {
        return string.Compare(Value, ((TypeEnumeration) obj).Value, StringComparison.Ordinal);
    }

    public static IEnumerable<T> GetAll<T>() where T : TypeEnumeration
    {
        return ((IEnumerable<FieldInfo>) typeof (T)
            .GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public))
            .Select<FieldInfo, object>((Func<FieldInfo, object>) (f => f.GetValue((object) null)))
            .Cast<T>();
    }
}