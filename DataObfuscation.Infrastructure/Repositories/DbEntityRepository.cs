using DataObfuscation.Infrastructure.Data;
using DataObfuscation.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataObfuscation.Infrastructure.Repositories
{
	public class DbEntityRepository<TEntity> : IEntityRepository<TEntity>
		where TEntity : BaseEntity
	{
		protected readonly DataContext _db;

		protected DbSet<TEntity> _entities => _db.Set<TEntity>();

		public DbEntityRepository(DataContext db)
		{
			_db = db;
		}

		public IQueryable<TEntity> GetEntities(int skip, int take)
		{
			return _db.Set<TEntity>().Skip(skip).Take(take);
		}

		public int Count()
		{
			return _entities.Count();
		}

		public void BulkUpdate(IList<TEntity> entities, BulkConfig config)
		{
			_db.BulkUpdate(entities, config);
		}
	}
}
