using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Webinars.Commands.CreateWebinar;

public sealed record CreateWebinarCommand(string Name, DateTime ScheduledOn) : ICommand<Guid>;