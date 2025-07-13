using System.Text.Json.Serialization;

namespace StealAllTheCats.Models.Api
{
    public class CatApiModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("breeds")]
        public List<TagApiModel> Breeds { get; set; } = [];
    }
}
