using store.Services.Contract;

namespace store.Services.Implementation
{
    public class MyApiService : IMyApiService
    {
        private readonly HttpClient _httpClient;

        public MyApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetApiResponseAsync(string endpoint)
        {
            var response = await _httpClient.GetStringAsync(endpoint);
            return response;
        }
    }
}
