namespace Domain.Primitives;

public interface IDomainEventHandler<TDomainEvent>
{
    Task Handle(TDomainEvent notification, CancellationToken cancellationToken);
}