using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Primitives;

#nullable disable
public abstract class BaseEntity : IEquatable<BaseEntity>
{
    protected BaseEntity(Guid id) => Id = id;

    protected BaseEntity() { }

    public Guid Id { get; private init; }
    public DateTime CreatedOn { get; private init; }
    public bool IsDeleted { get; private init; }

    public static bool operator ==(BaseEntity first, BaseEntity second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(BaseEntity first, BaseEntity second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        if(obj.GetType() != typeof(BaseEntity)) return false;

        if(obj is not BaseEntity entity) return false;

        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(BaseEntity other)
    {
        if (other == null) return false;

        if (other.GetType() != typeof(BaseEntity)) return false;

        if (other is not BaseEntity entity) return false;

        return entity.Id == Id;
    }
}
