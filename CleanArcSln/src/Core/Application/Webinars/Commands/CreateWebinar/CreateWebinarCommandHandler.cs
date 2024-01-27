using Domain.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Webinars.Commands.CreateWebinar;

internal sealed class CreateWebinarCommandHandler(IWebinarRepository _webinarRepository, IUnitOfWork _unitOfWork) 
    : ICommandHandler<CreateWebinarCommand, Guid>
{
    public async Task<Guid> Handle(CreateWebinarCommand request, CancellationToken cancellationToken)
    {
        var webinar = new Webinar(Guid.NewGuid(), request.Name, request.ScheduledOn);

        await _webinarRepository.Insert(webinar);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return webinar.Id;
    }
}
