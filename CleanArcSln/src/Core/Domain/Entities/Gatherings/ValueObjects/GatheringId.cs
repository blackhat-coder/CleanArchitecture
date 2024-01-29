using Domain.Primitives;

namespace Domain.Entities.Gatherings.ValueObjects;

public class GatheringId : ValueObject
{
    public Guid Value { get; private set; }
    public GatheringId(Guid value)
    {
        Value = value;
    }

    public static GatheringId NewGatheringId()
    {
        return new GatheringId(Guid.NewGuid());
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
