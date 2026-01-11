using System.Text;
using System.Text.Json;

namespace Logistics.Messaging.Serialization;

public static class MessageSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public static byte[] Serialize<T>(T message)
        => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message, Options));

    public static T Deserialize<T>(byte[] body)
        => JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body), Options)!;
}
