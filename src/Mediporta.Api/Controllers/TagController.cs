
using Mediporta.Api.Models;
using Mediporta.Api.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace Mediporta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly IItemService _itemService;

        public TagController(IItemService itemService)
        {
            _itemService = itemService;
        }




        [HttpGet("FromExternalApi")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _itemService.GetItemsFromExtrernalApi();
            return Ok(tags);
        }

        [HttpGet("FromDb")]
        public async Task<IActionResult> GetItemsFromDb(SortOrder sortOrder = SortOrder.Asceding, int page = 1, int pageSize = 10)
        {
           

            var tags = await _itemService.GetItemsFromDB();

            switch (sortOrder)
            {
                case SortOrder.Asceding:
                    tags = tags.OrderBy(x => x.Name);
                    break;
                case SortOrder.Desceding:
                    tags = tags.OrderByDescending(x => x.Name);
                    break;
                default:
                    break;
            }

            var totalTags = tags.Count();
            var totalPages = (int)Math.Ceiling((double)totalTags / pageSize);
            
            tags = tags.Skip((page - 1) * pageSize).Take(pageSize);

            return Ok(tags);
        }


        [HttpGet("RefreshFromExternalApi")]
        public async Task<IActionResult> GetRefreshTags()
        {
            var tags = await _itemService.RefreshItemsFromExternalApi();
            return Ok(tags);
        }


    }
}
