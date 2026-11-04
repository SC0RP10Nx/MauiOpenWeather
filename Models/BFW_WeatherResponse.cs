using MauiOpenWeather.Helper;
using System.Text.Json.Serialization;


namespace MauiOpenWeather.Models
{

    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Item
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("temperature")]
        [JsonConverter(typeof(StringToFloatConverter))]
        public float Temperature { get; set; }

        [JsonPropertyName("humidity")]
        [JsonConverter(typeof(StringToIntConverter))]
        public int Humidity { get; set; }

        [JsonPropertyName("windDirection")]
        public string WindDirection { get; set; }

        [JsonPropertyName("windSpeed")]
        [JsonConverter(typeof(StringToFloatConverter))]
        public float WindSpeed { get; set; }

        [JsonPropertyName("gustSpeed")]
        [JsonConverter(typeof(StringToFloatConverter))]
        public float GustSpeed { get; set; }

        [JsonPropertyName("rain")]
        [JsonConverter(typeof(StringToFloatConverter))]
        public float Rain { get; set; }

        [JsonPropertyName("batteryLow")]
        [JsonConverter(typeof(StringToBoolConverter))]
        public bool BatteryLow { get; set; }

        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(StringToDateTimeConverter))]

        public DateTime Timestamp { get; set; }

        [JsonPropertyName("createDate")]
        [JsonConverter(typeof(StringToDateTimeConverter))]
        public DateTime CreateDate { get; set; }
    }

    public class BFW_WeatherResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("err_msg")]
        public object ErrMsg { get; set; }

        [JsonPropertyName("records")]
        public List<Item> Items { get; set; }
    }


}
