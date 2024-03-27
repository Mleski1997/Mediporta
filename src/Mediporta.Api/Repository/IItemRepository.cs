using Mediporta.Api.Models;

namespace Mediporta.Api.Repository
{
    public interface IItemRepository
    {
        Task AddAsync(List<Item> items);
     
    }
}
