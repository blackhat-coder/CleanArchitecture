using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Webinars.Queries.GetWebinarById;

public sealed class GetWebinarByIdQueryHandler(IDbConnection _dbConnection) : IQyeryHandler<GetWebinarByIdQuery, WebinarResponse>
{

    public async Task<WebinarResponse> Handle(GetWebinarByIdQuery request, CancellationToken cancellationToken)
    {
        const string sql = @"SELECT * FROM webinars w WHERE w.Id = @WebinarId";

        var webinar = await _dbConnection.QueryFirstOrDefaultAsync<WebinarResponse>(
            sql, new { request.webinarId });

        if (webinar == null)
        {
            throw new ArgumentNullException(nameof(webinar));
        }

        return webinar;
    }
}
