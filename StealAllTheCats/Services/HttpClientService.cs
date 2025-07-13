using StealAllTheCats.Models.Api;
using StealAllTheCats.Services.Interfaces;
using System.Text.Json;

namespace StealAllTheCats.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<CatApiModel>> FetchCatsAsync()
        {
            var response = await _httpClient.GetAsync("/v1/images/search?limit=25&has_breeds=1");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var cats = JsonSerializer.Deserialize<List<CatApiModel>>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                PropertyNameCaseInsensitive = true
            });

            return cats ?? new List<CatApiModel>();
        }
    }
}
