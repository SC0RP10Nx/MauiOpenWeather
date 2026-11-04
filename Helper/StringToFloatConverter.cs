using System.Text.Json;
using System.Text.Json.Serialization;

namespace MauiOpenWeather.Helper
{
    public class StringToFloatConverter : JsonConverter<float>
    {
        public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            float myFloat = float.TryParse(value, out float result) ? result : 0;
            return myFloat;
        }

        public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
