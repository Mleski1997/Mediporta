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
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }



        [HttpGet]
        public async Task<IActionResult> GetTag()
        {
            var tags = await _tagService.GetTags();
            return Ok(tags);
            
        }
    }
}
