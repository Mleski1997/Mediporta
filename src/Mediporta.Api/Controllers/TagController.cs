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


     

        [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var tags = await _itemService.GetTags();
        return Ok(tags);
    }



    }
}
