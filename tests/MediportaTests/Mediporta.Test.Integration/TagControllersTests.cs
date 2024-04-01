using Mediporta.Api.Dto;
using Mediporta.Api.Models;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MediportaTests.Mediporta.Test.Integration
{
    public class TagControllerTests
    {
        [Fact]
        public async Task GetTags_FromExternalApi_ReturnsOkAndValidTags()
        {

            var app = new MediportaTestApp();
            var response = await app.Client.GetAsync("https://localhost:7281/api/Tag/FromExternalApi");


            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var contentString = await response.Content.ReadAsStringAsync();
            var tags = JsonConvert.DeserializeObject<List<Item>>(contentString);
            tags.ShouldNotBeEmpty();


            tags.ForEach(tag =>
            {
                tag.Name.ShouldNotBeNullOrWhiteSpace();
                tag.Count.ShouldBeGreaterThan(0);
            });
        }

        [Fact]
        public async Task GetPercentTags_ReturnsOkAndCorrectContent()
        {
            var app = new MediportaTestApp();
            var response = await app.Client.GetAsync("https://localhost:7281/api/Tag/Percent");


            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var contentString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ItemCountPercentDTO>>(contentString);


            result.ShouldNotBeEmpty();


            result.ShouldAllBe(item => !string.IsNullOrEmpty(item.Name));
            result.ShouldAllBe(item => item.Percent >= 0);
        }
    }
}
