using Mediporta.Api.Dto;
using Mediporta.Api.Models;

namespace Mediporta.Api.Service
{
    public interface IItemService
    {
        Task<List<Item>> GetItemsFromExtrernalApi();
        Task<IEnumerable<Item>> GetItemsFromDB();
        Task<IList<ItemCountPercentDTO>> PercentCount();


    }
}
