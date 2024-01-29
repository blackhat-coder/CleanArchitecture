using Domain.Primitives;

namespace Domain.Entities.Members.ValueObjects;

public class MemberId : ValueObject
{
    public Guid Value { get; private set; }
    public MemberId(Guid value)
    {
        Value = value;
    }

    public static MemberId NewMemberId()
    {
        return new MemberId(Guid.NewGuid());
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}