using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public sealed class Webinar : BaseEntity
{
    public Webinar(Guid id, string name, DateTime scheduledOn) : base(id)
    {
        Name = name;
        ScheduledOn = scheduledOn;
    }

    private Webinar()
    {
    }

    public string Name { get; private set; }
    public DateTime ScheduledOn { get; private set; }
}
