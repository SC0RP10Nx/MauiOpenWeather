using System.Text.Json;
using System.Text.Json.Serialization;

namespace MauiOpenWeather.Helper
{
    public class StringToIntConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            int myInt = int.TryParse(value, out int result) ? result : 0;
            return myInt;
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
