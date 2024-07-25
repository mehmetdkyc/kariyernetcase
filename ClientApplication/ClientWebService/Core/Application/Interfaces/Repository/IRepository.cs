using ClientService.Domain.Common;
using System.Linq.Expressions;

namespace ClientService.Application.Interfaces.GenericService
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(Expression<Func<T,bool>> expression);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);

    }
}
