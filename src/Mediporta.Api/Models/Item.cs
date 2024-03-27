using System.Text.Json.Serialization;

namespace Mediporta.Api.Models
{
    public class Item
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
