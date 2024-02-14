using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Primitives;


/// <summary>
/// AggregateRoots should inherit from this base class
/// We can make full use of the AggregateRoot when we rely on using Repository pattern,
/// You can only make changes through the aggregateRoot
/// </summary>
/// <typeparam name="T"></typeparam>
public class AggregateRoot<T>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    protected AggregateRoot(T id) { }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {

    }
}
