namespace ELKWEBAPI.Services
{
    public interface IElasticSerachService<T>
    {
        Task<string> CreateDocumentAsync(T documnet);

        Task<T> GetDocumentAsyn(int id);

        Task<IEnumerable<T>> GetAllDocumentsyn(int id);

        Task<string> UpdateDocumentAsyn(T documnet);

        Task<string> DeleteDocumentAsyn(int id);

    }
}
