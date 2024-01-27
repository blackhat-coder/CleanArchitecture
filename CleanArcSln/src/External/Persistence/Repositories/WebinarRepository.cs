using Domain.Abstractions;
using Domain.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;

public sealed class WebinarRepository(ApplicationDbContext _dbContext) : IWebinarRepository
{
    public Task InsertAsync(Webinar webinar)
    {
        _dbContext.Set<Webinar>().Add(webinar);
    }
}
