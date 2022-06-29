using FootballPlayers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FootballPlayers.Repositories
{
    public abstract class Repository<T> where T : class
    {
        protected readonly DbContext context;
        protected readonly DbSet<T> objects;
        public Repository(DbContext context)
        {
            this.context = context;
            objects = context.Set<T>();
        }

        public virtual async Task InsertAsync(T obj)
        {
            await objects.AddAsync(obj);
            await SaveAsync();
        }
        public abstract Task UpdateOrInsertAsync(T obj);
        public virtual async Task DeleteAsync(Guid id)
        {
            var obj = await FindByIdAsync(id);
            objects.Remove(obj);
            await SaveAsync();
        }
        public virtual async Task<T> FindByIdAsync(Guid id)=>
            await objects.FindAsync(id);
        public abstract Task<PageList<T>> GetAllAsync(int currentPage, int pageSize);
        protected async Task SaveAsync() =>
            await context.SaveChangesAsync();
    }
}
