using MauiOpenWeather.Models;
using System.Text.Json;

namespace MauiOpenWeather.Services
{
    public class BFW_WeatherService
    {
        private readonly string _apiKey;
        private readonly string _baseAdress;
        private readonly HttpClient _client;

        public BFW_WeatherService(string baseAdress, string apiKey)
        {
            _baseAdress = baseAdress;
            _apiKey = apiKey;
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_baseAdress)
            };
        }

        public async Task<BFW_WeatherResponse?> GetAsync(string endpoint)
        {
            string requestUri = string.Format("{0}/{1}?api_key={2}",
                _client.BaseAddress,
                endpoint,
                _apiKey
              
            );

            using HttpResponseMessage response = await _client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            return Deserialize(await response.Content.ReadAsStringAsync());
        }

        private static BFW_WeatherResponse? Deserialize(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<BFW_WeatherResponse>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
    }
}



