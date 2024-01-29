using Domain.Primitives;

namespace Domain.Entities.Attendees.ValueObjects;

public class AttendeeId : ValueObject
{
    public Guid Value { get; private set; }
    public AttendeeId(Guid value)
    {
        Value = value;
    }

    public static AttendeeId NewAttendeeId()
    {
        return new AttendeeId(Guid.NewGuid());
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}