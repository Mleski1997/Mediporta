using System.Text.Json.Serialization;

namespace Mediporta.Api.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortOrder
    {
        Asceding,  
        Desceding 
    }
}
