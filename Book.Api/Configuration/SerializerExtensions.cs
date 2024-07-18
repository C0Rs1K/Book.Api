using Book.Infrastructure.Shared.Converters;

namespace Book.Api.Configuration;

public static class SerializerExtensions
{
    public static IMvcBuilder AddJsonSerializer(this IMvcBuilder builder)
    {
        var options = SerializerOptions.Default;
        return builder
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.PropertyNameCaseInsensitive = options.PropertyNameCaseInsensitive;
                opt.JsonSerializerOptions.PropertyNamingPolicy = options.PropertyNamingPolicy;
                opt.JsonSerializerOptions.DefaultIgnoreCondition = options.DefaultIgnoreCondition;
                opt.JsonSerializerOptions.WriteIndented = options.WriteIndented;

                foreach (var converter in options.Converters)
                {
                    opt.JsonSerializerOptions.Converters.Add(converter);
                }
            });
    }
}