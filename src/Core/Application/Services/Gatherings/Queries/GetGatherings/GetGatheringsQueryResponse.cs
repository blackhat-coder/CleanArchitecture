using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Gatherings.Queries.GetGatherings;

public class GetGatheringsQueryResponse
{
    public string GatheringId { get; set; }
    public string CreatorId { get; set; }
    public string CreatorEmail { get; set; }
    public string GatheringName { get; set; }
    public string Location { get; set; }
    public DateTime ScheduledAt { get; set; }
}
