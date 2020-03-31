using EFCore.BulkExtensions;
using System.Collections.Generic;
using System.Linq;

namespace DataObfuscation.Infrastructure.Repositories
{
    public interface IEntityRepository<TEntity>
    {
        IQueryable<TEntity> GetEntities(int skip, int take);
        int Count();
        void BulkUpdate(IList<TEntity> entities, BulkConfig config);
	}
}
