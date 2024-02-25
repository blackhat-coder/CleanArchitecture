using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Outbox;

/// <summary>
/// Keeps track of which events has been consumed
/// </summary>
public class OutboxMessageConsumer
{
    /// <summary>
    /// Id of the OutBoxMessage
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The Event Handler being executed
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
