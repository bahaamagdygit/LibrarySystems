using Library.Application.Contracts;
using Library.Context;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Library.Infrastructure
{
    public class Repository<TEntity, Tid> : IRepository<TEntity, Tid> where TEntity : BaseEntity
    {
        private readonly LibraryContext _libraryContext;
        private readonly DbSet<TEntity> _Dbset;
        public Repository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
            _Dbset = _libraryContext.Set<TEntity>();
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return (await _Dbset.AddAsync(entity)).Entity;
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            return Task.FromResult((_Dbset.Remove(entity)).Entity);
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_Dbset.Select(s => s));
        }

        public async Task<TEntity> GetByIdAsync(Tid id)
        {
            return await _Dbset.FindAsync(id);
        }

		public async Task<TEntity> GetByUserNameAsync(Tid id)
		{
			return await _Dbset.FindAsync(id);
		}

		public async Task<int> SaveChangesAsync()
        {
            return await _libraryContext.SaveChangesAsync();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(_Dbset.Update(entity).Entity);
        }
    }
}
