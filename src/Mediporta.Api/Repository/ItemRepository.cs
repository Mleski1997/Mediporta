using Mediporta.Api.Data;
using Mediporta.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Mediporta.Api.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;

        public ItemRepository(DataContext context)
        {
            _context = context;
        }

      
        public async Task AddAsync(IEnumerable<Item> items)
        {

            await _context.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

      
        public async Task DeleteAsync()
        {
            _context.RemoveRange();
      
        }

        public async Task<IEnumerable<Item>> GetItemsFromDB() => await _context.Items.ToListAsync();
      

      
    }
}
