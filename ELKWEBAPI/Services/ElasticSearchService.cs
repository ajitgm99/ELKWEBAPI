
using Nest;

namespace ELKWEBAPI.Services
{
    public class ElasticSearchService<T> : IElasticSerachService<T> where T : class
    {
        private ElasticClient _elasticClient;
        public ElasticSearchService(ElasticClient elasticClient) 
        {
            _elasticClient = elasticClient;
        }
        public async Task<string> CreateDocumentAsync(T documnet)
        {
           var response= await _elasticClient.IndexDocumentAsync(documnet);
            return response.IsValid ? "Document created successfully" : "Failed to Create Document";
        }

        public async Task<string> DeleteDocumentAsyn(int id)
        {
            var response = await _elasticClient.DeleteAsync(new DocumentPath<T>(id));
            return response.IsValid ? "Document deleted successfully" : "Failed to delete Document";
        }

        public async Task<IEnumerable<T>> GetAllDocumentsyn(int id)
        {
            var response = await _elasticClient.SearchAsync<T>(s =>
                                    s.MatchAll()
                                    .Size(10000));

            return response.Documents;
        }

        public async Task<T> GetDocumentAsyn(int id)
        {
            var response = await _elasticClient.GetAsync(new DocumentPath<T>(id));

            return response.Source;
        }

        public async Task<string> UpdateDocumentAsyn(T documnet)
        {
            var response = await _elasticClient.UpdateAsync(new DocumentPath<T>(documnet), u => u
                            .Doc(documnet)
                            .RetryOnConflict(3));

            return response.IsValid ? "Document updated successfully" : "Failed to update Document";
        }
    }
}
