using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions;

public interface IUnitOfWork<out TContext>
{
    TContext Context { get; }

    void CreateTransaction();
    void Commit();
    void Rollback();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
