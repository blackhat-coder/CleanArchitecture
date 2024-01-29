using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Primitives;

#nullable disable
public abstract class BaseEntity<T> : IEquatable<BaseEntity<T>>
{
    protected BaseEntity(T id) => Id = id;

    protected BaseEntity() { }

    public T Id { get; private init; }
    public DateTime CreatedOn { get; private init; }
    public DateTime ModifiedOn { get; set; }
    public bool IsDeleted { get; private init; }

    public static bool operator ==(BaseEntity<T> first, BaseEntity<T> second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(BaseEntity<T> first, BaseEntity<T> second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        if(obj.GetType() != typeof(BaseEntity<T>)) return false;

        if(obj is not BaseEntity<T> entity) return false;

        return this.Equals(entity);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(BaseEntity<T> other)
    {
        if (other == null) return false;

        if (other.GetType() != typeof(BaseEntity<T>)) return false;

        if (other is not BaseEntity<T> entity) return false;

        return this.Equals(entity);
    }
}
