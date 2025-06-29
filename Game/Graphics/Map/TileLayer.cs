using System.Text.Json.Serialization; 

namespace TeamCherry.Project;

public class TileLayer: ILayer
{
    // TMJ properties
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("width")]
    public int LayerWidth { get; init; }

    [JsonPropertyName("height")]
    public int LayerHeight { get; init; }

    [JsonPropertyName("data")]
    public int[]? Data { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("visible")]
    public bool Visible { get; init; }

    [JsonPropertyName("id")]
    public int Id { get; init; }
}
