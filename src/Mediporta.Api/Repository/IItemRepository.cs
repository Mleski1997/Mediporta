using Mediporta.Api.Models;

namespace Mediporta.Api.Repository
{
    public interface IItemRepository
    {
        Task AddAsync(IEnumerable<Item> items);
        Task DeleteAsync();
        Task <IEnumerable<Item>> GetItemsFromDB();
      
     
    }
}
