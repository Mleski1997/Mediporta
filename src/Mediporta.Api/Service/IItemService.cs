using Mediporta.Api.Models;

namespace Mediporta.Api.Service
{
    public interface IItemService
    {
        Task<List<Item>> GetTags();
        Task UpdateDBFromExternalApi();
        
    }
}
