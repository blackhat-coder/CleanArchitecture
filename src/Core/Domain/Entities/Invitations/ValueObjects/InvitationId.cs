using Domain.Primitives;

namespace Domain.Entities.Invitations.ValueObjects;

public class InvitationId : ValueObject
{
    public Guid Value { get; private set; }
    public InvitationId(Guid value)
    {
        Value = value;
    }

    public static InvitationId NewInvitationId()
    {
        return new InvitationId(Guid.NewGuid());
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}