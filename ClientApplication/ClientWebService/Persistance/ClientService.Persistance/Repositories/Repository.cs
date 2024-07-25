using ClientService.Application.Interfaces.GenericService;
using ClientService.Domain.Common;
using ClientService.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClientService.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ClientContext _context;
        public Repository(ClientContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression) => await _context.Set<T>().Where(expression).ToListAsync();

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression) => await _context.Set<T>().Where(expression).FirstOrDefaultAsync();
        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
             _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
