using System.Text.Json;
using System.Text.Json.Serialization;

namespace Book.Infrastructure.Shared.Converters;

public class SerializerOptions
{
    public static JsonSerializerOptions Default
    {
        get
        {
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReadCommentHandling = JsonCommentHandling.Skip,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };
            serializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            return serializerOptions;
        }
    }
}