using System.Text.Json;
using System.Text.Json.Serialization;

namespace MauiOpenWeather.Helper
{
    public class IntToDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            long value = reader.GetInt64();

            return DateTimeOffset.FromUnixTimeSeconds(value).DateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
