using Newtonsoft.Json;
using static System.Text.RegularExpressions.Regex;

namespace macmod.Tools;

public class EnumConverter<T> : JsonConverter<T> where T : struct
{
    public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var token = reader.Value as string ?? reader.Value.ToString();
        var stripped = Replace(token, @"<[^>]+>", string.Empty);
        return Enum.TryParse<T>(stripped, out var result) ? result : default;
    }

    public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }
}