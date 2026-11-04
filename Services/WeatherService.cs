using MauiOpenWeather.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using MauiOpenWeather.Models;

namespace MauiOpenWeather.Services
{
    public class WeatherService
    {
        private readonly string _apiKey;
        private readonly string _baseAdress;
        private readonly HttpClient _client;

        public WeatherService(string baseAdress, string apiKey)
        {
            _baseAdress = baseAdress;
            _apiKey = apiKey;
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_baseAdress)
            };
        }

        public async Task<WeatherResponse?> GetAsync(string endpoint, WeatherParams @params)
        {
            string requestUri = string.Format("{0}/{1}?q={2}&appid={3}&units={4}&lang={5}",
                _client.BaseAddress,
                endpoint,
                @params.City,
                _apiKey,
                @params.Units.ToString().ToLower(),
                @params.Lang
            );

            using HttpResponseMessage response = await _client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            return Deserialize(await response.Content.ReadAsStringAsync());
        }

        private static WeatherResponse? Deserialize(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<WeatherResponse>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
    }
}



