using DataAccess.Entities.Common;
using DataAccess.Interfaces;
using Elastic.Clients.Elasticsearch;
using System.Globalization;
using System.Net.Http.Headers;

namespace DataAccess.Repositories
{
    public class ClientRepository<T> : IClientRepository<T> where T : BaseEntity
    {
        private readonly ElasticsearchClient _client;

        public ClientRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<IndexResponse> CreateAsync(T entity, string indexName)
        {

            var response = await _client.IndexAsync(entity, x => x.Index(indexName));
            return response;
        }

        public async Task<List<T>> GetAllAsync(string indexName)
        {
            var result = await _client.SearchAsync<T>(s => s.Index(indexName));
            return result.Documents.ToList();
        }
        //Verilen Expired Date'e eşit olan kayıtları getirir.
        public async Task<List<T>> GetAllByExpiredDateAsync(string indexName, string expiredDate)
        {
            DateTime dateTimeOffset;

            if (!DateTime.TryParseExact(expiredDate, "yyyy-MM-ddTHH:mm:ss",null, DateTimeStyles.None, out dateTimeOffset))
            {
                throw new Exception("Datetime convert ederken hata meydana geldi!");
            }
            var result = await _client.SearchAsync<T>(s => s.Index(indexName).Size(100)
                 .Query(q => q
                     .Match(m => m
                         .Field(f => f.ExpiredDate)
                             .Query(dateTimeOffset))));

            if (!result.IsValidResponse) throw new Exception(result.DebugInformation);
            return result.Documents.ToList();
        }
    }
}
