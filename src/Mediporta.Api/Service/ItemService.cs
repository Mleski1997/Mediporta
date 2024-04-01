using Mediporta.Api.Dto;
using Mediporta.Api.Exceptions;
using Mediporta.Api.Models;
using Mediporta.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Mediporta.Api.Service
{
    public class ItemService : IItemService
    {
        private readonly HttpClient _client;
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemService> _logger;

        public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger)
        {
            _client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            });
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task AddAsync(IEnumerable<Item> items) => await  _itemRepository.AddAsync(items);


        public async Task<IEnumerable<Item>> GetItemsFromDB() => await _itemRepository.GetItemsFromDB();
      

        public async Task<IEnumerable<Item>> GetItemsFromExtrernalApi()
        {
            var allTags = 0;    
            var minTags = 1000;
            var pageNumber = 1;

          try
            {
                while (allTags < minTags)
                {
                    var api = $"https://api.stackexchange.com/2.3/tags?order=desc&pagesize=100&page={pageNumber}&sort=popular&site=stackoverflow";
                    var response = await _client.GetAsync(api);

                    response.EnsureSuccessStatusCode();

                    var stream = await response.Content.ReadAsStreamAsync();
                    var tagResponse = await JsonSerializer.DeserializeAsync<ItemsDto>(stream);

                    if (tagResponse.items == null || tagResponse.items.Count == 0)

                    {
                        break;
                    }

                    await _itemRepository.AddAsync(tagResponse.items);
                    allTags += tagResponse.items.Count;

                    pageNumber++;


                }

                var result = await _itemRepository.GetItemsFromDB();
                
               
                return result;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while downloading tags from StackExchange API. Page: {PageNumber}", pageNumber);
                throw;

            }
        }

        public async Task<IEnumerable<ItemCountPercentDTO>> PercentCount()
        {
            try
            {
                var items = await _itemRepository.GetItemsFromDB();
                if (items == null)
                {
                    return new List<ItemCountPercentDTO>();
                }
                var sum = items.Sum(t => t.Count);

                var percent = items.Select(t => new ItemCountPercentDTO
                {
                    Name = t.Name,
                    Percent = (double)t.Count / sum * 100
                }).ToList();

               
               
                return percent;
               
                
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the percentage calculation of item counts.");
                throw;
            }
        }

        public async Task<IEnumerable<Item>> RefreshItemsFromExternalApi()
        {
           try
            {
                await _itemRepository.DeleteAsync();
                return await GetItemsFromExtrernalApi();
               
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error during resfreshing tags");
                throw;
            }
           
        }
    }
}