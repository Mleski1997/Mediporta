using Mediporta.Api.Dto;
using Mediporta.Api.Models;
using Mediporta.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Mediporta.Api.Service
{
    public class ItemService : IItemService
    {
        private readonly HttpClient _client;
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            });
            _itemRepository = itemRepository;
        }

    

        public async Task<List<Item>> GetTags()
        {
            var allTags = new List<Item>();
            var minTags = 1000;
            var pageNumber = 1;

            while (allTags.Count < minTags)
            {
                var api = $"https://api.stackexchange.com/2.3/tags?order=desc&pagesize=100&page={pageNumber}&&sort=popular&site=stackoverflow";
                var response = await _client.GetAsync(api);

                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                var tagResponse = await JsonSerializer.DeserializeAsync<ItemsDto>(stream);

                if (tagResponse.items == null || tagResponse.items.Count == 0)
                
                {
                    break;
                }

                allTags.AddRange(tagResponse.items);
                pageNumber++;
               

            }
            var tags =  allTags.Take(minTags).ToList();
            await _itemRepository.AddAsync(tags);
            return tags;
            

        }
   
    }
}
