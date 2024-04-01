using Mediporta.Api.Dto;
using Mediporta.Api.Models;

namespace Mediporta.Api.Service
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItemsFromExtrernalApi();
        Task<IEnumerable<Item>> RefreshItemsFromExternalApi();
        Task<IEnumerable<Item>> GetItemsFromDB();
        Task<IEnumerable<ItemCountPercentDTO>> PercentCount();
        Task AddAsync(IEnumerable<Item> items);
        


    }
}
