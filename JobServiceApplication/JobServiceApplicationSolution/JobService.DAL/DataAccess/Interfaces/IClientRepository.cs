using Elastic.Clients.Elasticsearch;

namespace DataAccess.Interfaces
{
    public interface IClientRepository<T>
    {
        Task<IndexResponse> CreateAsync(T entity, string indexName);
        Task<List<T>> GetAllAsync(string indexName);
        Task<List<T>> GetAllByExpiredDateAsync(string indexName,string expiredDate);
    }
}
