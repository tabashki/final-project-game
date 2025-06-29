using System.Text.Json.Serialization; 

namespace TeamCherry.Project;

public class TileLayer: ILayer
{
    // TMJ properties
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("width")]
    public int LayerWidth { get; set; }

    [JsonPropertyName("height")]
    public int LayerHeight { get; set; }

    [JsonPropertyName("data")]
    public int[]? Data { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("visible")]
    public bool Visible { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }
}