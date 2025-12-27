
using System.Net.Http.Headers;

namespace Agrc.Api.Services
{
    public class GraphClient
    {
        private readonly HttpClient _http;

        public GraphClient(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://graph.microsoft.com/v1.0/");
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "<GRAPH_ACCESS_TOKEN>");
        }

        public async Task<string> GetAsync(string url)
        {
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}

