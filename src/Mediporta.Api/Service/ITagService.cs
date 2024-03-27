using Mediporta.Api.Models;

namespace Mediporta.Api.Service
{
    public interface ITagService
    {
        Task<List<Item>> GetTags();
    }
}
