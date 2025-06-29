using System.Text.Json.Serialization; 

namespace TeamCherry.Project;

public class TileProperty
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("value")]
    public object? Value { get; init; }
}
