using Mediporta.Api.Models;
using MediportaTests.Mediporta.Test.Integration;
using Newtonsoft.Json;
using Shouldly;
using System.Net;

namespace test
{
    public class test
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
    }
}