using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mediporta.Api.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; } 
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}