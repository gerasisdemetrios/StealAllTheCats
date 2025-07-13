using System.Text.Json.Serialization;

namespace StealAllTheCats.Models.Api
{
    public class TagApiModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("temperament")]
        public string Temperament { get; set; }
    }
}
